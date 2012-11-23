namespace Sxta.Rti1516.Reflection
{
    using System;
    using Hla.Rti1516;

    /// <summary> 
    /// Describes a parameter.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class ParameterDescriptor : HLAInteractionParameter
    {
        /// <summary>
        ///  Returns the name of this parameter.
        /// </summary>
        /// <returns> the name of this parameter
        /// </returns>
        //virtual public System.String Name
        //{
        //    get { return parameter.Name; }
        //}

        /// <summary>
        ///  Returns the handle of this parameter.
        /// </summary>
        /// <returns> the handle of this parameter
        /// </returns>
        virtual public IParameterHandle Handle
        {
            get { return handle; }
        }

        /// <summary> The handle of the parameter.</summary>
        private IParameterHandle handle;

        //private HLAInteractionParameter parameter;

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the parameter
        /// </param>
        public ParameterDescriptor(System.String pName, IParameterHandle pHandle)
        {
            //parameter = new HLAInteractionParameter();

            //TODO. Fullfill the native type!!
            this.Name = pName;
            handle = pHandle;
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the parameter
        /// </param>
        public ParameterDescriptor(HLAInteractionParameter param, IParameterHandle pHandle)
            : base(param)
        {
            this.handle = pHandle;
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the parameter
        /// </param>
        public ParameterDescriptor(System.Xml.XmlElement parameterElement, IParameterHandle pHandle)
            : base(parameterElement)
        {
            this.handle = pHandle;
        }

    }
}