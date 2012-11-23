namespace ExternalSamples
{

    using System;
    using System.IO;
    using System.Collections.Generic;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;

    using Real = System.Single;

    //<fixedRecordData name="Vector3FloatStruct" 
    //        semantics="A vector of three floats used for position or velocity">
    //    <field name="XComponent" dataType="HLAfloat32BE" semantics="Component along the X axis" />
    //    <field name="YComponent" dataType="HLAfloat32BE" semantics="Component along the Y axis" />
    //    <field name="ZComponent" dataType="HLAfloat32BE" semantics="Component along the Z axis" />
    //</fixedRecordData>
    [HLAFixedRecordDataAttribute(Name = "Vector3FloatStruct",
                                 Semantics = "A vector of three floats used for position or velocity")]
    public struct Vector3FloatStruct
    {
        public Vector3FloatStruct(float px, float py, float pz)
        {
            XComponent = px;
            YComponent = py;
            ZComponent = pz;
        }

        [HLARecordFieldAttribute(Name = "XComponent",
                                 DataType = "HLAfloat32BE",
                                 Semantics = "Component along the X axis")]
        public float XComponent;

        [HLARecordFieldAttribute(Name = "YComponent",
                         DataType = "HLAfloat32BE",
                         Semantics = "Component along the Y axis")]
        public float YComponent;

        [HLARecordFieldAttribute(Name = "ZComponent",
                         DataType = "HLAfloat32BE",
                         Semantics = "Component along the Z axis")]
        public float ZComponent;

        public override string ToString()
        {
            return "[ " + XComponent + ", " + YComponent + ", " + ZComponent + " ]";
        }

        #region Overloaded operators + CLS compliant method equivalents

        /// <summary>
        ///		Returns the square root of a number.
        /// </summary>
        /// <remarks>This is one of the more expensive math operations.  Avoid when possible.</remarks>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Real Sqrt(Real number)
        {
            return (Real)System.Math.Sqrt(number);
        }

        /// <summary>
        ///    Gets the length (magnitude) of this Vector3.  The Sqrt operation is expensive, so 
        ///    only use this if you need the exact length of the Vector.  If vector lengths are only going
        ///    to be compared, use LengthSquared instead.
        /// </summary>
        public float Length
        {
            get
            {
                return Sqrt(this.XComponent * this.XComponent + this.YComponent * this.YComponent + this.ZComponent * this.ZComponent);
            }
        }

        /// <summary>
        ///		User to compare two Vector3FloatStruct instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return (left.XComponent == right.XComponent && left.YComponent == right.YComponent && left.ZComponent == right.ZComponent);
        }

        /// <summary>
        ///		User to compare two Vector3FloatStruct instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return (left.XComponent != right.XComponent || left.YComponent != right.YComponent || left.ZComponent != right.ZComponent);
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Multiply(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return left * right;
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator *(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return new Vector3FloatStruct(left.XComponent * right.XComponent, left.YComponent * right.YComponent, left.ZComponent * right.ZComponent);
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Divide(Vector3FloatStruct left, float scalar)
        {
            return left / scalar;
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator /(Vector3FloatStruct left, float scalar)
        {
            Vector3FloatStruct vector = new Vector3FloatStruct();

            // get the inverse of the scalar up front to avoid doing multiple divides later
            float inverse = 1.0f / scalar;

            vector.XComponent = left.XComponent * inverse;
            vector.YComponent = left.YComponent * inverse;
            vector.ZComponent = left.ZComponent * inverse;

            return vector;
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is added to another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Add(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return left + right;
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is added to another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator +(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return new Vector3FloatStruct(left.XComponent + right.XComponent, left.YComponent + right.YComponent, left.ZComponent + right.ZComponent);
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Multiply(Vector3FloatStruct left, float scalar)
        {
            return left * scalar;
        }

        /// <summary>
        ///		Used when a Vector3FloatStruct is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator *(Vector3FloatStruct left, float scalar)
        {
            return new Vector3FloatStruct(left.XComponent * scalar, left.YComponent * scalar, left.ZComponent * scalar);
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector3.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Multiply(float scalar, Vector3FloatStruct right)
        {
            return scalar * right;
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector3.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator *(float scalar, Vector3FloatStruct right)
        {
            return new Vector3FloatStruct(right.XComponent * scalar, right.YComponent * scalar, right.ZComponent * scalar);
        }

        /// <summary>
        ///		Used to subtract a Vector3FloatStruct from another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Subtract(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return left - right;
        }

        /// <summary>
        ///		Used to subtract a Vector3FloatStruct from another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator -(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            return new Vector3FloatStruct(left.XComponent - right.XComponent, left.YComponent - right.YComponent, left.ZComponent - right.ZComponent);
        }


        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector3FloatStruct Negate(Vector3FloatStruct left)
        {
            return -left;
        }

        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector3FloatStruct operator -(Vector3FloatStruct left)
        {
            return new Vector3FloatStruct(-left.XComponent, -left.YComponent, -left.ZComponent);
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all smaller
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            if (left.XComponent > right.XComponent && left.YComponent > right.YComponent && left.ZComponent > right.ZComponent)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all greater
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Vector3FloatStruct left, Vector3FloatStruct right)
        {
            if (left.XComponent < right.XComponent && left.YComponent < right.YComponent && left.ZComponent < right.ZComponent)
            {
                return true;
            }

            return false;
        }

        #endregion

    }

    ///<summary>
    ///A country. 
    ///</summary>
    /// <author> </author>
    [HLAObjectClassAttribute(Name = "ExternalCountry",
                             Sharing = HLAsharingType.PublishSubscribe,
                             Semantics = "A country defined in a plugin.")]
    public class ExternalCountry : HLAobjectRoot
    {

        protected ExternalCountry() : base() { }

        #region Constructor
        // Create an instance of Country
        static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        public static ExternalCountry NewExternalCountry()
        {
            return (ExternalCountry)NewInstance(myCallType);
        }

        #endregion

        ///<summary>
        ///Attribute #Name. 
        ///</summary>
        private String name;

        ///<summary>
        ///Attribute #Population. 
        ///</summary>
        private double population;

        ///<summary>
        ///Attribute #Position. 
        ///</summary>
        private Vector3FloatStruct position;

        [HLAAttribute(Name = "Name", Semantics = "The name of the country.")]
        public virtual String Name
        {
            get { return name;  }
            set { name = value; }
        }

        [HLAAttribute(Name = "Population", Semantics = "The population of the country.")]
        public virtual double Population
        {
            get { return population; }
            set { population = value; }
        }


        ///<summary>
        /// Gets/Sets the value of the Position field.
        ///</summary>
        [HLAAttribute(Name = "Position",
                      Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                      DataType = "Vector3FloatStruct",
                      UpdateType = Sxta.Rti1516.Reflection.HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = Sxta.Rti1516.Reflection.HLAownershipType.NoTransfer,
                      Transportation = "HLAbestEffort",
                      Order = Sxta.Rti1516.Reflection.HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual Vector3FloatStruct Position
        {
            set { position = value; }
            get { return position; }
        }

        public override string ToString()
        {
            return "ExternalCountry(" + base.ToString() + ", Name:" + Name + ", Population:" + Population + ", Position:" + Position + ")";
        }
    }
}
