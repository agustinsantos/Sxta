namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating an invalid message retraction handle.
    /// </summary>
    [global::System.Serializable]
    public sealed class InvalidMessageRetractionHandle : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InvalidMessageRetractionHandle() { }
        public InvalidMessageRetractionHandle(string message) : base(message) { }
        public InvalidMessageRetractionHandle(string message, Exception inner) : base(message, inner) { }
        private InvalidMessageRetractionHandle(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}