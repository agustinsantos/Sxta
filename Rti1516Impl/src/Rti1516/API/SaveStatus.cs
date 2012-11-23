namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents the status of a save operation.
	/// </summary>
	[Serializable]
	public sealed class SaveStatus
	{
		/// <summary> No save operation is in progress.</summary>
		public static readonly SaveStatus NO_SAVE_IN_PROGRESS = new SaveStatus(1);
		
		/// <summary> The federate has been instructed to save.</summary>
		public static readonly SaveStatus FEDERATE_INSTRUCTED_TO_SAVE = new SaveStatus(2);
		
		/// <summary> The federate is saving.</summary>
		public static readonly SaveStatus FEDERATE_SAVING = new SaveStatus(3);
		
		/// <summary> The federate is waiting for the federation to save.</summary>
		public static readonly SaveStatus FEDERATE_WAITING_FOR_FEDERATION_TO_SAVE = new SaveStatus(4);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherSaveStatus">the save status object to copy
		/// </param>
		public SaveStatus(SaveStatus otherSaveStatus)
		{
			val = otherSaveStatus.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this save status
		/// </param>
		private SaveStatus(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this save status for equality with another.
		/// </summary>
		/// <param name="otherSaveStatus">the other save status
		/// </param>
		/// <returns> <code>true</code> if the two save status objects are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherSaveStatus)
		{
			try
			{
				return (val == ((SaveStatus) otherSaveStatus).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this save status.
		/// </summary>
		/// <returns> a hash code corresponding to this save status
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this save status.
		/// </summary>
		/// <returns> a string representation of this save status
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(NO_SAVE_IN_PROGRESS))
			{
				return "No save operation is in progress.";
			}
			else if (this.Equals(FEDERATE_INSTRUCTED_TO_SAVE))
			{
				return "The federate has been instructed to save.";
			}
			else if (this.Equals(FEDERATE_SAVING))
			{
				return "The federate is saving.";
			}
			// this.Equals(FEDERATE_WAITING_FOR_FEDERATION_TO_SAVE)
			else
			{
				return "The federate is waiting for the federation to save.";
			}
		}
	}
}