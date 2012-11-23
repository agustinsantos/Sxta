namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Order type to be used for sending attributes or interactions. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAEnumeratedData(Name = "HLAorderType",
                       Representation = "HLAinteger32BE",
                       Semantics = "Order type to be used for sending attributes or interactions.")]
    public enum  HLAorderType
    {
        Receive = 0,
        TimeStamp = 1
    }
}
