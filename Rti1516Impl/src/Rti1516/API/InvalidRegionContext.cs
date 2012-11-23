namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid region context.
	/// </summary>
	[Serializable]
	public sealed class InvalidRegionContext:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InvalidRegionContext(System.String msg):base(msg)
		{
		}
	}
}