namespace Sxta.Rti1516.Tests.Rti1516
{
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
    using Country = Sxta.Rti1516.Samples.Country;

    /// <summary>Some simple operation on object instances.</summary>
    [TestFixture]
    public class ObjectOperationTest : TestCommon
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Start up the RTI system and the log system
        /// </summary>
        [SetUp]
        public override void Init()
        {
            InitAndCreateFederation();
        }

        /// <summary>
        /// Test get object instances under IEEE1516 conditions
        /// </summary>
        [Test]
        public void TestObjectInstanceName()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("Country");
#if TODO
                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot.Country");
#endif
                rtiAmbassador.ReserveObjectInstanceName("Country1");
                IObjectInstanceHandle objHandle1 = rtiAmbassador.RegisterObjectInstance(countryHandle, "Country1");
                string objName = rtiAmbassador.GetObjectInstanceName(objHandle1);
                Assert.AreEqual("Country1", objName);

                IObjectInstanceHandle objHandle2 = rtiAmbassador.RegisterObjectInstance(countryHandle);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while testing object instances:" + e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test get object instances using the AOP Engine
        /// </summary>
        [Test]
        public void TestObjectUsingAOP()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                Country country = Country.NewCountry();

                string objName = rtiAmbassador.GetObjectInstanceName(country.InstanceHandle);
                IObjectClassHandle classHandle = rtiAmbassador.GetKnownObjectClassHandle(country.InstanceHandle);
                string className = rtiAmbassador.GetObjectClassName(classHandle);
                Assert.AreEqual("Country", className);

            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while testing creation object instances:" + e);
            }

            // clean up for the next test
            Resign();
        }


        [TearDown]
        public override void TearDown()
        {
            DestroyFederation();
            base.TearDown();
        }

    }
}
