namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating the inability to perform a save operation.
	/// </summary>
	[Serializable]
	public sealed class UnableToPerformSave:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public UnableToPerformSave(System.String msg):base(msg)
		{
		}
	}
}