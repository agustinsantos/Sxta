namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating that the object class relevance advisory switch is on.
	/// </summary>
	[Serializable]
	public sealed class ObjectClassRelevanceAdvisorySwitchIsOn:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public ObjectClassRelevanceAdvisorySwitchIsOn(System.String msg):base(msg)
		{
		}
	}
}