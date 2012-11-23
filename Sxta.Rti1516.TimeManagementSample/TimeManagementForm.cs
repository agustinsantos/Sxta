using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Hla.Rti1516;
using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.Management;

using WeifenLuo.WinFormsUI.Docking;

namespace Sxta.Rti1516.TimeManagementSample
{
    public partial class TimeManagementForm : Form
    {
        delegate void DrawChangesDelegate();
        delegate void DrawBoxInHouseTimeDelegate(ILogicalTime time);

        protected XrtiExecutiveAmbassador rti;
        protected TimeManagementSimulation tms;

        private const String defaultValue = "Value not assigned";

        public TimeManagementForm(XrtiExecutiveAmbassador aRti, TimeManagementSimulation aTms)
        {
            InitializeComponent();
            InitializeScenario();

            this.rti = aRti;
            this.tms = aTms;
        }

        # region simulation methods

        // Initialize the required objects
        private LevelSet levelSet;
        private Scenario scenario;

        public Scenario Scenario
        {
            get { return scenario; }
        }

        private Image img;

        private void InitializeScenario()
        {
            levelSet = new LevelSet();
            levelSet.CurrentLevel = 1;

            // Load the levels in the LevelSet object and set the current level
            levelSet.SetLevelsInLevelSet("Resources/boxworld.xml");
            scenario = (Scenario)levelSet[levelSet.CurrentLevel - 1];

            // Draw the level on the screen
            DrawScenario();
        }

