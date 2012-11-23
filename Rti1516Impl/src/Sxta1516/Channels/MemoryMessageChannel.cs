namespace Sxta.Rti1516.Channels
{
    using System;

    // Import log4net classes.
    using log4net;


    /// <summary> 
    /// The class of Memory-Buffe based message channels, used for testing and inter-process communications 
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public class MemoryMessageChannel : IMessageChannel
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


        public event OnDataAvailable ChannelDataAvailable;


        /// <summary>
        /// Returns the maximum packet size supported by the best-effort
        /// component of this channel.
        /// </summary>
        /// <returns> the maximum packet size, in bytes
        /// </returns>
        public int MaximumPacketSize
        {
            get
            {
                return MAXIMUM_PACKET_SIZE;
            }

        }

        /// <summary> 
        /// Constructor for Memory channels.
        /// </summary>
        /// <exception cref="System.IO.IOException">  if an error occurs
        /// </exception>
        public MemoryMessageChannel()
        {
            try
            {
                memStream = new System.IO.MemoryStream();
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Exception caught!. Message : " + e.Message);
                throw e;
            }

            if (log.IsDebugEnabled)
                log.Debug("Created Memory Channel");
        }

                /// <summary> 
        /// Constructor for Memory channels.
        /// </summary>
        /// <exception cref="System.IO.IOException">  if an error occurs
        /// </exception>
        public MemoryMessageChannel(ChannelsManager mngr) : this()
        {
            channelManager = mngr;
        }

        /// <summary> 
        /// Returns an <code>object</code> corresponding to
        /// this channel for lock operations.
        /// </summary>
        /// <returns> a channel's output object
        /// </returns>
        public object SyncObject { get { return this; } }

        /// <summary> 
        /// Checks whether or not this channel is closed.
        /// </summary>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        public bool IsClosed
        {
            get
            {
                return memStream == null;
            }
        }

        /// <summary> 
        /// Sends a packet through this channel.
        /// </summary>
        /// <param name="packet">the packet to send
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public void SendPacket(byte[] packet)
        {
            try
            {
                memStream.Position = 0;
                memStream.Write(packet, 0, packet.Length);
                if (channelManager != null)
                    channelManager.FireDataAvailable(this);
            }
            catch (System.Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error writing packet:" + e.Message);

                throw e;
            }
        }

        /// <summary> 
        /// Receives a packet through this channel.
        /// Blocks until a packet is available.
        /// </summary>
        /// <param name="packet">the object to contain the received packet
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public void ReceivePacket(out byte[] packet)
        {
            try
            {
                memStream.Position = 0;
                packet = new byte[MAXIMUM_PACKET_SIZE];
                memStream.Read(packet, 0, packet.Length);
            }
            catch (System.Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error reading packet:" + e.Message);

                throw e;
            }
        }

        /// <summary> 
        /// Closes this channel.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public void Close()
        {
            memStream.Close();
            memStream = null;
        }

        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        public string Uri
        {
            get { return "memory://" ; }
        }

        /// <summary>
        /// Fire data available event
        /// </summary>
        public void FireDataAvailable()
        {
            ChannelDataAvailable(this);
        }


        /// <summary> 
        /// The maximum packet size for the best-effort component of this
        /// channel.
        /// </summary>
        public int MAXIMUM_PACKET_SIZE = 1500;

        /// <summary>
        /// Underlying stream for this transportation.
        /// </summary>
        private System.IO.MemoryStream memStream;

        private ChannelsManager channelManager;
    }

}