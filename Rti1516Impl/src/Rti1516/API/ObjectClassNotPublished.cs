namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that an object class is not published.
	/// </summary>
	[Serializable]
	public sealed class ObjectClassNotPublished:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectClassNotPublished(System.String msg):base(msg)
		{
		}
	}
}