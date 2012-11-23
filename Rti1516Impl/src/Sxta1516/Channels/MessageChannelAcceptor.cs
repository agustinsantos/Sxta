namespace Sxta.Rti1516.Channels
{

    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary> 
    /// The abstract base class of all message channel acceptors.
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public abstract class MessageChannelAcceptor
    {
        /// <summary> The server socket for incoming connections.</summary>
        protected System.Net.Sockets.TcpListener serverSocket;

        public virtual TcpListener ServerSocket
        {
            get { return serverSocket; }
        }


        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        public virtual string Uri
        {
            get { return "tcp://" + serverSocket.Server.LocalEndPoint; }
        }

        /// <summary> 
        /// Accepts a new message channel.  Blocks until a message channel
        /// is available for acceptance.
        /// </summary>
        /// <returns> the newly accepted channel
        /// </returns>
        /// <exception cref="System.IO.IOException">  if an IO error occurs
        /// </exception>
        public abstract TCPMessageChannel AcceptMessageChannel();

        /// <summary>
        /// Return a EndPoint with the IP address of this MessageChannelAcceptor
        /// </summary>
        /// <returns></returns>
        public virtual EndPoint LocalEndPoint
        {
            get
            {
                return serverSocket.Server.LocalEndPoint;
            }
        }

        /// <summary> 
        /// Closes this acceptor.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public abstract void Close();

        /// <summary> 
        /// Gets a value that indicates whether the acceptor is closed. 
        /// </summary>
        public abstract bool IsClosed { get;}

    }
}