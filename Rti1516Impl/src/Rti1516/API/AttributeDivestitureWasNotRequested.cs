namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception thrown when attribute divestiture was not requested.
    /// </summary>
    [global::System.Serializable]
    public sealed class AttributeDivestitureWasNotRequested : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AttributeDivestitureWasNotRequested() { }
        public AttributeDivestitureWasNotRequested(string message) : base(message) { }
        public AttributeDivestitureWasNotRequested(string message, Exception inner) : base(message, inner) { }
        private AttributeDivestitureWasNotRequested(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}