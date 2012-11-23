using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using Sxta.Core.Plugins;

namespace Sxta.Rti1516.WinMain
{
    public partial class PluginList : ToolWindow
    {
        public PluginList()
        {
            InitializeComponent();
        }

        public void ShowPlugins(PluginCollection plugins)
        {
            foreach (IPlugin plugin in plugins)
            {
                foreach (Extension extension in plugin.Extensions)
                {
                    foreach (IModule module in extension.ModulesCollection)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { plugin.Name, plugin.Description, plugin.Version, module.ID, module.Class }));
                    }
                }
            }
        }
    }
}