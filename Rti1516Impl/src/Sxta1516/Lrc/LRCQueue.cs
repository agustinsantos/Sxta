using System;
using System.Collections.Generic;
using System.Threading;

// Import log4net classes.
using log4net;

using C5;
using Hla.Rti1516;
using Sxta.Rti1516.Time;

namespace Sxta.Rti1516.Lrc
{

    /// <summary> 
    /// This class holds callback messages for the LRC. Internally it implements two separate queues,
    /// one for receive order messages and another for timestamped messages. When polled, any receive
    /// order messages are released first, following this, any TSO messages are released as long as
    /// their time is less than or equal to the federate's current time.
    /// </summary>
    public class LrcQueue
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary> 
        /// Informs the queue of an update to the constrained status of the federate. If the value is
        /// false, all messages in the TSO queue will be moved over to the RO queue
        /// </summary>
        public virtual bool Constrained
        {
            set
            {
                this.constrained = value;

                // if we are no longer constrained, move any messages in the TSO queue over to the RO queue
                if (this.constrained == false)
                {
                    lock (thisLock)
                    {
                        int queueSize = this.tsoQueue.Count;
                        for (int count = 0; count < queueSize; count++)
                        {
                            Callback tsorec = this.tsoQueue.DeleteMax();
                            tsorec.Time = null;
                            this.roQueue.Enqueue(tsorec);
                        }
                    }
                }
            }
        }

        /// <returns> Returns true if there are no messages queued for delivery (RO or TSO)
        /// </returns>
        public virtual bool IsEmpty
        {
            get
            {
                lock (thisLock)
                {
                    // test the queues //
                    return this.roQueue.Count == 0 && this.tsoQueue.IsEmpty;
                }
            }
        }

        public virtual bool IsEmptyROQueue
        {
            get
            {
                lock (thisLock)
                {
                    return this.roQueue.Count == 0;
                }
            }
        }

        public virtual bool IsEmptyTSOQueue
        {
            get
            {
                lock (thisLock)
                {
                    return this.tsoQueue.IsEmpty;
                }
            }
        }


        //----------------------------------------------------------
        //                      CONSTRUCTORS
        //----------------------------------------------------------
        /// <summary> Creates a new queue.</summary>
        public LrcQueue()
        {
            tsoComparer = new TsoComparer();
            tsoQueue = new IntervalHeap<Callback>(tsoComparer);
            roQueue = new Queue<Callback>();

            // is the federate constrained?
            constrained = false;

            // locking and concurrency //
            thisLock = new Object();
            condition = new Object();
        }

        //----------------------------------------------------------
        //                    INSTANCE METHODS
        //----------------------------------------------------------

