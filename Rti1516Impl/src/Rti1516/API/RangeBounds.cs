namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a numerical range.
	/// </summary>
	[Serializable]
	public struct RangeBounds
	{
		/// <summary> The lower bound of the range.</summary>
		public long Lower;
		
		/// <summary> The upper bound of the range.</summary>
		public long Upper;

        /// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pLower">the lower bound of the range
		/// </param>
		/// <param name="pUpper">the upper bound of the range
		/// </param>
		public RangeBounds(long pLower, long pUpper)
		{
			Lower = pLower;
			Upper = pUpper;
		}
	}
}