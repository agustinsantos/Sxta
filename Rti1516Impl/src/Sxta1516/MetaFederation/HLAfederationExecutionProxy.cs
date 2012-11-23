namespace Sxta.Rti1516.MetaFederation
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Represents an HLA federation execution. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Meta-Federation Object Model </author>
    [HLAObjectClass(Name = "HLAfederationExecution",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Semantics = "Represents an HLA federation execution.")]
    public class HLAfederationExecution : HLAobjectRoot, IHLAfederationExecution, IHLAmetaFederation
    {

        ///<summary>
        ///Attribute #federationName. 
        ///</summary>
        private String federationName;

        ///<summary> Returns a string representation of this HLAfederationExecution. </summary>
        ///<returns> a string representation of this HLAfederationExecution</returns>
        public override String ToString()
        {
            return "HLAfederationExecution(" +
                     "federationName: " + federationName + 
                   ")";
        }

        ///<summary>
        /// Gets/Sets the value of the federationName field.
        ///</summary>
        [HLAAttribute(Name = "federationName",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "HLAunicodeString",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                     OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAreliable",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual String FederationName
        {
            set {federationName = value; }
            get { return federationName; }
        }
    }
}