        /// <summary> Place the given message on the queue. If the message is timestamped, it is stored on a
        /// different internal queue. Timestamped messages will be ordered according to their time
        /// (lowest to highest). If the federate is *NOT* constrained, messages will automatically
        /// be stored in the RO queue.
        /// </summary>
        public virtual bool Add(Callback callback)
        {
            // check that we have a valid message
            if (callback == null)
            {
                return false;
            }

            lock (thisLock)
            {
                if (this.constrained == false)
                {
                    callback.Time = null;
                    try
                    {
                        this.roQueue.Enqueue(callback);

                        if (log.IsDebugEnabled)
                            log.Debug(callback.ToString() + " enqueues in roQueue");

                        lock (this.condition)
                        {
                            // signal to any threads waiting on the condition
                            System.Threading.Monitor.Pulse(this.condition);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

                // check if it is RO or TSO
                if (callback.IsTimeStamped())
                {
                    if (log.IsDebugEnabled)
                        log.Debug(callback.ToString() + " adds in tsoQueue");

                    bool added = this.tsoQueue.Add(callback);

                    if (added)
                    {
                        lock (this.condition)
                        {
                            // signal to any threads waiting on the condition
                            System.Threading.Monitor.Pulse(this.condition);
                        }
                    }

                    return added;
                }
                else
                {
                    try
                    {
                        this.roQueue.Enqueue(callback);

                        if (log.IsDebugEnabled)
                            log.Debug(callback.ToString() + " enqueues in roQueue");

                        lock (this.condition)
                        {
                            // signal to any threads waiting on the condition
                            System.Threading.Monitor.Pulse(this.condition);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary> This method will poll the lrc queue for messages. If there are none currently in the queue,
        /// it will wait as long as the given timeout (represented in ms) for something to be added
        /// before returning null if there is no change. If something is added in that time, it will be
        /// returned.
        /// <p/>
        /// If the thread is interrupted while waiting, an InterruptedException is thrown.
        /// </summary>
        public virtual Callback Poll(int timeoutMillis, ILogicalTime timeRequest)
        {
            lock (thisLock)
            {
                // 1. check to see if we have a message //
                // there is no need to aquire the lock as we are not accessing the queue directly
                // lock is reentrant, so we shouldn't take hit for aquiring it in poll()
                Callback callback = _Poll(timeRequest);
                if (callback != null)
                {
                    // there is a message so we can just return it
                    return callback;
                }
            }
            lock (condition)
            {
                // 2. need to wait for an update to come through //

                // wait on condition
                // TODO!!!. wait time resolution is milliseconds
                System.Threading.Monitor.Wait(this.condition, timeoutMillis);
            }
            lock (thisLock)
            {
                // we have been woken up:
                //  -if by timeout: return null
                //  -if by signal: return available message
                return _Poll(timeRequest);
            }
        }

        public virtual Callback Poll(ILogicalTime timeRequest)
        {
            lock (thisLock)
            {
                return _Poll(timeRequest);
            }
        }

        public virtual Callback PollROQueue(int timeoutMillis) // Time in ms
        {
            lock (thisLock)
            {
                // 1. check to see if we have a message //
                // there is no need to aquire the lock as we are not accessing the queue directly
                // lock is reentrant, so we shouldn't take hit for aquiring it in poll()
                Callback callback = _PollROQueue();
                if (callback != null)
                {
                    // there is a message so we can just return it
                    return callback;
                }
            }

            lock (this.condition)
            {
                // 2. need to wait for an update to come through //
                // wait on condition
                System.Threading.Monitor.Wait(this.condition, timeoutMillis);
            }

            lock (thisLock)
            {
                // we have been woken up:
                //  -if by timeout: return null
                //  -if by signal: return available message
                return _PollROQueue();
            }
        }

        public virtual Callback PollTSOQueue(ILogicalTime timeRequest, int timeoutMillis)
        {
            lock (thisLock)
            {
                // 1. check to see if we have a message //
                // there is no need to aquire the lock as we are not accessing the queue directly
                // lock is reentrant, so we shouldn't take hit for aquiring it in poll()
                Callback callback = _PollTSOQueue(timeRequest);
                if (callback != null)
                {
                    // there is a message so we can just return it
                    return callback;
                }
            }

            // 2. need to wait for an update to come through //
            lock (condition)
            {
                // wait on condition
                // TODO!!!. wait time resolution is milliseconds
                System.Threading.Monitor.Wait(this.condition, timeoutMillis);
            }

            lock (thisLock)
            {        // we have been woken up:
                //  -if by timeout: return null
                //  -if by signal: return available message
                return _PollTSOQueue(timeRequest);
            }
        }

        public virtual Callback PollROQueue()
        {
            lock (thisLock)
            {
                return _PollROQueue();
            }
        }

        public virtual Callback PollTSOQueue(ILogicalTime timeRequest)
        {
            lock (thisLock)
            {
                return _PollTSOQueue(timeRequest);
            }
        }

        public virtual ILogicalTime GetLITS()
        {
            lock (thisLock)
            {
                if (this.tsoQueue.Count != 0)
                {
                    Callback callback = this.tsoQueue.FindMax();
                    if (callback != null)
                    {
                        return callback.Time;
                    }
                }
                return null;
            }
        }

        protected virtual Callback _PollROQueue()
        {
            ///////////////////////////////
            // check for any RO messages //
            ///////////////////////////////
            if (this.roQueue.Count > 0)
            {
                Callback callback = this.roQueue.Dequeue();

                // we have an RO message, return it
                return callback;
            }
            else
            {
                return null;
            }
        }

        protected virtual Callback _PollTSOQueue(ILogicalTime timeRequest)
        {
            if (this.tsoQueue.Count > 0)
            {
                Callback callback = this.tsoQueue.FindMin();
                if (callback != null)
                {
                    // there is a message at the head of the set, is it of a releasable time?
                    if (callback.Time.CompareTo(timeRequest) <= 0)
                    {
                        // it is! release it - we also need to remove it
                        this.tsoQueue.DeleteMin();
                        return callback;
                    }
                }
            }
            return null;
        }

        /// <summary> Remove and return the head of the queue, or null if the queue is empty. <b>NOTE:</b> All
        /// receive-order messages will be processed first. Following this, all timestamp-order messages
        /// less than or equal to the current federate time will be released.
        /// </summary>
        protected virtual Callback _Poll(ILogicalTime timeRequest)
        {
            Callback callback = _PollROQueue();
            if (callback != null)
            {
                return callback;
            }

            ////////////////////////////////////////////////////////
            // no RO messages - check for releasable TSO messages //
            ////////////////////////////////////////////////////////
            if (this.tsoQueue.Count > 0)
            {
                callback = _PollTSOQueue(timeRequest);
                if (callback != null)
                {
                    return callback;
                }
            }

            //////////////////////////////////////
            // no messages that can be released //
            //////////////////////////////////////
            return null;
        }


        //----------------------------------------------------------
        //                   INSTANCE VARIABLES
        //----------------------------------------------------------

        private static TsoComparer tsoComparer;
        private IPriorityQueue<Callback> tsoQueue;
        private Queue<Callback> roQueue;

        // is the federate constrained?
        private bool constrained;

        // locking and concurrency //
        private Object thisLock;
        private Object condition;

    }

    internal class TsoComparer : IComparer<Callback>
    {
        /// <summary>
        /// Compares two TsoRec and returns a value indicating whether one is less than, 
        /// equal to, or greater than the other using Timestamp.
        /// If timestamps are equals, use federateindex and serial information.
        /// </summary>
        /// <param name="left">The first object to compare. </param>
        /// <param name="right">The second object to compare. </param>
        /// <returns>
        /// Returns less than zero if left is less than right.
        /// Returns Zero if left equals right.
        /// Returns greater than zero if left is greater than right.
        ///</returns>
        ///<remarks>
        /// Comparing a null reference with any reference type is allowed and does not generate 
        /// an exception. A null reference is considered to be less than any reference that is 
        /// not null.
        ///</remarks>
        public int Compare(Callback left, Callback right)
        {
            if (!left.IsTimeStamped() && !right.IsTimeStamped())
                return 0;
            else if (!left.IsTimeStamped())
                return -1;
            else if (!right.IsTimeStamped())
                return 1;
            else
            {
                int result = left.Time.CompareTo(right.Time);
                if (result == 0)
                {
                    result = left.FederateHandle.CompareTo(right.FederateHandle);

                    if (result == 0)
                    {
                        return left.InteractionIndex.CompareTo(right.InteractionIndex);
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return result;
                }
            }
        }
    }

}