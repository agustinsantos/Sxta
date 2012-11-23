namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception thrown when an attribute is already owned.
    /// </summary>
    [global::System.Serializable]
    public sealed class AttributeAlreadyOwned : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AttributeAlreadyOwned() { }
        public AttributeAlreadyOwned(string message) : base(message) { }
        public AttributeAlreadyOwned(string message, Exception inner) : base(message, inner) { }
        private AttributeAlreadyOwned(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}