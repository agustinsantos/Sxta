using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{

    /// <summary>
    /// <code>
    ///   <fixedRecordData
    ///            name="EntityIdentifierStruct"
    ///            semantics="-NULL-">
    ///            <field
    ///               name="FederateIdentifier"
    ///               dataType="FederateIdentifierStruct"
    ///               semantics="-NULL-"/>
    ///            <field
    ///               name="EntityNumber"
    ///               dataType="Unsignedinteger16BEN_slsh_Aperfectalways1"
    ///               semantics="-NULL-"/>
    ///    </fixedRecordData>
    /// </code>
    /// </summary>
    public struct EntityIdentifierStruct
    {
        public FederateIdentifierStruct FederateIdentifier;
        public UInt16 EntityNumber;
    }
}
