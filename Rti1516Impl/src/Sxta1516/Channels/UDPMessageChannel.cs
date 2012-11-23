namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    // Import log4net classes.
    using log4net;


    /// <summary> 
    /// The class of all UDP-based message channels, like all best-effort transports 
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public class UDPMessageChannel : DGramMessageChannel, IDisposable
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
        /// Returns the maximum packet size supported by the best-effort
        /// component of this channel.
        /// </summary>
        /// <returns> the maximum packet size, in bytes
        /// </returns>
        override public int MaximumPacketSize
        {
            get
            {
                return MAXIMUM_PACKET_SIZE;
            }

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
            get { return udpSocket; }
        }


        /// <summary> 
        /// Constructor for UDP channels.
        /// </summary>
        /// <param name="localInfo"> The local endpoint to which you bind the UDP connection.
        /// <param name="remoteInfo"> A struct with the host name  and the port number 
        /// of the remote host
        /// </param>
        /// <exception cref="System.IO.IOException">  if an error occurs
        /// </exception>
        public UDPMessageChannel(ConnectionInfo localInfo, ConnectionInfo remoteInfo)
        {
            try
            {
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                if (localInfo != null)
                {
                    System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(localInfo.Addr);
                    System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];
                    udpLocalAddress = new IPEndPoint(ipAddress, localInfo.Port);
                }
                else
                {
                    udpLocalAddress = new IPEndPoint(IPAddress.Any, 0);
                }
                udpSocket.Bind(udpLocalAddress);

                if (remoteInfo != null)
                {
                    System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(remoteInfo.Addr);
                    System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];
                    udpRemoteAddress = new IPEndPoint(ipAddress, remoteInfo.Port);
                    udpSocket.Connect(udpRemoteAddress);
                }
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
                log.Debug("Connected UDP, own channel local point: " + udpSocket.LocalEndPoint.ToString());
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
            get { return !udpSocket.IsBound; }
        }

        /// <summary> 
        /// Sends a packet through this channel.
        /// </summary>
        /// <param name="packet">the packet to send
        /// </param>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public override void SendPacket(byte[] packet)
        {
            if (udpRemoteAddress == null)
                return;
            try
            {
                udpSocket.SendTo(packet, SocketFlags.None, udpRemoteAddress);
            }
            catch (System.Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error sending UDP packet:" + e);

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
        public override void ReceivePacket(out byte[] packet)
        {
            try
            {
                int available = udpSocket.Available;
                if (available > 0)
                {
                    byte[] recBuffer = new byte[available];

                    EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                    int dataRead = udpSocket.ReceiveFrom(recBuffer, ref endPoint);

                    if (dataRead < recBuffer.Length)
                    {
                        byte[] newArray = new byte[dataRead];
                        Buffer.BlockCopy(recBuffer, 0, newArray, 0, dataRead);
                        packet = newArray;
                    }
                    else
                        packet = recBuffer;
                }
                else
                    packet = new byte[0];
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.Message);
            }
        }

        /// <summary> 
        /// Closes this channel.
        /// </summary>
        /// <exception cref="System.IO.IOException"> if an IO error occurs
        /// </exception>
        public override void Close()
        {
            udpSocket.Close();
        }

        /// <summary>
        /// Get the Uri (Uniform Resource Identifier) for this Connection 
        /// </summary>
        public override string Uri
        {
            get { return "udp://" + udpSocket.LocalEndPoint; }
        }

        /// <summary> 
        /// The maximum packet size for the best-effort component of this
        /// channel.
        /// </summary>
        private const int MAXIMUM_PACKET_SIZE = 1500;

        /// <summary> 
        /// Indicates if the channel is closed
        /// </summary>
        protected bool closed = false;

        /// <summary>
        /// Underlying socket
        /// </summary>
        protected internal System.Net.Sockets.Socket udpSocket;

        /// <summary> The address for outgoing packets.</summary>
        protected internal System.Net.IPEndPoint udpRemoteAddress = null;

        /// <summary> The local endpoint to which you bind the UDP connection.</summary>
        private System.Net.IPEndPoint udpLocalAddress = null;



        #region IDisposable Members

        public void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

}