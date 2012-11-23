namespace Sxta.Rti1516.Tests.Interactions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.IO;

    using NUnit.Framework;

    // Import log4net classes.
    using log4net;

    using Sxta.Rti1516;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    using Sxta.Rti1516.BoostrapProtocol;
    using BaseInteractionMessage = Sxta.Rti1516.Interactions.BaseInteractionMessage;
    using TransportationType = Hla.Rti1516.TransportationType;

    /// <summary>Some simple Intereaction Tests.</summary>
    [TestFixture]
    public class InteractionTests 
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

        private DescriptorManager descriptorManager;
        private ChannelsManager channelManager;
        private InteractionManager helper;
        private MyTestBootstrapObjectListener myListener;

        /// <summary>
        /// Start up the interaction manager and the log system
        /// </summary>
        [SetUp]
        public void Init()
        {
            FileInfo logFile = new System.IO.FileInfo("Log4Net.config");
            if (logFile.Exists)
            {
                // Log4Net is configured using a DOMConfigurator.
                log4net.Config.XmlConfigurator.Configure(logFile);
            }
            else
            {
                // Set up a simple configuration that logs on the console.
                log4net.Config.BasicConfigurator.Configure();
            }

            // Log an baseInfo level message
            if (log.IsDebugEnabled)
            {
                if (string.IsNullOrEmpty(System.Threading.Thread.CurrentThread.Name))
                    System.Threading.Thread.CurrentThread.Name = "Main(" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")";
                log.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Tests Start");
            }

            descriptorManager = new DescriptorManager();
            channelManager = new ChannelsManager();
            channelManager.StartNewConnection(new Uri("memory:1"));
            helper = new InteractionManager(descriptorManager, channelManager);

            Dictionary<TransportationType, ChannelType> channelMapping = new Dictionary<TransportationType, ChannelType>();
            channelMapping.Add(TransportationType.HLA_RELIABLE, ChannelType.MEMORY);
            channelMapping.Add(TransportationType.HLA_BEST_EFFORT, ChannelType.MEMORY);
            helper.SetTransportMapping(channelMapping);

            myListener = new MyTestBootstrapObjectListener();
            helper.AddInteractionListener(myListener);
        }

        [TearDown]
        public void TearDown()
        {
            // Log an baseInfo level message
            if (log.IsDebugEnabled)
                log.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Tests End");
        }

        /// <summary>
        /// Test to serialize/deserialize HLAcontinueMessage
        /// </summary>
        [Test]
        public void TestHLAcontinueMessage()
        {
            HLAcontinueMessage msg = new HLAcontinueMessage();
            msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data  1");

            myListener.LastMessage = null;
            helper.SendInteraction(msg);

            if (!(myListener.LastMessage is HLAcontinueMessage))
            {
                throw new Exception("Error reading HLAcontinueMessage");
            }
            else
            {
                HLAcontinueMessage msgLast = myListener.LastMessage as HLAcontinueMessage;

                Assert.AreEqual(msgLast.FederationExecutionHandle, msg.FederationExecutionHandle);
                Assert.AreEqual(msgLast.InteractionClassHandle, msg.InteractionClassHandle);
                Assert.AreEqual(msgLast.UserSuppliedTag, msg.UserSuppliedTag);
            }
        }
        /// <summary>
        /// Test to serialize/deserialize HlaGenericInteractionMessage
        /// </summary>
        [Test]
        public void TestHlaGenericInteractionMessage()
        {
            long ticks = System.DateTime.Now.Ticks;
            HLAGenericInteractionMessage msg = new HLAGenericInteractionMessage();
            msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data Generic");
            msg.ParameterList = new HLAparameterHandleValuePair[2];

            msg.ParameterList[0] = new HLAparameterHandleValuePair();
            msg.ParameterList[0].ParameterHandle = 1;
            msg.ParameterList[0].ParameterValue = BitConverter.GetBytes(Math.PI);

            msg.ParameterList[1] = new HLAparameterHandleValuePair();
            msg.ParameterList[1].ParameterHandle = 2;
            msg.ParameterList[1].ParameterValue = BitConverter.GetBytes(ticks);

            myListener.LastMessage = null;
            helper.SendInteraction(msg);

            if (!(myListener.LastMessage is HLAGenericInteractionMessage))
            {
                throw new Exception("Error reading HlaGenericInteractionMessage");
            }
            else
            {
                HLAGenericInteractionMessage lastMsg = myListener.LastMessage as HLAGenericInteractionMessage;

                Assert.AreEqual(lastMsg.FederationExecutionHandle, msg.FederationExecutionHandle);
                Assert.AreEqual(lastMsg.InteractionClassHandle, msg.InteractionClassHandle);
                Assert.AreEqual(lastMsg.UserSuppliedTag, msg.UserSuppliedTag);
                Assert.AreEqual(lastMsg.ParameterList.Length, msg.ParameterList.Length);
                Assert.AreEqual(lastMsg.ParameterList[0].ParameterValue, msg.ParameterList[0].ParameterValue);
                Assert.AreEqual(lastMsg.ParameterList[1].ParameterValue, msg.ParameterList[1].ParameterValue);
                Assert.AreEqual(Math.PI, BitConverter.ToDouble(msg.ParameterList[0].ParameterValue, 0));
                Assert.AreEqual(ticks, BitConverter.ToInt64(msg.ParameterList[1].ParameterValue, 0));
            }
        }

        /// <summary>
        /// Test to serialize/deserialize PeerAdvertisementMessage
        /// </summary>
        [Test]
        public void TestPeerAdvertisementMessage()
        {
            PeerAdvertisementInteractionMessage msg = new PeerAdvertisementInteractionMessage();
            msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data 2");
            msg.PeerName = "A name";
            msg.PeerDescription = "Some description";
            msg.PeerChannels = new ConnectionList();
            msg.PeerChannels.Add("tcp://localhost:7777");
            msg.PeerChannels.Add("udp://localhost:1234");
            msg.PeerChannels.Add("multi://multi://224.0.0.1:8080");

            myListener.LastMessage = null;
            helper.SendInteraction(msg);

            if (!(myListener.LastMessage is PeerAdvertisementInteractionMessage))
            {
                throw new Exception("Error reading PeerAdvertisementInteractionMessage");
            }
            else
            {
                PeerAdvertisementInteractionMessage msgLast = myListener.LastMessage as PeerAdvertisementInteractionMessage;

                Assert.AreEqual(msgLast.FederationExecutionHandle, msg.FederationExecutionHandle);
                Assert.AreEqual(msgLast.InteractionClassHandle, msg.InteractionClassHandle);
                Assert.AreEqual(msgLast.UserSuppliedTag, msg.UserSuppliedTag);
                Assert.AreEqual(msgLast.PeerName, msg.PeerName);
                Assert.AreEqual(msgLast.PeerDescription, msg.PeerDescription);
                Assert.AreEqual(msgLast.PeerChannels.Count, msg.PeerChannels.Count);
                for (int i = 0; i < msgLast.PeerChannels.Count; i++)
                    Assert.AreEqual(msgLast.PeerChannels[i], msg.PeerChannels[i]);
            }
        }
    }

    public class MyTestBootstrapObjectListener : IBootstrapObjectModelInteractionListener
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private BaseInteractionMessage message;
        public BaseInteractionMessage LastMessage
        {
            get { return message; }
            set { message = value; }
        }

        public MyTestBootstrapObjectListener()
        {
        }

        #region IBootstrapObjectModelInteractionListener Members

        public void OnReceiveHLAGenericInteraction(HLAGenericInteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
        }

        public void OnReceiveHLAinteractionFragment(HLAinteractionFragmentMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
        }


        public void OnReceiveHLAcontinue(HLAcontinueMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
        }

        public void OnReceivePeerAdvertisementInteraction(PeerAdvertisementInteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
        }

        #endregion

        #region IInteractionListener Members

        public void ReceiveInteraction(BaseInteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
        }

        #endregion
    }
}
