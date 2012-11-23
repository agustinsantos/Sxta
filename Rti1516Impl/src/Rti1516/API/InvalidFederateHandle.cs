namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating an invalid federate handle.
    /// </summary>
    [global::System.Serializable]
    public sealed class InvalidFederateHandle : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InvalidFederateHandle() { }
        public InvalidFederateHandle(string message) : base(message) { }
        public InvalidFederateHandle(string message, Exception inner) : base(message, inner) { }
        private InvalidFederateHandle(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}