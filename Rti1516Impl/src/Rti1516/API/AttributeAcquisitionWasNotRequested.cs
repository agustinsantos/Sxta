namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception thrown when attribute acquisition was not requested.
	/// </summary>
    [global::System.Serializable]
    public sealed class AttributeAcquisitionWasNotRequested : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AttributeAcquisitionWasNotRequested() { }
        public AttributeAcquisitionWasNotRequested(string message) : base(message) { }
        public AttributeAcquisitionWasNotRequested(string message, Exception inner) : base(message, inner) { }
        private AttributeAcquisitionWasNotRequested(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}