namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating an invalid attribute handle.
    /// </summary>
    [global::System.Serializable]
    public sealed class InvalidAttributeHandle : RTIexception
    {
        /// <summary> 
        /// Initializes a new instance of the InvalidAttributeHandle class. 
        /// </summary>
        public InvalidAttributeHandle() { }

        /// <summary> 
        /// Initializes a new instance of the InvalidAttributeHandle class with a specified error message. 
        /// </summary>
        /// <param name="message">The message that describes the error. 
        /// </param>
        public InvalidAttributeHandle(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the InvalidAttributeHandle class with a specified error message and
        /// a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">a detailed description of the exception</param>
        /// <param name="inner"> The exception that is the cause of the current exception, or a null reference if no inner exception is specified. </param>
        public InvalidAttributeHandle(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the InvalidAttributeHandle class with serialized data. 
        /// </summary>
        /// <param name="info">The <code>SerializationInfo</code> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <code>StreamingContext</code> that contains contextual information about the source or destination.</param>
        private InvalidAttributeHandle(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}