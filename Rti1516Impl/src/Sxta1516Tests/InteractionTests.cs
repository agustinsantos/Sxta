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

        DescriptorManager descriptorManager = new DescriptorManager();

        MemoryMessageChannel channel = new MemoryMessageChannel();
        private InteractionManager helper;
        private MyTestBootstrapObjectListener myListener;
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

            reliableChannel = new XrtiChannel(channel);
            Dictionary<TransportationType, XrtiChannel> channelList = new Dictionary<TransportationType, XrtiChannel>();
            channelList[TransportationType.HLA_RELIABLE] = reliableChannel;
            channelList[TransportationType.HLA_BEST_EFFORT] = reliableChannel;
            helper = new InteractionManager(descriptorManager, channelList);

            myListener = new MyTestBootstrapObjectListener(syncObject);
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
        /// Test to serialize/deserialize HlaGenericInteractionMessage
        /// </summary>
        [Test]
        public void TestHlaGenericInteractionMessage()
        {
            lock (syncObject)
            {
                long ticks = System.DateTime.Now.Ticks;
                HlaGenericInteractionMessage msg = new HlaGenericInteractionMessage();
                msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data Generic");
                msg.ParameterList = new HLAparameterHandleValuePair[2];

                msg.ParameterList[0] = new HLAparameterHandleValuePair();
                msg.ParameterList[0].ParameterHandle = 1;
                msg.ParameterList[0].ParameterValue = BitConverter.GetBytes(Math.PI);

                msg.ParameterList[1] = new HLAparameterHandleValuePair();
                msg.ParameterList[1].ParameterHandle = 2;
                msg.ParameterList[1].ParameterValue = BitConverter.GetBytes(ticks);

                channel.OutputStream.Position = 0;
                helper.SendInteraction(msg);

                channel.InputStream.Position = 0;
                myListener.LastMessage = null;
                reliableChannel.ReliableRead();

                System.Threading.Monitor.Wait(syncObject, milliSeconds);
                if (!(myListener.LastMessage is HlaGenericInteractionMessage))
                {
                    throw new RTIexception("Error reading HlaGenericInteractionMessage");
                }
                else
                {
                    HlaGenericInteractionMessage lastMsg = myListener.LastMessage as HlaGenericInteractionMessage;

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
        }

        /// <summary>
        /// Test to serialize/deserialize HLAcontinueMessage
        /// </summary>
        [Test]
        public void TestHLAcontinueMessage()
        {
            lock (syncObject)
            {
                HLAcontinueMessage msg = new HLAcontinueMessage();
                msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data  1");

                channel.OutputStream.Position = 0;
                helper.SendInteraction(msg);

                channel.InputStream.Position = 0;
                myListener.LastMessage = null;
                reliableChannel.ReliableRead();

                System.Threading.Monitor.Wait(syncObject, milliSeconds);
                if (!(myListener.LastMessage is HLAcontinueMessage))
                {
                    throw new RTIexception("Error reading HLAcontinueMessage");
                }
                else
                {
                    HLAcontinueMessage msgLast = myListener.LastMessage as HLAcontinueMessage;

                    Assert.AreEqual(msgLast.FederationExecutionHandle, msg.FederationExecutionHandle);
                    Assert.AreEqual(msgLast.InteractionClassHandle, msg.InteractionClassHandle);
                    Assert.AreEqual(msgLast.UserSuppliedTag, msg.UserSuppliedTag);
                }
            }
        }

        /// <summary>
        /// Test to serialize/deserialize HLArequestHandlesMessage
        /// </summary>
        [Test]
        public void TestHLArequestHandlesMessage()
        {
            lock (syncObject)
            {
                HLArequestHandlesMessage msg = new HLArequestHandlesMessage();
                msg.UserSuppliedTag = System.Text.UnicodeEncoding.Unicode.GetBytes("Sample user data 2");
                msg.BlockSize = 1000;

                channel.OutputStream.Position = 0;
                helper.SendInteraction(msg);

                channel.InputStream.Position = 0;
                myListener.LastMessage = null;
                reliableChannel.ReliableRead();

                System.Threading.Monitor.Wait(syncObject, milliSeconds);
                if (!(myListener.LastMessage is HLArequestHandlesMessage))
                {
                    throw new RTIexception("Error reading HLArequestHandlesMessage");
                }
                else
                {
                    HLArequestHandlesMessage msgLast = myListener.LastMessage as HLArequestHandlesMessage;

                    Assert.AreEqual(msgLast.FederationExecutionHandle, msg.FederationExecutionHandle);
                    Assert.AreEqual(msgLast.InteractionClassHandle, msg.InteractionClassHandle);
                    Assert.AreEqual(msgLast.UserSuppliedTag, msg.UserSuppliedTag);
                    Assert.AreEqual(msgLast.BlockSize, msg.BlockSize);
                }
            }
        }
    }

    public class MyTestBootstrapObjectListener : IBootstrapObjectModelInteractionListener
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object syncObject;

        private BaseIteractionMessage message;
        public BaseIteractionMessage LastMessage
        {
            get { return message; }
            set { message = value; }
        }

        public MyTestBootstrapObjectListener(object sync)
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

        #region IBootstrapObjectModelInteractionListener Members

        public void OnReceiveHlaGenericInteraction(HlaGenericInteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAinteractionFragment(HLAinteractionFragmentMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLArequestHandles(HLArequestHandlesMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAreportHandles(HLAreportHandlesMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAcontinue(HLAcontinueMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAregisterObjectInstance(HLAregisterObjectInstanceMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLArequestAttributeValueUpdate(HLArequestAttributeValueUpdateMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAupdateAttributeValuesBestEffort(HLAupdateAttributeValuesBestEffortMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        public void OnReceiveHLAupdateAttributeValuesReliable(HLAupdateAttributeValuesReliableMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        #endregion

        #region IInteractionListener Members

        public void ReceiveInteraction(Sxta.Rti1516.Serializers.XrtiEncoding.BaseIteractionMessage msg)
        {
            message = msg;
            if (log.IsDebugEnabled) log.Debug("Received LastMessage =  " + msg.ToString());
            PulseMonitor();
        }

        #endregion
    }
}

