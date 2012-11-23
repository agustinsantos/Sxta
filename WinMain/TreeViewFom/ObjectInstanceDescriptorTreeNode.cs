namespace Sxta.Rti1516.WinMain
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sxta.Rti1516.Reflection;

    /// <summary>
    /// Indicates that class represents a HLAObjectClass.
    /// </summary>
    public class ObjectInstanceDescriptorPropertiesInformation
    {

        [CategoryAttribute("General Information"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        public string Name
        {
            get
            {
                return info.Name;
            }
        }


        /// <summary> 
        /// Returns the handle of this object instance.
        /// </summary>
        /// <returns> the handle of this object instance
        /// </returns>
        [CategoryAttribute("General Information"),
                ReadOnlyAttribute(true),
                DescriptionAttribute("Todo.")]
        virtual public string Handle
        {
            get { return info.Handle.ToString(); }
        }

        /// <summary> 
        /// Returns the handle of the object instance class.
        /// </summary>
        /// <returns> the handle of the object instance class
        /// </returns>
        [CategoryAttribute("General Information"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("Todo.")]
        virtual public string ClassHandle
        {
            get { return info.ClassHandle.ToString(); }
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ObjectInstanceDescriptorPropertiesInformation(ObjectInstanceDescriptor obj)
        {
            info = obj;
        }

        protected ObjectInstanceDescriptor info;
    }
}
