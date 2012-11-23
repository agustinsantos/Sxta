using System;
namespace Hla.Rti1516
{
	
	/// <summary> 
	/// Factory for <code>IAttributeHandleValueMap</code> instances.
	/// </summary>
	public interface IAttributeHandleValueMapFactory
	{
		/// <summary> .
		/// Creates a new <code>IAttributeHandleValueMap</code> with the
		/// specified initial capacity
		/// </summary>
		/// <param name="capacity">the initial capacity of the map
		/// </param>
		/// <returns> the newly created <code>AttributeHandleMap</code>
		/// </returns>
		IAttributeHandleValueMap Create(int capacity);
	}
}