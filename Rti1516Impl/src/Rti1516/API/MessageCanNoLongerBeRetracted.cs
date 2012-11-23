namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a message can no longer be retracted.
	/// </summary>
	[Serializable]
	public sealed class MessageCanNoLongerBeRetracted:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public MessageCanNoLongerBeRetracted(System.String msg):base(msg)
		{
		}
	}
}