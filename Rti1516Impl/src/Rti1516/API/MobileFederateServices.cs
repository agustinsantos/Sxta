namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Conveys the interfaces for all services that a federate
	/// must supply and which may not execute in the federate's
	/// space.
	/// </summary>
	[Serializable]
	public sealed class MobileFederateServices
	{
		/// <summary> The logical time factory.</summary>
		public ILogicalTimeFactory _timeFactory;
		
		/// <summary> The logical time interval factory.</summary>
		public ILogicalTimeIntervalFactory _intervalFactory;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="timeFactory">the logical time factory
		/// </param>
		/// <param name="intervalFactory">the logical time interval factory
		/// </param>
		public MobileFederateServices(ILogicalTimeFactory timeFactory, ILogicalTimeIntervalFactory intervalFactory)
		{
            _timeFactory = timeFactory;
            _intervalFactory = intervalFactory;
		}
	}
}