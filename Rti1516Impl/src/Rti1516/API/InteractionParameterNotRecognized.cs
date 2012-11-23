namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception indicating that an interaction parameter was not recognized.
    /// </summary>
    [global::System.Serializable]
    public sealed class InteractionParameterNotRecognized : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InteractionParameterNotRecognized() { }
        public InteractionParameterNotRecognized(string message) : base(message) { }
        public InteractionParameterNotRecognized(string message, Exception inner) : base(message, inner) { }
        private InteractionParameterNotRecognized(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}