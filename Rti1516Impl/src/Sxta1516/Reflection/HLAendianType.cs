namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Endian type to be used for describing basic representations. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAEnumeratedData(Name = "HLAendianType",
                       Representation = "HLAinteger32BE",
                       Semantics = "Endian type to be used for describing basic representations.")]
    [Serializable]
    public enum  HLAendianType
    {
        Big = 0,
        Little = 1
    }
}
