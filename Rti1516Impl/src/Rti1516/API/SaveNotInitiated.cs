namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that a save operation has not been initiated.
	/// </summary>
	[Serializable]
	public sealed class SaveNotInitiated:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SaveNotInitiated(System.String msg):base(msg)
		{
		}
	}
}