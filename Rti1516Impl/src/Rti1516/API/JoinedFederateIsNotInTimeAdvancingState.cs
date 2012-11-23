namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that a joined federate is not in the time-advancing state.
	/// </summary>
	[Serializable]
	public sealed class JoinedFederateIsNotInTimeAdvancingState:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public JoinedFederateIsNotInTimeAdvancingState(System.String msg):base(msg)
		{
		}
	}
}