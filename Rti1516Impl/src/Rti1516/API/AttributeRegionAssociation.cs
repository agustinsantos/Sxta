namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An object that contains an <code>AttributeSet</code> and a
	/// <code>RegionSet</code>.
	/// </summary>
	[Serializable]
	public sealed class AttributeRegionAssociation
	{
		/// <summary> The attribute set.</summary>
		public IAttributeHandleSet ahset;
		
		/// <summary> The region set.</summary>
		public IRegionHandleSet rhset;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pAHset">the attribute set
		/// </param>
		/// <param name="pRHset">the region set
		/// </param>
		public AttributeRegionAssociation(IAttributeHandleSet pAHset, IRegionHandleSet pRHset)
		{
			ahset = pAHset;
			rhset = pRHset;
		}
	}
}