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
namespace Sxta1516.Tests
{
    /// <summary>Some descriptor manager Tests.</summary>
    [TestFixture]
    public class DescriptorManagerTests
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
                log.Debug("Descriptor Manager Tests Start");
            }

        }

        [TearDown]
        public void TearDown()
        {
            // Log an baseInfo level message
            if (log.IsDebugEnabled)
            {
                log.Debug("Descriptor Manager Ends");
            }
        }


        /// <summary>
        /// Test to read and process the boostrap file (XML)
        /// Check interaction class descriptors reading
        /// </summary>
        [Test]
        public void TestBooststrapObjectModelRead()
        {
            descriptorManager = new DescriptorManager();

            descriptorManager.AddBootstrapDescriptors(Sxta1516.ResourcesNames.BootstrapObjectModel);

            Assert.AreEqual((1 << 16) | 0, descriptorManager.Version);
            InteractionClassDescriptor icd;
            icd = descriptorManager.GetInteractionClassDescriptor("HLAinteractionFragment");
            Assert.AreEqual("HLAinteractionFragment", icd.Name);
            icd = descriptorManager.GetInteractionClassDescriptor("HLAcontinue");
            Assert.AreEqual("HLAcontinue", icd.Name);
            icd = descriptorManager.GetInteractionClassDescriptor("HLArequestHandles");
            Assert.AreEqual("HLArequestHandles", icd.Name);
        }

        /// <summary>
        /// Test to read and process the boostrap file (XML) and 
        /// a sample file HelloWorldObjectModel.xml
        /// Check interaction class descriptors reading
        /// </summary>
        [Test]
        public void TestHelloWorldObjectModelRead()
        {
            descriptorManager = new DescriptorManager();

            descriptorManager.AddBootstrapDescriptors(Sxta1516.ResourcesNames.BootstrapObjectModel);
            descriptorManager.AddBootstrapDescriptors(Sxta1516.ResourcesNames.HelloWorldObjectModel);

            Assert.AreEqual((1 << 16) | 0, descriptorManager.Version);
            InteractionClassDescriptor icd;
            icd = descriptorManager.GetInteractionClassDescriptor("HLAinteractionFragment");
            Assert.AreEqual("HLAinteractionFragment", icd.Name);
            icd = descriptorManager.GetInteractionClassDescriptor("HLAcontinue");
            Assert.AreEqual("HLAcontinue", icd.Name);
            icd = descriptorManager.GetInteractionClassDescriptor("HLArequestHandles");
            Assert.AreEqual("HLArequestHandles", icd.Name);

            icd = descriptorManager.GetInteractionClassDescriptor("Communication");
            Assert.AreEqual("Communication", icd.Name);
        }


    }
}

