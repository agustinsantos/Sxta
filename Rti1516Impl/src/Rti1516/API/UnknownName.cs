namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an unknown name.
	/// </summary>
	[Serializable]
	public sealed class UnknownName:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public UnknownName(System.String msg):base(msg)
		{
		}
	}
}