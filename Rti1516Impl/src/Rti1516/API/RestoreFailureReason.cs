namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a reason for failure to restore.
	/// </summary>
	[Serializable]
	public sealed class RestoreFailureReason
	{
		/// <summary> The run-time infrastructure was unable to restore the federate.</summary>
		public static readonly RestoreFailureReason RTI_UNABLE_TO_RESTORE = new RestoreFailureReason(1);
		
		/// <summary> The federate reported a failure.</summary>
		public static readonly RestoreFailureReason FEDERATE_REPORTED_FAILURE = new RestoreFailureReason(2);
		
		/// <summary> The federate had resigned from the federation.</summary>
		public static readonly RestoreFailureReason FEDERATE_RESIGNED = new RestoreFailureReason(3);
		
		/// <summary> The run-time infrastructure detected a failure.</summary>
		public static readonly RestoreFailureReason RTI_DETECTED_FAILURE = new RestoreFailureReason(4);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherRestoreFailureReason">the restoration failure reason object to copy
		/// </param>
		public RestoreFailureReason(RestoreFailureReason otherRestoreFailureReason)
		{
			val = otherRestoreFailureReason.val;
		}
		
		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this restoration failure reason
		/// </param>
		private RestoreFailureReason(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this restoration failure reason for equality with another.
		/// </summary>
        /// <param name="otherRestoreFailureReason">the other restoration failure reason
		/// </param>
		/// <returns> <code>true</code> if the two restoration failure reasons are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherRestoreFailureReason)
		{
			try
			{
				return (val == ((RestoreFailureReason) otherRestoreFailureReason).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this restoration failure reason.
		/// </summary>
		/// <returns> a hash code corresponding to this restoration failure reason
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this restoration failure reason.
		/// </summary>
		/// <returns> a string representation of this restoration failure reason
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(RTI_UNABLE_TO_RESTORE))
			{
				return "The run-time infrastructure was unable to restore the federate.";
			}
			else if (this.Equals(FEDERATE_REPORTED_FAILURE))
			{
				return "The federate reported a failure.";
			}
			else if (this.Equals(FEDERATE_RESIGNED))
			{
				return "The federate had resigned from the federation.";
			}
			// this.Equals(RTI_DETECTED_FAILURE)
			else
			{
				return "The run-time infrastructure detected a failure.";
			}
		}
	}
}