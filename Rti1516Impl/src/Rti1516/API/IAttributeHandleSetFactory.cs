namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IAttributeHandleSet</code>s.
	/// </summary>
	public interface AttributeHandleSetFactory
	{
		/// <summary> 
		/// Creates and returns a new <code>IAttributeHandleSet</code>.
		/// </summary>
		/// <returns> the newly created <code>IAttributeHandleSet</code>
		/// </returns>
		IAttributeHandleSet Create();
	}
}