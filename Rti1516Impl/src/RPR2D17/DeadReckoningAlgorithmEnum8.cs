using System;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///  <enumeratedData
    ///         name="DeadReckoningAlgorithmEnum8"
    ///         nameNotes="8"
    ///         representation="HLAoctet"
    ///         semantics="-NULL-">
    ///         <enumerator
    ///            name="Other"
    ///            values="0"/>
    ///         <enumerator
    ///            name="Static"
    ///            values="1"/>
    ///         <enumerator
    ///            name="DRM_FPW"
    ///            values="2"/>
    ///         <enumerator
    ///            name="DRM_RPW"
    ///            values="3"/>
    ///         <enumerator
    ///            name="DRM_RVW"
    ///            values="4"/>
    ///         <enumerator
    ///            name="DRM_FVW"
    ///            values="5"/>
    ///         <enumerator
    ///            name="DRM_FPB"
    ///            values="6"/>
    ///         <enumerator
    ///            name="DRM_RPB"
    ///            values="7"/>
    ///         <enumerator
    ///            name="DRM_RVB"
    ///            values="8"/>
    ///         <enumerator
    ///            name="DRM_FVB"
    ///            values="9"/>
    ///      </enumeratedData>
    /// </code>
    /// </summary>
    public enum DeadReckoningAlgorithmEnum8 : byte
    {
        Other = 0,
        Static = 1,
        DRM_FPW = 2,
        DRM_RPW = 3,
        DRM_RVW = 4,
        DRM_FVW = 5,
        DRM_FPB = 6,
        DRM_RPB = 7,
        DRM_RVB = 8,
        DRM_FVB = 9
    }
}
