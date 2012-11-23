namespace Sxta.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.IO;
    using System.Threading;

    // Import log4net classes.
    using log4net;

    using Hla.Rti1516;
    using Sxta.Rti1516;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.Time;

    using Sxta.Core.Plugins;


    public abstract class PluginSimulator : AbstractPluggableModule, IPluggableSimulationModule
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IRTIambassador rtiAmbassador;
        protected XrtiFederateAmbassador federateAmbassador;
        protected MobileFederateServices mobileServices;
        protected ILogicalTime time;
        protected ILogicalTimeInterval interval = new LongValuedLogicalTimeInterval(1);
        protected String federationName;
        
        [XmlMemberGenericListAttribute("ObjectModel", "FomModel", IsRequired = true)]
        protected List<ModuleFomEntry> fomList;

        [XmlMemberAttribute("Channels")]
        protected ChannelsConfiguration channelsInfo;

        public List<ModuleFomEntry> FomList
        {
            get { return fomList; }
        }

        public ChannelsConfiguration ChannelsInfo
        {
            get { return channelsInfo; }
        }

        protected void InitLogSystem()
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
                log.Debug(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + " Simulation Start");
            }
        }

        public abstract void StartSimulation(IRTIambassador rtiAmb);
       
        public abstract void StopSimulation();

        public ILogicalTime SimulationTime
        {
            get { return time; }
            set { time = value; }
        }

        public virtual void AdvanceTime()
        {
            rtiAmbassador.EvokeMultipleCallbacks(2.0, 10.0);
            time = time.Add(interval);
        }

        public virtual void JoinSimulation(IRTIambassador rtiAmb, string federateType, string federation)
        {
            if (rtiAmb == null)
                throw new System.ArgumentNullException("Rti ambassador could not be null");

            rtiAmbassador = rtiAmb;

            InitLogSystem();

            federationName = federation;

            try
            {
                //Build a Uri using a file path
                FileInfo file = new FileInfo(this.FomList[0].Uri);
                Uri fileUri = new Uri("file://" + file.FullName);

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
                // PATCH ANGEL: Si está inicializado el federateAmbassador no se crea de nuevo
                if (federateAmbassador == null)
                    federateAmbassador = new XrtiFederateAmbassador(rtiAmbassador);

                if (mobileServices == null)
                    mobileServices = new MobileFederateServices(new LongValuedLogicalTimeFactory(), new LongValuedLogicalTimeIntervalFactory());

                // Join to federation
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution(federateType, 
                                                                                       federationName, 
                                                                                       federateAmbassador, 
                                                                                       mobileServices);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Failed while joining to :" + federateAmbassador + ". Exception :" + e.Message);
            }
        }

        /// <summary>
        /// Centralize the resign processing just to make things easier. 
        /// </summary>
        public void Resign()
        {
            try
            {
                // clean up for the next simulation
                rtiAmbassador.ResignFederationExecution(ResignAction.NO_ACTION);
            }
            catch (FederateNotExecutionMember)
            {
                // Ignore this
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Exception in Resign : " + e);
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
                if (log.IsErrorEnabled)
                    log.Error("Exception in DestroyFederationExecution :" + e);
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
                if (log.IsErrorEnabled)
                    log.Error("Exception in DestroyFederationExecution :" + e);
            }
        }
    }
}
