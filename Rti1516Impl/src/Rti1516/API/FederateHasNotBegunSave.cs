namespace Hla.Rti1516
{
    using System;

    /// <summary> 
    /// An exception indicating that the federate has not begun the save operation.
    /// </summary>
    [global::System.Serializable]
    public sealed class FederateHasNotBegunSave : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public FederateHasNotBegunSave() { }
        public FederateHasNotBegunSave(string message) : base(message) { }
        public FederateHasNotBegunSave(string message, Exception inner) : base(message, inner) { }
        private FederateHasNotBegunSave(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}