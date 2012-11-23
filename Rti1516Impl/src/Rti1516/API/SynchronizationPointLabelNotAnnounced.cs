namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that a synchronization point label was not announced.
	/// </summary>
	[Serializable]
	public sealed class SynchronizationPointLabelNotAnnounced:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SynchronizationPointLabelNotAnnounced(System.String msg):base(msg)
		{
		}
	}
}