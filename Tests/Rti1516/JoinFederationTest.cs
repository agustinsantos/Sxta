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

    /// <summary>Some simple Join Federation Tests.</summary>
    [TestFixture]
    public class JoinFederationTest : TestCommon
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
        /// Test CreateFederationExecution under normal conditions
        /// </summary>
        [Test]
        public void TestValidJoin()
        {
            try
            {
                // Join to federation
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", federationName, federateAmbassador, mobileServices);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Failed while testing a valid join request: " + e.Message);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join a federation that does no exist
        /// </summary>
        [Test]
        public void TestJoinNonExistentFederation()
        {
            try
            {
                // Join to federation
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", "noSuchFederation", federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining a non-existent federation");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while trying to join a non-existent federation", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join a federation that does no exist (empty name)
        /// </summary>
        [Test]
        public void TestJoinFederationEmptyName()
        {
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", "", federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining a non-existent federation (empty name)");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while joining wrong federation (empty name)", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join a federation that does no exist (null name) 
        /// </summary>
        [Test]
        public void TestJoinFederationWithNullNameFederation()
        {
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("testFederate", null, federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining a non-existent federation (null name)");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while joining wrong federation (null name)", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join a federation using empty or null name
        /// </summary>
        [Test]
        public void TestJoinWithInvalidFederate()
        {
            /// try and join a federation using empty name
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("", federationName, federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining a with empty federate name");
            }
            catch (RTIinternalError)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while trying to join federation using null name", e);
            }

            // try and join a federation using null name
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution(null, federationName, federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining a with null federate name");
            }
            catch (RTIinternalError)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while trying to join federation using empty name", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join the rti to a second federation
        /// </summary>
        [Test]
        public void TestJoinAlreadyExecutionMember()
        {
            // set up by running a valid join
            try
            {
                // Join to federation
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", federationName, federateAmbassador, mobileServices);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Failed while testing a valid join request: " + e.Message);
            }

            // try and join the rtiamb to a second federation 
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("aValidName", federationName + "2", federateAmbassador, mobileServices);
                Assert.Fail("No exception while joining two federations through same ambassador");
            }
            catch (FederateAlreadyExecutionMember)
            {
                // SUCCESS
            }
            catch (RTIinternalError)
            {
                // ALSO VALID
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while joining two federations with same ambassador", e);
            }

            // clean up for the next test
            Resign();
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join with null federate ambassador
        /// </summary>
        [Test]
        public void TestJoinInvalidFedAmb()
        {
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", federationName, null, mobileServices);
                Assert.Fail("No exception while joining with null federate ambassador");
            }
            catch (RTIinternalError)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while joining with null federate ambassador", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution under invalid situations
        /// try and join with null Mobile Services
        /// </summary>
        [Test]
        public void TestJoinInvalidMobileServices()
        {
            try
            {
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", federationName, federateAmbassador, null);
                Assert.Fail("No exception while joining with null mobile services");
            }
            catch (RTIinternalError)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Invalid exception while joining with null mobile services", e);
            }
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

    }
}
