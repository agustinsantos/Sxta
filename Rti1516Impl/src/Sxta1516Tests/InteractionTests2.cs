using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using NUnit.Framework;

// Import log4net classes.
using log4net;

using Hla.Rti1516;
using Sxta.Rti1516;
using Sxta.Rti1516.Proxies;
using Sxta.Rti1516.Impl;
using Sxta.Rti1516.Serializers.XrtiEncoding;
namespace Sxta1516.Tests
{
    /// <summary>Some simple Intereaction Tests.</summary>
    [TestFixture]
    public class InteractionTests2
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

        DescriptorManager descriptorManager = new DescriptorManager();
        
        MemoryMessageChannel channel = new MemoryMessageChannel();
        private InteractionManager helper;
        private CountryInteractionListener myListener;
        private XrtiChannel reliableChannel;
        private object syncObject = new object();

        private const int milliSeconds = 1000;

        /// <summary>
        /// Start up the executive and create a RTI ambassador
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
                log.Debug("Interaction Tests Start");
            }

            descriptorManager.AddBootstrapDescriptors(Sxta1516.ResourcesNames.BootstrapObjectModel);
            descriptorManager.AddBootstrapDescriptors(Sxta1516.ResourcesNames.HelloWorldObjectModel);

            reliableChannel = new XrtiChannel(channel);
            IDictionary<TransportationType, XrtiChannel> channelList = new Dictionary<TransportationType, XrtiChannel>();
            channelList[TransportationType.HLA_RELIABLE] = reliableChannel;
            channelList[TransportationType.HLA_BEST_EFFORT] = reliableChannel;
            helper = new InteractionManager(descriptorManager, channelList);
            
            myListener = new CountryInteractionListener(syncObject);
            helper.AddInteractionListener(myListener);
        }

        [TearDown]
        public void TearDown()
        {
            // Log an baseInfo level message
            if (log.IsDebugEnabled)
            {
                log.Debug("Interaction Tests Ends");
            }

        }


        /// <summary>
        /// Test to serialize/deserialize CommunicationMessage
        /// </summary>
        [Test]
        public void TestCommunicationMessage()
        {
            lock (syncObject)
            {
                long ticks = System.DateTime.Now.Ticks;
                CommunicationMessage msg = new CommunicationMessage();
                msg.FederationExecutionHandle = 10;
                msg.UserSuppliedTag = BitConverter.GetBytes(ticks);
                msg.Message = "Say Hello";

                channel.OutputStream.Position = 0;
                helper.SendInteraction(msg);

                channel.InputStream.Position = 0;
                myListener.LastMessage = null;
                reliableChannel.ReliableRead();

                System.Threading.Monitor.Wait(syncObject, milliSeconds);
                if (!(myListener.LastMessage is CommunicationMessage))
                {
                    throw new RTIexception("Error reading CommunicationMessage");
                }
                else
                {
                    CommunicationMessage lastMsg = myListener.LastMessage as CommunicationMessage;

                    Assert.AreEqual(msg.FederationExecutionHandle, lastMsg.FederationExecutionHandle);
                    Assert.AreEqual(msg.InteractionClassHandle, lastMsg.InteractionClassHandle);
                    Assert.AreEqual(msg.UserSuppliedTag, lastMsg.UserSuppliedTag);
                    Assert.AreEqual(msg.Message, lastMsg.Message);
                }
            }
        }

    }

    public class CountryInteractionListener : IHelloWorldObjectModelInteractionListener
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object syncObject;

        private BaseIteractionMessage message;
        public BaseIteractionMessage LastMessage
        {
            get { return message; }
            set { message = value; }
        }

        public CountryInteractionListener(object sync)
        {
            syncObject = sync;
        }

        private void PulseMonitor()
        {
            lock (syncObject)
            {
                System.Threading.Monitor.Pulse(syncObject);

            }
        }

        #region IHelloWorldObjectModelInteractionListener Members

        public void OnReceiveCommunication(CommunicationMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled)
                log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        #endregion

        #region IInteractionListener Members

        public void ReceiveInteraction(Sxta.Rti1516.Serializers.XrtiEncoding.BaseIteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled)
                log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        #endregion
    }
}

