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

    /// <summary>Some simple Destroy Federation Tests.</summary>
    [TestFixture]
    public class DestroyFederationTest : TestCommon
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
            LogAndRtiInit();
        }

        /// <summary>
        /// Test DestroyFederationExecution under normal conditions
        /// </summary>
        [Test]
        public void TestValidDestroy()
        {
            //Build a Uri using a file path
            FileInfo file = new FileInfo(Sxta.Rti1516ResourcesNames.HelloWorldObjectModel);
            Uri fileUri = new Uri("file://" + file.FullName);
            try
            {
                //Create Federation
                rtiAmbassador.CreateFederationExecution(federationName, fileUri);

                // destroy the federation
                rtiAmbassador.DestroyFederationExecution(federationName);
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to destroy existing federation", e);
            }

            try
            {
                // ensure that federation is gone and can't be destroyed again
                rtiAmbassador.DestroyFederationExecution(federationName);
                Assert.Fail("Could not ensure that valid federation was destroyed");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to destroy existing federation", e);
            }
        }

        /// <summary>
        /// try and destroy a federation that does not exist
        /// try and destroy with null federation name
        /// </summary>
        [Test]
        public void TestInvalidDestroy()
        {
            try
            {
                rtiAmbassador.DestroyFederationExecution("noSuchFederation");
                Assert.Fail("No exception while destroying non-existent federation");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while destorying non-existent federation", e);
            }

            // try and destroy with null federation name
            try
            {
                rtiAmbassador.DestroyFederationExecution(null);
                Assert.Fail("No exception while destroying federation with null name");
            }
            catch (FederationExecutionDoesNotExist)
            {
                // SUCCESS
            }
            catch (RTIinternalError)
            {
                // ALSO FINE
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while destorying federation with null name", e);
            }
        }

        /// <summary>
        /// try and destroy a federation that does not exist
        /// </summary>
        [Test]
        public void TestDestroyWithFederate()
        {
            //Build a Uri using a file path
            FileInfo file = new FileInfo(Sxta.Rti1516ResourcesNames.HelloWorldObjectModel);
            Uri fileUri = new Uri("file://" + file.FullName);
            try
            {
                //Create Federation
                rtiAmbassador.CreateFederationExecution(federationName, fileUri);
                // Join to federation
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution("USA", federationName, federateAmbassador, mobileServices);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception while setting up destroyWithFederates test", e);
            }

            try
            {
                // destroy the federation //
                rtiAmbassador.DestroyFederationExecution(federationName);
                Assert.Fail("Was able to destroy a federation with active federates");
            }
            catch (FederatesCurrentlyJoined)
            {
                // SUCCESS
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while destroying federation with active federates", e);
            }
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

    }
}
