namespace Sxta.Rti1516.Channels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    // Import log4net classes.
    using log4net;


    public enum ChannelType { TCP, UDP, MULTICAST, MEMORY };

    public class ChannelsManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Boolean keepGoing;
        protected IList<MessageChannelAcceptor> channelAcceptorList = new List<MessageChannelAcceptor>();
        protected List<IMessageChannel> channelsList = new List<IMessageChannel>();
        protected List<Socket> listenList = new List<Socket>();
        protected Dictionary<Socket, IMessageChannel> channelsMap = new Dictionary<Socket, IMessageChannel>();
        protected Dictionary<ChannelType, IList<IMessageChannel>> channelsTypeMap = new Dictionary<ChannelType, IList<IMessageChannel>>();

        Thread channelsThread = null;

        //Set the event to nonsignaled state.
        protected ManualResetEvent clientConnected = new ManualResetEvent(false);

        // Track whether Dispose has been called.
        private bool disposed = false;

        public event OnDataAvailable ChannelDataAvailable;
        public event OnDataAvailable NewChannelAvailable;


        public bool IsServerRunning
        {
            get { return keepGoing; }
        }

        public bool IsClosed
        {
            get { return !keepGoing && channelAcceptorList.Count == 0; }
        }

        public ChannelsManager()
        {
        }

        public void Start()
        {
            lock (this)
            {
                if (channelsThread == null)
                {
                    channelsThread = new Thread(new ThreadStart(this.ThreadRun));
                    keepGoing = true;
                    clientConnected.Reset();
                    channelsThread.Start();

                    // Wait for the thread  
                    System.Threading.Monitor.Wait(this);
                }
                else
                    if (log.IsWarnEnabled)
                        log.Warn("Thread is already running. Doing nothing");
            }
        }

        public void StartNewConnection(Uri connection)
        {
            if (connection.Scheme.ToLower().Equals("tcp"))
            {
                StartNewTCPConnection(connection);
            }
            else if (connection.Scheme.ToLower().Equals("udp"))
            {
                StartNewUDPConnectedChannel(connection);
            }
            else if (connection.Scheme.ToLower().Equals("multi"))
            {
                StartNewMultiCastChannel(connection);
            }
            else if (connection.Scheme.ToLower().Equals("memory"))
            {
                StartNewMemoryChannel(connection);
            }
            else
            {
                throw new ArgumentException("Unkown protocol scheme: " + connection.Scheme + " in uri " + connection);
            }
        }

        /// <summary>
        /// Create a new acceptor using the connection info supplied by parameter.
        /// Start a new thread to process new connections
        /// </summary>
        /// <param name="listenerInfo">information for the connection, including address and port</param>
        /// <returns> the created acceptor </returns>
        public TCPMessageChannelAcceptor StartNewListener(ConnectionInfo listenerInfo)
        {
            TCPMessageChannelAcceptor channelAcceptor = null;

            lock (this)
            {
                try
                {
                    channelAcceptor = new TCPMessageChannelAcceptor(listenerInfo);

                    if (channelAcceptor == null)
                    {
                        return null;
                    }

                    channelAcceptorList.Add(channelAcceptor);
                    DoBeginAcceptSocket(channelAcceptor);

                    if (log.IsDebugEnabled)
                        log.Debug("Created acceptor running in port " + channelAcceptor.LocalEndPoint);
                }
                catch (SocketException e)
                {
                    if (log.IsWarnEnabled)
                        log.Warn("Can't open socket running in port :" + e.Message);
                    //maybe it is in use. Just Return.
                    return null;
                }
                catch (Exception e)
                {
                    throw e;
                }
                return channelAcceptor;
            }
        }

        /// <summary>
        /// Create a new UDP Channel using the connection info supplied by parameter.
        /// Start a new thread to process new connections
        /// </summary>
        /// <param name="info">information for the connection, including address and port</param>
        /// <returns> the created channel </returns>
        public UDPMessageChannel StartNewUDPLocalChannel(ConnectionInfo info)
        {
            UDPMessageChannel channel = null;
            lock (this)
            {
                try
                {
                    channel = new UDPMessageChannel(info, null);
                    if (channel == null)
                    {
                        return null;
                    }

                    AddChannel(channel);
                    if (log.IsDebugEnabled)
                        log.Debug("Created UDP Channel running in port " + channel.udpSocket.LocalEndPoint);
                }
                catch (SocketException e)
                {
                    if (log.IsWarnEnabled)
                        log.Warn("Can't open socket running in port :" + e.Message);
                    //maybe it is in use. Just Return.
                    return null;
                }
                catch (Exception e)
                {
                    throw e;
                }
                return channel;
            }
        }

        public IList<MessageChannelAcceptor> ChannelAcceptorsList
        {
            get { return channelAcceptorList; }
        }

        public IList<IMessageChannel> ChannelsList(ChannelType channelType)
        {
            if (channelsTypeMap.ContainsKey(channelType))
                return channelsTypeMap[channelType];
            else
                return null;
        }

        public void DumpChannelsInfo()
        {
            if (log.IsInfoEnabled)
            {
                log.Info("Channel Manager is running: " + this.IsServerRunning);
                log.Info("Channel Manager is closed: " + this.IsClosed);

                lock (listenList)
                {
                    if (channelAcceptorList.Count > 0)
                    {
                        log.Info("Acceptors:");
                        foreach (TCPMessageChannelAcceptor channelAcceptor in channelAcceptorList)
                        {
                            log.Info("\t" + channelAcceptor.LocalEndPoint);
                        }
                    }
                    if (channelsMap.Count > 0)
                    {
                        log.Info("Connections :");
                        foreach (Socket socket in channelsMap.Keys)
                        {
                            if (socket.ProtocolType == ProtocolType.Udp)
                                log.Info("\t" + socket.ProtocolType + "://" + socket.LocalEndPoint);
                            else
                                log.Info("\t" + socket.ProtocolType + "://" + socket.LocalEndPoint + " Remote Address =" + socket.RemoteEndPoint + " IsConnected :" + socket.Connected);
                        }
                    }
                }
            }
        }

        private void ThreadRun()
        {
            lock (this)
            {
                if (log.IsDebugEnabled)
                {
                    System.Threading.Thread.CurrentThread.Name = "ChannelsServer(" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")";
                }

                // notifies the server that it can continue
                System.Threading.Monitor.Pulse(this);
            }

            while (keepGoing)
            {
                //if (log.IsDebugEnabled)
                //    log.Debug("In Thread Inside keepGoing....");
                try
                {
                    BlockUntilDataIsAvailable();
                }
                catch (System.Threading.ThreadAbortException)
                {
                    if (log.IsDebugEnabled)
                        log.Debug("Thread has been aborted....");

                    break;
                }
                catch (System.IO.IOException ioe)
                {
                    if (log.IsErrorEnabled)
                        log.Error("IOException: " + ioe);
                }
            }

            if (log.IsDebugEnabled)
                log.Debug("Exiting Thread ....");
        }

        private bool CheckIfSocketIsConnected(Socket socket)
        {
            if (!socket.Connected && channelsMap.ContainsKey(socket))
            {
                if (log.IsDebugEnabled)
                    log.Debug("Client running in port " + socket.RemoteEndPoint + " is disconnected");

                RemoveChannel(channelsMap[socket]);
                if (listenList.Count == 0)
                    clientConnected.Reset();
                return false;
            }
            return true;
        }

        private int timeout = 3 * 1000 * 1000; //3 sg
        private void BlockUntilDataIsAvailable()
        {
            System.Collections.ArrayList readListenList;

            if (listenList.Count == 0 && keepGoing)
            {
                // Wait until a connection is made and processed before 
                // continuing.
                clientConnected.WaitOne();
                //if (log.IsDebugEnabled)
                //    log.Debug("After WaitOne in BlockUntilDataIsAvailable");
                if (listenList.Count == 0)
                    return;
            }


            readListenList = new ArrayList(listenList);
            ArrayList empty = new ArrayList();

            // A -1 value indicates an infinite time-out.
            // but a bug on MS (or is it just me?) makes the following select return immediately
            // making MS runtime hunging all the CPU
            // so I decide to make a loop and wait some seconds
            Socket.Select(readListenList, empty, empty, timeout);
            //if (log.IsDebugEnabled)
            //    log.Debug("After Socket.Select in BlockUntilDataIs Available");

            foreach (Socket socket in readListenList)
            {
                if (socket.SocketType == SocketType.Stream)
                {
                    try
                    {
                        if (socket.Available == 0 && CheckIfSocketIsConnected(socket))
                        {

                            // If the amounts of data available is 0 
                            // then we check if the socket has been disconnected
                            // socket.Connected gets a value that indicates whether a Socket 
                            // is connected to a remote host as of the last Send or Receive operation

                            byte[] buffer = new byte[1];
                            int len = socket.Send(buffer, 0);
                        }
                        else
                        {
                            if (log.IsDebugEnabled)
                                log.Debug("Data Available on Stream socket " + socket.Available);
                            FireDataAvailable(channelsMap[socket]);
                        }
                    }
                    catch (Exception)
                    {
                        //Check if the socket is connected
                        CheckIfSocketIsConnected(socket);
                    }
                }
                else if (socket.SocketType == SocketType.Dgram)
                {
                    if (log.IsDebugEnabled)
                        log.Debug("Data Available on Dgram socket " + socket.Available);
                    FireDataAvailable(channelsMap[socket]);
                }
            }
        }



        /// <summary>
        /// Fire data available event
        /// </summary>
        protected internal void FireDataAvailable(IMessageChannel channel)
        {
            try
            {
                if (ChannelDataAvailable != null)
                    ChannelDataAvailable(channel);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error in FireDataAvailable: " + e.Message);
            }
        }


        /// <summary>
        /// Fire new channel is open event
        /// </summary>
        protected void FireChannelAvailable(IMessageChannel channel)
        {
            try
            {
                if (NewChannelAvailable != null)
                    NewChannelAvailable(channel);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error in FireChannelAvailable: " + e.Message);
            }
        }

        // Accept one client connection asynchronously.
        public void DoBeginAcceptSocket(MessageChannelAcceptor listener)
        {
            // Start to listen for connections from a client.
            if (log.IsDebugEnabled)
                log.Debug("Waiting for a connection...");

            // Accept the connection. 
            // BeginAcceptSocket() creates the accepted socket.
            listener.ServerSocket.BeginAcceptSocket(new AsyncCallback(DoAcceptSocketCallback), listener);
        }

        // Process the client connection.
        public void DoAcceptSocketCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            MessageChannelAcceptor listener = (MessageChannelAcceptor)ar.AsyncState;

            if (listener.IsClosed)
                return;

            // End the operation and display the received data on the
            //console.
            Socket clientSocket = listener.ServerSocket.EndAcceptSocket(ar);

            // Process the connection here. (Add the client to a 
            // server table, read data, etc.)
            if (log.IsDebugEnabled)
            {
                log.Debug("Client connected to " + clientSocket.LocalEndPoint + " from " + clientSocket.RemoteEndPoint);
                log.Debug("Timeouts in mls: Receive " + clientSocket.ReceiveTimeout + ", Send " + clientSocket.SendTimeout);
            }
            // The socket will linger for 10 seconds after Socket.Close is called.
            LingerOption lingerOption = new LingerOption(true, 10);
            clientSocket.LingerState = lingerOption;

            TCPMessageChannel channel = new TCPMessageChannel(clientSocket);
            channel.ChannelDataAvailable += new OnDataAvailable(ProcessChannelDataAvailable);
            AddChannel(channel);

            // Accept a new connection
            if (keepGoing)
                DoBeginAcceptSocket(listener);
        }

        public void Close()
        {
            lock (this)
            {
                keepGoing = false;

                foreach (NetworkMessageChannel ch in channelsList)
                {
                    try
                    {
                        ch.InternalSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception)
                    {
                        //The socket could be disposed before the channel manager and 
                        // a objectdisposed exception or an invalidoperation exception
                        //could be raised
                    }
                }

                CloseThread();

                List<MessageChannelAcceptor> tmpAcceptorList = new List<MessageChannelAcceptor>(channelAcceptorList);
                foreach (MessageChannelAcceptor acceptor in tmpAcceptorList)
                {
                    CloseAcceptor(acceptor);
                }

                // I can't remove the channel until the thread is dead. If I remove the channel from
                // all data structures (hashs and maps), the sockets could be disposed
                // producing errors in the thread
                List<IMessageChannel> tmpChannelsList = new List<IMessageChannel>(channelsList);
                foreach (IMessageChannel ch in tmpChannelsList)
                {
                    ch.Close();
                    RemoveChannel(ch);
                }

                channelsThread = null;
            }
        }

        public void CloseAcceptor(MessageChannelAcceptor acceptor)
        {
            acceptor.Close();
            if (channelAcceptorList.Contains(acceptor))
                channelAcceptorList.Remove(acceptor);
        }

        private void CloseThread()
        {
            int count = 0;
            while (true)
            {
                if (log.IsDebugEnabled)
                    log.Debug("Trying to join our thread");

                // Signal the calling thread to continue.
                clientConnected.Set();
                if (channelsThread != null && channelsThread.ThreadState == ThreadState.Running)
                {
                    channelsThread.Join(1000 * 1000);
                    count++;
                }
                else
                    break;
                //something is wrong with our thread. 
                // The thread is busy. This could be produced
                // by the high level protocol layer. 
                if (count == 30)
                {
                    //throw new Exception("Internal thread is busy. I can't close the Channels Manager.");
                    channelsThread.Abort();
                    break;
                }
            }
        }

        private void AddChannel(IMessageChannel ch)
        {
            lock (listenList)
            {
                if (ch is NetworkMessageChannel)
                {
                    NetworkMessageChannel channel = ch as NetworkMessageChannel;

                    listenList.Add(channel.InternalSocket);
                    channelsMap.Add(channel.InternalSocket, channel);
                    channelsList.Add(channel);

                    if (channel is TCPMessageChannel)
                    {
                        if (!channelsTypeMap.ContainsKey(ChannelType.TCP))
                            channelsTypeMap.Add(ChannelType.TCP, new List<IMessageChannel>());
                        channelsTypeMap[ChannelType.TCP].Add(channel);
                    }
                    else if (channel is UDPMessageChannel)
                    {
                        if (!channelsTypeMap.ContainsKey(ChannelType.UDP))
                            channelsTypeMap.Add(ChannelType.UDP, new List<IMessageChannel>());
                        if ((channel as UDPMessageChannel).udpRemoteAddress != null)
                            channelsTypeMap[ChannelType.UDP].Add(channel);
                    }

                    else if (channel is MulticastMessageChannel)
                    {
                        if (!channelsTypeMap.ContainsKey(ChannelType.MULTICAST))
                            channelsTypeMap.Add(ChannelType.MULTICAST, new List<IMessageChannel>());
                        if ((channel as MulticastMessageChannel).udpMulticastAddress != null)
                            channelsTypeMap[ChannelType.MULTICAST].Add(channel);
                    }
                }
                else if (ch is MemoryMessageChannel)
                {
                    MemoryMessageChannel channel = ch as MemoryMessageChannel;
                    if (!channelsTypeMap.ContainsKey(ChannelType.MEMORY))
                        channelsTypeMap.Add(ChannelType.MEMORY, new List<IMessageChannel>());
                    channelsTypeMap[ChannelType.MEMORY].Add(channel);
                }
                else
                {
                    throw new Exception("Unknown Message Channel");
                }

                FireChannelAvailable(ch);

                if (listenList.Count == 1)
                {
                    // Signal the calling thread to continue.
                    clientConnected.Set();
                }
            }
        }

        private void RemoveChannel(IMessageChannel ch)
        {
            NetworkMessageChannel channel = ch as NetworkMessageChannel;
            lock (listenList)
            {
                listenList.Remove(channel.InternalSocket);
                channelsMap.Remove(channel.InternalSocket);

                if (ch is NetworkMessageChannel)
                {
                    channelsList.Remove(channel);

                    if (channel is TCPMessageChannel)
                    {
                        channelsTypeMap[ChannelType.TCP].Remove(channel);
                    }
                    else if (channel is UDPMessageChannel)
                    {
                        channelsTypeMap[ChannelType.UDP].Remove(channel);
                    }

                    else if (channel is MulticastMessageChannel)
                    {
                        channelsTypeMap[ChannelType.MULTICAST].Remove(channel);
                    }
                }
                else if (ch is MemoryMessageChannel)
                {
                    channelsTypeMap[ChannelType.MEMORY].Remove(channel);
                }
                else
                {
                    throw new Exception("Unknown Message Channel");
                }
                //FireChannelRemove(channel);
            }
        }

        private void StartNewTCPConnection(Uri connection)
        {
            ConnectionInfo info = new ConnectionInfo();
            info.Addr = connection.DnsSafeHost;
            info.Port = connection.Port;

            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(info.Addr);
            System.Net.IPAddress ipAddress = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ipAddress, info.Port);
            foreach (MessageChannelAcceptor acceptor in channelAcceptorList)
            {
                if (!acceptor.IsClosed && acceptor.LocalEndPoint != null && acceptor.LocalEndPoint.Equals(endpoint))
                {
                    //We are trying to connect to our own acceptor!
                    return;
                }
            }

            TCPMessageChannel channel = new TCPMessageChannel(info);

            if (log.IsDebugEnabled)
            {
                log.Debug("Connected from " + channel.InternalSocket.LocalEndPoint +
                          " to " + channel.InternalSocket.RemoteEndPoint);
                log.Debug("Timeouts in mls: Receive " + channel.InternalSocket.ReceiveTimeout +
                          ", Send " + channel.InternalSocket.SendTimeout);
            }

            AddChannel(channel);
        }

        private void StartNewUDPConnectedChannel(Uri connection)
        {
            ConnectionInfo info = new ConnectionInfo();
            info.Addr = connection.DnsSafeHost;
            info.Port = connection.Port;

            UDPMessageChannel channel = new UDPMessageChannel(null, info);
            AddChannel(channel);
            if (log.IsDebugEnabled)
                log.Debug("Created UDP Channel running in port " + channel.udpSocket.LocalEndPoint +
                          " remote addrs: " + channel.udpSocket.RemoteEndPoint);
        }

        private void StartNewMultiCastChannel(Uri connection)
        {
            ConnectionInfo info = new ConnectionInfo();
            info.Addr = connection.DnsSafeHost;
            info.Port = connection.Port;

            MulticastMessageChannel channel = new MulticastMessageChannel(info);
            AddChannel(channel);
            if (log.IsDebugEnabled)
                log.Debug("Created Multicast Channel running in port " + channel.udpSocket.LocalEndPoint +
                          " remote addrs: " + channel.udpSocket.RemoteEndPoint);
        }

        protected void StartNewMemoryChannel(Uri connection)
        {

            MemoryMessageChannel channel = new MemoryMessageChannel(this);
            if (connection.Query != null && connection.Query.StartsWith("?PACKET_SIZE="))
                channel.MAXIMUM_PACKET_SIZE = int.Parse(connection.Query.Substring(13));
            AddChannel(channel);
            if (log.IsDebugEnabled)
                log.Debug("Created Memory Channel.");
        }

        private void ProcessChannelDataAvailable(IMessageChannel channel)
        {
            if (log.IsDebugEnabled)
                log.Debug("Message from channel " + channel);
        }

        /// <summary>
        /// Destructor - stops the thread
        /// </summary>
        ~ChannelsManager()
        {
            Close();
        }
    }
}

