namespace Sxta.Rti1516.Reflection
{
    using Hla.Rti1516;

    [HLAObjectClassAttribute(Name = "HLAInteractionParameter",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "Represents an HLA interaction parameter.")]
    public class HLAInteractionParameter : HLAreflection
    {
        [HLAAttribute(Name = "dataType",
                      Semantics = "TODO.")]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        [HLAAttribute(Name = "dataTypeNotes",
                      Semantics = "TODO.")]
        public string DataTypeNotes
        {
            get { return dataTypeNotes; }
            set { dataTypeNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "semantics",
                      Semantics = "TODO.")]
        public string Semantics
        {
            get { return semantics; }
            set { semantics = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "semanticsNotes",
                      Semantics = "TODO.")]
        public string SemanticsNotes
        {
            get { return semanticsNotes; }
            set { semanticsNotes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [HLAAttribute(Name = "nativeType",
                      Semantics = "TODO.")]
        public System.Type NativeType
        {
            get { return nativeType; }
            set { nativeType = value; }
        }

        public HLAInteractionParameter()
        {
        }

        public HLAInteractionParameter(HLAInteractionParameter param)
        {
            this.Name = param.Name;
            this.NameNotes = param.NameNotes;
            this.dataType = param.DataType;
            this.dataTypeNotes = param.DataTypeNotes;
            this.semantics = param.Semantics;
            this.semanticsNotes = param.SemanticsNotes;
            this.nativeType = param.NativeType;
        }

        public HLAInteractionParameter(System.Xml.XmlElement parameterElement)
            : base(parameterElement)
        {
            this.DataType = parameterElement.GetAttribute("dataType");
            this.DataTypeNotes = parameterElement.GetAttribute("dataTypeNotes");
            this.Semantics = ReplaceNewLines(parameterElement.GetAttribute("semantics"));
            this.SemanticsNotes = parameterElement.GetAttribute("semanticsNotes");
        }

        protected string dataType;
        protected string dataTypeNotes;
        protected string semantics;
        protected string semanticsNotes;
        protected System.Type nativeType;
    }
}
