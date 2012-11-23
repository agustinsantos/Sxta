namespace Sxta.Rti1516.MetaFederation
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    using Sxta.Rti1516.Serializers.XrtiEncoding;


    ///<summary>
    ///Message for HLAjoinFederationExecution iteraction : Joins a federation
    ///execution. 
    ///</summary>
    [HLAInteractionClass(Name = "HLAjoinFederationExecution",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                    Semantics = "Joins a federation execution.",
                    Transportation = "HLAreliable")]
    public class HLAjoinFederationExecutionMessage : HLAmetaFederationMessage
    {
        String federationExecutionName;

        ///<summary>The name of the execution to join.</summary> 
        [HLAInteractionParameter(Name = "federationExecutionName",
                      Semantics = "The name of the execution to join.",
                      DataType = "HLAunicodeString")]
        public String FederationExecutionName
        {
            get { return federationExecutionName;}
            set { federationExecutionName = value;}
        }

        String federateType;

        ///<summary>The type of the joining federate.</summary> 
        [HLAInteractionParameter(Name = "federateType",
                      Semantics = "The type of the joining federate.",
                      DataType = "HLAunicodeString")]
        public String FederateType
        {
            get { return federateType;}
            set { federateType = value;}
        }

        long federateHandle;

        ///<summary>The handle of the joining federate.</summary> 
        [HLAInteractionParameter(Name = "federateHandle",
                      Semantics = "The handle of the joining federate.",
                      DataType = "HLAnormalizedHandle")]
        public long FederateHandle
        {
            get { return federateHandle;}
            set { federateHandle = value;}
        }

        ///<summary> Returns a string representation of this HLAjoinFederationExecutionMessage. </summary>
        ///<returns> a string representation of this HLAjoinFederationExecutionMessage</returns>
        public override string ToString()
        {
            return "HLAjoinFederationExecutionMessage(" + base.ToString()
                   + ", FederationExecutionName: " + FederationExecutionName
                   + ", FederateType: " + FederateType
                   + ", FederateHandle: " + FederateHandle + ")";
        }
    }
}
