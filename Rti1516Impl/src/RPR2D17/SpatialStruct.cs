using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///     <fixedRecordData
    ///         name="SpatialStruct"
    ///         semantics="-NULL-">
    ///         <field
    ///            name="DeadReckoningAlgorithm-A-Alternatives"
    ///            dataType="SpatialStruct-DeadReckoningAlgorithm"
    ///            semantics="-NULL-"/>
    ///      </fixedRecordData>
    /// </code>
    /// </summary>
    public struct SpatialStruct
    {
        public SpatialStruct_DeadReckoningAlgorithm DeadReckoningAlgorithm_A_Alternatives
        {
            get { return deadReckoningAlgorithm_A_Alternatives; }
            set { deadReckoningAlgorithm_A_Alternatives = value; }
        }

        private SpatialStruct_DeadReckoningAlgorithm deadReckoningAlgorithm_A_Alternatives;
    }


    /// <summary>
    /// <code>
    ///           <variantRecordData
    ///         name="SpatialStruct-DeadReckoningAlgorithm"
    ///         discriminant="DeadReckoningAlgorithm"
    ///         dataType="DeadReckoningAlgorithmEnum8"
    ///         encoding="HLAvariantRecord"
    ///         semantics="-NULL-">
    ///         <alternative
    ///            enumerator="Static"
    ///            name="SpatialStatic"
    ///            dataType="SpatialStaticStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_FPW"
    ///            name="SpatialFPW"
    ///            dataType="SpatialFPStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_RPW"
    ///            name="SpatialRPW"
    ///            dataType="SpatialRPStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_RVW"
    ///            name="SpatialRVW"
    ///            dataType="SpatialRVStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_FVW"
    ///            name="SpatialFVW"
    ///            dataType="SpatialFVStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_FPB"
    ///            name="SpatialFPB"
    ///            dataType="SpatialFPStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_RPB"
    ///            name="SpatialRPB"
    ///            dataType="SpatialRPStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_RVB"
    ///            name="SpatialRVB"
    ///            dataType="SpatialRVStruct"
    ///            semantics="-NULL-"/>
    ///         <alternative
    ///            enumerator="DRM_FVB"
    ///            name="SpatialFVB"
    ///            dataType="SpatialFVStruct"
    ///            semantics="-NULL-"/>
    ///      </variantRecordData>
    /// </code>
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public class SpatialStruct_DeadReckoningAlgorithm
    {
        public DeadReckoningAlgorithmEnum8 DeadReckoningAlgorithm
        {
            get { return deadReckoningAlgorithm; }
            set { deadReckoningAlgorithm = value; }
        }

        /// <summary>
        ///   <alternative
        ///       enumerator="Static"
        ///       name="SpatialStatic"
        ///       dataType="SpatialStaticStruct"
        ///       semantics="-NULL-"/>
        /// </summary>
        public SpatialStaticStruct SpatialStatic
        {
            get
            {
                if (deadReckoningAlgorithm != DeadReckoningAlgorithmEnum8.Static)
                    throw new Exception("Invalid Variant Record access. Discriminant is not Static");
                else
                    return spatialStatic;
            }
            set
            {
                if (deadReckoningAlgorithm != DeadReckoningAlgorithmEnum8.Static)
                    throw new Exception("Invalid Variant Record access. Discriminant is not Static");
                else
                    spatialStatic = value;
            }
        }

        /// <summary>
        ///     <alternative
        ///         enumerator="DRM_FPW"
        ///         name="SpatialFPW"
        ///         dataType="SpatialFPStruct"
        ///         semantics="-NULL-"/>
        /// </summary>
        public SpatialFPStruct SpatialFPW
        {
            get
            {
                if (deadReckoningAlgorithm != DeadReckoningAlgorithmEnum8.DRM_FPW)
                    throw new Exception("Invalid Variant Record access. Discriminant is not DRM_FPW");
                else
                    return spatialFPW;
            }
            set
            {
                if (deadReckoningAlgorithm != DeadReckoningAlgorithmEnum8.DRM_FPW)
                    throw new Exception("Invalid Variant Record access. Discriminant is not DRM_FPW");
                else
                    spatialFPW = value;
            }
        }

        [System.Runtime.InteropServices.FieldOffset(0)]
        private DeadReckoningAlgorithmEnum8 deadReckoningAlgorithm;

        [System.Runtime.InteropServices.FieldOffset(4)]
        private SpatialStaticStruct spatialStatic;
        
        [System.Runtime.InteropServices.FieldOffset(4)]
        private SpatialFPStruct spatialFPW;
    }
}
