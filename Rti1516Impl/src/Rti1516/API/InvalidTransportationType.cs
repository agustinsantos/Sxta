namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid transportation type.
	/// </summary>
	[Serializable]
	public sealed class InvalidTransportationType:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InvalidTransportationType(System.String msg):base(msg)
		{
		}
	}
}