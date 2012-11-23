namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    // Import log4net classes.
    using log4net;

    /// <summary>
    /// A struct with information for a connection
    /// </summary>
    public class ConnectionInfo
    {
        public string Addr;
        public int Port;
    }


    /// <summary> 
    /// An TCP message channel used for reliable/in-order message
    /// delivery.
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public class TCPMessageChannel : StreamMessageChannel
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
        /// Constructor for TCP channels.
        /// </summary>
        /// <param name="info"> a struct with the host name  and the port number 
        /// of the remote host
        /// </param>
        /// <exception cref="System.IO.IOException">  if an error occurs
        /// </exception>
        public TCPMessageChannel(ConnectionInfo info)
        {
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(info.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];
            tcpPacketAddress = new IPEndPoint(ipAddress, info.Port);

            try
            {
                Socket tmpS =
                    new Socket(tcpPacketAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tmpS.Connect(tcpPacketAddress);

                if (tmpS.Connected)
                {
                    tcpSocket = tmpS;
                }
                else
                    tcpSocket = null;
            }
            catch (SocketException e)
            {
                if (log.IsErrorEnabled)
                    log.Error("SocketException caught!. Message : " + e.Message);
                throw e;
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Exception caught!. Message : " + e.Message);
                throw e;
            }

            if (log.IsDebugEnabled)
                log.Debug("Connected, own channel local point: " + tcpSocket.LocalEndPoint);

            tcpStream = new NetworkStream(tcpSocket);
        }

        /// <summary> 
        /// Constructor for channels from federates.
        /// </summary>
        /// <param name="federateSocket">the newly accepted federate socket
        /// </param>
        /// <exception cref="System.IO.IOException">  if an IO error occurs
        /// </exception>
        public TCPMessageChannel(System.Net.Sockets.Socket socket)
        {
            tcpSocket = socket;
            tcpStream = new NetworkStream(tcpSocket);
            tcpPacketAddress = (IPEndPoint)tcpSocket.RemoteEndPoint;

            if (log.IsDebugEnabled)
                log.Debug("Connected, own channel local point: " + tcpSocket.LocalEndPoint);
        }

        /// <summary> 
        /// Returns the network <code>Stream</code> corresponding to this
        /// reliable channel.
        /// </summary>
        /// <returns> this channel's input stream
        /// </returns>
        /// <exception cref="System.IO.IOException">  in an IO error occurs
        /// </exception>
        override public System.IO.Stream InputStream
        {
            get { return tcpStream; }
        }

        /// <summary> 
        /// Returns the network <code>Stream</code> corresponding to this
        /// reliable channel.
        /// </summary>
        /// <returns> this channel's output stream
        /// </returns>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        override public System.IO.Stream OutputStream
        {
            get { return tcpStream; }
        }

        /// <summary> 
        /// Returns an <code>object</code> corresponding to the
        /// Best-effort component of this channel for lock operations.
        /// </summary>
        /// <returns> a channel's output object
        /// </returns>
        public override object SyncObject { get { return this; } }

        /// <summary> 
        /// Checks whether or not this channel is closed.
        /// </summary>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        public override bool IsClosed
        {
            get { return !tcpSocket.Connected; }

        }

        /// <summary> 
        /// The underlying socket.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        /// <returns> <code>true</code> if this channel has been closed,
        /// <code>false</code> otherwise
        /// </returns>
        public override Socket InternalSocket 
        {
            get { return tcpSocket; }
        }


        /// <summary> 
        /// Closes this channel.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public override void Close()
        {
            tcpSocket.Close();
        }

        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        public override string Uri
        {
            get { return "tcp://" + tcpSocket.LocalEndPoint; }
        }

        public override string ToString()
        {
            return "TCP Channel: Local Address: " + tcpSocket.LocalEndPoint + ", Remote address: " + tcpSocket.RemoteEndPoint;
        }

        /// <summary> The TCP socket, for reliable transportation.</summary>
        protected System.Net.Sockets.Socket tcpSocket;

        /// <summary> The associate network stream.</summary>
        protected NetworkStream tcpStream;

        /// <summary> The address for outgoing packets.</summary>
        protected System.Net.IPEndPoint tcpPacketAddress = null;

    }

}