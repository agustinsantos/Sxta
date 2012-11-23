namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that an object instance name is in use.
	/// </summary>
	[Serializable]
	public sealed class ObjectInstanceNameInUse:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectInstanceNameInUse(System.String msg):base(msg)
		{
		}
	}
}