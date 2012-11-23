namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Hla.Rti1516;

    using HLAorderType = Sxta.Rti1516.Reflection.HLAorderType;
    using HLAsharingType = Sxta.Rti1516.Reflection.HLAsharingType;
    using Sxta.Rti1516.XrtiHandles;

    /// <summary> 
    /// Describes an interaction class.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class InteractionClassDescriptor 
    {
        /// <summary> 
        /// Returns the name of this interaction class.
        /// </summary>
        /// <returns> the name of this interaction class
        /// </returns>
        virtual public System.String Name
        {
            get { return interactionClass.Name; }
        }

        /// <summary>
        /// Returns the handle of this interaction class.
        /// </summary>
        /// <returns> the handle of this interaction class
        /// </returns>
        virtual public IInteractionClassHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the descriptors of this interaction class's parents.
        /// </summary>
        /// <returns> the descriptors of this interaction class's parents
        /// </returns>
        virtual public List<InteractionClassDescriptor> ParentDescriptors
        {
            get { return parentDescriptors; }
        }

        /// <summary> 
        /// Returns the dimensions associated with this interaction class.
        /// </summary>
        /// <returns> the dimensions associated with this interaction class
        /// </returns>
        virtual public IDimensionHandleSet Dimensions
        {
            get { return dimensions; }
        }

        /// <summary> 
        /// Gets/sets the transportation type of this interaction class.
        /// </summary>
        virtual public TransportationType Transportation
        {
            get { return transportation; }
            set { transportation = value; }
        }

        /// <summary> 
        /// Gets/sets the order type of this interaction class.
        /// </summary>
        virtual public Sxta.Rti1516.Reflection.HLAorderType Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary> 
        /// Gets/sets whether or not this interaction class is published.
        /// </summary>
        virtual public bool Published
        {
            get { return published; }
            set { published = value; }
        }

        /// <summary> 
        /// Gets/sets whether or not this interaction class is subscribed.
        /// </summary>
        virtual public bool Subscribed
        {
            get { return subscribed; }
            set { subscribed = value; }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known parameters.
        /// Each element of the collection will be a <code>ParameterDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known parameters
        /// </returns>
        virtual public List<ParameterDescriptor> ParameterDescriptors
        {
            get
            {
                return new List<ParameterDescriptor>(parameterHandleDescriptorMap.Values);
            }

        }

        /// <summary> The handle of the interaction class.</summary>
        private IInteractionClassHandle handle;

        public Reflection.HLAinteractionClass interactionClass;

        /// <summary> The descriptors of the interaction class's parents.</summary>
        private List<InteractionClassDescriptor> parentDescriptors;

        /// <summary> The dimensions associated with the interaction class.</summary>
        private IDimensionHandleSet dimensions;

        /// <summary> The transportation type of this interaction class.</summary>
        private TransportationType transportation;

        /// <summary> The order type of this interaction class.</summary>
        private Sxta.Rti1516.Reflection.HLAorderType order;

        /// <summary> Whether or not this interaction class is published.</summary>
        private bool published;

        /// <summary> Whether or not this interaction class is subscribed.</summary>
        private bool subscribed;

        /// <summary> Maps parameter names to parameter descriptors.</summary>
        private Dictionary<string, ParameterDescriptor> parameterNameDescriptorMap = new Dictionary<string, ParameterDescriptor>();

        /// <summary> Maps parameter handles to parameter descriptors.</summary>
        private Dictionary<IParameterHandle, ParameterDescriptor> parameterHandleDescriptorMap = new Dictionary<IParameterHandle, ParameterDescriptor>();


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the interaction class
        /// </param>
        /// <param name="pHandle">the handle of the interaction class
        /// </param>
        /// <param name="pParentDescriptors">the descriptors of the interaction class's parents
        /// </param>
        /// <param name="pDimensions">the dimensions associated with the interaction class
        /// </param>
        /// <param name="pTransportation">the transportation type of the interaction class
        /// </param>
        /// <param name="pOrder">the order type of the interaction class
        /// </param>
        public InteractionClassDescriptor(XmlElement interactionElement, IInteractionClassHandle pHandle, List<InteractionClassDescriptor> pParentDescriptors, IDimensionHandleSet pDimensions, TransportationType pTransportation, Sxta.Rti1516.Reflection.HLAorderType pOrder)
        {
            interactionClass = new Sxta.Rti1516.Reflection.HLAinteractionClass(interactionElement);
            handle = pHandle;
            parentDescriptors = pParentDescriptors;
            dimensions = pDimensions;
            transportation = pTransportation;
            order = pOrder;
        }


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="interactClass">the info of the interaction class
        /// </param>
        /// <param name="pHandle">the handle of the interaction class
        /// </param>
        /// <param name="pParentDescriptors">the descriptors of the interaction class's parents
        /// </param>
        /// </param>
        public InteractionClassDescriptor(Sxta.Rti1516.Reflection.HLAinteractionClass interactClass, IInteractionClassHandle pHandle)
        {
            interactionClass = interactClass;
            handle = pHandle;
            parentDescriptors = new List<InteractionClassDescriptor>();
            dimensions = new XRTIDimensionHandleSet();
            transportation = "HLAreliable".Equals(interactClass.Transportation) ? TransportationType.HLA_RELIABLE : TransportationType.HLA_BEST_EFFORT;
            order = "Receive".Equals(interactClass.Order) ? Sxta.Rti1516.Reflection.HLAorderType.Receive : Sxta.Rti1516.Reflection.HLAorderType.TimeStamp;
        }

        /// <summary> 
        /// Notifies this object that an interaction class of interest has been
        /// added to the descriptor manager.
        /// </summary>
        /// <param name="icd">the interaction class descriptor
        /// </param>
        protected internal virtual void InteractionClassAdded(InteractionClassDescriptor icd)
        {
            parentDescriptors.Add(icd);
        }

        /// <summary> 
        /// Notifies this object that a parameter of interest has been
        /// added to the descriptor manager.
        /// </summary>
        /// <param name="pd">the parameter descriptor
        /// </param>
        protected internal virtual void ParameterAdded(ParameterDescriptor pd)
        {
            AddParameterDescriptor(pd);
        }

        /// <summary>
        /// Adds a parent descriptor.
        /// </summary>
        /// <param name="pd">the parent descriptor to Add
        /// </param>
        public virtual void AddParentDescriptor(InteractionClassDescriptor pd)
        {
            this.parentDescriptors.Add(pd);
        }

        /// <summary> 
        /// Adds a parameter descriptor.
        /// </summary>
        /// <param name="pd">the parameter descriptor to Add
        /// </param>
        public virtual void AddParameterDescriptor(ParameterDescriptor pd)
        {
            parameterNameDescriptorMap[pd.Name] = pd;
            parameterHandleDescriptorMap[pd.Handle] = pd;
        }

        /// <summary> 
        /// Removes a parameter descriptor.
        /// </summary>
        /// <param name="pd">the parameter descriptor to Remove
        /// </param>
        public virtual void RemoveParameterDescriptor(ParameterDescriptor pd)
        {
            if (parameterNameDescriptorMap[pd.Name] == pd)
            {
                parameterNameDescriptorMap.Remove(pd.Name);
            }

            if (parameterHandleDescriptorMap[pd.Handle] == pd)
            {
                parameterHandleDescriptorMap.Remove(pd.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the parameter with the given name.  First
        /// searches the parameters of this class, then the parameters of the
        /// parent classes.
        /// </summary>
        /// <param name="name">the name of the parameter
        /// </param>
        /// <returns> the parameter descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ParameterDescriptor GetParameterDescriptor(System.String name)
        {
            //UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMapget_javalangObject_3"'
            ParameterDescriptor pd = (ParameterDescriptor)parameterNameDescriptorMap[name];

            if (pd != null)
            {
                return pd;
            }
            else
            {
                System.Collections.IEnumerator it = parentDescriptors.GetEnumerator();

                //UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratorhasNext_3"'
                while (it.MoveNext() && pd == null)
                {
                    //UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratornext_3"'
                    pd = ((InteractionClassDescriptor)it.Current).GetParameterDescriptor(name);
                }

                return pd;
            }
        }

        /// <summary> 
        /// Returns the descriptor for the parameter with the given handle.  First
        /// searches the parameters of this class, then the parameters of the
        /// parent classes.
        /// </summary>
        /// <param name="handle">the handle of the parameter
        /// </param>
        /// <returns> the parameter descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual ParameterDescriptor GetParameterDescriptor(IParameterHandle handle)
        {
            ParameterDescriptor pd = parameterHandleDescriptorMap[handle];

            if (pd != null)
            {
                return pd;
            }
            else
            {
                System.Collections.IEnumerator it = parentDescriptors.GetEnumerator();

                //UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratorhasNext_3"'
                while (it.MoveNext() && pd == null)
                {
                    //UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratornext_3"'
                    pd = ((InteractionClassDescriptor)it.Current).GetParameterDescriptor(handle);
                }

                return pd;
            }
        }

        public override string ToString()
        {
            return "Interaction Class" + Name + " (" + Handle.ToString() + ")";
        }
    }
}