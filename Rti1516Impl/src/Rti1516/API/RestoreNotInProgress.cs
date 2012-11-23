namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a restore operation is not in progress.
	/// </summary>
	[Serializable]
	public sealed class RestoreNotInProgress:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RestoreNotInProgress(System.String msg):base(msg)
		{
		}
	}
}