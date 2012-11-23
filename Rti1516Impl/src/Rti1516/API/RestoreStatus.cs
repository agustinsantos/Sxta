namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a restoration status.
	/// </summary>
	[Serializable]
	public sealed class RestoreStatus
	{
		/// <summary> No restoration operation is in progress.</summary>
		public static readonly RestoreStatus NO_RESTORE_IN_PROGRESS = new RestoreStatus(1);
		
		/// <summary> The federate's request for a restoration operation is pending.</summary>
		public static readonly RestoreStatus FEDERATE_RESTORE_REQUEST_PENDING = new RestoreStatus(2);
		
		/// <summary> The federate is waiting for a restoration operation to begin.</summary>
		public static readonly RestoreStatus FEDERATE_WAITING_FOR_RESTORE_TO_BEGIN = new RestoreStatus(3);
		
		/// <summary> The federate is prepared to perform a restoration operation.</summary>
		public static readonly RestoreStatus FEDERATE_PREPARED_TO_RESTORE = new RestoreStatus(4);
		
		/// <summary> The federate is currently performing a restoration operation.</summary>
		public static readonly RestoreStatus FEDERATE_RESTORING = new RestoreStatus(5);
		
		/// <summary> The federate is waiting for the federation to be restored.</summary>
		public static readonly RestoreStatus FEDERATE_WAITING_FOR_FEDERATION_TO_RESTORE = new RestoreStatus(6);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherRestoreStatus">the restoration status object to copy
		/// </param>
		public RestoreStatus(RestoreStatus otherRestoreStatus)
		{
			val = otherRestoreStatus.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this restoration status
		/// </param>
		private RestoreStatus(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this restoration status for equality with another.
		/// </summary>
        /// <param name="otherRestoreStatus">the other restoration status
		/// </param>
		/// <returns> <code>true</code> if the two restoration status objects are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherRestoreStatus)
		{
			try
			{
				return (val == ((RestoreStatus) otherRestoreStatus).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this restoration status.
		/// </summary>
		/// <returns> a hash code corresponding to this restoration status
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this restoration status.
		/// </summary>
		/// <returns> a string representation of this restoration status
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(NO_RESTORE_IN_PROGRESS))
			{
				return "No restoration operation is in progress.";
			}
			else if (this.Equals(FEDERATE_RESTORE_REQUEST_PENDING))
			{
				return "The federate's request for a restoration operation is pending.";
			}
			else if (this.Equals(FEDERATE_WAITING_FOR_RESTORE_TO_BEGIN))
			{
				return "The federate is waiting for a restoration operation to begin.";
			}
			else if (this.Equals(FEDERATE_PREPARED_TO_RESTORE))
			{
				return "The federate is prepared to perform a restoration operation.";
			}
			else if (this.Equals(FEDERATE_RESTORING))
			{
				return "The federate is currently performing a restoration operation.";
			}
			// this.Equals(FEDERATE_WAITING_FOR_FEDERATION_TO_RESTORE)
			else
			{
				return "The federate is waiting for the federation to be restored.";
			}
		}
	}
}