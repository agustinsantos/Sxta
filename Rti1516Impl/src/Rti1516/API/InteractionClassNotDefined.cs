namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating that an interaction class was not defined.
    /// </summary>
    [global::System.Serializable]
    public sealed class InteractionClassNotDefined : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InteractionClassNotDefined() { }
        public InteractionClassNotDefined(string message) : base(message) { }
        public InteractionClassNotDefined(string message, Exception inner) : base(message, inner) { }
        private InteractionClassNotDefined(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}