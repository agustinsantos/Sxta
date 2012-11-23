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
    public partial class OutputWindow : ToolWindow
    {
        public OutputWindow()
        {
            InitializeComponent();
        }

        public ListBox OutputArea
        {
            get { return listBox1; }
        }
    }
}