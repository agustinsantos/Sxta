namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a request for time-constrained mode is pending.
	/// </summary>
	[Serializable]
	public sealed class RequestForTimeConstrainedPending:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RequestForTimeConstrainedPending(System.String msg):base(msg)
		{
		}
	}
}