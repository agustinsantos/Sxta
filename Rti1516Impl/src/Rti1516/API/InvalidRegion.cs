namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid region.
	/// </summary>
	[Serializable]
	public sealed class InvalidRegion:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InvalidRegion(System.String msg):base(msg)
		{
		}
	}
}