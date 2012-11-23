namespace Sxta.Rti1516.Reflection
{
    using System;
    using System.IO;

    using HlaEncodingReader = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingReader;
    using HlaEncodingWriter = Sxta.Rti1516.Serializers.XrtiEncoding.HlaEncodingWriter;

    ///<summary>
    ///Ownership type to be used for sending attributes. 
    ///</summary>
    /// <author> Sxta1516.DynamicCompiler.DynamicCompiler from Reflection Object Model </author>
    [HLAEnumeratedData(Name = "HLAownershipType",
                    Representation = "HLAinteger32BE",
                    Semantics = "Ownership type to be used for sending attributes.")]
    public enum  HLAownershipType
    {
        Divest = 0,
        Acquire = 1,
        DivestAcquire = 2,
        NoTransfer = 3
    }
}
