namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception indicating illegal time arithmetic.
    /// </summary>
    [global::System.Serializable]
    public sealed class IllegalTimeArithmetic : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public IllegalTimeArithmetic() { }
        public IllegalTimeArithmetic(string message) : base(message) { }
        public IllegalTimeArithmetic(string message, Exception inner) : base(message, inner) { }
        private IllegalTimeArithmetic(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}