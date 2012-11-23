namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a logical time already passed.
	/// </summary>
	[Serializable]
	public sealed class LogicalTimeAlreadyPassed:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public LogicalTimeAlreadyPassed(System.String msg):base(msg)
		{
		}
	}
}