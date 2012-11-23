using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///  <fixedRecordData
    ///          name="SpatialStaticStruct"
    ///          semantics="-NULL-">
    ///          <field
    ///             name="WorldLocation"
    ///             dataType="WorldLocationStruct"
    ///             semantics="-NULL-"/>
    ///          <field
    ///             name="IsFrozen"
    ///             nameNotes="48 78"
    ///             dataType="OMT13boolean"
    ///             semantics="Freeze Motion"/>
    ///          <field
    ///             name="Orientation"
    ///             dataType="OrientationStruct"
    ///             semantics="-NULL-"/>
    ///       </fixedRecordData>
    /// </code>
    /// </summary>
    public class SpatialStaticStruct
    {
        public WorldLocationStruct WorldLocation
        {
            get { return worldLocation; }
            set { worldLocation = value; }
        }

        public bool IsFrozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }

        public OrientationStruct Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        protected WorldLocationStruct worldLocation;
        protected bool isFrozen;
        protected OrientationStruct orientation;
    }
}
