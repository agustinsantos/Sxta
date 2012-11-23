namespace Sxta.Rti1516.TimeManagementSample
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
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.XrtiHandles;
    using Sxta.Rti1516.Time;
    using Sxta.Rti1516.BoostrapProtocol;

    using Sxta.Core.Plugins;

    public class FederationConfiguration
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class FederateConfiguration
    {
        [XmlMemberAttribute("Type", IsRequired = true)]
        string type = null;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        [XmlMemberAttribute("IsRegulator", IsRequired = false)]
        bool isRegulator = false;

        public bool IsRegulator
        {
            get { return isRegulator; }
            set { isRegulator = value; }
        }

        [XmlMemberAttribute("IsConstrained", IsRequired = false)]
        bool isConstrained = false;

        public bool IsConstrained
        {
            get { return isConstrained; }
            set { isConstrained = value; }
        }

        [XmlMemberAttribute("Lookahead", IsRequired = false)]
        double lookahead = 1.0;

        public double Lookahead
        {
            get { return lookahead; }
            set { lookahead = value; }
        }
    }

    public class ActorEntry
    {
        [XmlMemberAttribute("Name", IsRequired = true)]
        string name = null;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlMemberAttribute("Color", IsRequired = true)]
        string color = null;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    }

    public class HomeEntry
    {
        [XmlMemberAttribute("PosX", IsRequired = true)]
        int posX = 0;

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        [XmlMemberAttribute("PosY", IsRequired = true)]
        int posY = 0;

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        [XmlMemberAttribute("BoxesCount", IsRequired = true)]
        int boxesCount = 0;

        public int BoxesCount
        {
            get { return boxesCount; }
            set { boxesCount = value; }
        }
    }

    public class TargetEntry
    {
        [XmlMemberAttribute("PosX", IsRequired = true)]
        int posX = 0;

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        [XmlMemberAttribute("PosY", IsRequired = true)]
        int posY = 0;

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
    }

    public class SimulationEntry
    {
        [XmlMemberAttribute("Delay", IsRequired = true)]
        int delay = 0;

        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }
    }

    public class TimeManagementSimulation : Sxta.Plugins.PluginSimulator
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private volatile bool shouldStop = false;

        [XmlMemberAttribute("Federate", IsRequired = true)]
        protected FederateConfiguration federate;

        [XmlMemberAttribute("Federation", IsRequired = true)]
        protected FederationConfiguration federation;

        [XmlMemberAttribute("Actor", IsRequired = true)]
        protected ActorEntry actorEntry;

        [XmlMemberAttribute("Home", IsRequired = true)]
        protected HomeEntry homeEntry;

        [XmlMemberAttribute("Simulation", IsRequired = true)]
        protected SimulationEntry simulationEntry;

        protected Actor actor;
        protected Home target;
        protected Home home;

        private Thread simulationThread;

        private const int NumberOfSeconds = 300;
        protected ILogicalTimeInterval moveActorInterval;

        TimeManagementForm form;
        Scenario scenario;
        private Boolean simulationRunning;
        
        private TimeManagementFederateAmbassador timeManagementFederateAmbassador;
        private XrtiExecutiveAmbassador rti;

        public override void StartSimulation(IRTIambassador rtiAmb)
        {
            if (log.IsInfoEnabled)
                log.Info("Start Time Management simulation");

            try
            {
                rti = (XrtiExecutiveAmbassador)rtiAmb;
                form = new TimeManagementForm(rti, this);
                scenario = form.Scenario;

                federateAmbassador = new TimeManagementFederateAmbassador(rtiAmb);
                timeManagementFederateAmbassador = (TimeManagementFederateAmbassador)federateAmbassador;
                mobileServices = new MobileFederateServices(new DoubleValuedLogicalTimeFactory(), new DoubleValuedLogicalTimeIntervalFactory());
                JoinSimulation(rtiAmb, federate.Type, federation.Name);

                AddVisualElements();

                InitFederate();

                // Add Federation's interaction listener
                TimeManagementObjectModelInteractionListener l1 = new TimeManagementObjectModelInteractionListener(rti, mobileServices._timeFactory, home, moveActorInterval, form, this);
                rti.interactionManager.AddInteractionListener(l1);

                MyLowLevelManagementObjectModelInteractionListener l2 = new MyLowLevelManagementObjectModelInteractionListener(rti, "My", form);
                rti.interactionManager.AddInteractionListener(l2);
                /**/

                form.InitTimeManagementValueLabels();

                form.Show();
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Failed running simulation. Exception :" + e.Message);
            }
        }

        protected void InitFederate()
        {
            if (federate.IsRegulator)
                rtiAmbassador.EnableTimeRegulation(mobileServices._intervalFactory.MakeZero());
            if (federate.IsConstrained)
                rtiAmbassador.EnableTimeConstrained();

            ILogicalTimeInterval lookahead = new DoubleValuedLogicalTimeInterval(federate.Lookahead);
            rtiAmbassador.ModifyLookahead(lookahead);

            moveActorInterval = lookahead;
        }

        public override void StopSimulation()
        {
            if (log.IsInfoEnabled)
                log.Info("Stop Time Management simulation");
        }

        public void PauseSimulation()
        {
            simulationRunning = false;
        }

        public void ResumeSimulation()
        {
            if (simulationThread == null)
            {
                simulationThread = new Thread(new ThreadStart(SimulationLoop));
                simulationThread.Start();
            }
                
            simulationRunning = true;
        }

        protected void AddVisualElements()
        {
            // 1. Sets actor's house
            home = Home.NewHome(homeEntry.PosX, homeEntry.PosY, homeEntry.BoxesCount);

            // 2. Sets actor
            actor = Actor.NewActor(actorEntry.Name, actorEntry.Color, Actor.MoveDirection.Right, home);

            scenario.SetActor(actor);
        }

        protected void SetTarget()
        {
            // Finds target house
            const int MAX_TRIES = 1000;
            IList<Home> remoteHomes;

            int tries = 0;
            bool found = false;
            for (; tries < MAX_TRIES && !found; tries++)
            {
                rtiAmbassador.EvokeMultipleCallbacks(0.1, 0.5);

                remoteHomes = timeManagementFederateAmbassador.GetRemoteHomes();

                if (remoteHomes.Count > 0)
                {
                    target = remoteHomes[0];
                    scenario.SetTarget(target);

                    found = true;
                }

                if (log.IsDebugEnabled)
                {
                    log.Debug("FIN Intento nr." + tries);
                }
            }

            if (target == null)
            {
                throw new Exception("Remote federate not running");
            }
            else
            {
                form.UpdateUI();
            }

        }

        public void SimulationLoop()
        {
            long startTime = DateTime.Now.Ticks;
            SimulationTime = new DoubleValuedLogicalTime(startTime);
            ILogicalTime finalTime = new DoubleValuedLogicalTime(startTime + NumberOfSeconds * TimeSpan.TicksPerSecond);
            ILogicalTime lastTime = new DoubleValuedLogicalTime(startTime);

            SetTarget();

            while (SimulationTime.CompareTo(finalTime) < 0 && !shouldStop)
            {
                SimulationTime = new DoubleValuedLogicalTime(DateTime.Now.Ticks);

                if (simulationRunning)
                {
                    //SimulationMoveActors();
                    SimulationMoveActor(actor, target);

                    AdvanceTime();

                    form.UpdateUI(actor.Direction);

                    System.Threading.Thread.Sleep(simulationEntry.Delay);
                }

                lastTime = SimulationTime;
            }

            // clean up for the next test
            ResignAndDestroy();
        }

        private bool sendedBoxInHouseInteraction = false;

        protected void NotifiesBoxInHouse(ILogicalTime time)
        {
            if (!sendedBoxInHouseInteraction)
            {
                BoxInHouseMessage interaction = new BoxInHouseMessage();

                interaction.Time = new byte[time.EncodedLength()];
                time.Encode(interaction.Time, 0);

                rti.SendInteraction(interaction);

                sendedBoxInHouseInteraction = true;

                if (log.IsDebugEnabled)
                    log.Debug("Sends BoxInHouse interaction: " + interaction);
            }
        }

        public override void AdvanceTime()
        {
            ILogicalTime currentTime = rtiAmbassador.QueryLogicalTime();
            ILogicalTime nextTime = currentTime.Add(moveActorInterval);

            timeManagementFederateAmbassador.canAdvanceTime = false;

            form.UpdateTimeAdvanceOperationLabel();

            rtiAmbassador.TimeAdvanceRequest(nextTime);
            //rtiAmbassador.NextMessageRequest(nextTime);

            form.UpdateTimeManagementValueLabels();

            while (!timeManagementFederateAmbassador.canAdvanceTime)
            {
                rtiAmbassador.EvokeMultipleCallbacks(0.1, 0.5);
            }
        }

        /*
        protected void SimulationMoveActors()
        {
            foreach (Actor actor in actors)
            {
                SimulationMoveActor(actor, target);
            }
        }
        */

        private bool initialTime = true;

        protected void SimulationMoveActor(Actor anActor, Home aTarget)
        {
            if (isActorInLocation(anActor, anActor.Home))
            {
                if (anActor.Home.BoxesCount > 0 || initialTime)
                {
                    // Función que calcule la llegada
                    ILogicalTime currentTime = rti.QueryLogicalTime();
                    ILogicalTimeInterval intervalTimeArriveAtTheTarget = GetArrivalTimeToTarget(anActor, aTarget);
                    ILogicalTime timeArriveAtTheTarget = currentTime.Add(intervalTimeArriveAtTheTarget);

                    NotifiesBoxInHouse(timeArriveAtTheTarget);

                    MoveActor(anActor, aTarget);

                    anActor.Home.BoxesCount = 0;
                }
            }
            else
            {
                initialTime = false;

                if (!isActorInTarget(anActor, aTarget))
                {
                    MoveActor(anActor, aTarget);
                }
                // El muñeco está en el destino
                else
                {
                    scenario.MoveActorToHome(anActor.Home.PosX, anActor.Home.PosY);  
                    sendedBoxInHouseInteraction = false;
                }
            }
        }

        private ILogicalTimeInterval GetArrivalTimeToTarget(Actor anActor, Home aTarget)
        {
            int xDistance = anActor.PosX - aTarget.PosX;
            if (xDistance < 0) xDistance *= -1;

            int yDistance = anActor.PosY - aTarget.PosY;
            if (yDistance < 0) yDistance *= -1;

            return new DoubleValuedLogicalTimeInterval((xDistance + yDistance - 1) * federate.Lookahead);
        }

        private bool isActorInLocation(Actor anActor, Home aLocation)
        {
            return anActor.PosX == aLocation.PosX && anActor.PosY == aLocation.PosY;
        }

        private bool isActorInTarget(Actor anActor, Home aTarget)
        {
            return Math.Abs((anActor.PosX - aTarget.PosX) + (anActor.PosY - aTarget.PosY)) == 1;
        }

        protected void MoveActor(Actor anActor, Home aTarget)
        {
            if (anActor.PosX > aTarget.PosX)
                form.Scenario.MoveActor(Actor.MoveDirection.Left);

            else if (anActor.PosX < aTarget.PosX)
                form.Scenario.MoveActor(Actor.MoveDirection.Right);

            else if (anActor.PosY > aTarget.PosY)
                form.Scenario.MoveActor(Actor.MoveDirection.Up);

            else if (anActor.PosY < aTarget.PosY)
                form.Scenario.MoveActor(Actor.MoveDirection.Down);
        }

        public void RequestStopSimulation()
        {
            shouldStop = true;
        }
    }
}