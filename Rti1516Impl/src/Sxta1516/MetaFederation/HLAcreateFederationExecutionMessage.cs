namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;


    ///<summary>
    ///Message for HLAcreateFederationExecution iteraction : Creates a federation
    ///execution. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAcreateFederationExecution",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Creates a federation execution.",
                    Transportation = "HLAreliable")]
    public class HLAcreateFederationExecutionMessage : HLAmetaFederationMessage
    {
        String federationExecutionName;

        ///<summary>The name of the execution to create.</summary> 
        [HLAInteractionParameter(Name = "federationExecutionName",
                      Semantics = "The name of the execution to create.",
                      DataType = "HLAunicodeString")]
        public String FederationExecutionName
        {
            get { return federationExecutionName;}
            set { federationExecutionName = value;}
        }

        byte[] federationDescriptionDocument;

        ///<summary>The encoded federation description document.</summary> 
        [HLAInteractionParameter(Name = "federationDescriptionDocument",
                      Semantics = "The encoded federation description document.",
                      DataType = "HLAopaqueData")]
        public byte[] FederationDescriptionDocument
        {
            get { return federationDescriptionDocument;}
            set { federationDescriptionDocument = value;}
        }

        ///<summary> Returns a string representation of this HLAcreateFederationExecutionMessage. </summary>
        ///<returns> a string representation of this HLAcreateFederationExecutionMessage</returns>
        public override string ToString()
        {
            return "HLAcreateFederationExecutionMessage(" + base.ToString()
                   + ", FederationExecutionName: " + FederationExecutionName
                   + ", FederationDescriptionDocument: " + FederationDescriptionDocument + ")";
        }
    }
}
