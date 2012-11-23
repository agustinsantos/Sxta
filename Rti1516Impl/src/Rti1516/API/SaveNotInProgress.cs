namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that a save operation is not in progress.
	/// </summary>
	[Serializable]
	public sealed class SaveNotInProgress:RTIexception
	{
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SaveNotInProgress(System.String msg):base(msg)
		{
		}
	}
}