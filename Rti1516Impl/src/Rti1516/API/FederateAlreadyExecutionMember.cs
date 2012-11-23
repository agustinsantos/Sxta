namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating that the federate is already a member of the execution.
    /// </summary>
    [global::System.Serializable]
    public sealed class FederateAlreadyExecutionMember : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public FederateAlreadyExecutionMember() { }
        public FederateAlreadyExecutionMember(string message) : base(message) { }
        public FederateAlreadyExecutionMember(string message, Exception inner) : base(message, inner) { }
        private FederateAlreadyExecutionMember(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}