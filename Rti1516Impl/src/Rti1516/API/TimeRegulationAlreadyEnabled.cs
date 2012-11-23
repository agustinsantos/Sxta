namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that time regulation is already enabled.
	/// </summary>
	[Serializable]
	public sealed class TimeRegulationAlreadyEnabled:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public TimeRegulationAlreadyEnabled(System.String msg):base(msg)
		{
		}
	}
}