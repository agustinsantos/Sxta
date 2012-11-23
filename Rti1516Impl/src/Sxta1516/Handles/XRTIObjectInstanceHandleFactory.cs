using System;
using Hla.Rti1516;
using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;
namespace Sxta.Rti1516.XrtiHandles
{
	
	/// <summary> A factory for <code>IObjectInstanceHandle</code>s.  This factory is used only
	/// (outside of the run-time infrastructure) to Create
	/// <code>IObjectInstanceHandle</code>s received as attribute or parameter values.
	/// 
	/// </summary>
	/// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
	/// </author>
	
	[Serializable]
	public class XRTIObjectInstanceHandleFactory : IObjectInstanceHandleFactory
	{
		/// <summary> Decodes an object instance handle stored within the specified bufferStream,
		/// returning a new <code>IObjectInstanceHandle</code> instance representing
		/// the result.
		/// 
		/// </summary>
		/// <param name="bufferStream">the bufferStream that contains the encoded parameterValue
		/// </param>
		/// <param name="offset">the offset within the bufferStream at which the encoded parameterValue
		/// resides
		/// </param>
		/// <returns> an <code>IObjectInstanceHandle</code> corresponding to the decoded
		/// parameterValue
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member of
		/// the execution
		/// </exception>
		public virtual IObjectInstanceHandle Decode(byte[] buffer, int offset)
		{
			byte[] buf = new byte[8];
			
			Array.Copy(buffer, offset, buf, 0, 8);
			
			return new XRTIObjectInstanceHandle(EncodingHelpers.DecodeLong(buf));
		}
	}
}