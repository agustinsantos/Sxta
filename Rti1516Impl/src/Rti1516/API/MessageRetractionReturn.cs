namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Contains information concerning the validity of a message retraction
	/// handle.
	/// </summary>
	[Serializable]
	public struct MessageRetractionReturn
	{
		/// <summary> Whether or not the message retraction handle is valid.</summary>
		public bool retractionHandleIsValid;
		
		/// <summary> The message retraction handle.</summary>
		public IMessageRetractionHandle handle;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pRetractionHandleIsValid">whether or not the message
		/// retraction handle is valid
		/// </param>
		/// <param name="pHandle">the message retraction handle
		/// </param>
		public MessageRetractionReturn(bool pRetractionHandleIsValid, IMessageRetractionHandle pHandle)
		{
			retractionHandleIsValid = pRetractionHandleIsValid;
			handle = pHandle;
		}
	}
}