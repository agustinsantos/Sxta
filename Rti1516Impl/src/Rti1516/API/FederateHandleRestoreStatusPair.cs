namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An object that contains a <code>IFederateHandle</code> and a
    /// <code>RestoreStatus</code>.
    /// </summary>
    [global::System.Serializable]
    public sealed class FederateHandleRestoreStatusPair
    {
        /// <summary> The federate handle.</summary>
        private IFederateHandle handle;

        /// <summary> The restore status.</summary>
        private RestoreStatus status;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pHandle">the federate handle
        /// </param>
        /// <param name="pStatus">the restore status
        /// </param>
        public FederateHandleRestoreStatusPair(IFederateHandle pHandle, RestoreStatus pStatus)
        {
            handle = pHandle;
            status = pStatus;
        }

        /// <summary> The federate handle.</summary>
        public IFederateHandle Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        /// <summary> The restore status.</summary>
        public RestoreStatus Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}