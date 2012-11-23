namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that ownership acquisition is pending.
	/// </summary>
	[Serializable]
	public sealed class OwnershipAcquisitionPending:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public OwnershipAcquisitionPending(System.String msg):base(msg)
		{
		}
	}
}