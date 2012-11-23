namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// This factory is used only (outside of the RTI) to Create 
	/// <code>IInteractionClassHandle</code>s corresponding to interaction
	/// class handles received as attribute or parameter values.
	/// </summary>
	public interface IInteractionClassHandleFactory
	{
		/// <summary>
		///  Decodes an interaction class handle contained within the specified
		/// buffer, returning a corresponding instance of 
		/// <code>IInteractionClassHandle</code>.
		/// </summary>
		/// <param name="buffer">the buffer that contains the encoded handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which the handle is stored
		/// </param>
		/// <returns> an instance of <code>IInteractionClassHandle</code> corresponding
		/// to the encoded handle
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the interaction class handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of the
		/// execution
		/// </exception>
		IInteractionClassHandle Decode(byte[] buffer, int offset);
	}
}