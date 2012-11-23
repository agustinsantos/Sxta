namespace ExternalSamples
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Interactions;

    ///<summary>
    ///Message for Communication iteraction : A generic message. 
    ///</summary>
    [HLAInteractionClass(Name = "Communication",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "A generic message.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class CommunicationMessage : HLAinteractionRootMessage
    {
        String message;

        ///<summary>The contents of the message.</summary> 
        [HLAInteractionParameter(Name = "message",
                      Semantics = "The contents of the message.",
                      DataType = "HLAunicodeString")]
        public String Message
        {
            get { return message; }
            set { message = value; }
        }

        ///<summary> Returns a string representation of this CommunicationMessage. </summary>
        ///<returns> a string representation of this CommunicationMessage</returns>
        public override string ToString()
        {
            return "CommunicationMessage(" + base.ToString()
                   + ", Message: " + Message + ")";
        }
    }
}
