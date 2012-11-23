namespace Sxta.Rti1516.Channels
{
    using System;

    /// <summary> 
    /// The abstract base class of all DataGram-based message channels, like all best-effort transports 
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public abstract class DGramMessageChannel : NetworkMessageChannel
    {
        /// <summary> 
        /// Sends a packet through this channel.
        /// </summary>
        /// <param name="packet">the packet to send
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public abstract void SendPacket(byte[] packet);

        /// <summary> 
        /// Receives a packet through this channel.
        /// Blocks until a packet is available.
        /// </summary>
        /// <param name="packet">the object to contain the received packet
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public abstract void ReceivePacket(out byte[] packet);
    }
}