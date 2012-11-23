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

    using Nini.Config;

    using Hla.Rti1516;
    using Sxta.Rti1516;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.BaseApplication;
    using Sxta.Rti1516.Time;

    public abstract class TestCommon
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected XrtiExecutiveAmbassador rtiAmbassador;
        protected XrtiFederateAmbassador federateAmbassador;
        protected MobileFederateServices mobileServices;

        protected String federationName = "HelloWorld";
        protected MainApplication app;

        public abstract void Init();

        public void LogAndRtiInit()
        {
            app = new MainApplication(new string[] { "-dialog=false" });

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
                log.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Tests Start");
            }

            if (rtiAmbassador == null)
                rtiAmbassador = new XrtiExecutiveAmbassador(app.ConfigSource);
        }

        public void InitAndCreateFederation()
        {
            LogAndRtiInit();

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

            try
            {
                federateAmbassador = new TestFederate(rtiAmbassador);
                mobileServices = new MobileFederateServices(new LongValuedLogicalTimeFactory(), new LongValuedLogicalTimeIntervalFactory());
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
            }
        }

        public void InitCreateAndJoinFederation()
        {
            InitAndCreateFederation();
            JoinFederation();
        }

        public void JoinFederation()
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
        }
        public virtual void TearDown()
        {
            // Log an baseInfo level message
            if (log.IsDebugEnabled)
                log.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Tests End");
            if (rtiAmbassador != null)
                rtiAmbassador = null;
        }

        /// <summary>
        /// Centralize the resign processing just to make things easier. If there is a problem while
        /// attempting the resign, this method will use Assert.Fail() to fail the test rather than
        /// throw an exception. If the federate is not currently joined (and a
        /// FederateNotExecutionMember exception is thrown), it will be ignored.
        /// </summary>
        public void Resign()
        {
            try
            {
                // clean up for the next test
#if TODO
                rtiAmbassador.ResignFederationExecution(ResignAction.DELETE_OBJECTS_THEN_DIVEST);
#endif
                rtiAmbassador.ResignFederationExecution(ResignAction.NO_ACTION);
            }
            catch (FederateNotExecutionMember)
            {
                // Ignore this
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error(e.Message);
                Assert.Fail("Exception in Resign : ", e);
            }
        }

        public void ResignAndDestroy()
        {
            Resign();
            try
            {
                rtiAmbassador.DestroyFederationExecution(federationName);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception in DestroyFederationExecution :", e);
            }
        }

        public void DestroyFederation()
        {
            try
            {
                rtiAmbassador.DestroyFederationExecution(federationName);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception in DestroyFederationExecution :", e);
            }
        }

    }
}
