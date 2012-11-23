namespace Sxta.Rti1516.BoostrapProtocol
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;

    // Import log4net classes.
    using log4net;

    using Hla.Rti1516;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.Interactions;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.XrtiHandles;

    public partial class InteractionManager
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

        protected ChannelsManager channelManager;

        protected IHlaEncodingSerializer serializer;

        /// <summary> The outgoing interaction counter.</summary>
        private int outgoingInteractionCounter;

        /// <summary> The incoming interaction sequence number.</summary>
        private int incomingInteractionNumber;

        /// <summary> The incoming interaction bufferStream.</summary>
        private byte[] incomingInteractionBuffer;

        /// <summary> The amount of the incoming interaction bufferStream that's filled.</summary>
        private int incomingInteractionBufferFilled;

        private long interactionFragmentMessageHandle = 1;

        protected XrtiSerializerManager serializerManager = new XrtiSerializerManager();

        protected Dictionary<TransportationType, ChannelType> channel2TransportationMap = new Dictionary<TransportationType, ChannelType>();

        private DescriptorManager descriptorManager;

        private string peerName = "NoName";
        private string peerDescription = "No Description";

        /// <summary>
        /// sets/gets the name of the peer
        /// </summary>
        public string PeerName
        {
            get { return peerName; }
            set { peerName = value; }
        }

        /// <summary>
        /// Gets/Sets Description of the Peer
        /// In Jxta this function has been deprecated. It has been extended
        /// to allow arbitrary XML content with the getDesc and setDesc methods
        /// </summary>
        public string PeerDescription
        {
            get { return peerDescription; }
            set { peerDescription = value; }
        }

        /* COMMENT ANGEL: No se si tiene sentido tener esta propiedad en InteractionManager.
         *                Actualmente esta información (a que federación pertenece el objeto, importante para el envio de las interacciones)
         *                se encuentra en cada HLAobjectRoot en la propiedad OwnFederationExecutionHandle
         * 
         
        /// <summary> The handle of the federation execution to which the federate is joined.</summary>
        private long joinedFederationExecutionHandle = -1;
       
        public long InternalHandle
        {
            get { return joinedFederationExecutionHandle; }
            set { joinedFederationExecutionHandle = value; }
        }
        */

        public DescriptorManager DescriptorManager
        {
            get { return descriptorManager; }
        }

        public XrtiSerializerManager SerializerManager
        {
            get { return serializerManager; }
        }

        public void RegisterSerializer(Type objectType, long handle, IHlaEncodingSerializer aSerializer)
        {
            serializerManager.RegisterSerializer(objectType, handle, aSerializer);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dm"> a Descriptor Manager</param>
        /// <param name="channelMangr"> The channel manager of all interaction managed by this interaction manager</param>
        public InteractionManager(DescriptorManager dm, ChannelsManager channelMngr)
        {
            descriptorManager = dm;

            RegisterAssemblyHelpers(Assembly.GetExecutingAssembly());

            // TODO ANGEL: Gualtrapa. Borrame!!!!!!!!
            //RegisterHelloWorldHelperClass();

            if (descriptorManager != null)
            {
                //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.BootstrapObjectModel);
                RegisterHelperClass(Sxta.Rti1516ResourcesNames.BootstrapObjectModel);
            }

            channelManager = channelMngr;
            channelManager.ChannelDataAvailable += new OnDataAvailable(this.OnDataAvailable);
            channelManager.NewChannelAvailable += new OnDataAvailable(this.OnChannelAvailable);

            serializer = new HlaEncodingSerializer(1, null, serializerManager);

            channel2TransportationMap.Add(TransportationType.HLA_RELIABLE, ChannelType.TCP);
            channel2TransportationMap.Add(TransportationType.HLA_BEST_EFFORT, ChannelType.UDP);
            channel2TransportationMap.Add(new TransportationType("multicast"), ChannelType.MULTICAST);
            channel2TransportationMap.Add(new TransportationType("memory"), ChannelType.MEMORY);
        }


        public void SetTransportMapping(Dictionary<TransportationType, ChannelType> mapping)
        {
            channel2TransportationMap = mapping;
        }

        /// <summary> 
        /// Sends an interaction.
        /// </summary>
        /// <param name="msg">the message or interaction to send
        /// </param>
        public virtual void SendInteraction(TransportationType transport, BaseInteractionMessage msg)
        {
            if (!channel2TransportationMap.ContainsKey(transport))
                throw new Exception("Transport unknown");

            foreach (KeyValuePair<TransportationType, ChannelType> keyValue in channel2TransportationMap)
            {
                if (keyValue.Key.Equals(transport))
                {
                    try
                    {
                        ChannelType channelType = keyValue.Value;
                        if (channelType.Equals(ChannelType.TCP))
                        {
                            SendRealiableInteraction(channelType, msg);
                        }
                        else
                        {
                            SendBestEffortInteraction(channelType, msg);
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO. What should I do when the channel is remotly closed and lost??.
                        //TODO.
                        if (log.IsWarnEnabled)
                            log.Warn("Error sending interaction: " + e.Message);
                    }
                }
            }
        }


        /// <summary> 
        /// Sends an interaction with reliable (TCP) transportation.
        /// </summary>
        /// <param name="msg">the interaction message
        /// </param>
        /// <exception cref="InteractionClassNotPublished"> if the interaction class is not published
        /// </exception>
        /// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
        /// </exception>
        /// <exception cref="InteractionParameterNotDefined"> if one of the parameters is undefined
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress
        /// </exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public virtual void SendRealiableInteraction(ChannelType channelType, BaseInteractionMessage msg)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */

            IList<IMessageChannel> list = channelManager.ChannelsList(channelType);
            if (list != null)
                foreach (StreamMessageChannel messageChannel in list)
                {
                    try
                    {
                        SendRealiableInteraction(messageChannel, msg);
                    }
                    catch (Exception e)
                    {
                        //TODO. What should I do when the channel is remotly closed and lost??.
                        //TODO.
                        if (log.IsWarnEnabled)
                            log.Warn("Error sending interaction: " + e.Message);
                    }
                }
        }

        public virtual void SendRealiableInteraction(StreamMessageChannel messageChannel, BaseInteractionMessage msg)
        {
            try
            {
                lock (messageChannel.SyncObject)
                {
                    /*
                    if (log.IsDebugEnabled)
                        log.Debug("Sending Interaction: " + msg);
                    */
                    serializer.Serialize(messageChannel.OutputStream, msg);
                }
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.Message);
            }
        }

        /// <summary> 
        /// Sends an interaction with best-effort (UDP) transportation.
        /// </summary>
        /// <param name="msg">the interaction message
        /// </param>
        /// <exception cref="InteractionClassNotPublished"> if the interaction class is not published
        /// </exception>
        /// <exception cref="InteractionClassNotDefined"> if the interaction class is undefined
        /// </exception>
        /// <exception cref="InteractionParameterNotDefined"> if one of the parameters is undefined
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress
        /// </exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public virtual void SendBestEffortInteraction(ChannelType channelType, BaseInteractionMessage msg)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */

            try
            {
                System.IO.MemoryStream bufferStream = new System.IO.MemoryStream();
                serializer.Serialize(bufferStream, msg);

                byte[] buffer = bufferStream.ToArray();

                IList<IMessageChannel> list = channelManager.ChannelsList(channelType);
                if (list != null)
                    foreach (IMessageChannel messageChannel in list)
                    {
                        SendBestEffortInteraction(messageChannel, buffer);
                    }
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.Message);
            }
        }

        private void SendBestEffortInteraction(IMessageChannel messageChannel, BaseInteractionMessage msg)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */
            try
            {
                System.IO.MemoryStream bufferStream = new System.IO.MemoryStream();
                serializer.Serialize(bufferStream, msg);

                byte[] buffer = bufferStream.ToArray();

                SendBestEffortInteraction(messageChannel, buffer);
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.Message);
            }
        }

        private void SendBestEffortInteraction(IMessageChannel messageChannel, byte[] buffer)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */

            try
            {
                if (buffer.Length <= messageChannel.MaximumPacketSize)
                {
                    lock (messageChannel.SyncObject)
                    {
                        if (messageChannel is DGramMessageChannel)
                            (messageChannel as DGramMessageChannel).SendPacket(buffer);
                        else if (messageChannel is MemoryMessageChannel)
                            (messageChannel as MemoryMessageChannel).SendPacket(buffer);

                    }
                }
                else
                {
                    //We substract 45 because we need some space for the headers
                    //HlaEncodingSerializer needs 4 for MAGICNUMBER + 4 for version = 8 bytes
                    //BaseInteractionMessage needs 8 for FederationExecutionHandle + 4 UserSuppliedTag + 8 for InteractionClassHandle = 20 bytes
                    //HLAinteractionFragment needs 4 for InteractionNumber + 4 for InteractionSize +
                    //                             4 for FragmentOffset + 4 for length of fragment = 16 bytes
                    int packetSize = messageChannel.MaximumPacketSize - 45;

                    byte[] fragment = new byte[packetSize];

                    for (int i = 0; i < buffer.Length; i += packetSize)
                    {
                        if ((buffer.Length - i) >= packetSize)
                        {
                            Array.Copy(buffer, i, fragment, 0, packetSize);
                        }
                        else
                        {
                            fragment = new byte[buffer.Length - i];

                            Array.Copy(buffer, i, fragment, 0, fragment.Length);
                        }

                        HLAinteractionFragmentMessage fragmentMessage = new HLAinteractionFragmentMessage();
                        //fragmentMessage.FederationExecutionHandle = msg.FederationExecutionHandle;
                        //fragmentMessage.UserSuppliedTag = msg.UserSuppliedTag;
                        fragmentMessage.InteractionClassHandle = serializerManager.GetHandle(typeof(HLAinteractionFragmentMessage));
                        fragmentMessage.InteractionNumber = outgoingInteractionCounter;
                        fragmentMessage.InteractionSize = buffer.Length;
                        fragmentMessage.FragmentOffset = i;
                        fragmentMessage.FragmentContents = fragment;

                        SendBestEffortInteraction(messageChannel, fragmentMessage);
                    }

                    outgoingInteractionCounter++;
                }
            }
            catch (System.Exception e)
            {
                throw new RTIinternalError(e.Message);
            }
        }

        protected long InteractionFragmentMessageHandle
        {
            get { return interactionFragmentMessageHandle; }
            set { interactionFragmentMessageHandle = value; }
        }

        /// <summary>
        /// Read data from the reliable channel
        /// </summary>
        public void ReliableRead(StreamMessageChannel messageChannel)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */

            try
            {
                lock (messageChannel.SyncObject)
                {
                    object tmp = BaseInteractionMessage.NullBaseInteractionMessage;
                    object msg = serializer.Deserialize(messageChannel.InputStream, ref tmp);
                    if (msg is BaseInteractionMessage)
                    {
                        ReceiveInteraction(msg as BaseInteractionMessage);
                    }
                }
            }
            catch (System.Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(" Exception on ReliableRead: " + e.Message);
                //throw new RTIinternalError(e.Message);
            }
        }

        /// <summary>
        /// Read data from the best effort channel
        /// </summary>
        public void BestEffortRead(IMessageChannel messageChannel)
        {
            /*
            if (log.IsInfoEnabled)
                log.Info("On " + System.Reflection.MethodBase.GetCurrentMethod());
            */

            try
            {
                lock (messageChannel.SyncObject)
                {
                    byte[] buf;
                    if (messageChannel is DGramMessageChannel)
                        (messageChannel as DGramMessageChannel).ReceivePacket(out buf);
                    else
                        (messageChannel as MemoryMessageChannel).ReceivePacket(out buf);

                    System.IO.MemoryStream stream = new System.IO.MemoryStream(buf);
                    object tmp = BaseInteractionMessage.NullBaseInteractionMessage;
                    object msg = serializer.Deserialize(stream, ref tmp);
                    if (msg is HLAinteractionFragmentMessage)
                    {
                        OnReceiveHLAinteractionFragment(msg as HLAinteractionFragmentMessage);
                    }
                    else if (msg is BaseInteractionMessage)
                    {
                        ReceiveInteraction(msg as BaseInteractionMessage);
                    }
                }
            }
            catch (System.IO.IOException ioe)
            {
                throw new RTIinternalError(ioe.ToString());
            }
        }

        /// <summary>
        /// Notifies that there are data available in the channel
        /// </summary>
        public void OnDataAvailable(IMessageChannel channel)
        {
            if (channel is StreamMessageChannel)
                ReliableRead(channel as StreamMessageChannel);
            else
                BestEffortRead(channel);
        }

        /// <summary>
        /// Notifies that there is a new channel available
        /// </summary>
        public void OnChannelAvailable(IMessageChannel ch)
        {
            PeerAdvertisementInteractionMessage msg = new PeerAdvertisementInteractionMessage();
            msg.InteractionClassHandle = SerializerManager.GetHandle(typeof(PeerAdvertisementInteractionMessage));
            msg.PeerName = PeerName;
            msg.PeerDescription = PeerDescription;
            msg.PeerChannels = new ConnectionList();
            foreach (MessageChannelAcceptor channel in channelManager.ChannelAcceptorsList)
            {
                msg.PeerChannels.Add(channel.Uri);
            }

            IList<IMessageChannel> list = channelManager.ChannelsList(ChannelType.MULTICAST);
            if (list != null)
                foreach (IMessageChannel channel in list)
                {
                    msg.PeerChannels.Add(channel.Uri);
                }

            if (ch is TCPMessageChannel)
            {
                if (log.IsDebugEnabled)
                    log.Debug("New Channel detected. Sending a PeerAdvertisement message");

                SendRealiableInteraction(ch as StreamMessageChannel, msg);
            }
            else if (ch is MulticastMessageChannel)
            {
                if (log.IsDebugEnabled)
                    log.Debug("New Channel detected. Sending a PeerAdvertisement message");

                SendBestEffortInteraction(ch as DGramMessageChannel, msg);
            }
        }

        /// <summary> 
        /// A piece of a fragmented interaction. 
        /// </summary>
        /// <param name="msg">a fragmented interaction message
        /// </param>
        /// <exception cref="InteractionClassNotRecognized">  if the interaction class was not recognized
        /// </exception>
        /// <exception cref="InteractionParameterNotRecognized">  if a parameter of the interaction was not
        /// recognized
        /// </exception>
        /// <exception cref="InteractionClassNotSubscribed">  if the federate had not subscribed to the
        /// interaction class
        /// </exception>
        /// <exception cref="FederateInternalError"> if an error occurs in the federate
        /// </exception>
        public void OnReceiveHLAinteractionFragment(HLAinteractionFragmentMessage msg)
        {
            if (log.IsDebugEnabled)
                log.Debug("Received Message = " + msg.ToString());

            if (incomingInteractionBuffer == null || incomingInteractionNumber != msg.InteractionNumber)
            {
                incomingInteractionBuffer = new byte[msg.InteractionSize];
                incomingInteractionNumber = msg.InteractionNumber;
                incomingInteractionBufferFilled = 0;
            }

            Array.Copy(msg.FragmentContents, 0, incomingInteractionBuffer, msg.FragmentOffset, msg.FragmentContents.Length);

            incomingInteractionBufferFilled += msg.FragmentContents.Length;

            if (incomingInteractionBufferFilled == msg.InteractionSize)
            {
                System.IO.MemoryStream buffer = new System.IO.MemoryStream(incomingInteractionBuffer);
                object tmp = BaseInteractionMessage.NullBaseInteractionMessage;
                object newMessage = serializer.Deserialize(buffer, ref tmp);
                if (newMessage is BaseInteractionMessage)
                {
                    ReceiveInteraction(newMessage as BaseInteractionMessage);
                }
            }
        }

        private IDictionary<String, Type> FOMTypes = new Dictionary<String, Type>();

        public void RegisterHelperClass(String uriFOM)
        {
            DescriptorManager.AddDescriptors(uriFOM);
            String FOMname = DescriptorManager.ObjectModelInformation.Name;

            if (FOMTypes.ContainsKey(FOMname))
            {
                if (log.IsDebugEnabled)
                    log.Debug("InteractionHelper for " + FOMname + " have been created");

                System.Activator.CreateInstance(FOMTypes[FOMname], new object[] { this });
            }
            else
            {
                throw new Exception("Not exists a Helper for " + FOMname + "'s FOM");
                // Deberia intentar crear uno aqui usando el DynamicCompiler??
            }
        }

        public void RegisterAssemblyHelpers(Assembly assembly)
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            if (log.IsDebugEnabled)
                log.Debug("Assembly Name :" + assembly.FullName);

            try
            {
                Type[] Types = assembly.GetTypes();
                // Display all the types contained in the specified assembly.
                foreach (Type oType in Types)
                {

                    HLAinteractionHelperAttribute interactionClass =
                                    (HLAinteractionHelperAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLAinteractionHelperAttribute));

                    if (interactionClass != null)
                    {
                        //Get the Key value.   
                        if (log.IsDebugEnabled)
                            log.Debug("HLAinteractionHelperAttribute Found! in " + oType.FullName +
                                      ". InteractionClass Name: " + interactionClass.Name +
                                      ", Semantics :" + interactionClass.Semantics);
                        try
                        {
                            FOMTypes[interactionClass.FomName] = oType;
                        }
                        catch (Exception e)
                        {
                            if (log.IsWarnEnabled)
                                log.Warn("Error creating " + oType.FullName + ": " + e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Can't Create Instance: " + e.Message);
            }
        }

        private void RegisterHelloWorldHelperClass()
        {
            // TODO ANGEL: Gualtrapa. Borrame!!!!!!!!
            Assembly assembly = Assembly.LoadFile(new System.IO.FileInfo("Samples.dll").FullName);

            /* TODO. Buscar una solucion a esto de cargar la dll
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName[] names = assembly.GetReferencedAssemblies();
            */

            if (log.IsDebugEnabled)
                log.Debug("Assembly Name :" + assembly.FullName);

            try
            {
                Type[] Types = assembly.GetTypes();
                // Display all the types contained in the specified assembly.
                foreach (Type oType in Types)
                {

                    HLAinteractionHelperAttribute interactionClass =
                                    (HLAinteractionHelperAttribute)System.Attribute.GetCustomAttribute(oType, typeof(HLAinteractionHelperAttribute));

                    if (interactionClass != null)
                    {
                        //Get the Key value.   
                        if (log.IsDebugEnabled)
                            log.Debug("HLAinteractionHelperAttribute Found! in " + oType.FullName +
                                      ". InteractionClass Name: " + interactionClass.Name +
                                      ", Semantics :" + interactionClass.Semantics);
                        try
                        {
                            FOMTypes[interactionClass.FomName] = oType;
                        }
                        catch (Exception e)
                        {
                            if (log.IsWarnEnabled)
                                log.Warn("Error creating " + oType.FullName + ": " + e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Can't Create Instance: " + e.Message);
            }
        }
    }
}
