namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Factory for <code>IAttributeSetRegionSetPairList</code> instances.
	/// </summary>
	public interface IAttributeSetRegionSetPairListFactory
	{
		/// <summary> 
		/// Creates and returns a new <code>IAttributeSetRegionSetPairList</code>
		/// instance with the specified initial capacity.
		/// </summary>
		/// <param name="capacity">the initial capacity of the list
		/// </param>
		/// <returns> the newly created list
		/// </returns>
		IAttributeSetRegionSetPairList Create(int capacity);
	}
}