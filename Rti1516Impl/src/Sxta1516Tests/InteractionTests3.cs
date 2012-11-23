using System;
using System.Diagnostics;
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
    /// <summary>Some simple Interaction Tests.</summary>
    [TestFixture]
    public class InteractionTests3
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
    }
}

