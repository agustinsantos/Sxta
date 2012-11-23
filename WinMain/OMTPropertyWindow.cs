using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sxta.Rti1516.WinMain
{
    public partial class OMTPropertyWindow : ToolWindow
    {
        public OMTPropertyWindow()
        {
            InitializeComponent();
			comboBox.SelectedIndex = 0;
			propertyGrid.SelectedObject = propertyGrid;
        }

        public PropertyGrid InternalPropertyGrid
        {
            get { return propertyGrid; }
        }
    }
}