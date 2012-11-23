namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that an object class is not recognized.
	/// </summary>
	[Serializable]
	public sealed class ObjectClassNotRecognized:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectClassNotRecognized(System.String msg):base(msg)
		{
		}
	}
}