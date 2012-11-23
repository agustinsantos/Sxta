namespace Sxta.Rti1516.MilitarySample
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Reflection;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;

    // Import log4net classes.
    using log4net;


    public partial class ForceStructureExplorer : ToolWindow
    {
        private readonly String FIRST_NODE_NAME = "Force Structure";
        private readonly String FORCES_NODE_NAME = "Forces";

        public ForceStructureExplorer()
        {
            InitializeComponent();
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }

        public void ShowForceInformation(ForceStructure structure)
        {
            if (structure == null)
                return;

            TreeView treeViewForceStructure = this.treeView1;

            TreeNode firstNode = FindNode(treeViewForceStructure.Nodes, FIRST_NODE_NAME);
            if (firstNode == null)
            {
                firstNode = new TreeNode(FIRST_NODE_NAME);
                firstNode.ImageIndex = 3;
                firstNode.SelectedImageIndex = 3;
                firstNode.Tag = FIRST_NODE_NAME;
                treeViewForceStructure.Nodes.Add(firstNode);
            }

            TreeNode rootNode = FindNode(firstNode.Nodes, FORCES_NODE_NAME);
            if (rootNode == null)
            {
                rootNode = new TreeNode(FORCES_NODE_NAME);
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                rootNode.Tag = FORCES_NODE_NAME;
                firstNode.Nodes.Add(rootNode);
            }

            foreach (ForceSide forceSide in structure.ForceSides)
            {
                TreeNode node = AddForceTreeNode(rootNode, forceSide);
            }
        }

        private TreeNode AddForceTreeNode(TreeNode rootNode, ForceSide forceSide)
        {
            TreeNode node = new TreeNode(forceSide.ForceSideName);
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
            node.Tag = forceSide;
            rootNode.Nodes.Add(node);

            return node;
        }


        private TreeNode FindNode(TreeNodeCollection collection, string name)
        {
            foreach (TreeNode node in collection)
            {
                if (node.Text.Equals(name))
                    return node;
            }
            return null;
        }
    }
}