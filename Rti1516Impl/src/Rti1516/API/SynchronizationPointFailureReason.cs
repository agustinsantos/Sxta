using System;
namespace Hla.Rti1516
{
	
	/// <summary> 
	/// Represents the reason for the failure of a synchronization point.
	/// </summary>
	[Serializable]
	public sealed class SynchronizationPointFailureReason
	{
		/// <summary> The synchronization point label is not unique.</summary>
		public static readonly SynchronizationPointFailureReason SYNCHRONIZATION_POINT_LABEL_NOT_UNIQUE = new SynchronizationPointFailureReason(1);
		
		/// <summary> A synchronization set member is not joined.</summary>
		public static readonly SynchronizationPointFailureReason SYNCHRONIZATION_SET_MEMBER_NOT_JOINED = new SynchronizationPointFailureReason(2);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherReason">the synchronization point failure reason object to copy
		/// </param>
		public SynchronizationPointFailureReason(SynchronizationPointFailureReason otherReason)
		{
			val = otherReason.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this synchronization point failure
		/// reason
		/// </param>
		private SynchronizationPointFailureReason(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this synchronization point failure reason for equality with another.
		/// </summary>
		/// <param name="otherReason">the other synchronization point failure reason
		/// </param>
		/// <returns> <code>true</code> if the two synchronization point failure reasons are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherReason)
		{
			try
			{
				return (val == ((SynchronizationPointFailureReason) otherReason).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this synchronization point failure
		/// reason.
		/// </summary>
		/// <returns> a hash code corresponding to this synchronization point failure reason
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this synchronization point failure reason.
		/// </summary>
		/// <returns> a string representation of this synchronization point failure reason
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(SYNCHRONIZATION_POINT_LABEL_NOT_UNIQUE))
			{
				return "The synchronization point label is not unique.";
			}
			// this.Equals(SYNCHRONIZATION_SET_MEMBER_NOT_JOINED)
			else
			{
				return "A synchronization set member is not joined.";
			}
		}
	}
}