        private void DrawScenario()
        {
            int levelWidth = (scenario.Width) * Scenario.ITEM_SIZE;
            int levelHeight = (scenario.Height) * Scenario.ITEM_SIZE;

            this.ClientSize = new Size(levelWidth, levelHeight);
            screen.Size = new System.Drawing.Size(levelWidth, levelHeight);

            img = scenario.CreateScenario();
            screen.Image = img;
        }

        
        public void UpdateUI()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.screen.InvokeRequired)
            {
                DrawChangesDelegate d = new DrawChangesDelegate(_UpdateUI);
                Invoke(d);
            }
            else
            {
                img = scenario.GetScenario();
                screen.Image = img;
            }
        }

        internal Actor.MoveDirection direction;
        public void UpdateUI(Actor.MoveDirection direction)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.screen.InvokeRequired)
            {
                this.direction = direction;
                DrawChangesDelegate d = new DrawChangesDelegate(_UpdateUI);
                Invoke(d);
            }
            else
            {
                _UpdateUI();
            }
        }

        public void _UpdateUI()
        {
            scenario.DrawChanges(this.direction);
            img = scenario.GetScenario();
            screen.Image = img;
        }

        # endregion

        #region Rti's Labels methods

        public void InitTimeManagementValueLabels()
        {
            Text = Text + " - " + rti.Federate.HLAfederateType;
            UpdateTimeManagementValueLabels();
        }

        public void UpdateTimeManagementValueLabels()
        {
            if (panel1.InvokeRequired)
            {
                DrawChangesDelegate d = new DrawChangesDelegate(UpdateTimeManagementValueLabels);
                Invoke(d);
            }
            else
            {
                UpdateFederateLogicalTimeLabel();
                UpdateFederateLookaheadLabel();
                UpdateFederateGALTLabel();
                UpdateFederateStatusLabel();
                UpdateFederateRolLabel();
                UpdateFederatePendingTimeLabel();
                _UpdateTimeAdvanceOperationLabel();
            }
        }

        public void UpdateTimeAdvanceOperationLabel()
        {
            if (timeAdvanceOperationLabel.InvokeRequired)
            {
                DrawChangesDelegate d = new DrawChangesDelegate(UpdateTimeAdvanceOperationLabel);
                Invoke(d);
            }
            else
            {
                _UpdateTimeAdvanceOperationLabel();
            }
        }

        internal ILogicalTime boxInHouseTime;
        public void UpdateBoxInHouseTimeLabel(ILogicalTime time)
        {
            if (boxInHouseTimeLabel.InvokeRequired)
            {
                boxInHouseTime = time;
                DrawChangesDelegate d = new DrawChangesDelegate(_UpdateBoxInHouseTimeLabel);
                Invoke(d);
            }
        }

        private void _UpdateBoxInHouseTimeLabel()
        {
            boxInHouseTimeLabel.Text = boxInHouseTime.ToString();
        }

        private void _UpdateTimeAdvanceOperationLabel()
        {
            if (rti.Federate.HLAtimeManagerState == HLAtimeState.TimeAdvancing)
            {
                timeAdvanceOperationLabel.Text = "TAR";
            }
            else
            {
                timeAdvanceOperationLabel.Text = " - ";
            }
        }

        private void UpdateFederateLogicalTimeLabel()
        {
            ILogicalTime currentTime = rti.QueryLogicalTime();
            if (currentTime != null)
                this.currentTimeValueLabel.Text = currentTime.ToString();
            else
                this.currentTimeValueLabel.Text = defaultValue;
        }

        private void UpdateFederatePendingTimeLabel()
        {
            ILogicalTime pendingTime = ((Sxtafederate)rti.Federate).HLApendingTime;
            if (pendingTime != null)
                this.pendingTimeValueLabel.Text = pendingTime.ToString();
            else
                this.pendingTimeValueLabel.Text = defaultValue;
        }

        private void UpdateFederateLookaheadLabel()
        {
            ILogicalTimeInterval lookahead = rti.QueryLookahead();
            if (lookahead != null)
                this.lookaheadValueLabel.Text = lookahead.ToString();
            else
                this.lookaheadValueLabel.Text = defaultValue;
        }

        private void UpdateFederateGALTLabel()
        {
            ILogicalTime GALT = rti.Federate.HLAGALT;
            if (GALT != null)
                this.GALTValueLabel.Text = GALT.ToString();
            else
                this.GALTValueLabel.Text = defaultValue;
        }

        private void UpdateFederateStatusLabel()
        {
            if (rti.Federate.HLAtimeManagerState == HLAtimeState.TimeAdvancing)
            {
                this.statusValueLabel.Text = "PENDING";
            }
            else
            {
                this.statusValueLabel.Text = "GRANT";
            }
        }

        private void UpdateFederateRolLabel()
        {
            if (rti.Federate.HLAtimeConstrained && rti.Federate.HLAtimeRegulating)
                this.rolValueLabel.Text = "R + C";
            else if (rti.Federate.HLAtimeConstrained)
                this.rolValueLabel.Text = "Constrained";
            else if (rti.Federate.HLAtimeRegulating)
                this.rolValueLabel.Text = "Regulating";
            else
                this.rolValueLabel.Text = "Nothing";
        }

        #endregion

        # region menu options

        private void OnClickSetTimeRegulating(object sender, EventArgs e)
        {
            ILogicalTimeIntervalFactory factory = rti.LogicalTimeIntervalFactory;
            rti.EnableTimeRegulation(factory.MakeEpsilon());

            UpdateTimeManagementValueLabels();
        }

        private void OnClickSetTimeConstrained(object sender, EventArgs e)
        {
            rti.EnableTimeConstrained();

            UpdateTimeManagementValueLabels();
        }

        private void OnClickTimeAdvancedRequest(object sender, EventArgs e)
        {
            ILogicalTimeIntervalFactory factory = rti.LogicalTimeIntervalFactory;
            ILogicalTime time = rti.QueryLogicalTime().Add(factory.MakeEpsilon());
            rti.TimeAdvanceRequest(time);

            UpdateTimeManagementValueLabels();
        }

        private void OnClickNextMessageRequest(object sender, EventArgs e)
        {
            ILogicalTimeIntervalFactory factory = rti.LogicalTimeIntervalFactory;
            ILogicalTime time = rti.QueryLogicalTime().Add(factory.MakeEpsilon());
            rti.NextMessageRequest(time);

            UpdateTimeManagementValueLabels();
        }

        private void onClickExit(object sender, EventArgs e)
        {
            Close();
        }

        private void onClickRefresh(object sender, EventArgs e)
        {
            UpdateTimeManagementValueLabels();
        }

        # endregion

        private void simulationButton_Click(object sender, EventArgs e)
        {
            if (simulationButton.Text == "Pause")
            {
                tms.PauseSimulation();
                simulationButton.Text = "Go";
            }
            else
            {
                tms.ResumeSimulation();
                simulationButton.Text = "Pause";
            }
        }
    }
}