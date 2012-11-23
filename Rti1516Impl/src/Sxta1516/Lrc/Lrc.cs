using System;
using System.Collections.Generic;
using System.Text;

// Import log4net classes.
using log4net;

using Hla.Rti1516;

namespace Sxta.Rti1516.Lrc
{
    public class Lrc
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly long TICK_TIMEOUT = 500 * TimeSpan.TicksPerMillisecond; // 500 ms

        // Queues //
        protected LrcQueue callbackQueue;

        // State //
        protected LrcState state;

        public Lrc(LrcState state, LrcQueue callbackQueue)
        {
            this.state = state;
            this.callbackQueue = callbackQueue;
        }

        public void AddCallback(Callback callback)
        {
            lock (this)
            {
                this.callbackQueue.Add(callback);
            }
        }

        /// <summary>
        /// Process the callback queue for the given amount of time (in seconds). Wait at least as
        /// long as the given minimum time and keep processing messages up until the maximum time
        /// at most.
        /// <p/>
        /// <b>Note:</b> The current implementation ignores the minimum value, processing as many
        /// messages as possible up until the max time. If there are no messages, it will wait as
        /// long as the max time.
        /// </summary>
        /// <param name="min">the minimum value (in seconds)</param>
        /// <param name="max">the maximum value (in seconds)</param>
        /// <returns>true if there are more messages waiting to be processed, false otherwise</returns>
        public bool Tick(double min, double max)  // ms
        {
            lock (this)
            {
                // 1. check for a concurrent access issue //
                state.CheckAccess();

                // 2. signal that we are not ticking //
                state.Ticking = true;

                // 3. tick for the max amount of time. TickDuringTime() shouldn't return before that and it
                // will give us the most time to work (min is useless to us when we can just wait for max)
                long minTicks = (long)Math.Round(min * TimeSpan.TicksPerMillisecond);
                long maxTicks = (long)Math.Round(max * TimeSpan.TicksPerMillisecond);
                bool more = this.TickDuringTime(minTicks, maxTicks);

                // 4. reset the ticking status flag //
                state.Ticking = false;

                // 5. return true if there are more, false otherwise
                return more;
            }
        }

        /// <summary>
        /// Try and process a single callback. Once a single callback has been processed return. If
        /// there are no callbacks to process, waiting as long as the given time (in seconds) before
        /// giving up. Return true if there are more messages waiting to be processed, false if there
        /// are not.
        /// </summary>
        /// <param name="wait">The time to wait for a single message to be received (in seconds)</param>
        /// <returns>True if there are more messages waiting to be processed, false otherwise</returns>
        public bool TickSingle(double wait)  // ms
        {
            lock (this)
            {
                // 1. check for a concurrent access issue //
                state.CheckAccess();

                // 2. signal that we are not ticking //
                state.Ticking = true;

                // 3. tick for one message, waiting as long as the given wait time
                int maxTicks = (int)Math.Round(wait);
                bool more = this.TickSingle(maxTicks);

                // 4. reset the ticking status flag //
                state.Ticking = false;

                // 5. return info about any more available messages //
                return more;
            }
        }

        public void TickRO()
        {
            lock (this)
            {
                // 1. check for a concurrent access issue //
                state.CheckAccess();

                // 2. signal that we are not ticking //
                state.Ticking = true;

                // 3. tick RO messages
                long nowTime = System.DateTime.Now.Ticks;
                long endTime = nowTime + TICK_TIMEOUT;
                Callback c;

                while (!callbackQueue.IsEmptyROQueue && (nowTime < endTime))
                {
                    try
                    {
                        c = callbackQueue.PollROQueue();
                        if (c != null)
                        {
                            TickProcess(c);
                        }

                        // update the actual time
                        nowTime = System.DateTime.Now.Ticks;
                    }
                    catch (Exception e)
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error(e.Message);
                        }

                        break;
                    }
                }

