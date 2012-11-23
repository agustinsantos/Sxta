namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a region does not contain a specified dimension.
	/// </summary>
	[Serializable]
	public sealed class RegionDoesNotContainSpecifiedDimension:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RegionDoesNotContainSpecifiedDimension(System.String msg):base(msg)
		{
		}
	}
}