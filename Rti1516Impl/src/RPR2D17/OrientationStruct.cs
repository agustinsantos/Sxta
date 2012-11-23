using System;
namespace Sxta.RPR2D17
{
    /// <summary>
    /// The Orientation attribute describes the object’s orientation in space. 
    /// The object’s orientation is described by three angles: 
    /// Psi or heading, Theta or pitch, and Phi or roll. 
    /// The units for the three angles are in radians.
    /// The Orientation attribute is represented as a
    /// struct of three floats. 
    /// <code>
    ///    <fixedRecordData
    ///         name="OrientationStruct"
    ///         semantics="The Orientation attribute describes the object’s orientation in space">
    ///         <field
    ///            name="Psi"
    ///            dataType="HLAfloat32BEradiansperfectalways"
    ///            semantics="Psi angle in radians or heading"/>
    ///         <field
    ///            name="Theta"
    ///            dataType="HLAfloat32BEradiansperfectalways"
    ///            semantics="Theta angle in radians or pitch"/>
    ///         <field
    ///            name="Phi"
    ///            dataType="HLAfloat32BEradiansperfectalways"
    ///            semantics="Phi angle in radians or roll"/>
    ///      </fixedRecordData>
    /// </code>
    /// </summary>
    public struct OrientationStruct
    {
        public float Psi
        {
            get { return psi; }
            set { psi = value; }
        }

        public float Theta
        {
            get { return theta; }
            set { theta = value; }
        }

        public float Phi
        {
            get { return phi; }
            set { phi = value; }
        }

        private float psi, theta, phi;
    }
}
