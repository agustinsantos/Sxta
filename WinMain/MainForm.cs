namespace Sxta.Rti1516.WinMain
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    
    using WeifenLuo.WinFormsUI.Docking;

    using Sxta.Rti1516.WinMain.Customization;

    // Import log4net classes.
    using log4net;

    using Nini.Config;

    using Hla.Rti1516;
    using Sxta.Rti1516.Time;
    using Sxta.Rti1516.BaseApplication;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.BoostrapProtocol;
    using BaseInteractionMessage = Sxta.Rti1516.Interactions.BaseInteractionMessage;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Core.Plugins;
    using PluginSimulator = Sxta.Plugins.PluginSimulator;

    public partial class MainForm : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private OMTPropertyWindow m_propertyWindow = new OMTPropertyWindow();
        private ObjectModelExplorer m_objectModelExplorer;
        private DummyToolbox m_toolbox = new DummyToolbox();
        private OutputWindow m_outputWindow = new OutputWindow();
        private PluginList m_pluginList = new PluginList();
        private Sxta.GUIUtils.ListBoxTraceListener listBoxTrace;
        private ChannelsManager channelManager = new ChannelsManager();
        private InteractionManager protocolMngr;

        private XrtiExecutiveAmbassador rtiAmbassador;

        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private MainApplication app;
        private string peerName, peerDescription;
        private List<System.Diagnostics.Process> processList = new List<System.Diagnostics.Process>();
        private int countProcess = 1;

        public MainForm(string[] args)
        {
            InitializeComponent();

            app = new MainApplication(args);
            IConfig cfg = app.ConfigSource.Configs["PeerInfo"];
            if (cfg != null)
            {
                peerName = cfg.Get("PeerName", "NoNamePeer");
                peerDescription = cfg.Get("PeerDescription", "A generic peer description");
            }
            this.Text = "Main Application for " + peerName + " : " + peerDescription;
            OnTraceOn(null, null);
            // Log an info level message
            if (log.IsDebugEnabled)
            {
                System.Threading.Thread.CurrentThread.Name = "Main(" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")";
                log.Debug("Tests Server Start");
            }
            // Write output to the file and to the console screen.
            if (log.IsInfoEnabled)
                log.Info("Welcome to the Sxta Test Kit !");

            if (rtiAmbassador == null)
                rtiAmbassador = new XrtiExecutiveAmbassador(app.ConfigSource);
            OnLoadPlugins();

            showRightToLeft.Checked = (RightToLeft == RightToLeft.Yes);
            RightToLeftLayout = showRightToLeft.Checked;
            m_objectModelExplorer = new ObjectModelExplorer(rtiAmbassador.descriptorManager);
            m_objectModelExplorer.PropertyGrid = m_propertyWindow;
            m_objectModelExplorer.RightToLeftLayout = RightToLeftLayout;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemSolutionExplorer_Click(object sender, System.EventArgs e)
        {
            m_objectModelExplorer.Show(dockPanel);
        }

        private void menuItemPropertyWindow_Click(object sender, System.EventArgs e)
        {
            m_propertyWindow.Show(dockPanel);
        }

        private void menuItemToolbox_Click(object sender, System.EventArgs e)
        {
            m_toolbox.Show(dockPanel);
        }

        private void menuItemOutputWindow_Click(object sender, System.EventArgs e)
        {
            m_outputWindow.Show(dockPanel);
        }

        private void menuItemPluginList_Click(object sender, System.EventArgs e)
        {
            m_pluginList.Show(dockPanel);
        }

        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog(this);
        }

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }

        private void menuItemNew_Click(object sender, System.EventArgs e)
        {
            ResourceDoc dummyDoc = CreateNewDocument();
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(dockPanel);
        }

        private ResourceDoc CreateNewDocument()
        {
            ResourceDoc dummyDoc = new ResourceDoc();

            int count = 1;
            //string text = "C:\\MADFDKAJ\\ADAKFJASD\\ADFKDSAKFJASD\\ASDFKASDFJASDF\\ASDFIJADSFJ\\ASDFKDFDA" + count.ToString();
            string text = "Document" + count.ToString();
            while (FindDocument(text) != null)
            {
                count++;
                //text = "C:\\MADFDKAJ\\ADAKFJASD\\ADFKDSAKFJASD\\ASDFKASDFJASDF\\ASDFIJADSFJ\\ASDFKDFDA" + count.ToString();
                text = "Document" + count.ToString();
            }
            dummyDoc.Text = text;
            return dummyDoc;
        }

        private ResourceDoc CreateNewDocument(string text)
        {
            ResourceDoc dummyDoc = new ResourceDoc();
            dummyDoc.Text = text;
            return dummyDoc;
        }

        private void menuItemOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter = "Xml files (*.xml)|*.xml|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                string fileName = Path.GetFileName(fullName);

                if (FindDocument(fileName) != null)
                {
                    MessageBox.Show("The document: " + fileName + " has already opened!");
                    return;
                }

                ResourceDoc dummyDoc = new ResourceDoc();
                dummyDoc.Text = fileName;
                dummyDoc.Show(dockPanel);
                try
                {
                    dummyDoc.FileName = fullName;
                }
                catch (Exception exception)
                {
                    dummyDoc.Close();
                    MessageBox.Show(exception.Message);
                }

            }
        }

        private void menuItemFile_Popup(object sender, System.EventArgs e)
        {

            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                //menuItemClose.Enabled = menuItemCloseAll.Enabled = (ActiveMdiChild != null);
            }
            else
            {
                //menuItemClose.Enabled = (dockPanel.ActiveDocument != null);
                menuItemCloseAll.Enabled = (countProcess > 1);
            }

        }

        private void menuItemClose_Click(object sender, System.EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                ActiveMdiChild.Close();
            else if (dockPanel.ActiveDocument != null)
                dockPanel.ActiveDocument.DockHandler.Close();
        }

        private void menuItemCloseAll_Click(object sender, System.EventArgs e)
        {
            CloseAllDocuments();
        }

        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                    content.DockHandler.Close();
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ObjectModelExplorer).ToString())
                return m_objectModelExplorer;
            else if (persistString == typeof(OMTPropertyWindow).ToString())
                return m_propertyWindow;
            else if (persistString == typeof(DummyToolbox).ToString())
                return m_toolbox;
            else if (persistString == typeof(OutputWindow).ToString())
                return m_outputWindow;
            else if (persistString == typeof(PluginList).ToString())
                return m_pluginList;
            else
            {
                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(ResourceDoc).ToString())
                    return null;

                ResourceDoc dummyDoc = new ResourceDoc();
                if (parsedStrings[1] != string.Empty)
                    dummyDoc.FileName = parsedStrings[1];
                if (parsedStrings[2] != string.Empty)
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private void menuItemToolBar_Click(object sender, System.EventArgs e)
        {
            toolBar.Visible = menuItemToolBar.Checked = !menuItemToolBar.Checked;
        }

        private void menuItemStatusBar_Click(object sender, System.EventArgs e)
        {
            statusBar.Visible = menuItemStatusBar.Checked = !menuItemStatusBar.Checked;
        }

        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolBarButtonNew)
                menuItemNew_Click(null, null);
            else if (e.ClickedItem == toolBarButtonOpen)
                menuItemOpen_Click(null, null);
            else if (e.ClickedItem == toolBarButtonSolutionExplorer)
                menuItemSolutionExplorer_Click(null, null);
            else if (e.ClickedItem == toolBarButtonPropertyWindow)
                menuItemPropertyWindow_Click(null, null);
            else if (e.ClickedItem == toolBarButtonToolbox)
                menuItemToolbox_Click(null, null);
            else if (e.ClickedItem == toolBarButtonOutputWindow)
                menuItemOutputWindow_Click(null, null);
            else if (e.ClickedItem == toolBarButtonTaskList)
                menuItemPluginList_Click(null, null);
            else if (e.ClickedItem == toolBarButtonLayoutByCode)
                menuItemLayoutByCode_Click(null, null);
            else if (e.ClickedItem == toolBarButtonLayoutByXml)
                menuItemLayoutByXml_Click(null, null);
        }

        private void OnStartCommunications(object sender, System.EventArgs e)
        {
            if (!channelManager.IsClosed)
            {
                if (log.IsWarnEnabled)
                    log.Warn("Server is already started.");
                return;
            }
            if (channelManager.IsClosed)
            {
                if (protocolMngr == null)
                {
                    protocolMngr = new InteractionManager(new DescriptorManager(), channelManager);
                    protocolMngr.PeerName = peerName;
                    protocolMngr.PeerDescription = peerDescription;
                }
                channelManager.Start();
            }

            if (app.IsListener)
            {
                foreach (ConnectionInfo listener in app.GetTCPListenerInfoList())
                {
                    channelManager.StartNewListener(listener);
                }

                foreach (ConnectionInfo channel in app.UDPChannelsInfoList)
                {
                    channelManager.StartNewUDPLocalChannel(channel);
                }
            }
            foreach (string uri in app.ReliableConnectionList)
            {
                try
                {
                    channelManager.StartNewConnection(new Uri(uri));
                }
                catch (Exception ex)
                {
                    if (log.IsErrorEnabled)
                        log.Error("Error connecting " + uri + ". Exception: " + ex.Message);
                }
            }
            foreach (string uri in app.BestEffortConnectionList)
            {
                try
                {
                    channelManager.StartNewConnection(new Uri(uri));
                }
                catch (Exception ex)
                {
                    if (log.IsErrorEnabled)
                        log.Error("Error connecting " + uri + ". Exception: " + ex.Message);
                }
            }
        }

        private void menuItemTools_Popup(object sender, System.EventArgs e)
        {
            menuItemLockLayout.Checked = !this.dockPanel.AllowEndUserDocking;
        }

        private void menuItemLockLayout_Click(object sender, System.EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }

        private void menuItemLayoutByCode_Click(object sender, System.EventArgs e)
        {
            dockPanel.SuspendLayout(true);

            m_objectModelExplorer.Show(dockPanel, DockState.DockRight);
            m_propertyWindow.Show(m_objectModelExplorer.Pane, m_objectModelExplorer);
            m_toolbox.Show(dockPanel, new Rectangle(98, 133, 200, 383));
            m_outputWindow.Show(m_objectModelExplorer.Pane, DockAlignment.Bottom, 0.35);
            m_pluginList.Show(m_toolbox.Pane, DockAlignment.Left, 0.4);

            CloseAllDocuments();
            ResourceDoc doc1 = CreateNewDocument("Document1");
            ResourceDoc doc2 = CreateNewDocument("Document2");
            ResourceDoc doc3 = CreateNewDocument("Document3");
            ResourceDoc doc4 = CreateNewDocument("Document4");
            doc1.Show(dockPanel, DockState.Document);
            doc2.Show(doc1.Pane, null);
            doc3.Show(doc1.Pane, DockAlignment.Bottom, 0.5);
            doc4.Show(doc3.Pane, DockAlignment.Right, 0.5);

            dockPanel.ResumeLayout(true, true);
        }

        private void menuItemLayoutByXml_Click(object sender, System.EventArgs e)
        {
            dockPanel.SuspendLayout(true);

            // In order to load layout from XML, we need to close all the DockContents
            CloseAllContents();

            Assembly assembly = Assembly.GetAssembly(typeof(MainForm));
            Stream xmlStream = assembly.GetManifestResourceStream("Sxta.Rti1516.WinMain.Resources.DockPanel.xml");
            dockPanel.LoadFromXml(xmlStream, m_deserializeDockContent);
            xmlStream.Close();

            dockPanel.ResumeLayout(true, true);
        }

        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_objectModelExplorer.DockPanel = null;
            m_propertyWindow.DockPanel = null;
            m_toolbox.DockPanel = null;
            m_outputWindow.DockPanel = null;
            m_pluginList.DockPanel = null;

            // Close all other document windows
            CloseAllDocuments();
        }

        private void SetSchema(object sender, System.EventArgs e)
        {
            CloseAllContents();

            if (sender == menuItemSchemaVS2005)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2005);
            else if (sender == menuItemSchemaVS2003)
                Extender.SetSchema(dockPanel, Extender.Schema.VS2003);

            menuItemSchemaVS2005.Checked = (sender == menuItemSchemaVS2005);
            menuItemSchemaVS2003.Checked = (sender == menuItemSchemaVS2003);
        }

        private void SetDocumentStyle(object sender, System.EventArgs e)
        {
            DocumentStyle oldStyle = dockPanel.DocumentStyle;
            DocumentStyle newStyle;
            if (sender == menuItemDockingMdi)
                newStyle = DocumentStyle.DockingMdi;
            else if (sender == menuItemDockingWindow)
                newStyle = DocumentStyle.DockingWindow;
            else if (sender == menuItemDockingSdi)
                newStyle = DocumentStyle.DockingSdi;
            else
                newStyle = DocumentStyle.SystemMdi;

            if (oldStyle == newStyle)
                return;

            if (oldStyle == DocumentStyle.SystemMdi || newStyle == DocumentStyle.SystemMdi)
                CloseAllDocuments();

            dockPanel.DocumentStyle = newStyle;
            menuItemDockingMdi.Checked = (newStyle == DocumentStyle.DockingMdi);
            menuItemDockingWindow.Checked = (newStyle == DocumentStyle.DockingWindow);
            menuItemDockingSdi.Checked = (newStyle == DocumentStyle.DockingSdi);
            menuItemSystemMdi.Checked = (newStyle == DocumentStyle.SystemMdi);
            menuItemLayoutByCode.Enabled = (newStyle != DocumentStyle.SystemMdi);
            menuItemLayoutByXml.Enabled = (newStyle != DocumentStyle.SystemMdi);
            toolBarButtonLayoutByCode.Enabled = (newStyle != DocumentStyle.SystemMdi);
            toolBarButtonLayoutByXml.Enabled = (newStyle != DocumentStyle.SystemMdi);
        }

        private void menuItemCloseAllButThisOne_Click(object sender, System.EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.Documents)
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void menuItemShowDocumentIcon_Click(object sender, System.EventArgs e)
        {
            dockPanel.ShowDocumentIcon = menuItemShowDocumentIcon.Checked = !menuItemShowDocumentIcon.Checked;
        }

        private void showRightToLeft_Click(object sender, EventArgs e)
        {
            CloseAllContents();
            if (showRightToLeft.Checked)
            {
                this.RightToLeft = RightToLeft.No;
                this.RightToLeftLayout = false;
            }
            else
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            m_objectModelExplorer.RightToLeftLayout = this.RightToLeftLayout;
            showRightToLeft.Checked = !showRightToLeft.Checked;
        }

        private void exitWithoutSavingLayout_Click(object sender, EventArgs e)
        {
            m_bSaveLayout = false;
            Close();
            m_bSaveLayout = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (rtiAmbassador != null)
            {
                rtiAmbassador.Stop();
                rtiAmbassador = null;
            }
            if (channelManager != null)
                channelManager.Close();
            OnStopPrograms(null, null);
        }

        // --------------------------- TRACE ACTIONS -----------------------------------
        private void OnTraceOn(object sender, EventArgs e)
        {
            if (listBoxTrace == null)
            {
                // Create a text writer that writes to the console screen, and add
                // it to the trace listeners
                listBoxTrace = new Sxta.GUIUtils.ListBoxTraceListener(this.m_outputWindow.OutputArea);

                System.Diagnostics.Trace.Listeners.Add(listBoxTrace);
            }
        }
        private void OnTraceOff(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled)
                log.Info("OnTraceOff");
            if (listBoxTrace != null)
                System.Diagnostics.Trace.Listeners.Remove(listBoxTrace);
            listBoxTrace = null;

        }

        private void OnTraceClear(object sender, EventArgs e)
        {
            if (listBoxTrace != null)
                listBoxTrace.Clear();
        }

        // --------------------------- COMUNICATIONS ACTIONS -----------------------------------
        private void OnCloseCommunications(object sender, EventArgs e)
        {
            if (channelManager == null || !channelManager.IsClosed)
            {
                channelManager.Close();
            }
            else
                if (log.IsWarnEnabled)
                    log.Warn("Communications are not started");
        }

        private void OnDumpCommsInfo(object sender, EventArgs e)
        {
            if (channelManager != null)
            {
                channelManager.DumpChannelsInfo();
            }
            else
                if (log.IsWarnEnabled)
                    log.Warn("Communications are not started");
        }

        private void OnSendTCPMessage(object sender, EventArgs e)
        {
            if (channelManager != null && !channelManager.IsClosed)
            {
                BaseInteractionMessage msg = new BaseInteractionMessage();
                msg.InteractionClassHandle = protocolMngr.SerializerManager.GetHandle(msg.GetType());
                protocolMngr.SendRealiableInteraction(ChannelType.TCP, msg);
            }
            else
                if (log.IsWarnEnabled)
                    log.Warn("Communications are not started");
        }

        private void OnSendUDPMessage(object sender, EventArgs e)
        {
            if (channelManager != null && !channelManager.IsClosed)
            {
                BaseInteractionMessage msg = new BaseInteractionMessage();
                msg.InteractionClassHandle = protocolMngr.SerializerManager.GetHandle(msg.GetType());
                protocolMngr.SendBestEffortInteraction(ChannelType.UDP, msg);
            }
            else
                if (log.IsWarnEnabled)
                    log.Warn("Communications are not started");
        }

        // --------------------------- PROGRAM ACTIONS -----------------------------------
        private void OnStartNewProgram(object sender, EventArgs e)
        {
            try
            {
                string parameters = @"-config=""SxtaConfig" + countProcess + @".xml"" -dialog=false";
                string processName = Application.ExecutablePath;
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(processName, parameters);
                processList.Add(process);
                countProcess += 1;
            }
            catch (Exception ex)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error creating channelManager: " + ex);
            }
        }

        private void OnStopPrograms(object sender, EventArgs e)
        {
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process != null && !process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
            processList.Clear();
            countProcess = 1;
        }

        private void OpenFileToCompile(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                try
                {
                    OpenFile(fullName, null);
                    DescriptorManager descriptorManager = new DescriptorManager();
                    descriptorManager.AddDescriptors(fullName);
                    Sxta.Rti1516.DynamicCompiler.DynamicCompiler compiler = new Sxta.Rti1516.DynamicCompiler.DynamicCompiler(descriptorManager);
                    compiler.TargetDirectory = new FileInfo("Proxys");
                    compiler.CompileProxies();
                    if (log.IsInfoEnabled)
                    {
                        log.Info("Compilation of " + fullName + " finished ...");
                        log.Info("Look for generated files in " + compiler.TargetDirectory.FullName);
                    }
                    string fileName = compiler.TargetDirectory.FullName + Path.DirectorySeparatorChar + "generated.cs";
                    OpenFile(fileName, null);
                    fileName = compiler.TargetDirectory.FullName + Path.DirectorySeparatorChar + "Serializers.cs";
                    OpenFile(fileName, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }
        }

        private void OpenFile(string fileName, string useTextLabel)
        {
            if (FindDocument(fileName) != null)
            {
                MessageBox.Show("The document: " + fileName + " has already opened!");
                return;
            }

            ResourceDoc dummyDoc = new ResourceDoc();
            if (string.IsNullOrEmpty(useTextLabel))
                dummyDoc.Text = Path.GetFileName(fileName);
            else
                dummyDoc.Text = useTextLabel;

            dummyDoc.Show(dockPanel);
            try
            {
                dummyDoc.FileName = fileName;
            }
            catch (Exception exception)
            {
                dummyDoc.Close();
                MessageBox.Show(exception.Message);
            }


        }

        String federationName = "HelloWorld";
        XrtiFederateAmbassador sampleFederateAmbassador;
        MobileFederateServices mobileServices;

        private void OnCreateFederationHelloWorld(object sender, EventArgs e)
        {
            //Assembly assembly = Assembly.LoadFile(new FileInfo("Samples.dll").FullName);
            /*
            Object ob = assembly.CreateInstance("Sxta.Rti1516.Samples.PluginSimulator");

            if (ob is IService)
            {
                (ob as IService).Register(rtiAmbassador);
            }
            */

            //Build a Uri using a file path
            FileInfo file = new FileInfo(Sxta.Rti1516ResourcesNames.HelloWorldObjectModel);
            Uri fileUri = new Uri("file://" + file.FullName);
            try
            {
                //Create Federation
                rtiAmbassador.CreateFederationExecution(federationName, fileUri);

                JoinFederationMenuItem.Enabled = true;
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
                MessageBox.Show(exception.Message);
            }
        }

        private void OnJoinFederationHelloWorld(object sender, EventArgs e)
        {
            try
            {
                // Join to federation
                sampleFederateAmbassador = new XrtiFederateAmbassador(rtiAmbassador);
                mobileServices = new MobileFederateServices(new DoubleValuedLogicalTimeFactory(), new DoubleValuedLogicalTimeIntervalFactory());
                IFederateHandle federateHandle = rtiAmbassador.JoinFederationExecution(peerName, federationName, sampleFederateAmbassador, mobileServices);

                EvokeCallbackMenuItem.Enabled = true;
                EvokeCallbacksMenuItem.Enabled = true;
                SetTimeConstrainedMenuItem.Enabled = true;
                SetTimeRegulatingMenuItem.Enabled = true;
                TimeAdvanceRequestMinMenuItem.Enabled = true;
                TimeAdvanceRequestMenuItem.Enabled = true;
                Example1MenuItem.Enabled = true;
                Example2MenuItem.Enabled = true;
                Example3MenuItem.Enabled = true;
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
                MessageBox.Show(exception.Message);
            }
        }

        private void OnEvokeCallback(object sender, EventArgs e)
        {
            try
            {
                rtiAmbassador.EvokeCallback(0.5);

                OnDumpObjects(null, null);
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
                MessageBox.Show(exception.Message);
            }
        }

        private void OnEvokeCallbacks(object sender, EventArgs e)
        {
            try
            {
                rtiAmbassador.EvokeMultipleCallbacks(0.5, 1.0);

                OnDumpObjects(null, null);
            }
            catch (Exception exception)
            {
                if (log.IsErrorEnabled)
                    log.Error(exception.Message);
                MessageBox.Show(exception.Message);
            }
        }

        private void OnSetTimeRegulatingQuick(object sender, EventArgs e)
        {
            OnCreateFederationHelloWorld(null, null);
            OnJoinFederationHelloWorld(null, null);
            SetTimeRegulating(null, null);
        }

        private void OnSetTimeConstrainedQuick(object sender, EventArgs e)
        {
            OnCreateFederationHelloWorld(null, null);
            OnJoinFederationHelloWorld(null, null);
            SetTimeConstrained(null, null);
        }

        private void SetTimeRegulating(object sender, EventArgs e)
        {
            ILogicalTimeInterval lookahead = new DoubleValuedLogicalTimeInterval(1.0);
            ILogicalTimeIntervalFactory factory = ((XrtiExecutiveAmbassador)rtiAmbassador).LogicalTimeIntervalFactory;
            rtiAmbassador.EnableTimeRegulation(lookahead);

            OnDumpObjects(null, null);
        }

        private void SetTimeConstrained(object sender, EventArgs e)
        {
            rtiAmbassador.EnableTimeConstrained();

            OnDumpObjects(null, null);
        }

        private void TimeAdvancedRequest(object sender, EventArgs e)
        {
            ILogicalTimeInterval lookahead = new DoubleValuedLogicalTimeInterval(1.0);
            ILogicalTimeIntervalFactory factory = ((XrtiExecutiveAmbassador)rtiAmbassador).LogicalTimeIntervalFactory;
            ILogicalTime time = rtiAmbassador.Federate.HLAlogicalTime.Add(lookahead);
            rtiAmbassador.TimeAdvanceRequest(time);

            OnDumpObjects(null, null);
        }


        private void OnTimeAdvancedRequestMini(object sender, EventArgs e)
        {
            ILogicalTimeInterval lookahead = new DoubleValuedLogicalTimeInterval(0.1);
            ILogicalTimeIntervalFactory factory = ((XrtiExecutiveAmbassador)rtiAmbassador).LogicalTimeIntervalFactory;
            ILogicalTime time = rtiAmbassador.Federate.HLAlogicalTime.Add(lookahead);
            rtiAmbassador.TimeAdvanceRequest(time);

            OnDumpObjects(null, null);
        }

        private Sxta.Rti1516.Samples.Country aNewCountry;

        private void createCountry()
        {
            if (aNewCountry == null)
            {
                aNewCountry = Sxta.Rti1516.Samples.Country.NewCountry();

                aNewCountry.Name = "España";
                aNewCountry.Population = 40000000;
                aNewCountry.Position = new Sxta.Rti1516.Samples.Vector3FloatStruct(50, 100, 30);
            }
        }

        private void OnRunExampleCountryUpdatePopulation(object sender, EventArgs e)
        {
            createCountry();

            for (int i = 0; i < 5; i++)
            {
                aNewCountry.Population *= 1.05;
                System.Threading.Thread.Sleep(1000);
            }

            OnDumpObjects(null, null);
        }

        private void OnRunExampleCountryUpdatePosition(object sender, EventArgs e)
        {
            createCountry();

            ILogicalTime currentTime;
            ILogicalTimeInterval timeStep;
            ILogicalTime nextTime;

            for (int i = 0; i < 5; i++)
            {
                currentTime = rtiAmbassador.QueryLogicalTime();
                timeStep = rtiAmbassador.Federate.HLAlookahead;
                nextTime = currentTime.Add(timeStep);

                rtiAmbassador.TimeAdvanceRequest(nextTime);
                do
                {
                    rtiAmbassador.EvokeMultipleCallbacks(0.5, 1.0);
                    currentTime = rtiAmbassador.QueryLogicalTime();
                }
                while (!currentTime.Equals(nextTime));

                aNewCountry.Position = new Sxta.Rti1516.Samples.Vector3FloatStruct(
                    aNewCountry.Position.XComponent + 400 * i, aNewCountry.Position.YComponent + 200 * i, aNewCountry.Position.ZComponent + i / 3);

                System.Threading.Thread.Sleep(1000);
            }

            OnDumpObjects(null, null);
        }

        private void OnCreateACountry(object sender, EventArgs e)
        {
            Sxta.Rti1516.Samples.Country aNewCountry = Sxta.Rti1516.Samples.Country.NewCountry();

            aNewCountry.Name = "Rusia";
            aNewCountry.Population = 300000000;
            aNewCountry.Position = new Sxta.Rti1516.Samples.Vector3FloatStruct((float)400, (float)23.4, (float)234);

            OnDumpObjects(null, null);
        }

        private void OnRunExampleBoidsSimple(object sender, EventArgs e)
        {
            //Sxta.Rti1516.Samples.Vector3FloatStruct floorCenter = new Sxta.Rti1516.Samples.Vector3FloatStruct();
            /*
                        Random ran = new Random();
                        int numberBoids = ran.Next(1, 5);
                        Sxta.Rti1516.Samples.Boid[] myBoids = new Sxta.Rti1516.Samples.Boid[numberBoids];

                        for (int i = 0; i < numberBoids; i++)
                            myBoids[i] = Sxta.Rti1516.Samples.Boid.NewBoid();

                        for (int time = 0; time < 20; time++)
                        {
                            for (int i = 0; i < numberBoids; i++)
                                myBoids[i].UpdateEntity(20f);

                            //app.EvokeMultipleCallbacks();
                            //DumpObjects();
                            System.Threading.Thread.Sleep(2000);
                        }
                    */
        }

        private void OnRunExampleBoidsComplex(object sender, EventArgs e)
        {
            //Sxta.Rti1516.Samples.Vector3FloatStruct floorCenter = new Sxta.Rti1516.Samples.Vector3FloatStruct();
            /*
                        Random ran = new Random();
                        int numberBoids = ran.Next(1, 5);
                        Sxta.Rti1516.Samples.Boid[] myBoids = new Sxta.Rti1516.Samples.Boid[numberBoids];

                        for (int i = 0; i < numberBoids; i++)
                        {
                            myBoids[i] = Sxta.Rti1516.Samples.Boid.NewBoid();
                            myBoids[i].Position = new Sxta.Rti1516.Samples.Vector3FloatStruct((float)ran.Next(0, 100), (float)ran.Next(0, 100), (float)ran.Next(0, 100));
                            myBoids[i].Velocity = new Sxta.Rti1516.Samples.Vector3FloatStruct((float)ran.Next(0, 100), (float)ran.Next(0, 100), (float)ran.Next(0, 100));
                        }

                        for (int time = 0; time < 10; time++)
                        {
                            for (int i = 0; i < numberBoids; i++)
                                myBoids[i].UpdateEntity((float)ran.Next(0, 50));

                            //app.EvokeMultipleCallbacks();
                            //DumpObjects();
                            System.Threading.Thread.Sleep(2000);
                        }
                    */
        }

        private void FlushAttributeValues(object sender, EventArgs e)
        {
            Sxta.Rti1516.Samples.Country aCountry = Sxta.Rti1516.Samples.Country.NewCountry();
            aCountry.AutoFlushDisabled = true;

            aCountry.Name = "Croacia";
            aCountry.Population = 10000000;
            aCountry.Position = new Sxta.Rti1516.Samples.Vector3FloatStruct(90, 30, 23);

            System.Threading.Thread.Sleep(5000);

            aCountry.FlushAttributeValues(new byte[1]);
        }

        private void OnDumpObjects(object sender, EventArgs e)
        {
            this.m_objectModelExplorer.ShowInformationFromRtiAmbassador((MetaFederateAmbassador)rtiAmbassador.MetaFederateAmbassador,
                                                                        rtiAmbassador);
        }

        private void SendPublishObjectClassAttributesInteraction(object sender, EventArgs e)
        {
            Sxta.Rti1516.Management.HLApublishObjectClassAttributesMessage msg = new Sxta.Rti1516.Management.HLApublishObjectClassAttributesMessage();
            msg.HLAobjectClass = new Sxta.Rti1516.XrtiHandles.XRTIObjectClassHandle(99999999999);

            Sxta.Rti1516.Management.HLAfederateHandle HLAfederate = new Sxta.Rti1516.Management.HLAfederateHandle();
            HLAfederate.data = 333333;

            msg.HLAfederate = HLAfederate;

            msg.FederationExecutionHandle = -1;
            msg.UserSuppliedTag = new byte[1];

            AttributeHandleSetFactory factory = new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandleSetFactory();
            msg.HLAattributeList = factory.Create();

            msg.HLAattributeList.Add(new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandle(123456789));
            msg.HLAattributeList.Add(new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandle(987654321));

            rtiAmbassador.interactionManager.SendInteraction(msg);

            if (log.IsInfoEnabled)
                log.Info("Sent " + msg);
        }

        private void SendPublishInteractionClassInteraction(object sender, EventArgs e)
        {
            Sxta.Rti1516.Management.HLApublishInteractionClassMessage msg = new Sxta.Rti1516.Management.HLApublishInteractionClassMessage();
            msg.HLAinteractionClass = new Sxta.Rti1516.XrtiHandles.XRTIInteractionClassHandle(99999999999);

            Sxta.Rti1516.Management.HLAfederateHandle HLAfederate = new Sxta.Rti1516.Management.HLAfederateHandle();
            HLAfederate.data = 333333;

            msg.HLAfederate = HLAfederate;

            msg.FederationExecutionHandle = -1;
            msg.UserSuppliedTag = new byte[1];

            rtiAmbassador.interactionManager.SendInteraction(msg);

            if (log.IsInfoEnabled)
                log.Info("Sent " + msg);
        }

        private void SendSubscribeObjectClassAttributesInteraction(object sender, EventArgs e)
        {
            Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage msg = new Sxta.Rti1516.Management.HLAsubscribeObjectClassAttributesMessage();
            msg.HLAobjectClass = new Sxta.Rti1516.XrtiHandles.XRTIObjectClassHandle(99999999999);

            Sxta.Rti1516.Management.HLAfederateHandle HLAfederate = new Sxta.Rti1516.Management.HLAfederateHandle();
            HLAfederate.data = 333333;

            msg.HLAfederate = HLAfederate;

            msg.FederationExecutionHandle = -1;
            msg.UserSuppliedTag = new byte[1];

            AttributeHandleSetFactory factory = new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandleSetFactory();
            msg.HLAattributeList = factory.Create();

            msg.HLAattributeList.Add(new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandle(123456789));
            msg.HLAattributeList.Add(new Sxta.Rti1516.XrtiHandles.XRTIAttributeHandle(987654321));

            rtiAmbassador.interactionManager.SendInteraction(msg);

            if (log.IsInfoEnabled)
                log.Info("Sent " + msg);
        }

        private void SendSubscribeInteractionClassInteraction(object sender, EventArgs e)
        {
            Sxta.Rti1516.Management.HLAsubscribeInteractionClassMessage msg = new Sxta.Rti1516.Management.HLAsubscribeInteractionClassMessage();
            msg.HLAinteractionClass = new Sxta.Rti1516.XrtiHandles.XRTIInteractionClassHandle(99999999999);

            Sxta.Rti1516.Management.HLAfederateHandle HLAfederate = new Sxta.Rti1516.Management.HLAfederateHandle();
            HLAfederate.data = 333333;

            msg.HLAfederate = HLAfederate;

            msg.FederationExecutionHandle = -1;
            msg.UserSuppliedTag = new byte[1];

            rtiAmbassador.interactionManager.SendInteraction(msg);

            if (log.IsInfoEnabled)
                log.Info("Sent " + msg);
        }

        private void OnLoadPlugins()
        {
            PluginManager pluginManager = PluginManager.Instance;
            pluginManager.ScanForPlugins();
            m_pluginList.ShowPlugins(pluginManager.Plugins);
            foreach (IPlugin plugin in pluginManager.Plugins)
            {
                foreach (Extension extension in plugin.Extensions)
                {
                    foreach (IModule module in extension.ModulesCollection)
                    {
                        if (module is PluginSimulator)
                        {
                            PluginSimulator simulationModule = module as PluginSimulator;
                            simulationModule.Plugin = plugin;
                            //CompilePlugin(simulationModule);
                            foreach (Assembly asm in plugin.RuntimeLibraries.Values)
                            {
                                rtiAmbassador.RegisterAssembly(asm);
                            }

                            ToolStripMenuItem moduleToolStripMenuItem = new ToolStripMenuItem(); ;
                            moduleToolStripMenuItem.Name = module.ID + "ToolStripMenuItem";
                            moduleToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
                            moduleToolStripMenuItem.Text = module.ID;
                            moduleToolStripMenuItem.Click += new System.EventHandler(this.OnRunSimulation);
                            moduleToolStripMenuItem.Tag = simulationModule;
                            this.simulationToolStripMenuItem.DropDownItems.Add(moduleToolStripMenuItem);
                        }
                    }
                }
            }

        }

        private void CompilePlugin(PluginSimulator simulationModule)
        {
            string fullName = simulationModule.FomList[0].Uri;
            try
            {
                OpenFile(fullName, null);
                DescriptorManager descriptorManager = new DescriptorManager();
                descriptorManager.AddDescriptors(fullName);
                Sxta.Rti1516.DynamicCompiler.DynamicCompiler compiler = new Sxta.Rti1516.DynamicCompiler.DynamicCompiler(descriptorManager);
                compiler.TargetDirectory = new FileInfo("Proxys");
                foreach (Assembly asm in simulationModule.Plugin.RuntimeLibraries.Values)
                {
                    compiler.AssemblyDependencies.Add(asm);
                }
                compiler.PackageDependencies.Add("Mogre");
                compiler.PackagePrefix = simulationModule.Plugin.Name;
                compiler.CompileProxies();
                if (log.IsInfoEnabled)
                {
                    log.Info("Compilation of " + fullName + " finished ...");
                    log.Info("Look for generated files in " + compiler.TargetDirectory.FullName);
                }
                string fileName = compiler.TargetDirectory.FullName + Path.DirectorySeparatorChar + "generated.cs";
                OpenFile(fileName, simulationModule.Plugin.Name + Path.DirectorySeparatorChar + "generated.cs");
                fileName = compiler.generatedSerializersFile;
                OpenFile(fileName, simulationModule.Plugin.Name + Path.DirectorySeparatorChar + "serializers.cs");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void OnRunSimulation(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;
            ToolStripMenuItem module = sender as ToolStripMenuItem;
            PluginSimulator sim = module.Tag as PluginSimulator;
            sim.StartSimulation(rtiAmbassador);
        }

        private void InferSchemaFromXML(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                try
                {
                    XmlReader reader = XmlReader.Create(fullName);
                    XmlSchemaSet schemaSet = new XmlSchemaSet();
                    XmlSchemaInference inference = new XmlSchemaInference();
                    schemaSet = inference.InferSchema(reader);
                    // Display the inferred schema.
                    using (FileStream stream = new FileStream("Schema.xsd", FileMode.OpenOrCreate))
                    {
                        foreach (XmlSchema schema in schemaSet.Schemas())
                        {
                            schema.Write(stream);
                        }
                    }
                    OpenFile("Schema.xsd", null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }
        }
    }
}