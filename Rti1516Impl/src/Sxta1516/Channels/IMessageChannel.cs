namespace Sxta.Rti1516.Channels
{
    using System;

    public delegate void OnDataAvailable(IMessageChannel sender);

    /// <summary> 
    /// The  interface of all message channelsincluiding both best-effort (packet-based) and 
    /// reliable (stream-based) components.
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public interface IMessageChannel
    {
        event OnDataAvailable ChannelDataAvailable;

        /// <summary> 
        /// Returns an <code>object</code> corresponding to
        /// this channel for lock operations.
        /// </summary>
        /// <returns> a channel's output object
        /// </returns>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        object SyncObject { get;}

        /// <summary> 
        /// Checks whether or not this channel is closed.
        /// </summary>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        bool IsClosed { get;}

        /// <summary> 
        /// Closes this channel.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an error occurs
        /// </exception>
        void Close();

        /// <summary> 
        /// Returns the maximum packet size supported by the best-effort
        /// component of this channel.
        /// </summary>
        /// <returns> 
        /// the maximum packet size, in bytes.
        /// A -1 value indicates an infinite size (as TCP)
        /// </returns>
        int MaximumPacketSize { get;}

        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        string Uri { get;}

        /// <summary>
        /// Fire data available event
        /// </summary>
        void FireDataAvailable();
    }

}