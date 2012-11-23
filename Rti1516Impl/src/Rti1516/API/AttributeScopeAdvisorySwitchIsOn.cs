namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception thrown when the attribute scope advisory switch is on.
	/// </summary>
	[Serializable]
	public sealed class AttributeScopeAdvisorySwitchIsOn:RTIexception
	{
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public AttributeScopeAdvisorySwitchIsOn(System.String msg):base(msg)
		{
		}
	}
}