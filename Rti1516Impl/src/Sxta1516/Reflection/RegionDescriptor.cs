namespace Sxta.Rti1516.Reflection
{
    using System;
    using Hla.Rti1516;

    /// <summary>
    ///  Describes a region.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class RegionDescriptor
    {
        /// <summary>
        ///  Returns the handle of this region.
        /// </summary>
        /// <returns> the handle of this region.
        /// </returns>
        virtual public IRegionHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the dimensions of this region.
        /// </summary>
        /// <returns> the dimensions of this region
        /// </returns>
        virtual public IDimensionHandleSet Dimensions
        {
            get { return dimensions; }
        }

        /// <summary> The handle of the region.</summary>
        private IRegionHandle handle;

        /// <summary> The dimensions of the region.</summary>
        private IDimensionHandleSet dimensions;

        /// <summary> Maps dimension handles to range bounds.</summary>
        //UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMap_3"'
        private System.Collections.Hashtable dimensionRangeBoundsMap;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pHandle">the handle of the region
        /// </param>
        /// <param name="pDimensions">the dimensions of the region
        /// </param>
        public RegionDescriptor(IRegionHandle pHandle, IDimensionHandleSet pDimensions)
        {
            handle = pHandle;
            dimensions = pDimensions;

            //UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilHashMap_3"'
            dimensionRangeBoundsMap = new System.Collections.Hashtable();

            System.Collections.IEnumerator it = dimensions.GetEnumerator();

            //UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073_javautilIteratorhasNext_3"'
            while (it.MoveNext())
            {
                RangeBounds rb = new RangeBounds(System.Int64.MinValue, System.Int64.MaxValue);
                dimensionRangeBoundsMap[(IDimensionHandle)it.Current] = rb;
            }
        }

        /// <summary> 
        /// Sets the range bounds for the specified dimension.
        /// </summary>
        /// <param name="dimension">the handle of the dimension
        /// </param>
        /// <param name="bounds">the new bounds for the dimension
        /// </param>
        public virtual void setRangeBounds(IDimensionHandle dimension, RangeBounds bounds)
        {
            dimensionRangeBoundsMap[dimension] = bounds;
        }

        /// <summary> 
        /// Returns the range bounds for the specified dimension.
        /// </summary>
        /// <param name="dimension">the handle of the dimension of interest
        /// </param>
        /// <returns> the range bounds of the specified dimension, or <code>null</code> if
        /// no such dimension exists
        /// </returns>
        public virtual RangeBounds getRangeBounds(IDimensionHandle dimension)
        {
            return (RangeBounds)dimensionRangeBoundsMap[dimension];
        }
    }
}