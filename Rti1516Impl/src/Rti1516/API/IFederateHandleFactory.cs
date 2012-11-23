namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// This factory is used only (outside of the RTI) to Create
	/// <code>IFederateHandle</code> objects corresponding to federate
	/// handles received as attribute or parameter values.
	/// </summary>
	public interface IFederateHandleFactory
	{
		/// <summary> 
		/// Decodes a federate handle within the specified buffer and
		/// returns a corresponding <code>IFederateHandle</code> instance.
		/// </summary>
		/// <param name="buffer">the buffer from which to Decode the federate handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which the encoded
		/// federate handle resides
		/// </param>
		/// <returns> the new <code>IFederateHandle</code> instance corresponding
		/// to the encoded handle
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the federate handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of the execution
		/// </exception>
		IFederateHandle Decode(byte[] buffer, int offset);
	}
}