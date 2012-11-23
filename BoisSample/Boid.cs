namespace Sxta.Rti1516.BoidSample
{
    using System;
    using System.Collections.Generic;

    using Mogre;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;

    ///<summary>
    ///Boids models the behaviour of flocking animals (eg. birds) by simple rules
    ///which describe only the behaviour of individuals. For a full explanation and
    ///an informative history of this algorithm see Craig Reynolds' boids page (http://www.red3d.com/cwr/boids/).
    ///</summary>
    [HLAObjectClass(Name = "Boid",
                    Sharing = Sxta.Rti1516.Reflection.HLAsharingType.PublishSubscribe,
                    Semantics = "Boids models the behaviour of flocking animals (eg. birds) by simple rules which describe only the behaviour of individuals. For a full explanation and an informative history of this algorithm see Craig Reynolds' boids page (http://www.red3d.com/cwr/boids/).")]
    public class Boid : HLAobjectRoot
    {
        #region Constructor
        // Create an instance of Country
        static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

        protected Boid() : base() { }
        public static Boid NewBoid()
        {
            return (Boid)NewInstance(myCallType);
        }

        public static Boid NewBoid(float velFactor, BoidsManager bh, Vector3 floorCenter, string boidColor)
        {
            return (Boid)NewInstance(myCallType, velFactor, bh, floorCenter, boidColor);
        }

        #endregion

        protected Boid(float velFactor, BoidsManager bh, Vector3 floorCenter, string boidColor)
        {
            maxSpeed = MAX_SPEED * velFactor;    // to vary the maxSpeed
            beh = bh;
            //center = floorCenter;
            MAX_PT += floorCenter;
            MIN_PT += floorCenter;

            Position = floorCenter + new Vector3(RandPosn(),
                                                (float)(random.NextDouble() * 6.0),
                                                RandPosn());
            Velocity = new Vector3(RandVel(), RandVel(), RandVel());
            Color = boidColor;
        }

        ///<summary>
        /// Gets/Sets the value of the Position field.
        ///</summary>
        [HLAAttribute(Name = "Position",
                      Sharing = HLAsharingType.PublishSubscribe,
                      DataType = "Vector3",
                      UpdateType = HLAupdateType.Static,
                      UpdateCondition = "NA",
                      OwnerShip = HLAownershipType.NoTransfer,
                      Transportation = "HLAbestEffort",
                      Order = HLAorderType.Receive,
                      Dimensions = "NA")]
        public virtual Vector3 Position
        {
            get { return boidPos; }
            set { boidPos = value; }
        }

        [HLAAttribute(Name = "Velocity", Semantics = "boid's velocity")]
        public virtual Vector3 Velocity
        {
            get { return boidVel; }
            set { boidVel = value; }
        }

        [HLAAttribute(Name = "Color", Semantics = "boid's color")]
        public virtual string Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// top-level method for updating the boid
        /// </summary>
        public void UpdateEntity(float timeSinceLastFrame)
        {
            // update the boid's vel & posn, but keep within scene bounds
            Velocity = CalcNewVel();
            Position = Position + Velocity * (timeSinceLastFrame * VelocityFactor);
            KeepInBounds();
        }

        public override string ToString()
        {
            return "Boid(" + base.ToString() + ", Color:" + Color + ", Position:" + Position + ", Velocity:" + Velocity + ")";
        }

        /// <summary>
        /// Adjust the boid's position and velocity so it stays 
        /// within the volume of space defined by MIN_PT and MAX_PT. 
        /// Also check if perching should be started.
        /// </summary>
        private void KeepInBounds()
        {
            Vector3 pos = Position;
            Vector3 vel = Velocity;

            // check if x part of the boid's position is within the volume
            if (pos.x > MAX_PT.x)
            {     // beyond max boundary
                pos.x = MAX_PT.x;         // put back at edge
                vel.x = -System.Math.Abs(vel.x);   // move away from boundary
            }
            else if (pos.x < MIN_PT.x)
            {
                pos.x = MIN_PT.x;
                vel.x = System.Math.Abs(vel.x);
            }

            // check if z part is within the volume
            if (pos.z > MAX_PT.z)
            {
                pos.z = MAX_PT.z;
                vel.z = -System.Math.Abs(vel.z);
            }
            else if (boidPos.z < MIN_PT.z)
            {
                pos.z = MIN_PT.z;
                vel.z = System.Math.Abs(vel.z);
            }

            // check if y part is within the volume
            if (pos.y > MAX_PT.y)
            {
                pos.y = MAX_PT.y;
                vel.y = -System.Math.Abs(vel.y);
            }
            else if (pos.y < MIN_PT.y)
            {
                pos.y = MIN_PT.y;
                vel.y = System.Math.Abs(vel.y);
            }
            Position = pos;
            Velocity = vel;
        }

        /// <summary>
        /// Apply the velocity rules, storing the new velocities
        /// in the velChanges ArrayList. Then add the velocities 
        /// together, and limit the total speed to maxSpeed.
        /// 
        /// The velocity rules are only applied if there are no obstacles
        ///   -- obstacle avoidance has priority over the velocity rules
        /// </summary>
        /// <returns>a new velocity</returns>
        private Vector3 CalcNewVel()
        {
            // then carry out the velocity rules
            newVel = boidVel + DoVelocityRules();
            newVel *= LimitMaxSpeed();
            return newVel;
        }


        /// <summary>
        /// override this method to add new velocity rules
        /// </summary>
        protected Vector3 DoVelocityRules()
        {
            return beh.Cohesion(boidPos) + beh.Separation(boidPos) + beh.Alignment(boidPos, boidVel);
            //return beh.CohesionSeparationAlignment(boidPos, boidVel);
        }

        private float LimitMaxSpeed()
        // scale boid speed so no faster than maxSpeed
        {
            float speed = boidVel.Length;
            if (speed > maxSpeed)
                return maxSpeed / speed;
            else
                return 1.0f;   // no scaling
        }

        /// <summary>
        /// return a float between -FLOOR_LEN/2 and FLOOR_LEN/2
        /// </summary>
        /// <returns>a random position</returns>
        private float RandPosn()
        {
            return (float)(random.NextDouble() * FLOOR_LEN - FLOOR_LEN / 2);
        }

        /// <summary>
        /// return a float between -MAX_SPEED/2 and MAX_SPEED/2
        /// </summary>
        /// <returns> a random velocity</returns>
        private float RandVel()
        {
            return (float)(random.NextDouble() * MAX_SPEED * 2 - MAX_SPEED);
        }

        #region Protected and Private

        // available to subclasses
        protected BoidsManager beh;

        // stores all the velocity changes generated by the 
        // velocity rules at each update
        protected Vector3 boidPos = new Vector3();
        protected Vector3 boidVel = new Vector3();
        protected string color = "green";

        protected static Random random = new Random();
        private float maxSpeed;

        // used for repeated calculations
        private Vector3 newVel = new Vector3();     // for holding the new boid velocity

        // the boundaries for boid movement in the scene
        private static int FLOOR_LEN = 100;  // should be even

        private Vector3 MIN_PT =
            new Vector3(-(float)FLOOR_LEN / 2.0f, 0.05f, -(float)FLOOR_LEN / 2.0f);
        private Vector3 MAX_PT =
           new Vector3((float)FLOOR_LEN / 2.0f, 8.0f, (float)FLOOR_LEN / 2.0f);

        private float MAX_SPEED = 0.2f;

        private static float VelocityFactor = 20.0f / TimeSpan.TicksPerSecond;

        #endregion
    }
}
