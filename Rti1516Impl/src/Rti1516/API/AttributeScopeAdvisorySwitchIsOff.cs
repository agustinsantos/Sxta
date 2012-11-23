namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception thrown when the attribute scope advisory switch is off.
	/// </summary>
	[Serializable]
	public sealed class AttributeScopeAdvisorySwitchIsOff:RTIexception
	{
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public AttributeScopeAdvisorySwitchIsOff(System.String msg):base(msg)
		{
		}
	}
}