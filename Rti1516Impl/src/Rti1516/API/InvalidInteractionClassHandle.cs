namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An exception indicating an invalid interaction class handle.
	/// </summary>
    [global::System.Serializable]
    public sealed class InvalidInteractionClassHandle : RTIexception
    {
        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="msg">a detailed description of the exception
        /// </param>
        public InvalidInteractionClassHandle() { }
        public InvalidInteractionClassHandle(string message) : base(message) { }
        public InvalidInteractionClassHandle(string message, Exception inner) : base(message, inner) { }
        private InvalidInteractionClassHandle(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}