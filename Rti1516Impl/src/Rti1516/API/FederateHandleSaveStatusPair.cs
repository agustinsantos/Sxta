namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  An object that contains a <code>IFederateHandle</code> and a
	/// <code>SaveStatus</code>.
	/// </summary>
	[Serializable]
	public sealed class FederateHandleSaveStatusPair
	{
		/// <summary> The federate handle.</summary>
		private IFederateHandle handle;
		
		/// <summary> The save status.</summary>
        private SaveStatus status;
		
		
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="pHandle">the federate handle
		/// </param>
		/// <param name="pStatus">the federate status
		/// </param>
		public FederateHandleSaveStatusPair(IFederateHandle pHandle, SaveStatus pStatus)
		{
			handle = pHandle;
			status = pStatus;
		}

        /// <summary> The federate handle.</summary>
        public IFederateHandle Handle
        {
            set { handle = value; }
            get { return handle; }
        }

        /// <summary> The save status.</summary>
        public SaveStatus Status
        {
            set { status = value; }
            get { return status; }
        }


	}
}