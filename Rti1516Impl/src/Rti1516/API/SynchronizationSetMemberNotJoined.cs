namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that a synchronization set member has not joined.
	/// </summary>
	[Serializable]
	public sealed class SynchronizationSetMemberNotJoined:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SynchronizationSetMemberNotJoined(System.String msg):base(msg)
		{
		}
	}
}