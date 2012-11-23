namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A type-safe handle for a region.
	/// </summary>
	public interface IRegionHandle
	{
		/// <summary> 
		/// Checks this region handle for equality with another.
		/// </summary>
		/// <param name="otherRegionHandle">the other region handle
		/// </param>
		/// <returns> <code>true</code> if the two handles represent
		/// the same region, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherRegionHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this
		/// region handle.
		/// </summary>
		/// <returns> the hash code corresponding to this region handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns a string representation of this region handle.
		/// </summary>
		/// <returns> a string representation of this region handle
		/// </returns>
		System.String ToString();
	}
}