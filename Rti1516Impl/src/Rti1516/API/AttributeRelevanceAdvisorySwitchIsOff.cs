namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception thrown when the attribute relevance advisory switch is off.
    /// </summary>
    [global::System.Serializable]
    public sealed class AttributeRelevanceAdvisorySwitchIsOff : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AttributeRelevanceAdvisorySwitchIsOff() { }
        public AttributeRelevanceAdvisorySwitchIsOff(string message) : base(message) { }
        public AttributeRelevanceAdvisorySwitchIsOff(string message, Exception inner) : base(message, inner) { }
        private AttributeRelevanceAdvisorySwitchIsOff(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}