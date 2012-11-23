using System;

namespace Sxta.Rti1516.Reflection
{


    /// <summary>
    /// Indicates that class represents a HLAAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
    public class HLAInteractionClassAttribute : HLAReflectionAttribute
    {
        public HLAinteractionClass InteractionClassInfo
        {
            get { return ((HLAinteractionClass)baseInfo); }
        }

        public string Semantics
        {
            get { return ((HLAinteractionClass)baseInfo).Semantics; }
            set { ((HLAinteractionClass)baseInfo).Semantics = value; }
        }

        public string SemanticsNotes
        {
            get { return ((HLAinteractionClass)baseInfo).SemanticsNotes; }
            set { ((HLAinteractionClass)baseInfo).SemanticsNotes = value; }
        }

        public HLAsharingType Sharing
        {
            get { return ((HLAinteractionClass)baseInfo).Sharing; }
            set { ((HLAinteractionClass)baseInfo).Sharing = value; }
        }

        public string SharingNotes
        {
            get { return ((HLAinteractionClass)baseInfo).SharingNotes; }
            set { ((HLAinteractionClass)baseInfo).SharingNotes = value; }
        }

        public string Dimensions
        {
            get { return ((HLAinteractionClass)baseInfo).Dimensions; }
            set { ((HLAinteractionClass)baseInfo).Dimensions = value; }
        }

        public string Transportation
        {
            get { return ((HLAinteractionClass)baseInfo).Transportation; }
            set { ((HLAinteractionClass)baseInfo).Transportation = value; }
        }

        public HLAorderType Order
        {
            get { return ((HLAinteractionClass)baseInfo).Order; }
            set { ((HLAinteractionClass)baseInfo).Order = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAInteractionClassAttribute()
        {
            baseInfo = new HLAinteractionClass();
        }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public class HLAinteractionHelperAttribute : Attribute
    {
        protected string name;
        protected string semantics;
        protected string fomName;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Semantics
        {
            get { return semantics; }
            set { semantics = value; }
        }

        public string FomName
        {
            get { return fomName; }
            set { fomName = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAinteractionHelperAttribute()
        {
        }
    }
}
