namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  The value returned by a time query.
	/// </summary>
	[Serializable]
	public sealed class TimeQueryReturn
	{
		/// <summary> Whether or not the returned time is valid.</summary>
		public bool timeIsValid;
		
		/// <summary> The returned time.</summary>
		public ILogicalTime time;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pTimeIsValid">whether or not the returned time is valid
		/// </param>
		/// <param name="pTime">the returned time
		/// </param>
		public TimeQueryReturn(bool pTimeIsValid, ILogicalTime pTime)
		{
			timeIsValid = pTimeIsValid;
			time = pTime;
		}
	}
}