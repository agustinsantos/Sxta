namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Xml;

    using Hla.Rti1516;
    using HLAsharingType = Sxta.Rti1516.Reflection.HLAsharingType;

    /// <summary> 
    /// Describes an object class.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class ObjectClassDescriptor 
    {
        /// <summary> 
        /// Returns the name of this object class.
        /// </summary>
        /// <returns> the name of this object class
        /// </returns>
        virtual public System.String Name
        {
            get { return objectDescription.Name; }
        }

        /// <summary> 
        /// Returns the native type of this object class.
        /// </summary>
        /// <returns> the native type of this object class
        /// </returns>
        virtual public System.Type NativeName
        {
            get { return nativeName; }
        }


        /// <summary> 
        /// Returns the handle of this object class.
        /// </summary>
        /// <returns> the handle of this object class
        /// </returns>
        virtual public IObjectClassHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the descriptors of this object class's parents.
        /// </summary>
        /// <returns> the descriptors of this object class's parents
        /// </returns>
        virtual public IList<ObjectClassDescriptor> ParentDescriptors
        {
            get { return parentDescriptors; }
        }

        /// <summary> 
        /// Returns an immutable collection containing the descriptors of all known attributes.
        /// Each element of the collection will be an <code>AttributeDescriptor</code>.
        /// </summary>
        /// <returns> an immutable collection containing the descriptors of all known attributes
        /// </returns>
        virtual public IList<AttributeDescriptor> AttributeDescriptors
        {
            get 
            {
                return new List<AttributeDescriptor>(attributeHandleDescriptorMap.Values); 
            }
        }

        /// <summary> 
        /// Gets the handles of all attributes associated with this object class,
        /// including those of all parents.
        /// </summary>
        /// <returns> a collection containing all of this object class's attribute handles
        /// </returns>
        virtual public System.Collections.ICollection AttributeHandles
        {
            get
            {
                //UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashSet_3"'
                SupportClass.HashSetSupport handles = new SupportClass.HashSetSupport();

                //UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMapkeySet_3"'
                handles.AddAll(new SupportClass.HashSetSupport(attributeHandleDescriptorMap.Keys));

                System.Collections.IEnumerator it = parentDescriptors.GetEnumerator();

                //UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratorhasNext_3"'
                while (it.MoveNext())
                {
                    //UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratornext_3"'
                    handles.AddAll(((ObjectClassDescriptor)it.Current).AttributeHandles);
                }

                return handles;
            }

        }

        /// <summary> The native Type of the object class.</summary>
        protected internal System.Type nativeName;

        /// <summary> The handle of the object class.</summary>
        private IObjectClassHandle handle;

        //TODO
        public Sxta.Rti1516.Reflection.HLAObjectClass objectDescription;

        /// <summary> The descriptors of the object class's parents.</summary>
        private List<ObjectClassDescriptor> parentDescriptors;

        /// <summary> Maps attribute names to attribute descriptors.</summary>
        private Dictionary<string, AttributeDescriptor> attributeNameDescriptorMap
            = new Dictionary<string, AttributeDescriptor>();

        /// <summary> Maps attribute handles to attribute descriptors.</summary>
        private Dictionary<IAttributeHandle, AttributeDescriptor> attributeHandleDescriptorMap
            = new Dictionary<IAttributeHandle,AttributeDescriptor>();

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="objClass">the object class description
        /// </param>
        /// <param name="pHandle">the handle of the object class
        /// </param>
        /// <param name="pParentDescriptors">the descriptors of the object class's parents
        /// </param>
        public ObjectClassDescriptor(Sxta.Rti1516.Reflection.HLAObjectClass objClass, IObjectClassHandle pHandle, List<ObjectClassDescriptor> pParentDescriptors)
        {
            objectDescription = objClass;
            handle = pHandle;
            parentDescriptors = pParentDescriptors;
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the object class
        /// </param>
        /// <param name="pHandle">the handle of the object class
        /// </param>
        /// <param name="pParentDescriptors">the descriptors of the object class's parents
        /// </param>es
        public ObjectClassDescriptor(XmlElement objClassElement, IObjectClassHandle pHandle, List<ObjectClassDescriptor> pParentDescriptors)
        {
            objectDescription = new Sxta.Rti1516.Reflection.HLAObjectClass(objClassElement);
            //nativeName = Assembly.GetExecutingAssembly().GetType(objectDescription.Name);
            handle = pHandle;
            parentDescriptors = pParentDescriptors;
        }

        /// <summary> Notifies this object that an object class of interest has been
        /// added to the descriptor manager.
        /// 
        /// </summary>
        /// <param name="ocd">the object class descriptor
        /// </param>
        protected internal virtual void ObjectClassAdded(ObjectClassDescriptor ocd)
        {
            parentDescriptors.Add(ocd);
        }

        /// <summary> Notifies this object that an attribute of interest has been
        /// added to the descriptor manager.
        /// 
        /// </summary>
        /// <param name="ad">the attribute descriptor
        /// </param>
        protected internal virtual void AttributeAdded(AttributeDescriptor ad)
        {
            AddAttributeDescriptor(ad);
        }

        /// <summary> Adds a parent descriptor.
        /// 
        /// </summary>
        /// <param name="pd">the parent descriptor to Add
        /// </param>
        public virtual void AddParentDescriptor(ObjectClassDescriptor od)
        {
            this.parentDescriptors.Add(od);
        }

        /// <summary> 
        /// Adds an attribute descriptor.
        /// </summary>
        /// <param name="ad">the attribute descriptor to Add
        /// </param>
        public virtual void AddAttributeDescriptor(AttributeDescriptor ad)
        {
                attributeNameDescriptorMap[ad.Name] = ad;
                attributeHandleDescriptorMap[ad.Handle] = ad;
        }

        /// <summary> 
        /// Removes an attribute descriptor.
        /// </summary>
        /// <param name="ad">the attribute descriptor to Remove
        /// </param>
        public virtual void RemoveAttributeDescriptor(AttributeDescriptor ad)
        {
            if (attributeNameDescriptorMap[ad.Name] == ad)
            {
                attributeNameDescriptorMap.Remove(ad.Name);
            }

            if (attributeHandleDescriptorMap[ad.Handle] == ad)
            {
                attributeHandleDescriptorMap.Remove(ad.Handle);
            }
        }

        /// <summary> 
        /// Returns the descriptor for the attribute with the given name.  First searches the
        /// attributes of this class, then the attributes of the parent classes.
        /// </summary>
        /// <param name="name">the name of the attribute
        /// </param>
        /// <returns> the attribute descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual AttributeDescriptor GetAttributeDescriptor(System.String name)
        {
            AttributeDescriptor ad = null;

            if (attributeNameDescriptorMap.ContainsKey(name))
            {
                return attributeNameDescriptorMap[name];
            }
            else
            {
                System.Collections.IEnumerator it = parentDescriptors.GetEnumerator();
                bool encontrado = false;
                ObjectClassDescriptor parent;

                while (it.MoveNext() && !encontrado)
                {
                    parent = (ObjectClassDescriptor)it.Current;
                    ad = parent.GetAttributeDescriptor(name);

                    if (ad != null)
                        encontrado = true;
                }

                if (encontrado)
                    return ad;
                else
                    return null;
            }
        }

        /// <summary> 
        /// Returns the descriptor for the attribute with the given handle.  First searches the
        /// attributes of this class, then the attributes of the parent classes.
        /// </summary>
        /// <param name="handle">the handle of the attribute
        /// </param>
        /// <returns> the attribute descriptor, or <code>null</code> if no such
        /// descriptor exists
        /// </returns>
        public virtual AttributeDescriptor GetAttributeDescriptor(IAttributeHandle handle)
        {
            AttributeDescriptor ad = null;

            if (attributeHandleDescriptorMap.ContainsKey(handle))
            {
                return attributeHandleDescriptorMap[handle];
            }
            else
            {
                System.Collections.IEnumerator it = parentDescriptors.GetEnumerator();
                bool encontrado = false;
                ObjectClassDescriptor parent;

                while (it.MoveNext() && !encontrado)
                {
                    parent = (ObjectClassDescriptor)it.Current;
                    ad = parent.GetAttributeDescriptor(handle);

                    if (ad != null)
                        encontrado = true;
                }

                if (encontrado)
                    return ad;
                else
                    return null;
            }
        }

        public override string ToString()
        {
            return "Object Class " + Name + " (" + Handle.ToString() + ")";
        }

    }
}