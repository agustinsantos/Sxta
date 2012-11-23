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
    ///This object class is the root class of all MFOM object classes. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Meta-Federation Object Model </author>
    [HLAObjectClass(Name = "HLAmetaFederation",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.Neither,
                    Semantics = "This object class is the root class of all MFOM object classes.")]
    public class HLAmetaFederationProxy :  IHLAmetaFederation, IHLAobjectRoot    
    {

        ///<summary> Returns a string representation of this HLAmetaFederationProxy. </summary>
        ///<returns> a string representation of this HLAmetaFederationProxy</returns>
        public override String ToString()
        {
            return "HLAmetaFederationProxy()";
        }
    }
}
