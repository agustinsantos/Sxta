namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a name was not found.
	/// </summary>
	[Serializable]
	public sealed class NameNotFound:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public NameNotFound(System.String msg):base(msg)
		{
		}
	}
}