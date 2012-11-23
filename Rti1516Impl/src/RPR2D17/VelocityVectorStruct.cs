using System;
namespace Sxta.RPR2D17
{
    /// <summary>
    /// <code>
    ///    <fixedRecordData
    ///         name="VelocityVectorStruct"
    ///         semantics="-NULL-">
    ///         <field
    ///            name="XVelocity"
    ///            dataType="HLAfloat32BEm_slsh_sperfectalways"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="YVelocity"
    ///            dataType="HLAfloat32BEm_slsh_sperfectalways"
    ///            semantics="-NULL-"/>
    ///         <field
    ///            name="ZVelocity"
    ///            dataType="HLAfloat32BEm_slsh_sperfectalways"
    ///            semantics="-NULL-"/>
    ///      </fixedRecordData>
    /// </code>
    /// </summary>
    public struct VelocityVectorStruct
    {
        public float XVelocity
        {
            get { return xVelocity; }
            set { xVelocity = value; }
        }

        public float YVelocity
        {
            get { return yVelocity; }
            set { yVelocity = value; }
        }

        public float ZVelocity
        {
            get { return zVelocity; }
            set { zVelocity = value; }
        }

        private float xVelocity, yVelocity, zVelocity;
    }
}
