namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that the object class relevance advisory switch is off.
	/// </summary>
	[Serializable]
	public sealed class ObjectClassRelevanceAdvisorySwitchIsOff:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectClassRelevanceAdvisorySwitchIsOff(System.String msg):base(msg)
		{
		}
	}
}