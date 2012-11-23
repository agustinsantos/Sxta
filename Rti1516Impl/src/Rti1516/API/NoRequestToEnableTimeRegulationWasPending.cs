namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that no request to enable time regulation was pending.
	/// </summary>
	[Serializable]
	public sealed class NoRequestToEnableTimeRegulationWasPending:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public NoRequestToEnableTimeRegulationWasPending(System.String msg):base(msg)
		{
		}
	}
}