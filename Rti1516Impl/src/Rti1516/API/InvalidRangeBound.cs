namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid range bound.
	/// </summary>
	[Serializable]
	public sealed class InvalidRangeBound:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InvalidRangeBound(System.String msg):base(msg)
		{
		}
	}
}