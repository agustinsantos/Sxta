namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> 
    /// This packages the attributes supplied to the RTI for various DDM services with
    /// the regions to be used with the attributes.  Elements are 
    /// <code>AttributeRegionAssociation</code>s.  All operations are required, none optional.
    /// Methods <code>Add</code>, <code>addAll</code>, and <code>set</code> should throw 
    /// <code>IllegalArgumentException</code> to enforce type of elements.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIAttributeSetRegionSetPairList : System.Collections.Generic.List<AttributeRegionAssociation>, IAttributeSetRegionSetPairList
    {
        /// <summary> Constructor.
        /// 
        /// </summary>
        /// <param name="capacity">the initial list capacity
        /// </param>
        protected internal XRTIAttributeSetRegionSetPairList(int capacity)
            : base(capacity)
        {
        }
        /*		
                /// <summary> Adds an element to this list at the specified location.
                /// 
                /// </summary>
                /// <param name="index">the location at which to Add the element
                /// </param>
                /// <param name="element">the element to Add
                /// </param>
                /// <exception cref=""> IllegalArgumentException if the element is not
                /// an <code>AttributeRegionAssociation</code>
                /// </exception>
                public override void  Insert(int index, System.Object element)
                {
                    if (!(element is AttributeRegionAssociation))
                    {
                        throw new System.ArgumentException("element must be AttributeRegionAssociation");
                    }
			
                    base.Insert(index, element);
                }
		
                /// <summary> Adds an element to the end of this list.
                /// 
                /// </summary>
                /// <param name="element">the element to Add
                /// </param>
                /// <returns> <code>true</code>, indicating that the list has changed
                /// </returns>
                /// <exception cref=""> IllegalArgumentException if the element is not
                /// an <code>AttributeRegionAssociation</code>
                /// </exception>
                //UPGRADE_ISSUE: The equivalent in .NET for method 'java.util.Vector.Add' returns a different type. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1224_3"'
                public override int Add(System.Object element)
                {
                    if (!(element is AttributeRegionAssociation))
                    {
                        throw new System.ArgumentException("element must be AttributeRegionAssociation");
                    }
			
                    return base.Add(element);
                }
		
                /// <summary> Adds the elements contained in the specified collection to this list.
                /// 
                /// </summary>
                /// <param name="c">the collection containing the elements to Add
                /// </param>
                /// <returns> <code>true</code> if the list changed as a result of this method call
                /// </returns>
                /// <exception cref=""> IllegalArgumentException if the collection is not an
                /// <code>IAttributeSetRegionSetPairList</code>
                /// </exception>
                //UPGRADE_NOTE: The equivalent of method 'java.util.Vector.addAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
                public virtual bool addAll(System.Collections.ICollection c)
                {
                    if (!(c is IAttributeSetRegionSetPairList))
                    {
                        throw new System.ArgumentException("collection must be IAttributeSetRegionSetPairList");
                    }
			
                    base.AddRange(c);
                    return true;
                }
		
                /// <summary> Adds the elements contained in the specified collection to this list at
                /// the specified location.
                /// 
                /// </summary>
                /// <param name="c">the collection containing the elements to Add
                /// </param>
                /// <param name="index">the location at which to Add the elements
                /// </param>
                /// <exception cref=""> IllegalArgumentException if the collection is not an
                /// <code>IAttributeSetRegionSetPairList</code>
                /// </exception>
                //UPGRADE_NOTE: The equivalent of method 'java.util.Vector.addAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
                public virtual bool addAll(int index, System.Collections.ICollection c)
                {
                    if (!(c is IAttributeSetRegionSetPairList))
                    {
                        throw new System.ArgumentException("collection must be IAttributeSetRegionSetPairList");
                    }
			
                    base.InsertRange(index, c);
                    return true;
                }
		
                /// <summary> Sets the element at the specified index.
                /// 
                /// </summary>
                /// <param name="index">the index of the element to set
                /// </param>
                /// <param name="element">the new parameterValue for that index
                /// </param>
                /// <returns> the element previously stored at that index
                /// </returns>
                /// <exception cref=""> IllegalArgumentException if the element is not
                /// an <code>AttributeRegionAssociation</code>
                /// </exception>
                //UPGRADE_NOTE: The equivalent of method 'java.util.Vector.set' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
                public virtual System.Object set_Renamed(int index, System.Object element)
                {
                    if (!(element is AttributeRegionAssociation))
                    {
                        throw new System.ArgumentException("element must be AttributeRegionAssociation");
                    }
			
                    System.Object tempObject;
                    tempObject = base[index];
                    base[index] = element;
                    return tempObject;
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public void  RemoveAt(System.Int32 index)
                {
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public void  Remove(System.Object value)
                {
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Int32 IndexOf(System.Object value)
                {
                    return 0;
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public void  Clear()
                {
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Boolean Contains(System.Object value)
                {
                    return false;
                }
                //UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1306_3"'
                virtual public void  CopyTo(System.Array array, System.Int32 index)
                {
                    for (int i = index; i < this.Count; i++)
                        array.SetValue(this[i], i);
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Collections.IEnumerator GetEnumerator()
                {
                    return null;
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Object this[System.Int32 index]
                {
                    get
                    {
                        return null;
                    }
			
                    set
                    {
                    }
			
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Boolean IsReadOnly
                {
                    get
                    {
                        return false;
                    }
			
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Boolean IsFixedSize
                {
                    get
                    {
                        return false;
                    }
			
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Int32 Count
                {
                    get
                    {
                        return 0;
                    }
			
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Object SyncRoot
                {
                    get
                    {
                        return null;
                    }
			
                }
                //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Boolean IsSynchronized
                {
                    get
                    {
                        return false;
                    }
			
                }
                //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
                virtual public System.Object Clone()
                {
                    return null;
                }
             */
    }
}