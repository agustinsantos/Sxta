namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that the specified save label does not exist.
	/// </summary>
	[Serializable]
	public sealed class SpecifiedSaveLabelDoesNotExist:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public SpecifiedSaveLabelDoesNotExist(System.String msg):base(msg)
		{
		}
	}
}