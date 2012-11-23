namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>IObjectClassHandle</code>s.  This factory is used only
	/// (outside of the run-time infrastructure) to Create <code>IObjectClassHandle</code>s
	/// received as attribute or parameter values.
	/// </summary>
	public interface IObjectClassHandleFactory
	{
		/// <summary> 
		/// Decodes an object class handle stored within the specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer that contains the encoded handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which the encoded
		/// handle is located
		/// </param>
		/// <returns> an <code>IObjectClassHandle</code> representing the decoded
		/// handle
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of the execution
		/// </exception>
		IObjectClassHandle Decode(byte[] buffer, int offset);
	}
}