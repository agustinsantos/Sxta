namespace Sxta.Rti1516.BoidSample
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.IO;
    using System.Threading;

    // Import log4net classes.
    using log4net;
    using Mogre;

    using Hla.Rti1516;
    using Sxta.Rti1516;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.Time;

    using Sxta.Core.Plugins;

    public class BoidsSimulation : Sxta.Plugins.PluginSimulator
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [XmlMemberAttribute("Color")]
        protected string color;

        private volatile bool shouldStop = false;

        private Thread simulationThread;
        BoidsManager boidsManager;
        private const int NUMBER_BOIDS = 20;
        private const float RefreshPerSecond = 60.0f / 1.0f;
        private const int NumberOfSeconds = 360;
        private const long TicksPerRefresh = (long)((float)TimeSpan.TicksPerSecond / RefreshPerSecond);
        protected ILogicalTimeInterval refreshInterval = new LongValuedLogicalTimeInterval(TicksPerRefresh);

        public override void StartSimulation(IRTIambassador rtiAmb)
        {
            if (log.IsInfoEnabled)
                log.Info("Start Boids simulation");

            JoinSimulation(rtiAmb, color, "Boids");

            try
            {
                simulationThread = new Thread(new ThreadStart(SimulationLoop));
                boidsManager = new BoidsManager(federateAmbassador);
                boidsManager.BuildBoids(NUMBER_BOIDS, new Vector3(750, 60, 750), color);
                BoidsSample app = new BoidsSample(boidsManager, color);
                simulationThread.Start();
                app.Go();
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
                log.Info("Stop Boids simulation");
        }

        public void SimulationLoop()
        {
            long startTime = DateTime.Now.Ticks;
            SimulationTime = new LongValuedLogicalTime(startTime);
            ILogicalTime finalTime = new LongValuedLogicalTime(startTime + NumberOfSeconds * TimeSpan.TicksPerSecond);
            ILogicalTime lastTime = new LongValuedLogicalTime(startTime);
            while (SimulationTime.CompareTo(finalTime) < 0 && !shouldStop)
            {
                SimulationTime = new LongValuedLogicalTime(DateTime.Now.Ticks);
                boidsManager.DoSimulation(((LongValuedLogicalTimeInterval)time.Distance(lastTime)).Value);
                AdvanceTime();
                lastTime = SimulationTime;
            }

            // clean up for the next test
            ResignAndDestroy();
        }

        public void RequestStopSimulation()
        {
            shouldStop = true;
        }

        protected ILogicalTime nextRefreshTime = new LongValuedLogicalTime(0);
        protected int sending = 0;
        protected int sendingPerIterations = 1;
        public override void AdvanceTime()
        {
            rtiAmbassador.EvokeMultipleCallbacks(0.01, 0.2);

            for (int i = 0; i < sendingPerIterations && sending < boidsManager.BoidsList.Count; i++)
            {
                Boid boid = boidsManager.BoidsList[sending];
                boid.FlushAttributeValues(null);
                sending++;
            }

            if (SimulationTime.CompareTo(nextRefreshTime) > 0)
            {
                nextRefreshTime = SimulationTime.Add(refreshInterval);
                if (sending >= boidsManager.BoidsList.Count)
                {
                    sendingPerIterations -= 1;
                    sending = 0;
                }
                else
                {
                    sendingPerIterations += 1;
                }

                /*
                federateAmbassador.DumpObjects();
                
                for (int i = 0; i < boidsManager.BoidsList.Count; i++)
                {
                    Boid boid = boidsManager.BoidsList[i];
                    boid.FlushAttributeValues(null);
                }
                */
            }
        }
    }
}
