namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception thrown when an attribute is not owned.
	/// </summary>
    [global::System.Serializable]
    public sealed class AttributeNotOwned : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AttributeNotOwned() { }
        public AttributeNotOwned(string message) : base(message) { }
        public AttributeNotOwned(string message, Exception inner) : base(message, inner) { }
        private AttributeNotOwned(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}