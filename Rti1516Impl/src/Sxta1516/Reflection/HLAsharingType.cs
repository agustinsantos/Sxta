namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Sharing type to be used for sending attributes or interactions. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAEnumeratedData(Name = "HLAsharingType",
                       Representation = "HLAinteger32BE",
                       Semantics = "Sharing type to be used for sending attributes or interactions.")]
    public enum  HLAsharingType
    {
        Publish = 0,
        Subscribe = 1,
        PublishSubscribe = 2,
        Neither = 3
    }
}
