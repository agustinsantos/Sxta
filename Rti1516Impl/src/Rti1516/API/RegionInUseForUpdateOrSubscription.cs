namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a region is in use for update or subscription.
	/// </summary>
	[Serializable]
	public sealed class RegionInUseForUpdateOrSubscription:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RegionInUseForUpdateOrSubscription(System.String msg):base(msg)
		{
		}
	}
}