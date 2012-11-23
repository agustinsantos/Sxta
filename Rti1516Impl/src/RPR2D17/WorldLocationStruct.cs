using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///    <fixedRecordData
    ///         name="WorldLocationStruct"
    ///         semantics="-NULL-">
    ///         <field
    ///            name="X"
    ///            dataType="HLAfloat64BEmetersperfectalways"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Y"
    ///            dataType="HLAfloat64BEmetersperfectalways"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="Z"
    ///            dataType="HLAfloat64BEmetersperfectalways"
    ///            semantics="-NULL-"/>
    ///      </fixedRecordData>
    /// </code>
    /// </summary>
    public struct WorldLocationStruct
    {
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        private double x, y, z;
    }
}
