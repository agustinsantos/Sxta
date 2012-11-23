namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a save operation is in progress.
	/// </summary>
	[Serializable]
	public sealed class SaveInProgress:RTIexception
	{
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SaveInProgress(System.String msg):base(msg)
		{
		}
	}
}