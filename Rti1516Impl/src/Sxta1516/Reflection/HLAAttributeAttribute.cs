using System;

using Hla.Rti1516;
//using Sxta.Rti1516.Impl;
//using Sxta.Rti1516.Proxies;
namespace Sxta.Rti1516.Reflection
{

    /// <summary>
    /// HLA Attribute changed delegate.
    /// </summary>
    public delegate void HLAAttributeEventDelegate();

    /// <summary>
    /// Indicates that class represents a HLAAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class HLAAttributeAttribute : HLAReflectionAttribute
    {
        public HLAattribute AttributeInfo
        {
            get { return ((HLAattribute)baseInfo); }
        }

        public HLAsharingType Sharing
        {
            get { return ((HLAattribute)baseInfo).Sharing; }
            set { ((HLAattribute)baseInfo).Sharing = value; }
        }

        public string SharingNotes
        {
            get { return ((HLAattribute)baseInfo).SharingNotes; }
            set { ((HLAattribute)baseInfo).SharingNotes = value; }
        }

        public string SemanticsNotes
        {
            get { return ((HLAattribute)baseInfo).SemanticsNotes; }
            set { ((HLAattribute)baseInfo).SemanticsNotes = value; }
        }

        public string Semantics
        {
            get { return ((HLAattribute)baseInfo).Semantics; }
            set { ((HLAattribute)baseInfo).Semantics = value; }
        }

        public string DataType
        {
            get { return ((HLAattribute)baseInfo).DataType; }
            set { ((HLAattribute)baseInfo).DataType = value; }
        }

        public HLAupdateType UpdateType
        {
            get { return ((HLAattribute)baseInfo).UpdateType; }
            set { ((HLAattribute)baseInfo).UpdateType = value; }
        }
        public string UpdateCondition
        {
            get { return ((HLAattribute)baseInfo).UpdateCondition; }
            set { ((HLAattribute)baseInfo).UpdateCondition = value; }
        }
        public HLAownershipType OwnerShip
        {
            get { return ((HLAattribute)baseInfo).Ownership; }
            set { ((HLAattribute)baseInfo).Ownership = value; }
        }

        public string Dimensions
        {
            get { return ((HLAattribute)baseInfo).Dimensions; }
            set { ((HLAattribute)baseInfo).Dimensions = value; }
        }

        public string Transportation
        {
            get { return ((HLAattribute)baseInfo).Transportation; }
            set { ((HLAattribute)baseInfo).Transportation = value; }
        }

        public HLAorderType Order
        {
            get { return ((HLAattribute)baseInfo).Order; }
            set { ((HLAattribute)baseInfo).Order = value; }
        }

        protected bool isValid = false;
        protected bool isDirty = false;

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAAttributeAttribute()
        {
            baseInfo = new HLAattribute();
        }

        public System.Reflection.PropertyInfo propInfo;

        public HLAobjectRoot realobject;

        public virtual void FlushAttributeValues(string attrName, params object[] arguments)
        {
            IsValid = true;
            IsDirty = true;

            if (realobject != null && !realobject.AutoFlushDisabled)
            {
                if (string.IsNullOrEmpty(attrName))
                {
                    //realobject.UpdateAttributeValues(methodMsg.MethodName.Substring(4, methodMsg.MethodName.Length - 4), methodMsg.Args[0]);
                }
                else
                {
                    realobject.UpdateAttributeValues(attrName, arguments[0]);
                }
            }
        }
    }


}


