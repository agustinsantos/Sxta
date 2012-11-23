namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that time-constrained mode is not enabled.
	/// </summary>
	[Serializable]
	public sealed class TimeConstrainedIsNotEnabled:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public TimeConstrainedIsNotEnabled(System.String msg):base(msg)
		{
		}
	}
}