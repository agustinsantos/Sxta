namespace Sxta.Rti1516.WinMain
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sxta.Rti1516.Reflection;

    // PATCH ANGEL

    [DefaultPropertyAttribute("Value")]
    public class HLAAttributePropertiesInformationValue : HLAAttributePropertiesInformation
    {
        protected object value;

        [CategoryAttribute("Code Information"),
            ReadOnlyAttribute(true),
            DescriptionAttribute("Property's value")]
        public string Value
        {
            get 
            {
                if (value != null)
                    return value.ToString();
                else
                    return "No Value Assigned";
            }
        }

        public object ObjectValue
        {
            get { return value; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAAttributePropertiesInformationValue(IHLAattribute attr, System.Reflection.PropertyInfo method, object pValue)
            : base(attr, method)
        {
            this.value = pValue;
        }
    }

    // END PATCH

    [DefaultPropertyAttribute("Name")]
    public class HLAAttributePropertiesInformation
    {
        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Name
        {
            get { return attrInfo.Name; }
            set { attrInfo.Name = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
        DisplayNameAttribute("Name Notes"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string NameNotes
        {
            get { return attrInfo.NameNotes; }
            set { attrInfo.NameNotes = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public HLAsharingType Sharing
        {
            get { return attrInfo.Sharing; }
            set { attrInfo.Sharing = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
            ReadOnlyAttribute(true),
            DescriptionAttribute("Todo.")]
        public string SharingNotes
        {
            get { return attrInfo.SharingNotes; }
            set { attrInfo.SharingNotes = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string SemanticsNotes
        {
            get
            {
                return attrInfo.SemanticsNotes;
            }
            set
            {
                attrInfo.SemanticsNotes = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Semantics
        {
            get
            {
                return attrInfo.Semantics;
            }
            set
            {
                attrInfo.Semantics = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string DataType
        {
            get
            {
                return attrInfo.DataType;
            }
            set
            {
                attrInfo.DataType = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public HLAupdateType UpdateType
        {
            get
            {
                return attrInfo.UpdateType;
            }
            set
            {
                attrInfo.UpdateType = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string UpdateCondition
        {
            get
            {
                return attrInfo.UpdateCondition;
            }
            set
            {
                attrInfo.UpdateCondition = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public HLAownershipType OwnerShip
        {
            get
            {
                return attrInfo.Ownership;
            }
            set
            {
                attrInfo.Ownership = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Dimensions
        {
            get
            {
                return attrInfo.Dimensions;
            }
            set
            {
                attrInfo.Dimensions = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Transportation
        {
            get
            {
                return attrInfo.Transportation;
            }
            set
            {
                attrInfo.Transportation = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public HLAorderType Order
        {
            get
            {
                return attrInfo.Order;
            }
            set
            {
                attrInfo.Order = value;
            }
        }


        [CategoryAttribute("Code Information"),
            ReadOnlyAttribute(true),
            DescriptionAttribute("Todo.")]
        public string PropertyName
        {
            get 
            {
                if (propInfo != null)
                    return propInfo.Name;
                else
                    return "No Method Defined";
            }
        }

        [CategoryAttribute("Code Information"),
            ReadOnlyAttribute(true),
            DescriptionAttribute("Todo.")]
        public string ValueTypeName
        {
            get
            {
                if (propInfo != null)
                    return propInfo.PropertyType.Name;
                else
                    return "No Method Defined";
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAAttributePropertiesInformation(IHLAattribute attr, System.Reflection.PropertyInfo method)
        {
            attrInfo = attr;
            propInfo = method;
        }

        protected IHLAattribute attrInfo;
        protected System.Reflection.PropertyInfo propInfo;
    }
}
