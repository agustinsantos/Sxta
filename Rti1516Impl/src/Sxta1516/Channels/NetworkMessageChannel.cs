namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Net.Sockets;

    /// <summary> 
    /// The abstract base class of all network-based message channels including all TCP and UDP transports 
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public abstract class NetworkMessageChannel : IMessageChannel
    {

        public event OnDataAvailable ChannelDataAvailable;

        /// <summary> 
        /// The underlying socket.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        public abstract Socket InternalSocket { get;}


        /// <summary> 
        /// Returns an <code>object</code> corresponding to the
        /// Best-effort component of this channel for lock operations.
        /// </summary>
        /// <returns> a channel's output object
        /// </returns>
        public abstract object SyncObject { get;}

        /// <summary> 
        /// Checks whether or not this channel is closed.
        /// </summary>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        public abstract bool IsClosed { get;}

        /// <summary> 
        /// Closes this channel.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public abstract void Close();

        /// <summary> 
        /// Returns the maximum packet size supported by the best-effort
        /// component of this channel.
        /// </summary>
        /// <returns> the maximum packet size, in bytes
        /// </returns>
        public abstract int MaximumPacketSize { get;}

        /// <summary>
        /// Fire data available event
        /// </summary>
        public void FireDataAvailable()
        {
            ChannelDataAvailable(this);
        }

        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        public abstract string Uri { get;}
    }

}
