namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that an object class is not defined.
	/// </summary>
	[Serializable]
	public sealed class ObjectClassNotDefined:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectClassNotDefined(System.String msg):base(msg)
		{
		}
	}
}