using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{

    /// <summary>
    /// <code>
    ///    <fixedRecordData
    ///       name="EntityTypeStruct"
    ///       nameNotes="5 29"
    ///       semantics="-NULL-">
    ///         <field
    ///            name="EntityKind"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Domain"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="CountryCode"
    ///            dataType="Unsignedinteger16BEN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Category"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Subcategory"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Specific"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Extra"
    ///            dataType="HLAoctetN_slsh_Aperfectalways1"
    ///            semantics="-NULL-"/>
    ///    </fixedRecordData>
    /// </code>
    /// </summary>
    public struct EntityTypeStruct
    {
        public byte EntityKind;
        public byte Domain;
        public byte Category;
        public byte Subcategory;
        public UInt16 CountryCode;
        public byte Specific;
        public byte Extra;
    }

    public class EntityTypeSupport
    {
        public static string[] EntityKindNames = new string[]{  "Other",            // 0
                                                                "Platform",         // 1
                                                                "Munition",         // 2
                                                                "Life form",        // 3
                                                                "Environmental",    // 4
                                                                "Cultural feature", // 5
                                                                "Supply",           // 6
                                                                "Radio",            // 7
                                                                "Expendable",       // 8
                                                                "Sensor/Emitter"};  // 9

        public static string[] EntityPlatformDomainNames = new string[]{   "Other",        // 0
                                                                            "Land",         // 1
                                                                            "Air",          // 2
                                                                            "Surface",      // 3
                                                                            "Subsurface",   // 4
                                                                            "Space"};       // 5

        public static bool IsEntityKindValid(byte kind)
        {
            return ((kind >= 0) && (kind < EntityKindNames.Length));
        }
    }
}
