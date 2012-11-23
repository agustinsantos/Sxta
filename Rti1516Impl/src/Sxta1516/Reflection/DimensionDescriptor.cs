namespace Sxta.Rti1516.Reflection
{
    using System;
    using Hla.Rti1516;
    using HLAdimension = Sxta.Rti1516.Reflection.HLAdimension;

    /// <summary> 
    /// Describes a dimension.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class DimensionDescriptor
    {
        /// <summary> 
        /// Returns the name of this dimension.
        /// </summary>
        /// <returns> the name of this dimension
        /// </returns>
        virtual public System.String Name
        {
            get { return name; }
        }

        /// <summary> 
        /// Returns the handle of this dimension.
        /// </summary>
        /// <returns> the handle of this dimension
        /// </returns>
        virtual public IDimensionHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the upper bound of this dimension.
        /// </summary>
        /// <returns> the upper bound of this dimension
        /// </returns>
        virtual public long UpperBound
        {
            get { return upperBound; }
        }

        /// <summary> The name of the dimension.</summary>
        private System.String name;

        /// <summary> The handle of the dimension.</summary>
        private IDimensionHandle handle;

        /// <summary> The upper bound of the dimension.</summary>
        private long upperBound;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the dimension
        /// </param>
        /// <param name="pHandle">the handle of the dimension
        /// </param>
        /// <param name="pUpperBound">the upper bound of the dimension
        /// </param>
        public DimensionDescriptor(System.String pName, IDimensionHandle pHandle, long pUpperBound)
        {
            name = pName;
            handle = pHandle;
            upperBound = pUpperBound;
        }
    }
}