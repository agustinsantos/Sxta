namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that an object instance name has not been reserved.
	/// </summary>
	[Serializable]
	public sealed class ObjectInstanceNameNotReserved:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectInstanceNameNotReserved(System.String msg):base(msg)
		{
		}
	}
}