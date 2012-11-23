namespace Hla.Rti1516
{
    using System;

    /// <summary>
    ///  An exception thrown when asynchronous delivery is already enabled.
    /// </summary>
    [global::System.Serializable]
    public sealed class AsynchronousDeliveryAlreadyEnabled : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public AsynchronousDeliveryAlreadyEnabled() { }
        public AsynchronousDeliveryAlreadyEnabled(string message) : base(message) { }
        public AsynchronousDeliveryAlreadyEnabled(string message, Exception inner) : base(message, inner) { }
        private AsynchronousDeliveryAlreadyEnabled(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }


    }
}