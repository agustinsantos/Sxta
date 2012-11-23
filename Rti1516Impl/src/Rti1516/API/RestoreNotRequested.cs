namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a restore operation was not requested.
	/// </summary>
	[Serializable]
	public sealed class RestoreNotRequested:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RestoreNotRequested(System.String msg):base(msg)
		{
		}
	}
}