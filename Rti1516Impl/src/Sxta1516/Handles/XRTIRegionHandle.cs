namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    /// <summary> 
    /// A type-safe handle for a region.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public struct XRTIRegionHandle : IRegionHandle
    {
        /// <summary> 
        /// Returns this handle's unique identifier.
        /// </summary>
        /// <returns> this handle's unique identifier
        /// </returns>
        internal long Identifier
        {
            get
            {
                return identifier;
            }

        }

        /// <summary> The region identifier.</summary>
        private long identifier;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pIdentifier">the region identifier
        /// </param>
        internal XRTIRegionHandle(long pIdentifier)
        {
            identifier = pIdentifier;
        }

        /// <summary> 
        /// Checks this region handle for equality with another.
        /// </summary>
        /// <param name="otherRegionHandle">the other region handle
        /// </param>
        /// <returns> <code>true</code> if the two handles represent
        /// the same region, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherRegionHandle)
        {
            try
            {
                return (identifier == ((XRTIRegionHandle)otherRegionHandle).identifier);
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this
        /// region handle.
        /// </summary>
        /// <returns> the hash code corresponding to this region handle
        /// </returns>
        public override int GetHashCode()
        {
            return (int)identifier;
        }

        /// <summary>
        ///  Returns a string representation of this region handle.
        /// </summary>
        /// <returns> a string representation of this region handle
        /// </returns>
        public override System.String ToString()
        {
            return "region handle #" + identifier;
        }
    }
}