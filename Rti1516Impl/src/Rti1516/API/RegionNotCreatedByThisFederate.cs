namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a region was not created by this federate.
	/// </summary>
	[Serializable]
	public sealed class RegionNotCreatedByThisFederate:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RegionNotCreatedByThisFederate(System.String msg):base(msg)
		{
		}
	}
}