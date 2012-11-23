using System;
using System.Reflection;
using Sxta.Rti1516.Reflection;

//<basicData name="HLAoctet"
//           size="8"
//           interpretation="8-bit value"
//           endian="Big"
//           encoding="Assumed to be portable among hardware devices."/>
[assembly: HLABasicDataAttribute(Name = "HLAoctet",
                              Size = 8,
                              Interpretation = "8-bit value",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.Byte),
                              Encoding = "Assumed to be portable among hardware devices.")]

//<basicData name="HLAinteger16BE"
//           size="16"
//           interpretation="Integer in the range [-2^15, 2^15 - 1]"
//           endian="Big"
//           encoding="16-bit two's complement signed integer. The most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger16BE",
                              Size = 16,
                              Interpretation = "Integer in the range [-2^15, 2^15 - 1]",
                              Endian = HLAendianType.Big,
                              NativeType=typeof(System.Int16),
                              Encoding = "16-bit two's complement signed integer. The most significant bit contains the sign.")]

//<basicData name="HLAinteger16LE"
//           size="16"
//           interpretation="Integer in the range [-2^15, 2^15 - 1]"
//           endian="Little"
//           encoding="16-bit two's complement signed integer. The
//                     most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger16LE",
                              Size = 16,
                              Interpretation = "Integer in the range [-2^15, 2^15 - 1]",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.Int16),
                              Encoding = "16-bit two's complement signed integer. The most significant bit contains the sign.")]

//<basicData name="HLAinteger32BE"
//           size="32"
//           interpretation="Integer in the range [-2^31, 2^31 - 1]"
//           endian="Big"
//           encoding="32-bit two's complement signed integer. The most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger32BE",
                              Size = 32,
                              Interpretation = "Integer in the range [-2^31, 2^31 - 1]",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.Int32),
                              Encoding = "32-bit two's complement signed integer. The most significant bit contains the sign.")]

//<basicData name="HLAinteger32LE"
//           size="32"
//           interpretation="Integer in the range [-2^31, 2^31 - 1]"
//           endian="Little"
//           encoding="32-bit two's complement signed integer. The
//                     most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger32LE",
                              Size = 32,
                              Interpretation = "Integer in the range [-2^31, 2^31 - 1]",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.Int32),
                              Encoding = "32-bit two's complement signed integer. The most significant bit contains the sign.")]


//<basicData name="HLAinteger64BE"
//           size="64"
//           interpretation="Integer in the range [-2^63, 2^63 - 1]"
//           endian="Big"
//           encoding="64-bit two's complement signed integer first. The most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger64BE",
                              Size = 64,
                              Interpretation = "Integer in the range [-2^63, 2^63 - 1]",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.Int64),
                              Encoding = "64-bit two's complement signed integer. The most significant bit contains the sign.")]

//<basicData name="HLAinteger64LE"
//           size="64"
//           interpretation="Integer in the range [-2^63, 2^63 - 1]"
//           endian="Little"
//           encoding="64-bit two's complement signed integer first.
//                     The most significant bit contains the sign."/>
[assembly: HLABasicDataAttribute(Name = "HLAinteger64LE",
                              Size = 64,
                              Interpretation = "Integer in the range [-2^63, 2^63 - 1]",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.Int64),
                              Encoding = "64-bit two's complement signed integer. The most significant bit contains the sign.")]

//<basicData name="HLAfloat32BE"
//           size="32"
//           interpretation="Single-precision floating point number"
//           endian="Big"
//           encoding="32-bit IEEE normalized single-precision format. See IEEE Std 754-1985."/>
[assembly: HLABasicDataAttribute(Name = "HLAfloat32BE",
                              Size = 32,
                              Interpretation = "Single-precision floating point number",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.Single),
                              Encoding = "32-bit IEEE normalized single-precision format. See IEEE Std 754-1985.")]

//<basicData name="HLAfloat32LE"
//           size="32"
//           interpretation="Single-precision floating point number"
//           endian="Little"
//           encoding="32-bit IEEE normalized single-precision format.
//                     See IEEE Std 754-1985"/>
[assembly: HLABasicDataAttribute(Name = "HLAfloat32LE",
                              Size = 32,
                              Interpretation = "Single-precision floating point number",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.Single),
                              Encoding = "32-bit IEEE normalized single-precision format. See IEEE Std 754-1985.")]

//<basicData name="HLAfloat64BE"
//           size="64"
//           interpretation="Double-precision floating point number"
//           endian="Big"
//           encoding="64-bit IEEE normalized double-precision format.
//                     See IEEE Std 754-1985."/>
[assembly: HLABasicDataAttribute(Name = "HLAfloat64BE",
                              Size = 64,
                              Interpretation = "Double-precision floating point number",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.Double),
                              Encoding = "64-bit IEEE normalized double-precision format. See IEEE Std 754-1985.")]

//<basicData name="HLAfloat64LE"
//           size="64"
//           interpretation="Double-precision floating point number"
//           endian="Little"
//           encoding="64-bit IEEE normalized double-precision format.
//                     See IEEE Std 754-1985"/>
[assembly: HLABasicDataAttribute(Name = "HLAfloat64LE",
                              Size = 64,
                              Interpretation = "Double-precision floating point number",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.Double),
                              Encoding = "64-bit IEEE normalized double-precision format. See IEEE Std 754-1985.")]


//<basicData name="HLAoctetPairBE"
//           size="16"
//           interpretation="16-bit value"
//           endian="Big"
//           encoding="Assumed to be portable among hardware devices."/>
[assembly: HLABasicDataAttribute(Name = "HLAoctetPairBE",
                              Size = 16,
                              Interpretation = "16-bit value",
                              Endian = HLAendianType.Big,
                              NativeType = typeof(System.UInt16),
                              Encoding = "Assumed to be portable among hardware devices.")]


//<basicData name="HLAoctetPairLE"
//           size="16"
//           interpretation="16-bit value"
//           endian="Little"
//           encoding="Assumed to be portable among hardware devices."/>
[assembly: HLABasicDataAttribute(Name = "HLAoctetPairLE",
                              Size = 16,
                              Interpretation = "16-bit value",
                              Endian = HLAendianType.Little,
                              NativeType = typeof(System.UInt16),
                              Encoding = "Assumed to be portable among hardware devices.")]
