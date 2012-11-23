namespace Sxta.Rti1516.WinMain
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sxta.Rti1516.Reflection;

    /// <summary>
    /// Indicates that class represents a HLAObjectClass.
    /// </summary>
    [DefaultPropertyAttribute("Name")]
    public class HLAObjectClassPropertiesInformation
    {

        [CategoryAttribute("FOM-Configuration"), 
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Name
        {
            get
            {
                return objectClassInfo.Name;
            }
            set
            {
                objectClassInfo.Name = value;
            }
        }

        [CategoryAttribute("Code Information"), 
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string ClassName
        {
            get
            {
                return className;
            }
        }

        [CategoryAttribute("FOM-Configuration"), 
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string NameNotes
        {
            get
            {
                return objectClassInfo.NameNotes;
            }
            set
            {
                objectClassInfo.NameNotes = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"), 
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public HLAsharingType Sharing
        {
            get
            {
                return objectClassInfo.Sharing;
            }
            set
            {
                objectClassInfo.Sharing = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string SharingNotes
        {
            get
            {
                return objectClassInfo.SharingNotes;
            }
            set
            {
                objectClassInfo.SharingNotes = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string SemanticsNotes
        {
            get
            {
                return objectClassInfo.SharingNotes;
            }
            set
            {
                objectClassInfo.SharingNotes = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"), 
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Semantics
        {
            get
            {
                return objectClassInfo.Semantics;
            }
            set
            {
                objectClassInfo.Semantics = value;
            }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAObjectClassPropertiesInformation(HLAObjectClass objClass, string fullClassName)
        {
            objectClassInfo = objClass;
            className = fullClassName;
        }

        protected HLAObjectClass objectClassInfo;
        protected string className;

    }
}
