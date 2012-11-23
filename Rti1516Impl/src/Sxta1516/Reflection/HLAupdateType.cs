namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Update type to be used for sending attributes. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAEnumeratedData(Name = "HLAupdateType",
                       Representation = "HLAinteger32BE",
                       Semantics = "Update type to be used for sending attributes.")]
    public enum  HLAupdateType
    {
        Static = 0,
        Periodic = 1,
        Conditional = 2,
        NA = 3
    }
}
