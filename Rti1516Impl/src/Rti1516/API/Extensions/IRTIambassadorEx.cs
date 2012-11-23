namespace Hla.Rti1516.Extensions
{
    using System;
    using CouldNotOpenFDD = Hla.Rti1516.CouldNotOpenFDD;
    using ErrorReadingFDD = Hla.Rti1516.ErrorReadingFDD;
    using FederateNotExecutionMember = Hla.Rti1516.FederateNotExecutionMember;
    using RestoreInProgress = Hla.Rti1516.RestoreInProgress;
    using RTIambassador = Hla.Rti1516.IRTIambassador;
    using RTIinternalError = Hla.Rti1516.RTIinternalError;
    using SaveInProgress = Hla.Rti1516.SaveInProgress;

	/// <summary> 
	/// An extended version of the <code>IRTIambassador</code> interface.
	/// </summary>
	/// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
	/// </author>
	public interface IRtiAmbassadorExt : IRTIambassador
	{
		/// <summary> 
		/// Merges the object model contained in the specified federation
		/// description document with the current federation object model.
		/// </summary>
		/// <param name="fdd">the location of the federation description document
		/// </param>
		/// <exception cref="CouldNotOpenFDD"> if the federation description document could not
		/// be opened
		/// </exception>
		/// <exception cref="ErrorReadingFDD"> if an error occurred while reading the federation
		/// description document
		/// </exception>
		/// <exception cref="RTIinternalError"> if an internal error occurred in the
		/// run-time infrastructure
		/// </exception>
		void  MergeFdd(System.Uri fdd);
	}
}