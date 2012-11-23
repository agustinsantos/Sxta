namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using System.Collections.Generic;

    using Hla.Rti1516;

    /// <summary> 
    /// Maps <code>IParameterHandle</code>s to <code>byte[]</code>s.  All operations
    /// are required, none optional.  Null mappings are not allowed.  Methods
    /// <code>put</code>, <code>putAll</code>, and <code>Remove</code> should throw
    /// <code>IllegalArgumentException</code> to enforce types of keys and mappings.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIParameterHandleValueMap : System.Collections.Generic.Dictionary<IParameterHandle, byte[]>, IParameterHandleValueMap
    {

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="capacity">the initial map capacity
        /// </param>
        protected internal XRTIParameterHandleValueMap(int capacity)
            : base(capacity)
        {
        }

        /*
        /// <summary> Associates the specified parameterValue with the specified key.
        /// 
        /// </summary>
        /// <param name="key">the key object
        /// </param>
        /// <param name="parameterValue">the parameterValue object
        /// </param>
        /// <returns> the previous parameterValue associated with the specified
        /// key, or <code>null</code> for none
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the key is not
        /// a <code>IParameterHandle</code> or the parameterValue is not
        /// a <code>byte[]</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.HashMap.put' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        private byte[] put(IParameterHandle key, byte[] value_Renamed)
        {
            if (!(key is IParameterHandle))
            {
                throw new System.ArgumentException("key must be IParameterHandle");
            }
            else if (!(value_Renamed is byte[]))
            {
                throw new System.ArgumentException("parameterValue must be byte[]");
            }
			
            System.Object tempObject;
            tempObject = base[key];
            base[key] = value_Renamed;
            return tempObject;
        }
		
        /// <summary> Adds all of the mappings contained in the specified map to this map.
        /// 
        /// </summary>
        /// <param name="t">the map whose mappings are to be added
        /// </param>
        /// <exception cref=""> IllegalArgumentException if the map is not a
        /// <code>IParameterHandleValueMap</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.HashMap.putAll' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        private void putAll(IParameterHandleValueMap t)
        {
            if (!(t is IParameterHandleValueMap))
            {
                throw new System.ArgumentException("map must be IParameterHandleValueMap");
            }
			
            SupportClass.MapSupport.PutAll((System.Collections.Hashtable) this, t);
            if (d != null)
            {
                System.Collections.ArrayList keys = new System.Collections.ArrayList(d.Keys);
                System.Collections.ArrayList values = new System.Collections.ArrayList(d.Values);

                for (int i = 0; i < keys.Count; i++)
                    target[keys[i]] = values[i];
            }

        }
		
        /// <summary> 
        /// Removes the mapping associated with the specified key.
        /// </summary>
        /// <param name="key">the key whose mapping is to be removed
        /// </param>
        /// <returns> the parameterValue that was mapped to the specified key,
        /// or <code>null</code> for none
        /// </returns>
        /// <exception cref=""> IllegalArgumentException if the key is not a
        /// <code>IParameterHandle</code>
        /// </exception>
        //UPGRADE_NOTE: The equivalent of method 'java.util.HashMap.Remove' is not an override method. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1143_3"'
        private System.Object remove(System.Object key)
        {
            if (!(key is IParameterHandle))
            {
                throw new System.ArgumentException("key must be IParameterHandle");
            }
			
            System.Object tempObject;
            tempObject = base[key];
            base.Remove(key);
            return tempObject;
        }

        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public void  Remove(System.Object key)
        {
            if (!(key is IParameterHandle))
            {
                throw new System.ArgumentException("key must be IParameterHandle");
            }
            base.Remove(key);
        }

        public void  Add(System.Object key, System.Object val)
        {
            if (!(key is IParameterHandle))
            {
                throw new System.ArgumentException("key must be IParameterHandle");
            }
            else if (!(val is byte[]))
            {
                throw new System.ArgumentException("parameterValue must be byte[]");
            }
            base.Add(key, val);
        }
		
        //UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1306_3"'
        virtual public void  CopyTo(System.Array array, System.Int32 index)
        {
            System.Object[] keys = new System.Object[this.Count];
            System.Object[] values = new System.Object[this.Count];
            if (this.Keys != null)
                this.Keys.CopyTo(keys, index);
            if (this.Values != null)
                this.Values.CopyTo(values, index);
            for (int i = index; i < this.Count; i++)
                if (keys[i] != null || values[i] != null)
                    array.SetValue(new System.Collections.DictionaryEntry(keys[i], values[i]), i);
        }
        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
        }

        virtual public System.Object this[System.Object key]
        {
            get
            {
                return base[key];
            }
			
            set
            {
                if (base.ContainsKey(key))
                    base[key] = value;
                else
                    this.Add(key, value);
            }
			
        }
        */
    }
}