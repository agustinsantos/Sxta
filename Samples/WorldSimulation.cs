namespace ExternalSamples
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
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Time;

    using Sxta.Core.Plugins;

    public class WorldSimulation : Sxta.Plugins.PluginSimulator, IExternalSamplesObjectModelInteractionListener
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Thread internalThread = null;

        public override void StartSimulation(IRTIambassador rtiAmb)
        {
            JoinSimulation(rtiAmb, "Europe", "HelloWorld");

            try
            {
                internalThread = new Thread(new ThreadStart(SimulationLoop));
                internalThread.Start();
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Failed running simulation. Exception :" + e.Message);
            }

        }

        public override void StopSimulation()
        {
            if (log.IsInfoEnabled)
                log.Info("Stop simulation");
        }

        public void SimulationLoop()
        {
            ((XrtiExecutiveAmbassador)rtiAmbassador).interactionManager.AddInteractionListener(this);

            List<ExternalCountry> countriesList = new List<ExternalCountry>();
            Random ran = new Random();

            for (int i = 0; i < 1; i++)
            {
                ExternalCountry aCountry = ExternalCountry.NewExternalCountry();
                aCountry.Name = "Country[" + i + "]";
                aCountry.Population = ran.Next(1000, 2000);
                countriesList.Add(aCountry);
            }

            SimulationTime = new LongValuedLogicalTime(0);
            ILogicalTime finalTime = new LongValuedLogicalTime(1000);
            while (SimulationTime.CompareTo(finalTime) < 0)
            {
                federateAmbassador.DumpObjects();

                foreach (ExternalCountry country in countriesList)
                {
                    country.Population *= 1.0 + ran.NextDouble() * 0.1; // some random increase in range (1.00 and 1.10)
                }
                CommunicationMessage msg = new CommunicationMessage();
                msg.Message = "Hi, I finished my time " + SimulationTime.ToString();
                Thread.Sleep(4 * 1000);
                ((XrtiExecutiveAmbassador)rtiAmbassador).SendInteraction(msg);
                AdvanceTime();
            }

            if (log.IsInfoEnabled)
                log.Info("Finaliza el bucle de simulación");

            // clean up for the next test
            ResignAndDestroy();
        }

        public IList<HLAobjectRoot> KnownCountryList
        {
            get
            {
                //List<Country> list = new List<Country>(federateAmbassador.GetObjects());
                return federateAmbassador.GetObjectsCollection();
            }
        }

        #region IExternalSamplesObjectModelInteractionListener Members

        public void OnReceiveCommunication(CommunicationMessage msg)
        {
            if (log.IsInfoEnabled)
                log.Info("On receive a new communication msg : "+ msg);
        }

        #endregion

        #region IInteractionListener Members

        public void ReceiveInteraction(Sxta.Rti1516.Interactions.BaseInteractionMessage msg)
        {
            if (log.IsInfoEnabled)
                log.Info("On receive a new interaction: " + msg);
        }

        #endregion
    }
}
