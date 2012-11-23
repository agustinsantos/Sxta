namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IParameterHandleValueMap</code> instances.
	/// </summary>
	public interface IParameterHandleValueMapFactory
	{
		/// <summary> Creates a new <code>IParameterHandleValueMap</code> instance with 
		/// the specified initial capacity.
		/// 
		/// </summary>
		/// <param name="capacity">the initial map capacity
		/// </param>
		/// <returns> the newly created <code>IParameterHandleValueMap</code>
		/// </returns>
		IParameterHandleValueMap Create(int capacity);
	}
}