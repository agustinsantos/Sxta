namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that no request to enable time-constrained mode was
	/// pending.
	/// </summary>
	[Serializable]
	public sealed class NoRequestToEnableTimeConstrainedWasPending:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public NoRequestToEnableTimeConstrainedWasPending(System.String msg):base(msg)
		{
		}
	}
}