                // 4. reset the ticking status flag //
                state.Ticking = false;
            }
        }

        public void TickTSO(ILogicalTime timeRequest)
        {
            lock (this)
            {
                // 1. check for a concurrent access issue //
                state.CheckAccess();

                // 2. signal that we are not ticking //
                state.Ticking = true;

                // 3. tick RO messages
                long nowTime = System.DateTime.Now.Ticks;
                long endTime = nowTime + TICK_TIMEOUT;

                Callback c;

                do
                {
                    try
                    {
                        c = callbackQueue.PollTSOQueue(timeRequest);
                        if (c != null)
                        {
                            TickProcess(c);
                        }

                        // update the actual time
                        nowTime = System.DateTime.Now.Ticks;
                    }
                    catch (Exception e)
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error(e.Message);
                        }

                        break;
                    }
                } while (c != null && (nowTime < endTime));

                // 4. reset the ticking status flag //
                state.Ticking = false;
            }
        }

        /// <summary>
        /// Do some tick processing at least the given amount of time (in ticks units). This method
        /// will return true if at least one message was processed in the given time, false otherwise. 
        /// Converting from milisecond to nanoseconds and from Ticks
        /// nanosecond = milisecond * 1.000.000; 
        /// TimeSpan.TicksPerMillisecond  = 10.000
        /// each tick = 100 nanosecond (10e-8 s)
        /// </summary>
        /// <param name="nanos"></param>
        /// <returns></returns>
        private bool TickDuringTime(long minTicks, long maxTicks) // Ticks per ms
        {
            long nowTime = System.DateTime.Now.Ticks;
            long endTime = nowTime + maxTicks;
            long ticksToFinish;

            int minTimeout = (int)Math.Round((double)(minTicks / TimeSpan.TicksPerMillisecond));
            Callback callback = callbackQueue.Poll(minTimeout, state.TimeRequest);

            if (callback != null)
            {
                // Process first callback
                try
                {
                    TickProcess(callback);
                }
                catch (Exception e)
                {
                    if (log.IsErrorEnabled)
                    {
                        log.Error(e.Message);
                    }
                }

                // Process others callbacks
                do
                {
                    try
                    {
                        // get a message, waiting for at most the available time
                        callback = callbackQueue.Poll(state.TimeRequest);

                        // if there is a message, process it
                        if (callback != null)
                        {
                            TickProcess(callback);
                        }
                    }
                    catch (Exception e)
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error(e.Message);
                        }
                        //break;
                    }
                    finally
                    {
                        // update the actual time
                        nowTime = System.DateTime.Now.Ticks;
                    }
                }
                while (endTime > nowTime && callback != null);
            }

            // return queue status information
            return !callbackQueue.IsEmpty;
        }

        /*
         * private bool TickDuringTime(long minTicks, long maxTicks) // Ticks per ms
        {
            long nowTime = System.DateTime.Now.Ticks;
            long endTime = nowTime + maxTicks;
            long ticksToFinish;

            // poll the queue with a timeout //
            Callback callback = null;
            do
            {
                ticksToFinish = endTime - nowTime;

                try
                {
                    // get a message, waiting for at most the available time
                    callback = callbackQueue.Poll((int)Math.Round((double)(ticksToFinish / TimeSpan.TicksPerMillisecond)), state.TimeRequest);

                    // if there is a message, process it
                    if (callback != null)
                    {
                        TickProcess(callback);
                    }
                    else
                    {
                        break;
                    }

                    // update the actual time
                    nowTime = System.DateTime.Now.Ticks;
                }
                catch (Exception e)
                {
                    if (log.IsErrorEnabled)
                    {
                        log.Error(e.Message);
                    }

                    break;
                }
            }
            while (endTime > nowTime);

            // return queue status information
            return !callbackQueue.IsEmpty;
        }
         * */

        /// <summary>
        /// This tick() implementation is meant to support the 1516-style evoke single callback
        /// facility. It will only process a <b>SINGLE</b> callback. If there is none available, it will
        /// wait at most until the given time out. Once the timeout has expired or a single callback
        /// has been processed, it will return: true if there are <i>more callbacks available for
        /// processing</i>, or false if there are no more callbacks.
        /// </summary>
        /// <param name="timeout">The time in nanoseconds to wait for a callback if there is not currently one available.</param>
        /// <returns>True if there are more callbacks waiting, false if there are none</returns>
        private bool TickSingle(int timeout)
        {
            try
            {
                // fetch a single callback, waiting only as long as we are given //
                Callback callback = callbackQueue.Poll(timeout, state.TimeRequest);

                // process the callback message if there is one //
                if (callback != null)
                    TickProcess(callback);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error(e.Message);
                }
            }

            // check to see if there are any more waiting messages //
            return !callbackQueue.IsEmpty;
        }

        /// <summary>
        /// Passes the given message to the callback sink (wrapped up in a context) 
        /// </summary>
        /// <param name="message">the message to processs</param>
        private void TickProcess(Callback callback)
        {
            try
            {
                if (callback != null)
                {
                    callback.Call();

                    if (log.IsDebugEnabled)
                    {
                        if (callback.Time != null)
                        {
                            log.Debug("PROCESS TSO [time = " + callback.Time + "] " + callback);
                        }
                        else
                        {
                            log.Debug("PROCESS RO " + callback);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("FAILURE Exception encountered while processing callback:" + e);
            }
        }

        public ILogicalTime GetLITS()
        {
            return this.callbackQueue.GetLITS();
        }
    }
}
