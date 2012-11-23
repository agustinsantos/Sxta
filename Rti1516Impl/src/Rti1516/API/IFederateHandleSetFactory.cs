namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IFederateHandleSet</code> instances.
	/// </summary>
	public interface IFederateHandleSetFactory
	{
		/// <summary>
		///  Creates and returns a new <code>IFederateHandleSet</code>.
		/// </summary>
		/// <returns> the newly created <code>IFederateHandleSet</code>
		/// </returns>
		IFederateHandleSet Create();
	}
}