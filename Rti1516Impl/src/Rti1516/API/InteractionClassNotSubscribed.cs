namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An exception indicating that an interaction class was not subscribed.
	/// </summary>
	[Serializable]
	public sealed class InteractionClassNotSubscribed:RTIexception
	{
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="msg">a detailed description of the exception
		/// </param>
		public InteractionClassNotSubscribed(System.String msg):base(msg)
		{
		}
	}
}