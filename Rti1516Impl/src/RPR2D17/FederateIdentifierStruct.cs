using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///  <fixedRecordData
    ///         name="FederateIdentifierStruct"
    ///         semantics="-NULL-">
    ///         <field
    ///            name="SiteID"
    ///            dataType="Unsignedinteger16BEN_slsh_AperfectalwaysN_slsh_A"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="ApplicationID"
    ///            dataType="Unsignedinteger16BEN_slsh_AperfectalwaysN_slsh_A"
    ///            semantics="-NULL-"/>
    ///    </fixedRecordData>
    /// </code>
    /// </summary>
    public struct FederateIdentifierStruct
    {
        public UInt16 SiteID;
        public UInt16 ApplicationID;
    }
}
