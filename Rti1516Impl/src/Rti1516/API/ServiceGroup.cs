namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Represents a service group.
	/// </summary>
	[Serializable]
	public sealed class ServiceGroup
	{
		/// <summary> The federation management service group.</summary>
		public static readonly ServiceGroup FEDERATION_MANAGEMENT = new ServiceGroup(1);
		
		/// <summary> The declaration management service group.</summary>
		public static readonly ServiceGroup DECLARATION_MANAGEMENT = new ServiceGroup(2);
		
		/// <summary> The object management service group.</summary>
		public static readonly ServiceGroup OBJECT_MANAGEMENT = new ServiceGroup(3);
		
		/// <summary> The ownership management service group.</summary>
		public static readonly ServiceGroup OWNERSHIP_MANAGEMENT = new ServiceGroup(4);
		
		/// <summary> The time management service group.</summary>
		public static readonly ServiceGroup TIME_MANAGEMENT = new ServiceGroup(5);
		
		/// <summary> The data distribution management service group.</summary>
		public static readonly ServiceGroup DATA_DISTRIBUTION_MANAGEMENT = new ServiceGroup(6);
		
		/// <summary> The support services group.</summary>
		public static readonly ServiceGroup SUPPORT_SERVICES = new ServiceGroup(7);
		
		/// <summary> The value of the instance.</summary>
		private int val;
		
		
		/// <summary> 
		/// Copy constructor.
		/// </summary>
		/// <param name="otherServiceGroup">the service group object to copy
		/// </param>
		public ServiceGroup(ServiceGroup otherServiceGroup)
		{
			val = otherServiceGroup.val;
		}
		
		/// <summary> 
		/// Private constructor.
		/// </summary>
		/// <param name="pValue">the integer value corresponding to this service group
		/// </param>
		private ServiceGroup(int pValue)
		{
			val = pValue;
		}
		
		/// <summary> 
		/// Compares this service group for equality with another.
		/// </summary>
		/// <param name="otherServiceGroup">the other service group
		/// </param>
		/// <returns> <code>true</code> if the two service group objects are equal,
		/// <code>false</code> otherwise
		/// </returns>
		public  override bool Equals(System.Object otherServiceGroup)
		{
			try
			{
				return (val == ((ServiceGroup) otherServiceGroup).val);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this service group.
		/// </summary>
		/// <returns> a hash code corresponding to this service group
		/// </returns>
		public override int GetHashCode()
		{
			return val;
		}
		
		/// <summary> 
		/// Returns a string representation of this service group.
		/// </summary>
		/// <returns> a string representation of this service group
		/// </returns>
		public override System.String ToString()
		{
			if (this.Equals(FEDERATION_MANAGEMENT))
			{
				return "federation management";
			}
			else if (this.Equals(DECLARATION_MANAGEMENT))
			{
				return "declaration management";
			}
			else if (this.Equals(OBJECT_MANAGEMENT))
			{
				return "object management";
			}
			else if (this.Equals(OWNERSHIP_MANAGEMENT))
			{
				return "ownership management";
			}
			else if (this.Equals(TIME_MANAGEMENT))
			{
				return "time management";
			}
			else if (this.Equals(DATA_DISTRIBUTION_MANAGEMENT))
			{
				return "data distribution management";
			}
			// this.Equals(SUPPORT_SERVICES)
			else
			{
				return "support services";
			}
		}
	}
}