namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary> 
    /// An acceptor for Internet message channels.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class TCPMessageChannelAcceptor : MessageChannelAcceptor
    {
        protected bool closed = false;

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="port">the port on which to accept incoming connections
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public TCPMessageChannelAcceptor(ConnectionInfo info)
        {
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(info.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];

            serverSocket = new System.Net.Sockets.TcpListener(ipAddress, info.Port);
            serverSocket.Start();

            closed = false;
        }

        /// <summary> 
        /// Accepts a new message channel.  Blocks until a message channel
        /// is available for acceptance.
        /// </summary>
        /// <returns> the newly accepted channel
        /// </returns>
        /// <exception cref="System.IO.IOException">  if an IO error occurs
        /// </exception>
        public override TCPMessageChannel AcceptMessageChannel()
        {
            try
            {
                return new TCPMessageChannel(serverSocket.AcceptSocket());
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                if (closed)
                    return null;
                else
                    throw ex;
            }
        }

        /// <summary> 
        /// Closes this acceptor.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public override void Close()
        {
            closed = true;
            serverSocket.Stop();
        }

        /// <summary> 
        /// Gets a value that indicates whether the acceptor is closed. 
        /// </summary>
        public override bool IsClosed
        {
            get { return closed; }
        }


        /// <summary>
        /// Destructor - stops the listener listening
        /// </summary>
        ~TCPMessageChannelAcceptor()
        {
            if (!closed)
                Close();
        }
    }
}