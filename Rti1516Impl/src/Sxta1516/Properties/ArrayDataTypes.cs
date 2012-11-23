using System;
using System.Reflection;
using Sxta.Rti1516.Reflection;

//<arrayData name="HLAASCIIstring"
//           dataType="HLAASCIIchar"
//           cardinality="Dynamic"
//           encoding="HLAvariableArray"
//           semantics="ASCII String representation."/>
[assembly: HLAArrayDataAttribute(Name = "HLAASCIIstring",
                                 DataType = "HLAASCIIchar",
                                 Cardinality="Dynamic",
                                 Encoding="HLAvariableArray",
                                 HasNativeSerializer = true,
                                 Semantics = "ASCII String representation.")]


//<arrayData name="HLAunicodeString"
//           dataType="HLAunicodeChar"
//           cardinality="Dynamic"
//           encoding="HLAvariableArray"
//           semantics="Unicode string representation."/>
[assembly: HLAArrayDataAttribute(Name = "HLAunicodeString",
                                 DataType = "HLAunicodeChar",
                                 Cardinality = "Dynamic",
                                 Encoding = "HLAvariableArray",
                                 HasNativeSerializer = true,
                                 Semantics = "Unicode string representation.")]

//<arrayData name="HLAopaqueData"
//           dataType="HLAbyte"
//           cardinality="Dynamic"
//           encoding="HLAvariableArray"
//           semantics="Uninterpreted sequence of bytes."/>
[assembly: HLAArrayDataAttribute(Name = "HLAopaqueData",
                                 DataType = "HLAbyte",
                                 Cardinality = "Dynamic",
                                 Encoding = "HLAvariableArray",
                                 HasNativeSerializer = true,
                                 Semantics = "Uninterpreted sequence of bytes.")]
