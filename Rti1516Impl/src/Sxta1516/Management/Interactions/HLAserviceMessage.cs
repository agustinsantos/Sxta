using System;
using System.Collections.Generic;
using System.Text;

using Sxta.Rti1516.Reflection;
using Sxta.Rti1516.Interactions;
using Sxta.Rti1516.Serializers.XrtiEncoding;

namespace Sxta.Rti1516.Management
{
    ///<summary>
    ///Message for HLAservice iteraction : The interaction class shall be acted upon
    ///by the RTI. These interactions shall invoke HLA services on behalf of another
    ///joined federate. They shall cause the RTI to react as if the service has invoked
    ///by that other joined federate. If exceptions arise as a result of the use
    ///of these interactions, they shall be reported via the HLAmanager. HLAfederate.HLAreport.HLAreportMOMexception
    ///interaction to all joined federates that subscribe to this interaction. There
    ///are two ways an error can occur: the sending federate does not provide all
    ///the required arguments as parameters or the preconditions of the spoofed service
    ///are not met. Each type of error is reported via the HLAMOMreportMOMexception.
    ///NOTE?These interactions shall have the potential to disrupt normal federation
    ///execution and should be used with great care. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAservice",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "The interaction class shall be acted upon by the RTI. These interactions shall invoke HLA services on behalf of another joined federate. They shall cause the RTI to react as if the service has invoked by that other joined federate. If exceptions arise as a result of the use of these interactions, they shall be reported via the HLAmanager. HLAfederate.HLAreport.HLAreportMOMexception interaction to all joined federates that subscribe to this interaction. There are two ways an error can occur: the sending federate does not provide all the required arguments as parameters or the preconditions of the spoofed service are not met. Each type of error is reported via the HLAMOMreportMOMexception. NOTE?These interactions shall have the potential to disrupt normal federation execution and should be used with great care.",
                    Dimensions = "NA",
                    Transportation = "HLAreliable")]
    public class HLAserviceMessage : HLAfederateMessage
    {
        ///<summary> Returns a string representation of this HLAserviceMessage. </summary>
        ///<returns> a string representation of this HLAserviceMessage</returns>
        public override string ToString()
        {
            return "HLAserviceMessage(" + base.ToString() + ")";
        }
    }

    ///<summary>
    ///A HLA serializer for HLAserviceMessage. 
    ///</summary>
    public class HLAserviceMessageXrtiSerializer : HLAfederateMessageXrtiSerializer
    {
        ///<summary> Constructor </summary>
        public HLAserviceMessageXrtiSerializer(XrtiSerializerManager manager)
            : base(manager)
        {
        }

        ///<summary> Writes this HLAserviceMessage to the specified stream.</summary>
        ///<param name="writer"> the output stream to write to</param>
        ///<param name="obj"> the object to serialize</param>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override void Serialize(HlaEncodingWriter writer, object obj)
        {
            base.Serialize(writer, obj);
        }

        ///<summary> Reads this HLAserviceMessage from the specified stream.</summary>
        ///<param name="reader"> the input stream to read from</param>
        ///<returns> the object</returns>
        ///<exception cref="System.IO.IOException"> if an error occurs</exception>
        public override object Deserialize(HlaEncodingReader reader, ref object msg)
        {
            HLAserviceMessage decodedValue;
            if (!(msg is HLAserviceMessage))
            {
                decodedValue = new HLAserviceMessage();
                BaseInteractionMessage baseMsg = msg as BaseInteractionMessage;
                decodedValue.InteractionClassHandle = baseMsg.InteractionClassHandle;
                decodedValue.FederationExecutionHandle = baseMsg.FederationExecutionHandle;
                decodedValue.UserSuppliedTag = baseMsg.UserSuppliedTag;
            }
            else
            {
                decodedValue = msg as HLAserviceMessage;
            }
            object tmp = decodedValue;
            decodedValue = base.Deserialize(reader, ref tmp) as HLAserviceMessage;
            return decodedValue;
        }
    }

}
