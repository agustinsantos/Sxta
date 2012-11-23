using System;
using System.Reflection;
using Sxta.Rti1516.Reflection;

//<objectModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
//             xsi:noNamespaceSchemaLocation="HLA.xsd"
//             DTDversion="1516.2"
//             name="Reflection Object Model"
//             type="FOM"
//             version="1.0">
//  ....
//</objectModel>
[assembly: HLAObjectModelAttribute(Name = "Reflection Object Model",
                                   DTDversion = "1516.2",
                                   ObjectModelType = HLAObjectModelType.FOM,
                                   Version = "1.0")]
