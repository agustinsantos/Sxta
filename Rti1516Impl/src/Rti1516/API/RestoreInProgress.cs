namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a restore operation is in progress.
	/// </summary>
	[Serializable]
	public sealed class RestoreInProgress:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RestoreInProgress(System.String msg):base(msg)
		{
		}
	}
}