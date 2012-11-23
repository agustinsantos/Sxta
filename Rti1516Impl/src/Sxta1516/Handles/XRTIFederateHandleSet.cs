namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> All <code>Set</code> operations are required, none are optional.  Methods
    /// <code>add</code> and <code>Remove</code> should throw
    /// <code>IllegalArgumentException</code> if the argument is not a
    /// <code>FederateHandleHandle</code>.  Methods <code>addAll</code>,
    /// <code>RemoveAll</code> and <code>RetainAll</code> should throw
    /// <code>IllegalArgumentException</code> if the argument is not a
    /// <code>IFederateHandleSet</code>.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIFederateHandleSet : System.Collections.Generic.List<IFederateHandle>, IFederateHandleSet
    {
        /// <summary> Constructor.</summary>
        protected internal XRTIFederateHandleSet()
            : base()
        {
        }

        /*
        /// <summary> Adds the specified object to this set.
        /// 
        /// </summary>
        /// <param name="o">the object to add
        /// </param>
        /// <returns> <code>true</code> if the set changed as a result of this
        /// method call, <code>false</code> otherwise
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the object is not a
        /// <code>IFederateHandle</code>
        /// </exception>
        public override System.Boolean Add(System.Object o)
        {
            if (!(o is IFederateHandle))
            {
                throw new System.ArgumentException("object must be IFederateHandle");
            }
			
            return base.Add(o);
        }
		
        /// <summary> Removes the specified object from this set.
        /// 
        /// </summary>
        /// <param name="o">the object to Remove
        /// </param>
        /// <returns> <code>true</code> if the set changed as a result of this
        /// method call, <code>false</code> otherwise
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the object is not a
        /// <code>IFederateHandle</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.HashSet.Remove' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        public virtual bool remove(System.Object o)
        {
            if (!(o is IFederateHandle))
            {
                throw new System.ArgumentException("object must be IFederateHandle");
            }
			
            System.Boolean tempBoolean;
            tempBoolean = base.Contains(o);
            base.Remove(o);
            return tempBoolean;
        }
		
        /// <summary> Adds all of the objects in the specified collection to this set.
        /// 
        /// </summary>
        /// <param name="c">the collection containing the objects to add
        /// </param>
        /// <returns> <code>true</code> if the set changed as a result of this
        /// method call, <code>false</code> otherwise
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the collection is not a
        /// <code>IFederateHandleSet</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.AbstractCollection.addAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        public virtual bool addAll(System.Collections.ICollection c)
        {
            if (!(c is IFederateHandleSet))
            {
                throw new System.ArgumentException("collection must be IFederateHandleSet");
            }
			
            return this.AddAll(c);
        }
		
        /// <summary> Removes all of the objects in the specified collection from this set.
        /// 
        /// </summary>
        /// <param name="c">the collection containing the objects to Remove
        /// </param>
        /// <returns> <code>true</code> if the set changed as a result of this
        /// method call, <code>false</code> otherwise
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the collection is not a
        /// <code>IFederateHandleSet</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.AbstractSet.RemoveAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        public virtual bool removeAll(System.Collections.ICollection c)
        {
            if (!(c is IFederateHandleSet))
            {
                throw new System.ArgumentException("collection must be IFederateHandleSet");
            }
			
            return SupportClass.ICollectionSupport.RemoveAll((SupportClass.HashSetSupport) this, c);
        }
		
        /// <summary> Removes all of the objects in this set except for those present in the
        /// specified collection.
        /// 
        /// </summary>
        /// <param name="c">the collection containing the objects to retain
        /// </param>
        /// <returns> <code>true</code> if the set changed as a result of this
        /// method call, <code>false</code> otherwise
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the collection is not a
        /// <code>IFederateHandleSet</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.AbstractCollection.RetainAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        public virtual bool retainAll(System.Collections.ICollection c)
        {
            if (!(c is IFederateHandleSet))
            {
                throw new System.ArgumentException("collection must be IFederateHandleSet");
            }
			
            return SupportClass.ICollectionSupport.RetainAll((SupportClass.HashSetSupport) this, c);
        }
        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        virtual public System.Object Clone()
        {
            return null;
        }
         */
    }
}