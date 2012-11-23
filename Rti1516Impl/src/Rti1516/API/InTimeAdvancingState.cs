namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception indicating that the federate is in a time-advancing state.
    /// </summary>
    [global::System.Serializable]
    public sealed class InTimeAdvancingState : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InTimeAdvancingState() { }
        public InTimeAdvancingState(string message) : base(message) { }
        public InTimeAdvancingState(string message, Exception inner) : base(message, inner) { }
        private InTimeAdvancingState(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}