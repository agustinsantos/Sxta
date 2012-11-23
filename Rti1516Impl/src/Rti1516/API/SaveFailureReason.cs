namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents the reason for the failure of a save operation.
	/// </summary>
	[Serializable]
	public sealed class SaveFailureReason
	{
		/// <summary> The run-time infrastructure was unable to save.</summary>
		public static readonly SaveFailureReason RTI_UNABLE_TO_SAVE = new SaveFailureReason(1);
		
		/// <summary> The federate reported a failure.</summary>
		public static readonly SaveFailureReason FEDERATE_REPORTED_FAILURE = new SaveFailureReason(2);
		
		/// <summary> The federate has resigned from the federation.</summary>
		public static readonly SaveFailureReason FEDERATE_RESIGNED = new SaveFailureReason(3);
		
		/// <summary> The run-time infrastructure detected a failure.</summary>
		public static readonly SaveFailureReason RTI_DETECTED_FAILURE = new SaveFailureReason(4);
		
		/// <summary> The save time cannot be honored.</summary>
		public static readonly SaveFailureReason SAVE_TIME_CANNOT_BE_HONORED = new SaveFailureReason(5);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherSaveFailureReason">the save failure reason object to copy
		/// </param>
		public SaveFailureReason(SaveFailureReason otherSaveFailureReason)
		{
			val = otherSaveFailureReason.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this save failure reason
		/// </param>
		private SaveFailureReason(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this save failure reason for equality with another.
		/// </summary>
		/// <param name="otherSaveFailureReason">the other save failure reason
		/// </param>
		/// <returns> <code>true</code> if the two save failure reasons are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherSaveFailureReason)
		{
			try
			{
				return (val == ((SaveFailureReason) otherSaveFailureReason).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this save failure reason.
		/// </summary>
		/// <returns> a hash code corresponding to this save failure reason
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this save failure reason.
		/// </summary>
		/// <returns> a string representation of this save failure reason
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(RTI_UNABLE_TO_SAVE))
			{
				return "The run-time infrastructure was unable to save.";
			}
			else if (this.Equals(FEDERATE_REPORTED_FAILURE))
			{
				return "The federate reported a failure.";
			}
			else if (this.Equals(FEDERATE_RESIGNED))
			{
				return "The federate has resigned from the federation.";
			}
			else if (this.Equals(RTI_DETECTED_FAILURE))
			{
				return "The run-time infrastructure detected a failure.";
			}
			// this.Equals(SAVE_TIME_CANNOT_BE_HONORED)
			else
			{
				return "The save time cannot be honored.";
			}
		}
	}
}