namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IParameterHandle</code>s.  This factory is used only
	/// (outside of the run-time infrastructure) to Create 
	/// <code>IParameterHandle</code>s received as attribute or parameter values.
	/// </summary>
	public interface IParameterHandleFactory
	{
		/// <summary> 
		/// Decodes a parameter handle stored within the specified
		/// buffer.
		/// </summary>
		/// <param name="buffer">the buffer that contains the encoded parameter
		/// handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which the
		/// parameter handle is stored
		/// </param>
		/// <returns> a <code>IParameterHandle</code> representing the
		/// decoded handle
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the parameter handle could not be
		/// decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not
		/// a member of the execution
		/// </exception>
		IParameterHandle Decode(byte[] buffer, int offset);
	}
}