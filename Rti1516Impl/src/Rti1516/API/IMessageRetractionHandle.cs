namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A handle associated with messages that may be used to request and
	/// perform retractions.
	/// </summary>
	public interface IMessageRetractionHandle
	{
		/// <summary> 
		/// Checks this message retraction handle for equality with another.
		/// </summary>
		/// <param name="otherMessageRetractionHandle">the other message retraction handle
		/// to compare this to
		/// </param>
		/// <returns> <code>true</code> if the message retraction handles are equal,
		/// <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherMessageRetractionHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this message
		/// retraction handle
		/// </summary>
		/// <returns> a hash code corresponding to this message retraction
		/// handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns a string representation of this message retraction handle.
		/// </summary>
		/// <returns> a string representation of this message retraction handle
		/// </returns>
		System.String ToString();
	}
}