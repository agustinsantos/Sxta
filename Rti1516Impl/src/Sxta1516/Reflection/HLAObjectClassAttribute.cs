using System;

namespace Sxta.Rti1516.Reflection
{
    /// <summary>
    /// Indicates that class represents a HLAObjectClass.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public class HLAObjectClassAttribute : HLAReflectionAttribute
    {

        public HLAObjectClass ObjectClassInfo
        {
            get { return ((HLAObjectClass)baseInfo); }
        }

        public HLAsharingType Sharing
        {
            get { return ((HLAObjectClass)baseInfo).Sharing; }
            set { ((HLAObjectClass)baseInfo).Sharing = value; }
        }

        public string SharingNotes
        {
            get { return ((HLAObjectClass)baseInfo).SharingNotes; }
            set { ((HLAObjectClass)baseInfo).SharingNotes = value; }
        }

        public string SemanticsNotes
        {
            get { return ((HLAObjectClass)baseInfo).SemanticsNotes; }
            set { ((HLAObjectClass)baseInfo).SemanticsNotes = value; }
        }

        public string Semantics
        {
            get { return ((HLAObjectClass)baseInfo).Semantics; }
            set { ((HLAObjectClass)baseInfo).Semantics = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAObjectClassAttribute()
        {
            baseInfo = new HLAObjectClass();
        }
    }

}
