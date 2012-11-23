namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid transportation name.
	/// </summary>
	[Serializable]
	public sealed class InvalidTransportationName:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InvalidTransportationName(System.String msg):base(msg)
		{
		}
	}
}