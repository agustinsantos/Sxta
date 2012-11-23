namespace Sxta.Rti1516.WinMain
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sxta.Rti1516.Reflection;


    [DefaultPropertyAttribute("Name")]
    public class HLAInteractionClassPropertiesInformation
    {
        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Name
        {
            get { return interactionInfo.Name; }
            set { interactionInfo.Name = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
        DisplayNameAttribute("Name Notes"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string NameNotes
        {
            get { return interactionInfo.NameNotes; }
            set { interactionInfo.NameNotes = value; }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Semantics
        {
            get
            {
                return interactionInfo.Semantics;
            }
            set
            {
                interactionInfo.Semantics = value;
            }
        }

        [CategoryAttribute("FOM-Configuration"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string SemanticsNotes
        {
            get
            {
                return interactionInfo.SemanticsNotes;
            }
            set
            {
                interactionInfo.SemanticsNotes = value;
            }
        }

        [CategoryAttribute("Code Information"),
            ReadOnlyAttribute(true),
            DescriptionAttribute("Todo.")]
        public string MethodName
        {
            get 
            {
                if (methodInfo != null)
                    return methodInfo.Name;
                else
                    return "No Method Defined";
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAInteractionClassPropertiesInformation(HLAinteractionClass interactionClass, System.Reflection.MethodInfo method)
        {
            interactionInfo = interactionClass;
            methodInfo = method;
        }

        HLAinteractionClass interactionInfo;
        System.Reflection.MethodInfo methodInfo;
    }
}
