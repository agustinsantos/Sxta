namespace Sxta.Rti1516.TimeManagementSample
{
    partial class TimeManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTimeRegulatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTimeConstrainedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeAdvancedRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextMessageRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.boxInHouseTimeLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timeAdvanceOperationLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pendingTimeValueLabel = new System.Windows.Forms.Label();
            this.GALTValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentTimeValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lookaheadValueLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rolValueLabel = new System.Windows.Forms.Label();
            this.simulationButton = new System.Windows.Forms.Button();
            this.screen = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 514);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(702, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.onClickRefresh);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.onClickExit);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setTimeRegulatingToolStripMenuItem,
            this.setTimeConstrainedToolStripMenuItem,
            this.timeAdvancedRequestToolStripMenuItem,
            this.nextMessageRequestToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // setTimeRegulatingToolStripMenuItem
            // 
            this.setTimeRegulatingToolStripMenuItem.Name = "setTimeRegulatingToolStripMenuItem";
            this.setTimeRegulatingToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setTimeRegulatingToolStripMenuItem.Text = "Set time regulating";
            this.setTimeRegulatingToolStripMenuItem.Click += new System.EventHandler(this.OnClickSetTimeRegulating);
            // 
            // setTimeConstrainedToolStripMenuItem
            // 
            this.setTimeConstrainedToolStripMenuItem.Name = "setTimeConstrainedToolStripMenuItem";
            this.setTimeConstrainedToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setTimeConstrainedToolStripMenuItem.Text = "Set time constrained";
            this.setTimeConstrainedToolStripMenuItem.Click += new System.EventHandler(this.OnClickSetTimeConstrained);
            // 
            // timeAdvancedRequestToolStripMenuItem
            // 
            this.timeAdvancedRequestToolStripMenuItem.Name = "timeAdvancedRequestToolStripMenuItem";
            this.timeAdvancedRequestToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.timeAdvancedRequestToolStripMenuItem.Text = "Time advanced request";
            this.timeAdvancedRequestToolStripMenuItem.Click += new System.EventHandler(this.OnClickTimeAdvancedRequest);
            // 
            // nextMessageRequestToolStripMenuItem
            // 
            this.nextMessageRequestToolStripMenuItem.Name = "nextMessageRequestToolStripMenuItem";
            this.nextMessageRequestToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.nextMessageRequestToolStripMenuItem.Text = "Next message request";
            this.nextMessageRequestToolStripMenuItem.Click += new System.EventHandler(this.OnClickNextMessageRequest);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.boxInHouseTimeLabel);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.simulationButton);
            this.panel1.Controls.Add(this.screen);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 486);
            this.panel1.TabIndex = 2;
            // 
            // boxInHouseTimeLabel
            // 
            this.boxInHouseTimeLabel.AutoSize = true;
            this.boxInHouseTimeLabel.Location = new System.Drawing.Point(593, 252);
            this.boxInHouseTimeLabel.Name = "boxInHouseTimeLabel";
            this.boxInHouseTimeLabel.Size = new System.Drawing.Size(10, 13);
            this.boxInHouseTimeLabel.TabIndex = 17;
            this.boxInHouseTimeLabel.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(488, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Next box at time";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.timeAdvanceOperationLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.statusValueLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pendingTimeValueLabel);
            this.groupBox2.Controls.Add(this.GALTValueLabel);
            this.groupBox2.Location = new System.Drawing.Point(488, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 124);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advance Time";
            // 
            // timeAdvanceOperationLabel
            // 
            this.timeAdvanceOperationLabel.AutoSize = true;
            this.timeAdvanceOperationLabel.Location = new System.Drawing.Point(95, 25);
            this.timeAdvanceOperationLabel.Name = "timeAdvanceOperationLabel";
            this.timeAdvanceOperationLabel.Size = new System.Drawing.Size(10, 13);
            this.timeAdvanceOperationLabel.TabIndex = 13;
            this.timeAdvanceOperationLabel.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Operation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "PendingTime";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Status";
            // 
            // statusValueLabel
            // 
            this.statusValueLabel.AutoSize = true;
            this.statusValueLabel.Location = new System.Drawing.Point(92, 99);
            this.statusValueLabel.Name = "statusValueLabel";
            this.statusValueLabel.Size = new System.Drawing.Size(13, 13);
            this.statusValueLabel.TabIndex = 5;
            this.statusValueLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "GALT";
            // 
            // pendingTimeValueLabel
            // 
            this.pendingTimeValueLabel.AutoSize = true;
            this.pendingTimeValueLabel.Location = new System.Drawing.Point(92, 49);
            this.pendingTimeValueLabel.Name = "pendingTimeValueLabel";
            this.pendingTimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.pendingTimeValueLabel.TabIndex = 11;
            this.pendingTimeValueLabel.Text = "0";
            // 
            // GALTValueLabel
            // 
            this.GALTValueLabel.AutoSize = true;
            this.GALTValueLabel.Location = new System.Drawing.Point(92, 74);
            this.GALTValueLabel.Name = "GALTValueLabel";
            this.GALTValueLabel.Size = new System.Drawing.Size(13, 13);
            this.GALTValueLabel.TabIndex = 7;
            this.GALTValueLabel.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.currentTimeValueLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lookaheadValueLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.rolValueLabel);
            this.groupBox1.Location = new System.Drawing.Point(488, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 95);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Time";
            // 
            // currentTimeValueLabel
            // 
            this.currentTimeValueLabel.AutoSize = true;
            this.currentTimeValueLabel.Location = new System.Drawing.Point(92, 44);
            this.currentTimeValueLabel.Name = "currentTimeValueLabel";
            this.currentTimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.currentTimeValueLabel.TabIndex = 1;
            this.currentTimeValueLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lookahead";
            // 
            // lookaheadValueLabel
            // 
            this.lookaheadValueLabel.AutoSize = true;
            this.lookaheadValueLabel.Location = new System.Drawing.Point(92, 68);
            this.lookaheadValueLabel.Name = "lookaheadValueLabel";
            this.lookaheadValueLabel.Size = new System.Drawing.Size(13, 13);
            this.lookaheadValueLabel.TabIndex = 3;
            this.lookaheadValueLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mode";
            // 
            // rolValueLabel
            // 
            this.rolValueLabel.AutoSize = true;
            this.rolValueLabel.Location = new System.Drawing.Point(92, 21);
            this.rolValueLabel.Name = "rolValueLabel";
            this.rolValueLabel.Size = new System.Drawing.Size(44, 13);
            this.rolValueLabel.TabIndex = 9;
            this.rolValueLabel.Text = "Nothing";
            // 
            // simulationButton
            // 
            this.simulationButton.Location = new System.Drawing.Point(540, 285);
            this.simulationButton.Name = "simulationButton";
            this.simulationButton.Size = new System.Drawing.Size(103, 23);
            this.simulationButton.TabIndex = 13;
            this.simulationButton.Text = "Go";
            this.simulationButton.UseVisualStyleBackColor = true;
            this.simulationButton.Click += new System.EventHandler(this.simulationButton_Click);
            // 
            // screen
            // 
            this.screen.Location = new System.Drawing.Point(0, 3);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(482, 465);
            this.screen.TabIndex = 12;
            this.screen.TabStop = false;
            // 
            // TimeManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 536);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(710, 570);
            this.Name = "TimeManagementForm";
            this.Text = "Time Management Sample";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label currentTimeValueLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lookaheadValueLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTimeRegulatingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTimeConstrainedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeAdvancedRequestToolStripMenuItem;
        private System.Windows.Forms.Label statusValueLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label GALTValueLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem nextMessageRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Label rolValueLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label pendingTimeValueLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.Button simulationButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label timeAdvanceOperationLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label boxInHouseTimeLabel;
        private System.Windows.Forms.Label label9;
    }
}

