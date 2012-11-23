namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a region is unknown.
	/// </summary>
	[Serializable]
	public sealed class RegionNotKnown:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RegionNotKnown(System.String msg):base(msg)
		{
		}
	}
}