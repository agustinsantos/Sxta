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

    /// <summary>Some simple Create Federation Tests.</summary>
    [TestFixture]
    public class CreateFederationTest : TestCommon
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string InvalidObjectModel = "";

        /// <summary>
        /// Start up the RTI system and the log system
        /// </summary>
        [SetUp]
        public override void Init()
        {
            LogAndRtiInit();
        }

        /// <summary>
        /// Test CreateFederationExecution under normal conditions
        /// </summary>
        [Test]
        public void TestValidCreate()
        {
            //Build a Uri using a file path
            FileInfo file = new FileInfo(Sxta.Rti1516ResourcesNames.HelloWorldObjectModel);
            Uri fileUri = new Uri("file://" + file.FullName);
            try
            {
                //Create Federation
                rtiAmbassador.CreateFederationExecution(federationName, fileUri);
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
            }

            // ensure that federation can't be created again (now that it has already been created 
            try
            {
                rtiAmbassador.CreateFederationExecution(federationName, fileUri);
                Assert.Fail("Could not ensure that valid federation was created");
            }
            catch (FederationExecutionAlreadyExists)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while testing creation of existing federation", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution exceptions
        /// </summary>
        [Test]
        public void TestCreateWithInvalidFom()
        {
            Assert.Fail("Test for creating federation with invalid FOM is not implemented");
            Uri invalidFomUri = new Uri("file://" + new FileInfo(InvalidObjectModel).FullName);
#if TODO
            // attempt to create with invalid fom //
            try
            {
                rtiAmbassador.CreateFederationExecution(federationName, invalidFileUri);
                Assert.Fail("No exception while creating federation with invalid FOM");
            }
            catch (ErrorReadingFDD)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while testing create with invalid FOM", e);
            }
#endif
        }

        /// <summary>
        /// Test CreateFederationExecution exceptions
        /// </summary>
        [Test]
        public void TestCreateWithNullParameters()
        {
            //Build a Uri using a file path
            FileInfo file = new FileInfo(Sxta.Rti1516ResourcesNames.HelloWorldObjectModel);
            Uri fileUri = new Uri("file://" + file.FullName);
            try
            {
                //Create Federation
                rtiAmbassador.CreateFederationExecution(null, fileUri);
                Assert.Fail("No exception while creating federation with null name");
            }
            catch (RTIinternalError)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while testing create with create with null name", e);
            }

            // attempt to create with null fom //
            try
            {
                rtiAmbassador.CreateFederationExecution(federationName, null);
                Assert.Fail("No exception while creating federation with null FOM");
            }
            catch (CouldNotOpenFDD)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while testing create with null FOM", e);
            }
        }

        /// <summary>
        /// Test CreateFederationExecution exceptions
        /// </summary>
        [Test]
        public void TestCreateWithInvalidURL()
        {
            Uri invalidFileUri = new Uri("file://dummyURL");

            // attempt to create with invalid fom URL //
            try
            {
                rtiAmbassador.CreateFederationExecution(federationName, invalidFileUri);
                Assert.Fail("No exception while creating federation with invalid FOM (invalid URL)");
            }
            catch (CouldNotOpenFDD)
            {
                // SUCCESS!
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong exception while testing create with invalid FOM (invalid URL)", e);
            }
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

    }
}
