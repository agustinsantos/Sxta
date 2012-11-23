namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    /// <summary>
    /// Indicates that class represents a HLAAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class HLAInteractionParameterAttribute : HLAReflectionAttribute
    {
        public HLAInteractionParameter ParameterInfo
        {
            get { return ((HLAInteractionParameter)baseInfo); }
        }

        public string DataType
        {
            get { return ((HLAInteractionParameter)baseInfo).DataType; }
            set { ((HLAInteractionParameter)baseInfo).DataType = value; }
        }

        public string DataTypeNotes
        {
            get { return ((HLAInteractionParameter)baseInfo).DataTypeNotes; }
            set { ((HLAInteractionParameter)baseInfo).DataTypeNotes = value; }
        }

        public string SemanticsNotes
        {
            get { return ((HLAInteractionParameter)baseInfo).SemanticsNotes; }
            set { ((HLAInteractionParameter)baseInfo).SemanticsNotes = value; }
        }

        public string Semantics
        {
            get { return ((HLAInteractionParameter)baseInfo).Semantics; }
            set { ((HLAInteractionParameter)baseInfo).Semantics = value; }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAInteractionParameterAttribute()
        {
            baseInfo = new HLAInteractionParameter();
        }
    }
}
