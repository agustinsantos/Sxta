namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IDimensionHandleSet</code> instances.
	/// </summary>
	public interface IDimensionHandleSetFactory
	{
		/// <summary> 
		/// Creates and returns a new <code>IDimensionHandleSet</code>.
		/// </summary>
		/// <returns> the newly created <code>IDimensionHandleSet</code>
		/// </returns>
		IDimensionHandleSet Create();
	}
}