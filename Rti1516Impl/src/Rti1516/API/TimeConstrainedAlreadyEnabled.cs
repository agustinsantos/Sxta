namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that time-constrained mode has already been enabled.
	/// </summary>
	[Serializable]
	public sealed class TimeConstrainedAlreadyEnabled:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public TimeConstrainedAlreadyEnabled(System.String msg):base(msg)
		{
		}
	}
}