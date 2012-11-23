namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// This factory is used only (outside of the RTI) to Create
	/// <code>IDimensionHandle</code>s received as attribute or parameter
	/// values.
	/// </summary>
	public interface IDimensionHandleFactory
	{
		/// <summary> 
		/// Decodes a dimension handle within the specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer containing the encoded handle
		/// </param>
		/// <param name="offset">the location of the handle within the buffer
		/// </param>
		/// <returns> the <code>IDimensionHandle</code> instance 
		/// corresponding to the decoded handle
		/// </returns>
		/// <exception cref="CouldNotDecode">  if the dimension handle could not
		/// be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember">  if the federate is not
		/// a member of the execution
		/// </exception>
		IDimensionHandle Decode(byte[] buffer, int offset);
	}
}