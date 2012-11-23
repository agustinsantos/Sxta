namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating an internal error in the run-time infrastructure.
	/// </summary>
	[Serializable]
	public class RTIinternalError:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public RTIinternalError(System.String msg):base(msg)
		{
		}
	}
}