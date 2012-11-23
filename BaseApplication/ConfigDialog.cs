namespace Sxta.Rti1516.BaseApplication
{

    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Net;
    using System.Xml;

    // Import log4net classes.
    using log4net;

    using Nini.Config;

    /// <summary>
    /// Summary description for WinMainForm.
    /// </summary>
    public class ConfigDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBasic;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.TabPage tabRendezvous;
        private System.Windows.Forms.TabPage tabSecurity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TextBox PeerName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox TCPEnable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TcpInterfacePort;
        private System.Windows.Forms.ComboBox TcpInterfaceAddress;
        private System.Windows.Forms.ListBox TCPConnectionsList;
        private System.Windows.Forms.CheckBox isRendevous;
        private System.Windows.Forms.TextBox TcpAddress;
        private System.Windows.Forms.TextBox TcpPort;
        private System.Windows.Forms.TextBox PeerDescription;
        private System.Windows.Forms.Label PeerLabelDescription;
        private System.Windows.Forms.GroupBox TCPPanel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GroupBox groupBox1;
        private TextBox UDPInterfacePort;
        private ComboBox UdpInterfaceAddress;
        private CheckBox UDPEnable;
        private TextBox Log4NetFileBox;
        private Label label9;
        private Button button3;
        private Button button4;
        private ListBox UDPConnectionsList;
        private TextBox UdpPort;
        private TextBox UdpAddress;
        private Label label1;

        private IConfigSource config;

        public ConfigDialog(ref IConfigSource source)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            config = source;

            IConfig cfg = config.Configs["PeerInfo"];
            if (cfg != null)
            {
                PeerName.Text = cfg.Get("PeerName", "PeerA");
                PeerDescription.Text = cfg.Get("PeerDescription", "A generic peer description");
            }
            cfg = config.Configs["Channels"];
            if (cfg != null)
            {
                ConfigTransportPanel(cfg.Get("DefaultAddr"),
                                     cfg.Get("TcpPort", "disable"),
                                     cfg.Get("UdpPort", "disable"), true);
            }
            else
            {
                ConfigTransportPanel("localhost", "7777", "1234", true);
            }
            cfg = config.Configs["RendezVous"];
            if (cfg != null)
            {
                foreach (string addrs in cfg.Get("ReliableAddrs", "").Split('|'))
                {
                    if (!string.IsNullOrEmpty(addrs))
                        TCPConnectionsList.Items.Add(addrs);
                }
                foreach (string addrs in cfg.Get("BestEffortAddrs", "").Split('|'))
                {
                    if (!string.IsNullOrEmpty(addrs))
                        UDPConnectionsList.Items.Add(addrs);
                }
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.helpLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.PeerDescription = new System.Windows.Forms.TextBox();
            this.PeerLabelDescription = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.PeerName = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.Log4NetFileBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UDPInterfacePort = new System.Windows.Forms.TextBox();
            this.UdpInterfaceAddress = new System.Windows.Forms.ComboBox();
            this.UDPEnable = new System.Windows.Forms.CheckBox();
            this.TCPPanel = new System.Windows.Forms.GroupBox();
            this.TcpInterfacePort = new System.Windows.Forms.TextBox();
            this.TcpInterfaceAddress = new System.Windows.Forms.ComboBox();
            this.TCPEnable = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabRendezvous = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.UDPConnectionsList = new System.Windows.Forms.ListBox();
            this.UdpPort = new System.Windows.Forms.TextBox();
            this.UdpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TCPConnectionsList = new System.Windows.Forms.ListBox();
            this.TcpPort = new System.Windows.Forms.TextBox();
            this.TcpAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.isRendevous = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TCPPanel.SuspendLayout();
            this.tabRendezvous.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpLabel
            // 
            this.helpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.helpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpLabel.Location = new System.Drawing.Point(0, 9);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(757, 99);
            this.helpLabel.TabIndex = 1;
            this.helpLabel.Text = "See \"http://sxta.sourceforge.net/index.html\" for help.";
            this.helpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabBasic);
            this.tabControl1.Controls.Add(this.tabAdvanced);
            this.tabControl1.Controls.Add(this.tabRendezvous);
            this.tabControl1.Controls.Add(this.tabSecurity);
            this.tabControl1.Location = new System.Drawing.Point(0, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 404);
            this.tabControl1.TabIndex = 2;
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.PeerDescription);
            this.tabBasic.Controls.Add(this.PeerLabelDescription);
            this.tabBasic.Controls.Add(this.textBox2);
            this.tabBasic.Controls.Add(this.textBox1);
            this.tabBasic.Controls.Add(this.label8);
            this.tabBasic.Controls.Add(this.PeerName);
            this.tabBasic.Controls.Add(this.checkBox1);
            this.tabBasic.Controls.Add(this.label4);
            this.tabBasic.Controls.Add(this.label3);
            this.tabBasic.Controls.Add(this.label2);
            this.tabBasic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabBasic.Location = new System.Drawing.Point(4, 25);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Size = new System.Drawing.Size(760, 375);
            this.tabBasic.TabIndex = 0;
            this.tabBasic.Text = "Basic";
            // 
            // PeerDescription
            // 
            this.PeerDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeerDescription.Location = new System.Drawing.Point(202, 138);
            this.PeerDescription.Name = "PeerDescription";
            this.PeerDescription.Size = new System.Drawing.Size(441, 23);
            this.PeerDescription.TabIndex = 11;
            // 
            // PeerLabelDescription
            // 
            this.PeerLabelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeerLabelDescription.Location = new System.Drawing.Point(67, 138);
            this.PeerLabelDescription.Name = "PeerLabelDescription";
            this.PeerLabelDescription.Size = new System.Drawing.Size(115, 19);
            this.PeerLabelDescription.TabIndex = 10;
            this.PeerLabelDescription.Text = "Peer Description:";
            this.PeerLabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(518, 212);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(106, 23);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "8080";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(269, 212);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 23);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "myProxy.myDomain";
            // 
            // label8
            // 
            this.label8.Enabled = false;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(154, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Proxy Address:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PeerName
            // 
            this.PeerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeerName.Location = new System.Drawing.Point(202, 111);
            this.PeerName.Name = "PeerName";
            this.PeerName.Size = new System.Drawing.Size(297, 23);
            this.PeerName.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(134, 175);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(356, 28);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Use Proxy Server (if behind firewall)";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(509, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 26);
            this.label4.TabIndex = 2;
            this.label4.Text = "(Mandatory)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Peer Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(259, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Basic settings";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.label9);
            this.tabAdvanced.Controls.Add(this.Log4NetFileBox);
            this.tabAdvanced.Controls.Add(this.groupBox1);
            this.tabAdvanced.Controls.Add(this.TCPPanel);
            this.tabAdvanced.Controls.Add(this.label5);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(760, 375);
            this.tabAdvanced.TabIndex = 1;
            this.tabAdvanced.Text = "Advanced";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 22);
            this.label9.TabIndex = 8;
            this.label9.Text = "Log4Net File";
            // 
            // Log4NetFileBox
            // 
            this.Log4NetFileBox.Location = new System.Drawing.Point(105, 59);
            this.Log4NetFileBox.Name = "Log4NetFileBox";
            this.Log4NetFileBox.Size = new System.Drawing.Size(267, 22);
            this.Log4NetFileBox.TabIndex = 7;
            this.Log4NetFileBox.Text = "SxtaLog4NetConfig.xml";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UDPInterfacePort);
            this.groupBox1.Controls.Add(this.UdpInterfaceAddress);
            this.groupBox1.Controls.Add(this.UDPEnable);
            this.groupBox1.Location = new System.Drawing.Point(3, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 109);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Best-Effort Settings (UDP)";
            // 
            // UDPInterfacePort
            // 
            this.UDPInterfacePort.Location = new System.Drawing.Point(430, 31);
            this.UDPInterfacePort.Name = "UDPInterfacePort";
            this.UDPInterfacePort.Size = new System.Drawing.Size(115, 22);
            this.UDPInterfacePort.TabIndex = 5;
            // 
            // UdpInterfaceAddress
            // 
            this.UdpInterfaceAddress.Location = new System.Drawing.Point(139, 31);
            this.UdpInterfaceAddress.Name = "UdpInterfaceAddress";
            this.UdpInterfaceAddress.Size = new System.Drawing.Size(260, 24);
            this.UdpInterfaceAddress.TabIndex = 4;
            // 
            // UDPEnable
            // 
            this.UDPEnable.Location = new System.Drawing.Point(13, 29);
            this.UDPEnable.Name = "UDPEnable";
            this.UDPEnable.Size = new System.Drawing.Size(125, 24);
            this.UDPEnable.TabIndex = 1;
            this.UDPEnable.Text = "Enable";
            // 
            // TCPPanel
            // 
            this.TCPPanel.Controls.Add(this.TcpInterfacePort);
            this.TCPPanel.Controls.Add(this.TcpInterfaceAddress);
            this.TCPPanel.Controls.Add(this.TCPEnable);
            this.TCPPanel.Location = new System.Drawing.Point(0, 111);
            this.TCPPanel.Name = "TCPPanel";
            this.TCPPanel.Size = new System.Drawing.Size(749, 101);
            this.TCPPanel.TabIndex = 2;
            this.TCPPanel.TabStop = false;
            this.TCPPanel.Text = "Reliable Settings (TCP)";
            // 
            // TcpInterfacePort
            // 
            this.TcpInterfacePort.Location = new System.Drawing.Point(430, 31);
            this.TcpInterfacePort.Name = "TcpInterfacePort";
            this.TcpInterfacePort.Size = new System.Drawing.Size(115, 22);
            this.TcpInterfacePort.TabIndex = 5;
            // 
            // TcpInterfaceAddress
            // 
            this.TcpInterfaceAddress.Location = new System.Drawing.Point(139, 31);
            this.TcpInterfaceAddress.Name = "TcpInterfaceAddress";
            this.TcpInterfaceAddress.Size = new System.Drawing.Size(260, 24);
            this.TcpInterfaceAddress.TabIndex = 4;
            // 
            // TCPEnable
            // 
            this.TCPEnable.Location = new System.Drawing.Point(13, 29);
            this.TCPEnable.Name = "TCPEnable";
            this.TCPEnable.Size = new System.Drawing.Size(125, 28);
            this.TCPEnable.TabIndex = 1;
            this.TCPEnable.Text = "Enable";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(250, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 28);
            this.label5.TabIndex = 1;
            this.label5.Text = "Experienced Users Only";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabRendezvous
            // 
            this.tabRendezvous.Controls.Add(this.groupBox2);
            this.tabRendezvous.Controls.Add(this.label6);
            this.tabRendezvous.Location = new System.Drawing.Point(4, 25);
            this.tabRendezvous.Name = "tabRendezvous";
            this.tabRendezvous.Size = new System.Drawing.Size(760, 375);
            this.tabRendezvous.TabIndex = 2;
            this.tabRendezvous.Text = "RendezVous/Relays";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.UDPConnectionsList);
            this.groupBox2.Controls.Add(this.UdpPort);
            this.groupBox2.Controls.Add(this.UdpAddress);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.TCPConnectionsList);
            this.groupBox2.Controls.Add(this.TcpPort);
            this.groupBox2.Controls.Add(this.TcpAddress);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.isRendevous);
            this.groupBox2.Location = new System.Drawing.Point(10, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(748, 231);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rendez-vous Settings";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(671, 166);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 27);
            this.button3.TabIndex = 13;
            this.button3.Text = "-";
            this.button3.Click += new System.EventHandler(this.RemoveUdpConnection);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(671, 129);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(29, 27);
            this.button4.TabIndex = 12;
            this.button4.Text = "+";
            this.button4.Click += new System.EventHandler(this.AddUdpConnection);
            // 
            // UDPConnectionsList
            // 
            this.UDPConnectionsList.ItemHeight = 16;
            this.UDPConnectionsList.Location = new System.Drawing.Point(403, 138);
            this.UDPConnectionsList.Name = "UDPConnectionsList";
            this.UDPConnectionsList.Size = new System.Drawing.Size(259, 68);
            this.UDPConnectionsList.TabIndex = 11;
            // 
            // UdpPort
            // 
            this.UdpPort.Location = new System.Drawing.Point(597, 111);
            this.UdpPort.Name = "UdpPort";
            this.UdpPort.Size = new System.Drawing.Size(65, 22);
            this.UdpPort.TabIndex = 10;
            // 
            // UdpAddress
            // 
            this.UdpAddress.Location = new System.Drawing.Point(403, 111);
            this.UdpAddress.Name = "UdpAddress";
            this.UdpAddress.Size = new System.Drawing.Size(158, 22);
            this.UdpAddress.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(403, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "UDP Connections";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(278, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 27);
            this.button2.TabIndex = 7;
            this.button2.Text = "-";
            this.button2.Click += new System.EventHandler(this.RemoveTcpConnection);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(278, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 27);
            this.button1.TabIndex = 6;
            this.button1.Text = "+";
            this.button1.Click += new System.EventHandler(this.AddTcpConnection);
            // 
            // TCPConnectionsList
            // 
            this.TCPConnectionsList.ItemHeight = 16;
            this.TCPConnectionsList.Location = new System.Drawing.Point(10, 138);
            this.TCPConnectionsList.Name = "TCPConnectionsList";
            this.TCPConnectionsList.Size = new System.Drawing.Size(259, 68);
            this.TCPConnectionsList.TabIndex = 5;
            // 
            // TcpPort
            // 
            this.TcpPort.Location = new System.Drawing.Point(203, 111);
            this.TcpPort.Name = "TcpPort";
            this.TcpPort.Size = new System.Drawing.Size(66, 22);
            this.TcpPort.TabIndex = 4;
            // 
            // TcpAddress
            // 
            this.TcpAddress.AcceptsTab = true;
            this.TcpAddress.Location = new System.Drawing.Point(10, 111);
            this.TcpAddress.Name = "TcpAddress";
            this.TcpAddress.Size = new System.Drawing.Size(172, 22);
            this.TcpAddress.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(221, 27);
            this.label11.TabIndex = 2;
            this.label11.Text = "TCP Connections:";
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(10, 46);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(172, 28);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Act as a JxtaProxy";
            // 
            // isRendevous
            // 
            this.isRendevous.Checked = true;
            this.isRendevous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isRendevous.Location = new System.Drawing.Point(10, 18);
            this.isRendevous.Name = "isRendevous";
            this.isRendevous.Size = new System.Drawing.Size(182, 28);
            this.isRendevous.TabIndex = 0;
            this.isRendevous.Text = "Act as a Rendezvous";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(259, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 28);
            this.label6.TabIndex = 2;
            this.label6.Text = "Experienced Users Only";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabSecurity
            // 
            this.tabSecurity.Controls.Add(this.label7);
            this.tabSecurity.Location = new System.Drawing.Point(4, 25);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Size = new System.Drawing.Size(760, 375);
            this.tabSecurity.TabIndex = 3;
            this.tabSecurity.Text = "Security";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(230, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(240, 28);
            this.label7.TabIndex = 3;
            this.label7.Text = "Security Settings";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonOK
            // 
            this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK.Location = new System.Drawing.Point(259, 456);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(79, 25);
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.Click += new System.EventHandler(this.OnButtonOK);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel.Location = new System.Drawing.Point(384, 456);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(79, 25);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.Click += new System.EventHandler(this.OnButtonCancel);
            // 
            // ConfigDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(767, 493);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.helpLabel);
            this.Name = "ConfigDialog";
            this.Text = "Jxta Configurator";
            this.tabControl1.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.tabBasic.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TCPPanel.ResumeLayout(false);
            this.TCPPanel.PerformLayout();
            this.tabRendezvous.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabSecurity.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private bool done = false;
        private bool canceled = false;

        public bool UntilDone()
        {
            this.Activate();
            this.ShowDialog();

            try
            {
                while (!done) ;
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn(e.Message);
            }

            if (canceled)
                throw new
                    Exception("Canceled during configuration");
            return (done);

        }

        private void OnButtonOK(object sender, System.EventArgs e)
        {
            if (SaveValues())
            {
                done = true;
                Dispose();
            }
        }

        private void OnButtonCancel(object sender, System.EventArgs e)
        {
            done = true;
            canceled = true;
            Dispose();
        }

        private bool VerifyInput()
        {

            if (string.IsNullOrEmpty(this.PeerName.Text))
            {
                this.helpLabel.ForeColor = System.Drawing.Color.DarkRed;
                this.helpLabel.Text = "A peer name is required.";
                return false;
            }
            return true;
        }


        private bool SaveValues()
        {
            if (!VerifyInput()) return false;

            IConfig cfg = config.Configs["PeerInfo"];
            if (cfg == null)
            {
                cfg = config.AddConfig("PeerInfo");
            }

            cfg.Set("PeerName", PeerName.Text);
            cfg.Set("PeerDescription", PeerDescription.Text);

            cfg = config.Configs["Channels"];
            if (cfg == null)
            {
                cfg = config.AddConfig("Channels");
            }
            cfg.Set("DefaultAddr", TcpInterfaceAddress.SelectedItem);
            cfg.Set("TcpPort", TcpInterfacePort.Text);
            cfg.Set("UdpPort", UDPInterfacePort.Text);
            cfg.Set("TcpEnable", TCPEnable.Checked);
            cfg.Set("UdpEnable", UDPEnable.Checked);

            cfg = config.Configs["RendezVous"];
            if (cfg == null)
            {
                cfg = config.AddConfig("RendezVous");
            }
            string addressStr = "";
            for (int i = 0; i < TCPConnectionsList.Items.Count; i++)
            {
                string addrs = (string)TCPConnectionsList.Items[i];
                addressStr += addrs;
                if (i != TCPConnectionsList.Items.Count - 1)
                    addressStr += "|";
            }
            cfg.Set("ReliableAddrs", addressStr);
            addressStr = "";
            for (int i = 0; i < UDPConnectionsList.Items.Count; i++)
            {
                string addrs = (string)UDPConnectionsList.Items[i];
                addressStr += addrs;
                if (i != UDPConnectionsList.Items.Count - 1)
                    addressStr += "|";
            }
            cfg.Set("BestEffortAddrs", addressStr);

            return true;
        }

        private void ConfigTransportPanel(String defaultInterfaceAddr,
                                          String tcpPort, String udpPort,
                                          bool defaultState)
        {

            if (tcpPort.ToLower().Equals("disable"))
            {
                TCPEnable.Checked = false;
            }
            else
            {
                TcpInterfacePort.Text = tcpPort;
                TCPEnable.Checked = defaultState;
            }

            if (udpPort.ToLower().Equals("disable"))
            {
                UDPEnable.Checked = false;
            }
            else
            {
                UDPInterfacePort.Text = udpPort;
                UDPEnable.Checked = defaultState;
            }

            ConfigIfAddrPanel(defaultInterfaceAddr);
        }

        private void ConfigIfAddrPanel(String defaultInterfaceAddr)
        {
            try
            {
                String hostName = Dns.GetHostName();
                System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(hostName);
                IPAddress[] all = System.Net.Dns.GetHostEntry(hostEntry.HostName).AddressList;
                foreach (IPAddress ip in all)
                {
                    TcpInterfaceAddress.Items.Add(ip.ToString());
                    UdpInterfaceAddress.Items.Add(ip.ToString());
                }

                if (all.Length != 0 || !string.IsNullOrEmpty(defaultInterfaceAddr))
                {
                    foreach (IPAddress ip in all)
                    {
                        if (defaultInterfaceAddr.Equals(ip.ToString()))
                        {
                            TcpInterfaceAddress.SelectedItem = defaultInterfaceAddr;
                            UdpInterfaceAddress.SelectedItem = defaultInterfaceAddr;
                        }
                    }
                }
                if (TcpInterfaceAddress.SelectedItem == null && TcpInterfaceAddress.Items.Count > 0)
                    TcpInterfaceAddress.SelectedItem = TcpInterfaceAddress.Items[0];
                if (UdpInterfaceAddress.SelectedItem == null && UdpInterfaceAddress.Items.Count > 0)
                    UdpInterfaceAddress.SelectedItem = UdpInterfaceAddress.Items[0];
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn(e);
            }
        }

        private void AddTcpConnection(object sender, System.EventArgs e)
        {
            string newRdv = "tcp://" + TcpAddress.Text + ":" + TcpPort.Text;
            TCPConnectionsList.Items.Add(newRdv);
        }

        private void RemoveTcpConnection(object sender, System.EventArgs e)
        {
            ListBox.SelectedIndexCollection list = TCPConnectionsList.SelectedIndices;
            foreach (int pos in list)
            {
                TCPConnectionsList.Items.RemoveAt(pos);
            }
        }

        private void AddUdpConnection(object sender, EventArgs e)
        {
            string newRdv = "udp://" + UdpAddress.Text + ":" + UdpPort.Text;
            UDPConnectionsList.Items.Add(newRdv);
        }

        private void RemoveUdpConnection(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection list = UDPConnectionsList.SelectedIndices;
            foreach (int pos in list)
            {
                UDPConnectionsList.Items.RemoveAt(pos);
            }
        }
    }
}
