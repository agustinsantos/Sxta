namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a type of resignation action.
	/// </summary>
	[Serializable]
	public sealed class ResignAction
	{
		/// <summary> Upon resignation, divest attributes unconditionally.</summary>
		public static readonly ResignAction UNCONDITIONALLY_DIVEST_ATTRIBUTES = new ResignAction(1);
		
		/// <summary> Upon resignation, Delete objects.</summary>
		public static readonly ResignAction DELETE_OBJECTS = new ResignAction(2);
		
		/// <summary>  Upon resignation, cancel pending ownership acquisitions.</summary>
		public static readonly ResignAction CANCEL_PENDING_OWNERSHIP_ACQUISITIONS = new ResignAction(3);
		
		/// <summary> Upon resignation, Delete objects and then divest attributes.</summary>
		public static readonly ResignAction DELETE_OBJECTS_THEN_DIVEST = new ResignAction(4);
		
		/// <summary> Upon resignation, cancel pending ownership acquisitions, Delete objects, and
		/// divest attributes.
		/// </summary>
		public static readonly ResignAction CANCEL_THEN_DELETE_THEN_DIVEST = new ResignAction(5);
		
		/// <summary> Take no action upon resignation.</summary>
		public static readonly ResignAction NO_ACTION = new ResignAction(6);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherResignAction">the resignation action object to copy
		/// </param>
		public ResignAction(ResignAction otherResignAction)
		{
			val = otherResignAction.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this resignation action
		/// </param>
		private ResignAction(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this resignation action for equality with another.
		/// </summary>
		/// <param name="otherResignAction">the other resignation action
		/// </param>
		/// <returns> <code>true</code> if the two resignation actions are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherResignAction)
		{
			try
			{
				return (val == ((ResignAction) otherResignAction).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this resignation action.
		/// </summary>
		/// <returns> a hash code corresponding to this resignation action
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this resignation action.
		/// </summary>
		/// <returns> a string representation of this resignation action
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(UNCONDITIONALLY_DIVEST_ATTRIBUTES))
			{
				return "unconditionally divest attributes";
			}
			else if (this.Equals(DELETE_OBJECTS))
			{
				return "Delete objects";
			}
			else if (this.Equals(CANCEL_PENDING_OWNERSHIP_ACQUISITIONS))
			{
				return "cancel pending ownership acquisitions";
			}
			else if (this.Equals(DELETE_OBJECTS_THEN_DIVEST))
			{
				return "Delete objects, divest attributes";
			}
			else if (this.Equals(CANCEL_THEN_DELETE_THEN_DIVEST))
			{
				return "cancel pending ownership acquisitions, Delete objects, divest attributes";
			}
			// this.Equals(NO_ACTION)
			else
			{
				return "no action";
			}
		}
	}
}