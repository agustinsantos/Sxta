namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// This packages the attributes supplied to the RTI for various DDM services with
	/// the regions to be used with the attributes.  Elements are 
	/// <code>AttributeRegionAssociation</code>s.  All operations are required, none optional.
	/// Methods should throw <code>IllegalArgumentException</code> to enforce type of elements.
	/// </summary>
    public interface IAttributeSetRegionSetPairList : System.Collections.Generic.ICollection<AttributeRegionAssociation>
	{
	}
}