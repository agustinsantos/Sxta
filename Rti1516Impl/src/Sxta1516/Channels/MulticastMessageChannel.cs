namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    // Import log4net classes.
    using log4net;


    /// <summary> 
    /// The class of all Multicast-based message channels
    /// </summary>
    /// <author> Agustin Santos.
    /// </author>
    public class MulticastMessageChannel : DGramMessageChannel
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
        public MulticastMessageChannel(ConnectionInfo multicastInfo)
        {
            try
            {
                // Create the Socket
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Set the reuse address option
                udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

                // Create an IPEndPoint and bind to it
                IPEndPoint bindaddrs = new IPEndPoint(IPAddress.Any, multicastInfo.Port);
                udpSocket.Bind(bindaddrs);

                // Define a MulticastOption object specifying the multicast group 
                // address and the local IPAddress.
                // The multicast group address is the same as the address used by the server.
                System.Net.IPAddress ipAddress = IPAddress.Parse(multicastInfo.Addr);
                MulticastOption mcastOption = new MulticastOption(ipAddress, IPAddress.Any);

                // IP multicast loopback.
                udpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, 1);

                // Add membership in the multicast group
                //BIG NOTE: If you have an exception at this point and your computer is disconnected
                // using Windows, the problem is due to a lack of a loopback interface.
                // You need to install a loopback interface Adapter.
                // The Microsoft Loopback adapter is a tool for testing in a 
                // virtual network environment where access to a network is not feasible.
                // Click Start, point to Settings, click Control Panel, and then double-click Add/Remove Hardware.
                // You will find it in the Manufacturers section, Microsoft. 
                udpSocket.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        new MulticastOption(ipAddress, IPAddress.Any));

                // Set the Time to Live                          
                udpSocket.SetSocketOption(SocketOptionLevel.IP,
                                            SocketOptionName.MulticastTimeToLive, 2);

                
                udpMulticastAddress = new IPEndPoint(ipAddress, multicastInfo.Port);
                udpSocket.Connect(udpMulticastAddress);
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
                log.Debug("Connected Multicast, own channel local point: " + udpSocket.LocalEndPoint
                        + " Multicast Address " + udpSocket.RemoteEndPoint);
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
            try
            {
                udpSocket.Send(packet, SocketFlags.None);
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
            get { return "multi://" + udpSocket.LocalEndPoint; }
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

        /// <summary> 
        /// Initialize the multicast address group and multicast port.
        /// Both address and port are selected from the allowed sets as
        /// defined in the related RFC documents.
        /// Multicast IP addresses are within the Class D range of 224.0.0.0-239.255.255.255
        /// </summary>
        protected internal System.Net.IPEndPoint udpMulticastAddress = null;


    }

}