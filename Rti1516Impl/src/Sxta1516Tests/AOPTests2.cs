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
    public class AOPTests2
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
        public void TestCreationAndUpdateValue()
        {
            CountryListener2 countryListener = new CountryListener2();
            Sxta.Samples.Country.AddIHLAobjectRootCreationListener(countryListener);

            Sxta.Samples.Country myCountry = new Sxta.Samples.Country();

            Sxta.Samples.Country countryProxy = countryListener.LastObject as Sxta.Samples.Country;

            myCountry.AddIHLAobjectRootListener(countryListener);

            myCountry.Name = "A COUNTRY";
            Assert.AreEqual(countryListener.LastMethodName, "Name");
            Assert.AreEqual(countryListener.LastValue, myCountry.Name);
            Assert.AreEqual(myCountry.Name, countryProxy.Name);
            if (log.IsDebugEnabled)
                log.Debug("After Country.Name is modified the object is " + countryProxy);

            myCountry.Population = 1000;
            Assert.AreEqual(countryListener.LastMethodName, "Population");
            Assert.AreEqual(countryListener.LastValue, myCountry.Population);
            Assert.AreEqual(myCountry.Population, countryProxy.Population);
            if (log.IsDebugEnabled)
                log.Debug("After Country.Population is modified the object is " + countryProxy);

        }
    }

    public class CountryListener2 : IHLAobjectRootListener, IHlaCreateObjectRootListener
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

        #region IHLAobjectRootListener Members

        public void OnReceiveUpdateAttributeValues(Hla.Rti1516.IObjectInstanceHandle instanceHandle, string methodName, object newValue)
        {
            name = methodName;
            val = newValue;
            Sxta.Rti1516.HLAAttributes.HLAobjectRoot obj = registedObjects[instanceHandle] as Sxta.Rti1516.HLAAttributes.HLAobjectRoot;
            if (log.IsDebugEnabled)
            {
                log.Debug("The method " + methodName + " has been called; new value:" + newValue + "; parameter type = " + newValue.GetType());
                log.Debug("The object is " + obj);
            }
        }


        public void OnReceiveUpdateAttributeValues(IObjectInstanceHandle instanceHandle, IDictionary<string, object> methodNameValueMap)
        {
            if (log.IsDebugEnabled)
            {
                Sxta.Rti1516.HLAAttributes.HLAobjectRoot obj = registedObjects[instanceHandle] as Sxta.Rti1516.HLAAttributes.HLAobjectRoot;
                log.Debug("The object is " + obj);

                foreach (KeyValuePair<string, object> entry in methodNameValueMap)
                {
                    log.Debug("The method " + entry.Key + " from object " + instanceHandle + " has been called; new value:" + entry.Value + "; parameter type = " + entry.Value.GetType());
                }
            }
        }

        #endregion

        private Sxta.Rti1516.HLAAttributes.HLAobjectRoot newObj;
        private IDictionary<IObjectInstanceHandle, object> registedObjects = new Dictionary<IObjectInstanceHandle, object>();

        public Sxta.Rti1516.HLAAttributes.HLAobjectRoot LastObject
        {
            get { return newObj; }
            set { newObj = value; }
        }

        public object RegistedObject(Hla.Rti1516.IObjectInstanceHandle handle)
        {
            return registedObjects[handle];
        }

        #region IHlaCreateObjectRootListener Members

        public void OnReceiveCreatedNewObject(object newObject)
        {
            if (newObject is Sxta.Rti1516.HLAAttributes.HLAobjectRoot)
            {
                newObj = newObject as Sxta.Rti1516.HLAAttributes.HLAobjectRoot;
                registedObjects[newObj.InstanceHandle] = newObj;
            }
            if (log.IsDebugEnabled)
                log.Debug("A new object has been created " + newObject + " with type:" + newObject.GetType().Name + " and fullname:" + newObject.GetType().FullName);
        }

        #endregion
    }
}

