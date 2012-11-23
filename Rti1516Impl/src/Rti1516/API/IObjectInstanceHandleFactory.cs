using System;
namespace Hla.Rti1516
{
	
	/// <summary> 
	/// A factory for <code>IObjectInstanceHandle</code>s.  This factory is used only
	/// (outside of the run-time infrastructure) to Create
	/// <code>IObjectInstanceHandle</code>s received as attribute or parameter values.
	/// </summary>
	public interface IObjectInstanceHandleFactory
	{
		/// <summary> 
		/// Decodes an object instance handle stored within the specified buffer,
		/// returning a new <code>IObjectInstanceHandle</code> instance representing
		/// the result.
		/// </summary>
		/// <param name="buffer">the buffer that contains the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which the encoded value
		/// resides
		/// </param>
		/// <returns> an <code>IObjectInstanceHandle</code> corresponding to the decoded
		/// value
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of
		/// the execution
		/// </exception>
		IObjectInstanceHandle Decode(byte[] buffer, int offset);
	}
}