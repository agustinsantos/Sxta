using System;
using Hla.Rti1516;
using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;
namespace Sxta.Rti1516.XrtiHandles
{
	/// <summary> 
	/// A factory for <code>IParameterHandle</code>s.  This factory is used only
	/// (outside of the run-time infrastructure) to Create 
	/// <code>IParameterHandle</code>s received as attribute or parameter values.
	/// </summary>
	/// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
	/// </author>
	[Serializable]
	public class XRTIParameterHandleFactory : IParameterHandleFactory
	{
		/// <summary> 
		/// Decodes a parameter handle stored within the specified
		/// bufferStream.
		/// </summary>
		/// <param name="bufferStream">the bufferStream that contains the encoded parameter
		/// handle
		/// </param>
		/// <param name="offset">the offset within the bufferStream at which the
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
		public virtual IParameterHandle Decode(byte[] buffer, int offset)
		{
			return new XRTIParameterHandle(BitConverter.ToInt64(buffer, offset));
		}
	}
}