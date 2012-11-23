namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a request for time regulation is pending.
	/// </summary>
	[Serializable]
	public sealed class RequestForTimeRegulationPending:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RequestForTimeRegulationPending(System.String msg):base(msg)
		{
		}
	}
}