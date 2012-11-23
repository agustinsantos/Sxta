namespace Sxta.Rti1516.Channels
{
    using System;

    /// <summary> 
    /// The abstract base class of all stream-based message channels, like all reliable transports 
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public abstract class StreamMessageChannel : NetworkMessageChannel
    {
        /// <summary> 
        /// Returns the <code>Stream</code> corresponding to this
        /// reliable channel.
        /// </summary>
        /// <returns> this channel's input stream
        /// </returns>
        public abstract System.IO.Stream InputStream { get;}

        /// <summary> 
        /// Returns the <code>Stream</code> corresponding to this
        /// reliable channel.
        /// </summary>
        /// <returns> this channel's output stream
        /// </returns>
        public abstract System.IO.Stream OutputStream { get;}

        /// <summary> 
        /// Returns the maximum packet size supported by the best-effort
        /// component of this channel.
        /// </summary>
        /// <returns> the maximum packet size, in bytes
        /// </returns>
        public override int MaximumPacketSize { get { return -1; } }
    }
}