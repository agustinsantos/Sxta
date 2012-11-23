namespace Sxta.Rti1516.WinMain
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

    using Hla.Rti1516;
    using Sxta.Rti1516.BaseApplication;
    using Sxta.Rti1516.Channels;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.BoostrapProtocol;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.Management;

    public partial class ObjectModelExplorer : ToolWindow
    {
        private OMTPropertyWindow propertyGrid;

        private readonly String META_FEDERATION_NODE_NAME = "MetaFederation";
        private readonly String DESCRIPTORS_NODE_NAME = "Descriptors";
        private readonly String INSTANCES_NODE_NAME = "Instances";
        private readonly String FEDERATIONS_NODE_NAME = "Federations";
        private readonly String FEDERATES_NODE_NAME = "Federates";

        public ObjectModelExplorer(DescriptorManager descriptorManager)
        {
            InitializeComponent();
            //DescriptorManager descriptorManager = new DescriptorManager();
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.BootstrapObjectModel);
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.LowLevelManagementObjectModel);
            //descriptorManager.AddDescriptors(Sxta.Rti1516ResourcesNames.ManagementObjectModel);

            ShowInformationFromDescriptorManager(descriptorManager, META_FEDERATION_NODE_NAME);
        }
        public OMTPropertyWindow PropertyGrid
        {
            get { return propertyGrid; }
            set { propertyGrid = value; }
        }

        private void TreeViewAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            this.propertyGrid.InternalPropertyGrid.SelectedObject = e.Node.Tag;
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }

        public void ShowInformationFromDescriptorManager(DescriptorManager descriptorManager, String firstNodeName)
        {
            if (descriptorManager == null)
                return;

            TreeView treeViewDescriptorManager = this.treeView1;

            TreeNode firstNode = FindNode(treeViewDescriptorManager.Nodes, firstNodeName);
            if (firstNode == null)
            {
                firstNode = new TreeNode(firstNodeName);
                firstNode.ImageIndex = 3;
                firstNode.SelectedImageIndex = 3;
                firstNode.Tag = firstNodeName;
                treeViewDescriptorManager.Nodes.Add(firstNode);
            }

            TreeNode rootNode = FindNode(firstNode.Nodes, DESCRIPTORS_NODE_NAME);
            if (rootNode == null)
            {
                rootNode = new TreeNode(DESCRIPTORS_NODE_NAME);
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                rootNode.Tag = DESCRIPTORS_NODE_NAME;
                firstNode.Nodes.Add(rootNode);
            }
            
            foreach (ObjectClassDescriptor objDescriptor in descriptorManager.ObjectClassDescriptors)
            {
                TreeNode node = AddClassTreeNode2(rootNode, objDescriptor);
            }
            foreach (InteractionClassDescriptor interactionDescriptor in descriptorManager.InteractionClassDescriptors)
            {
                TreeNode node = AddInteractionTreeNode(rootNode, interactionDescriptor);
            }

            //if (treeViewDescriptorManager != null)
            //{

                //treeViewDescriptorManager.Dock = System.Windows.Forms.DockStyle.Fill;
                //treeViewDescriptorManager.Location = new System.Drawing.Point(4, 4);
                //treeViewDescriptorManager.Margin = new System.Windows.Forms.Padding(4);
                //treeViewDescriptorManager.Name = "DescriptorManager";
                //treeViewDescriptorManager.Size = new System.Drawing.Size(251, 268);
                //treeViewDescriptorManager.TabIndex = 0;
                //treeViewDescriptorManager.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterSelect);
            //}
        }

        private TreeNode AddClassTreeNode2(TreeNode rootNode, ObjectClassDescriptor objDescriptor)
        {   
            TreeNodeCollection parentNodes;
            if (objDescriptor == null)
            {
                TreeNode parentNode = FindNode(rootNode.Nodes, "HLAobjectRoot");
                if (parentNode == null)
                {
                    //tv.ImageList = imageList1;

                    parentNode = new TreeNode("HLAobjectRoot");
                    parentNode.Name = "HLAobjectRoot";
                    parentNode.ImageIndex = 0;
                    parentNode.SelectedImageIndex = 0;

                    //tv.Nodes.Add(parentNode);
                    rootNode.Nodes.Add(parentNode);
                }
                return parentNode;
            }
            else if (objDescriptor.ParentDescriptors.Count != 0)
            {
                TreeNode parentNode = AddClassTreeNode2(rootNode, objDescriptor.ParentDescriptors[0]);
                parentNodes = parentNode.Nodes;
            }
            else
                //parentNodes = tv.Nodes;
                parentNodes = rootNode.Nodes;

            TreeNode tmpNode = FindNode(parentNodes, objDescriptor.Name);
            if (tmpNode != null)
                return tmpNode;
            else
            {
                TreeNode node = new TreeNode(objDescriptor.Name);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                HLAObjectClassPropertiesInformation nodeInfo = new HLAObjectClassPropertiesInformation(objDescriptor.objectDescription, null);
                node.Tag = nodeInfo;

                foreach (AttributeDescriptor attributeDescriptor in objDescriptor.AttributeDescriptors)
                {
                    TreeNode nodeAttr = new TreeNode(attributeDescriptor.Name);
                    HLAAttributePropertiesInformation nodeAttrInfo = new HLAAttributePropertiesInformation(attributeDescriptor.attribute, null);
                    nodeAttr.ImageIndex = 2;
                    nodeAttr.SelectedImageIndex = 2;
                    nodeAttr.Tag = nodeAttrInfo;
                    node.Nodes.Add(nodeAttr);
                }
                parentNodes.Add(node);

                return node;
            }
        }

        private TreeNode AddInteractionTreeNode(TreeNode rootNode, InteractionClassDescriptor interactionDescriptor)
        {
            TreeNodeCollection parentNodes;
            if (interactionDescriptor.ParentDescriptors.Count != 0)
            {
                TreeNode parentNode = AddInteractionTreeNode(rootNode, interactionDescriptor.ParentDescriptors[0]);
                parentNodes = parentNode.Nodes;
            }
            else
                parentNodes = rootNode.Nodes;

            TreeNode tmpNode = FindNode(parentNodes, interactionDescriptor.Name);
            if (tmpNode != null)
                return tmpNode;
            else
            {
                TreeNode node = new TreeNode(interactionDescriptor.Name);
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                HLAInteractionClassPropertiesInformation nodeInfo = new HLAInteractionClassPropertiesInformation(interactionDescriptor.interactionClass, null);
                node.Tag = nodeInfo;
                parentNodes.Add(node);

                return node;
            }
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

        public void ShowInformationFromRtiAmbassador(MetaFederateAmbassador metaFederateAmbassador, XrtiExecutiveAmbassador rti)
        {
            TreeView objectTreeView = this.treeView1;
            objectTreeView.ImageList = this.imageList1;

            if (metaFederateAmbassador != null)
            {
                DumpMetaFederationObjects(metaFederateAmbassador, META_FEDERATION_NODE_NAME);
            }

            if (rti.FederateAmbassador != null)
            {
                IList<HLAobjectRoot> objectsFederation = ((FederateAmbassador)rti.FederateAmbassador).GetObjects();

                DumpFederationObjects(metaFederateAmbassador, objectsFederation, "Federation : " + rti.FederationName, rti.FederationName);
            }

            //objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            //objectTreeView.Location = new System.Drawing.Point(4, 4);
            //objectTreeView.Margin = new System.Windows.Forms.Padding(4);
            //objectTreeView.Name = "Object View";
            //objectTreeView.Size = new System.Drawing.Size(251, 268);
            //objectTreeView.TabIndex = 0;
            //objectTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterSelect);
        }

        private void CreateObjectPropertiesNodes(object obj, TreeNode nodeObject)
        {
            foreach (PropertyInfo propInfo in obj.GetType().BaseType.GetProperties(
                                            BindingFlags.Instance | BindingFlags.Public))  // | BindingFlags.DeclaredOnly
            {
                object[] arrayOfCustomAttributes = propInfo.GetCustomAttributes(true);
                // object[] arrayOfCustomAttributes = propInfo.GetCustomAttributes(typeof(HLAAttributeAttribute), false);

                foreach (Attribute custumAttr in arrayOfCustomAttributes)
                {
                    if (custumAttr is HLAAttributeAttribute)
                    {
                        HLAAttributeAttribute attr = custumAttr as HLAAttributeAttribute;

                        object newValue = propInfo.GetValue(obj, null);

                        TreeNode nodeProperty = FindNode(nodeObject.Nodes, attr.Name);
                        if (nodeProperty != null)
                        {
                            object oldValue = ((HLAAttributePropertiesInformationValue)nodeProperty.Tag).ObjectValue;
                            if (newValue != null && !newValue.Equals(oldValue))
                            {
                                HLAAttributePropertiesInformationValue nodeInfo = new HLAAttributePropertiesInformationValue(attr.AttributeInfo, propInfo, newValue);

                                // Updates the UI if the selected object is the same that the object that its properties has been changed
                                if (this.propertyGrid.InternalPropertyGrid.SelectedObject.Equals(nodeProperty.Tag))
                                {
                                    this.propertyGrid.InternalPropertyGrid.SelectedObject = nodeInfo;
                                }

                                nodeProperty.Tag = nodeInfo;
                            }
                        }
                        else
                        {
                            nodeProperty = new TreeNode(attr.Name);
                            nodeProperty.ImageIndex = 1;
                            nodeProperty.SelectedImageIndex = 1;

                            HLAAttributePropertiesInformationValue nodeInfo = new HLAAttributePropertiesInformationValue(attr.AttributeInfo, propInfo, newValue);
                            nodeProperty.Tag = nodeInfo;
                            nodeObject.Nodes.Add(nodeProperty);
                        }

                    }
                }
            }
        }

        private void DumpMetaFederationObjects(MetaFederateAmbassador federateAmbassador, String parentNodeName)
        {
            String location;

            TreeNode parentNode = FindNode(treeView1.Nodes, parentNodeName);
            if (parentNode == null)
            {
                parentNode = new TreeNode(parentNodeName);
                parentNode.ImageIndex = 3;
                parentNode.SelectedImageIndex = 3;
                parentNode.Tag = parentNodeName;
                this.treeView1.Nodes.Add(parentNode);
            }

            TreeNode instancesNode = FindNode(parentNode.Nodes, INSTANCES_NODE_NAME);
            if (instancesNode == null)
            {
                instancesNode = new TreeNode(INSTANCES_NODE_NAME);
                instancesNode.ImageIndex = 3;
                instancesNode.SelectedImageIndex = 3;
                instancesNode.Tag = INSTANCES_NODE_NAME;
                parentNode.Nodes.Add(instancesNode);
            }

            TreeNode federationsNode = FindNode(instancesNode.Nodes, FEDERATIONS_NODE_NAME);
            if (federationsNode == null)
            {
                federationsNode = new TreeNode(FEDERATIONS_NODE_NAME);
                federationsNode.ImageIndex = 3;
                federationsNode.SelectedImageIndex = 3;
                federationsNode.Tag = FEDERATIONS_NODE_NAME;
                instancesNode.Nodes.Add(federationsNode);
            }

            foreach (HLAfederation federation in federateAmbassador.GetFederations())
            {
                location = "Remote";
                if (federation.HLAprivilegeToDeleteObject)  location = "Local";

                String nodeName = federation.HLAfederationName + "[" + location + ", " + federation.InstanceHandle + "]";

                TreeNode node = FindNode(federationsNode.Nodes, nodeName);
                if (node == null)
                {
                    node = new TreeNode(nodeName);
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    node.Tag = nodeName;
                    federationsNode.Nodes.Add(node);
                }

                CreateObjectPropertiesNodes(federation, node);
            }

            TreeNode federatesNode = FindNode(instancesNode.Nodes, FEDERATES_NODE_NAME);
            if (federatesNode == null)
            {
                federatesNode = new TreeNode(FEDERATES_NODE_NAME);
                federatesNode.ImageIndex = 3;
                federatesNode.SelectedImageIndex = 3;
                federatesNode.Tag = FEDERATES_NODE_NAME;
                instancesNode.Nodes.Add(federatesNode);
            }

            foreach (Sxtafederate federate in federateAmbassador.GetFederates())
            {
                location = "Remote";
                if (federate.HLAprivilegeToDeleteObject)
                    location = "Local";

                String nodeName = federate.HLAfederateType + "[" + location + ", " + federate.InstanceHandle + "]";

                TreeNode node = FindNode(federatesNode.Nodes, nodeName);
                if (node == null)
                {
                    node = new TreeNode(nodeName);
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    node.Tag = nodeName;
                    federatesNode.Nodes.Add(node);
                }

                CreateObjectPropertiesNodes(federate, node);
            }
        }

        private void DumpFederationObjects(MetaFederateAmbassador metaFederateAmbassador, IList<HLAobjectRoot> objects, String parentNodeName, String federationName)
        {
            TreeNode parentNode = FindNode(treeView1.Nodes, parentNodeName);
            if (parentNode == null)
            {
                parentNode = new TreeNode(parentNodeName);
                parentNode.ImageIndex = 3;
                parentNode.SelectedImageIndex = 3;
                parentNode.Tag = parentNodeName;
                this.treeView1.Nodes.Add(parentNode);

                HLAfederation federationObject = metaFederateAmbassador.GetFederation(federationName);

                if (federationObject != null)
                {
                    DescriptorManager descriptorManager = new DescriptorManager();
                    descriptorManager.AddDescriptors(federationObject.HLAFDDID);

                    ShowInformationFromDescriptorManager(descriptorManager, parentNodeName);
                }
            }

            AddHLAobjectRootNodes(parentNode, objects);
        }

        private void AddHLAobjectRootNodes(TreeNode parentNode, IList<HLAobjectRoot> objects)
        {
            String location;
            TreeNode rootNode = FindNode(parentNode.Nodes, INSTANCES_NODE_NAME);
            if (rootNode == null)
            {
                rootNode = new TreeNode(INSTANCES_NODE_NAME);
                rootNode.ImageIndex = 3;
                rootNode.SelectedImageIndex = 3;
                rootNode.Tag = INSTANCES_NODE_NAME;
                parentNode.Nodes.Add(rootNode);
            }

            foreach (HLAobjectRoot objRoot in objects)
            {
                location = "Remote";
                if (objRoot.HLAprivilegeToDeleteObject) location = "Local";

                String nodeName = objRoot.GetType().BaseType.Name + "[" + location + ", " + objRoot.InstanceHandle + "]";

                TreeNode node = FindNode(rootNode.Nodes, nodeName);
                if (node == null)
                {
                    node = new TreeNode(nodeName);
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    node.Tag = objRoot.ToString();
                    rootNode.Nodes.Add(node);
                }

                CreateObjectPropertiesNodes(objRoot, node);
            }
        }


#if PENDIENTE_DE_INTEGRAR
        private TreeNode AddClassTreeNode(TreeView tv, HLAObjectClassAttribute attr, Type classType)
        {
            TreeNode parentNode;
            if (classType == typeof(HLAobjectRoot) || attr == null)
            {
                parentNode = FindNode(tv.Nodes, "HLAobjectRoot");
                if (parentNode == null)
                {
                    tv.ImageList = imageList1;

                    parentNode = new TreeNode("HLAobjectRoot");
                    parentNode.Name = "HLAobjectRoot";
                    parentNode.ImageIndex = 0;
                    parentNode.SelectedImageIndex = 0;

                    tv.Nodes.Add(parentNode);
                }
                return parentNode;
            }
            else
            {
                HLAObjectClassAttribute objectClass = (HLAObjectClassAttribute)System.Attribute.GetCustomAttribute(classType.BaseType, typeof(HLAObjectClassAttribute));

                parentNode = AddClassTreeNode(tv, objectClass, classType.BaseType);
            }

            TreeNode tmpNode = FindNode(parentNode.Nodes, attr.Name);
            if (tmpNode != null)
                return tmpNode;
            else
            {
                TreeNode node = new TreeNode(attr.Name);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                HLAObjectClassPropertiesInformation nodeInfo = new HLAObjectClassPropertiesInformation(attr.ObjectClassInfo, classType.FullName);
                node.Tag = nodeInfo;

                parentNode.Nodes.Add(node);

                return node;
            }
        }
#endif
    }
}