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
    /// <summary>Some AOP Tests.</summary>
    [TestFixture]
    public class AOPTests
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

            ObjectManager objectManager = new ObjectManager();
            Sxta.Rti1516.HLAAttributes.HLAobjectRoot.AddIHLAobjectRootCreationListener(objectManager);
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
        /// Test create an object
        /// </summary>
        [Test]
        public void TestCreation()
        {
            CountryCreationListener countryCreationListener = new CountryCreationListener();
            Sxta.Samples.Country.AddIHLAobjectRootCreationListener(countryCreationListener);

            Sxta.Samples.Country myCountry = new Sxta.Samples.Country();

            Assert.AreEqual(countryCreationListener.LastObject, myCountry);
        }


        /// <summary>
        /// Test to update a value of an object
        /// </summary>
        [Test]
        public void TestUpdateValue()
        {
            CountryListener countryListener = new CountryListener();

            Sxta.Samples.Country myCountry = new Sxta.Samples.Country();
            myCountry.AddIHLAobjectRootListener(countryListener);

            myCountry.Name = "A COUNTRY";
            Assert.AreEqual(countryListener.LastMethodName, "Name");
            Assert.AreEqual(countryListener.LastValue, myCountry.Name);
            
            myCountry.Population = 1000;
            Assert.AreEqual(countryListener.LastMethodName, "Population");
            Assert.AreEqual(countryListener.LastValue, myCountry.Population);
        }
    }


    public class CountryListener : IHLAobjectRootListener
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object val;
        public object LastValue
        {
            get { return val; }
            set { val = value; }
        }

        private string name;
        public string LastMethodName
        {
            get { return name; }
            set { name = value; }
        }

        public CountryListener()
        {
        }

        #region IHLAobjectRootListener Members

        public void OnReceiveUpdateAttributeValues(Hla.Rti1516.IObjectInstanceHandle instanceHandle, string methodName, object newValue)
        {
            name = methodName;
            val = newValue;
            if (log.IsDebugEnabled)
                log.Debug("The method " + methodName + " has been called; new value:" + newValue + "; parameter type = " + newValue.GetType());
        }

        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap)
        {
            if (log.IsDebugEnabled)
            {
                foreach (KeyValuePair<string, object> entry in methodNameValueMap)
                {
                    log.Debug("The method " + entry.Key + " from object " + instanceHandle + " has been called; new value:" + entry.Value + "; parameter type = " + entry.Value.GetType());
                }
            }
        }

        #endregion
    }

    public class CountryCreationListener : IHlaCreateObjectRootListener
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object  newObj;
        public object LastObject
        {
            get { return newObj; }
            set { newObj = value; }
        }

        #region IHlaCreateObjectRootListener Members

        public void OnReceiveCreatedNewObject(object newObject)
        {
            newObj = newObject;
            if (log.IsDebugEnabled)
                log.Debug("A new object has been created " + newObject);
        }

        #endregion
    }
}

