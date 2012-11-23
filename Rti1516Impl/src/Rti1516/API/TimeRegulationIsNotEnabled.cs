namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that time regulation is not enabled.
	/// </summary>
	[Serializable]
	public sealed class TimeRegulationIsNotEnabled:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public TimeRegulationIsNotEnabled(System.String msg):base(msg)
		{
		}
	}
}