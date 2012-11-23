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

    /// <summary>Some simple operation on Handles.</summary>
    [TestFixture]
    public class HandleOperationTest : TestCommon
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
        /// Test get class handles under normal conditions
        /// </summary>
        [Test]
        public void TestFetchWithoutQualifiedName()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                IObjectClassHandle rootHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot");
                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("Country");
                IObjectClassHandle boidHandle = rtiAmbassador.GetObjectClassHandle("Boid");
                IObjectClassHandle goodBoidHandle = rtiAmbassador.GetObjectClassHandle("Boid.GoodBoid");
                IObjectClassHandle badBoidHandle = rtiAmbassador.GetObjectClassHandle("Boid.DepredatorBoid");

                string className = rtiAmbassador.GetObjectClassName(countryHandle);
                Assert.AreEqual("Country", className);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Exception while fetching object class handles with qualified names :" + e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test get class handles under normal conditions
        /// </summary>
        [Test]
        public void TestFetchWithQualifiedName()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                IObjectClassHandle rootHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot");
                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot.Country");
                IObjectClassHandle boidHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot.Boid");
                IObjectClassHandle goodBoidHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot.Boid.GoodBoid");
                IObjectClassHandle badBoidHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot.Boid.DepredatorBoid");

                string className = rtiAmbassador.GetObjectClassName(countryHandle);
                Assert.AreEqual("HLAobjectRoot.Country", className);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Exception while fetching object class handles with qualified names :" + e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test get attribute handles under normal conditions
        /// </summary>
        [Test]
        public void TestAttributeHandles()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("Country");

                IAttributeHandle nameHandle = rtiAmbassador.GetAttributeHandle(countryHandle, "Name");
                IAttributeHandle populationHandle = rtiAmbassador.GetAttributeHandle(countryHandle, "Population");

                string nameAttribute = rtiAmbassador.GetAttributeName(countryHandle, nameHandle);
                Assert.AreEqual("Name", nameAttribute);

                string populationAttribute = rtiAmbassador.GetAttributeName(countryHandle, populationHandle);
                Assert.AreEqual("Population", populationAttribute);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while fetching the object model handles:" + e);
            }

            // clean up for the next test
            Resign();
        }
        /// <summary>
        /// Test get attribute handles under normal conditions
        /// </summary>
        [Test]
        public void TestAttributeHandles2()
        {
            // get the FOM handles //
            try
            {
                JoinFederation();

                IObjectClassHandle countryHandle = rtiAmbassador.GetObjectClassHandle("HLAobjectRoot");

                IAttributeHandle attrHandle = rtiAmbassador.GetAttributeHandle(countryHandle, "HLAprivilegeToDelete");

                string privilegeToDeleteName = rtiAmbassador.GetAttributeName(countryHandle, attrHandle);
                Assert.AreEqual("HLAprivilegeToDelete", privilegeToDeleteName);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while fetching the object model handles:" + e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test get interaction handles under normal conditions
        /// </summary>
        [Test]
        public void TestInteractionHandlesWithQualifiedName()
        {
            try
            {
                JoinFederation();

                IInteractionClassHandle commHandle = rtiAmbassador.GetInteractionClassHandle("HLAinteractionRoot.Communication");

                IParameterHandle messageHandle = rtiAmbassador.GetParameterHandle(commHandle, "message");

                string messageName = rtiAmbassador.GetParameterName(commHandle, messageHandle);
                Assert.AreEqual("message", messageName);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while fetching the object model handles:"+ e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test get interaction handles under normal conditions
        /// </summary>
        [Test]
        public void TestInteractionHandlesWithoutQualifiedName()
        {
            try
            {
                JoinFederation();

                IInteractionClassHandle commHandle = rtiAmbassador.GetInteractionClassHandle("Communication");

                IParameterHandle messageHandle = rtiAmbassador.GetParameterHandle(commHandle, "message");

                string messageName = rtiAmbassador.GetParameterName(commHandle, messageHandle);
                Assert.AreEqual("message", messageName);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Unexpected exception while fetching the object model handles:" + e);
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
