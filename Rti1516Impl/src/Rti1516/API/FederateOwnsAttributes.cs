namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception indicating that the federate owns the attributes.
    /// </summary>
    [global::System.Serializable]
    public sealed class FederateOwnsAttributes : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public FederateOwnsAttributes() { }
        public FederateOwnsAttributes(string message) : base(message) { }
        public FederateOwnsAttributes(string message, Exception inner) : base(message, inner) { }
        private FederateOwnsAttributes(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}