namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;


    ///<summary>
    ///Message for HLAdestroyFederationExecution iteraction : Destroys a federation
    ///execution. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAdestroyFederationExecution",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Destroys a federation execution.",
                    Transportation = "HLAreliable")]
    public class HLAdestroyFederationExecutionMessage : HLAmetaFederationMessage
    {
        String federationExecutionName;

        ///<summary>The name of the execution to destroy.</summary> 
        [HLAInteractionParameter(Name = "federationExecutionName",
                      Semantics = "The name of the execution to destroy.",
                      DataType = "HLAunicodeString")]
        public String FederationExecutionName
        {
            get { return federationExecutionName;}
            set { federationExecutionName = value;}
        }

        ///<summary> Returns a string representation of this HLAdestroyFederationExecutionMessage. </summary>
        ///<returns> a string representation of this HLAdestroyFederationExecutionMessage</returns>
        public override string ToString()
        {
            return "HLAdestroyFederationExecutionMessage(" + base.ToString()
                   + ", FederationExecutionName: " + FederationExecutionName + ")";
        }
    }
}
