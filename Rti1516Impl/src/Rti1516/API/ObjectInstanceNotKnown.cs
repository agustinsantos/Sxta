namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that an object instance is unknown.
	/// </summary>
	[Serializable]
	public sealed class ObjectInstanceNotKnown:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectInstanceNotKnown(System.String msg):base(msg)
		{
		}
	}
}