namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IRegionHandleSet</code>s.
	/// </summary>
	public interface IRegionHandleSetFactory
	{
		/// <summary> 
		/// Creates and returns a new <code>IRegionHandleSet</code>.
		/// </summary>
		/// <returns> the newly created <code>IRegionHandleSet</code>
		/// </returns>
		IRegionHandleSet Create();
	}
}