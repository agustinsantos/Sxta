using System;
using System.Threading;
using System.Collections.Generic;
using Mogre;

using Hla.Rti1516;

using Sxta.Rti1516.Ambassadors;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.BoidSample
{
    public delegate void AddNewBoid(Boid boid);
    public class BoidsManager
    {
        public event AddNewBoid OnNewBoid;

        protected static float PROXIMITY = 2.5f;
        // flockmates are boids that are this close (or closer)

        // scaling factors for the velocities calculated by the rules
        private static float COHESION_WEIGHT = 0.2f;
        private static float SEPERATION_WEIGHT = 0.2f;
        private static float ALIGNMENT_WEIGHT = 0.2f;


        /// <summary>
        /// holds Boid subclass objects
        /// </summary>
        protected List<Boid> boidsList = new List<Boid>();
        protected List<Boid> knownBoidsList = new List<Boid>();
        protected XrtiFederateAmbassador federateAmbassador;

        // used for repeated calculations
        private Vector3 avgPosn = new Vector3();
        private Vector3 distFrom = new Vector3();
        private Vector3 moveAway = new Vector3();
        private Vector3 avgVel = new Vector3();


        public BoidsManager(XrtiFederateAmbassador fed)
        {
            federateAmbassador = fed;
            federateAmbassador.OnNewObject += new FederateAmbassador.NewObjectDiscovered(OnDiscoverNewBoid);
        }

        public void BuildBoids(int numBoids, Vector3 floorCenter, string color)
        {
            Boid boid;
            for (int index = 0; index < numBoids; index++)
            {
                boid = Boid.NewBoid(1.0f, this, floorCenter, color);
                boid.AutoFlushDisabled = true;
                boidsList.Add(boid);
                OnDiscoverNewBoid(boid);
            }
        }

        public List<Boid> BoidsList
        {
            get { return boidsList; }
        }

        public List<Boid> KnownBoidsList
        {
            get
            {
                return knownBoidsList;
            }
        }

        /// <summary>
        /// A boid tries to fly towards the average position of its flockmates. 
        /// 
        /// The velocity is a small step from the boid's current
        /// position (stored in boidPos) towards the average position.
        /// </summary>
        /// <param name="boidPos"></param>
        /// <returns></returns>
        public Vector3 Cohesion(Vector3 boidPos)
        {
            avgPosn = new Vector3(0, 0, 0);   // reset and default answer
            int numFlockMates = 0;
            Vector3 pos;

            for (int index = 0; index < KnownBoidsList.Count; index++)
            {
                distFrom = boidPos;
                pos = (KnownBoidsList[index] as Boid).Position;
                distFrom -= pos;
                if (distFrom.Length < PROXIMITY)
                {   // is the boid a flockmate?
                    avgPosn += pos;		// add position to tally
                    numFlockMates++;
                }
            }

            avgPosn -= boidPos;  // don't include the boid itself
            numFlockMates--;

            if (numFlockMates > 0)
            {   // there were flockmates
                avgPosn *= 1.0f / numFlockMates;   // calculate average position
                // calculate a small step towards the avg. posn
                avgPosn -= boidPos;
                avgPosn *= COHESION_WEIGHT;
            }
            return avgPosn;
        }



        /// <summary>
        /// A boid tries to keep a small distance away from its flockmates
        /// The velocity is the average distance of the flockmates from the boid,
        /// scaled so that the boid moves a little bit away instead
        /// of a mighty leap.
        /// </summary>
        /// <param name="boidPos"></param>
        /// <returns></returns>
        public Vector3 Separation(Vector3 boidPos)
        {
            moveAway = new Vector3(0, 0, 0);
            int numFlockMates = 0;

            for (int index = 0; index < KnownBoidsList.Count; index++)
            {
                distFrom = boidPos;
                distFrom -= (KnownBoidsList[index] as Boid).Position;
                if (distFrom.Length < PROXIMITY)
                {   // is the boid a flockmate?
                    moveAway += distFrom;	   // add distance away to tally
                    numFlockMates++;
                }
            }
            numFlockMates--;    // don't count the boid's distance from itself
            if (numFlockMates > 0)
            {
                moveAway *= 1.0f / numFlockMates;   // calculate average distance from
                // scale to reduce distance moved away
                moveAway *= SEPERATION_WEIGHT;
            }
            return moveAway;
        }

        /// <summary>
        /// A boid tries to travel with the average velocity of its flockmates.
        /// The velocity is scaled so that the boid adjusts its velocity 
        /// gradually.
        /// </summary>
        /// <param name="boidPos"></param>
        /// <param name="boidVel"></param>
        /// <returns></returns>
        public Vector3 Alignment(Vector3 boidPos, Vector3 boidVel)
        {
            avgVel = new Vector3(0, 0, 0);
            int numFlockMates = 0;
            Vector3 pos;
            Boid b;

            for (int index = 0; index < KnownBoidsList.Count; index++)
            {
                b = KnownBoidsList[index] as Boid;

                distFrom = boidPos;
                pos = b.Position;
                distFrom -= pos;
                if (distFrom.Length < PROXIMITY)
                {  // is the boid a flockmate?
                    avgVel += b.Velocity;
                    // add its velcoity to the tally		
                    numFlockMates++;
                }
            }
            avgVel -= boidVel;  // don't include boid's own velocity
            numFlockMates--;

            if (numFlockMates > 0)
            {
                avgVel *= 1.0f / numFlockMates;
                // scale to reduce velocity change towards the average
                avgVel *= ALIGNMENT_WEIGHT;
            }
            return avgVel;
        }

        public Vector3 CohesionSeparationAlignment(Vector3 boidPos, Vector3 boidVel)
        {
            avgPosn = new Vector3(0, 0, 0);   // reset and default answer
            moveAway = new Vector3(0, 0, 0);
            avgVel = new Vector3(0, 0, 0);
            int numFlockMates = 0;
            Vector3 pos;
            Boid b;

            for (int index = 0; index < KnownBoidsList.Count; index++)
            {
                b = KnownBoidsList[index] as Boid;

                distFrom = boidPos;
                pos = b.Position;
                distFrom -= pos;
                if (distFrom.Length < PROXIMITY)
                {   // is the boid a flockmate?
                    avgPosn += pos;		    // add position to tally
                    moveAway += distFrom;	// add distance away to tally
                    avgVel += b.Velocity;   // add its velcoity to the tally
                    numFlockMates++;
                }
            }
            avgVel -= boidVel;  // don't include boid's own velocity
            numFlockMates--;

            if (numFlockMates > 0)
            {
                // there were flockmates

                avgPosn *= 1.0f / numFlockMates;   // calculate average position
                // calculate a small step towards the avg. posn
                avgPosn -= boidPos;
                avgPosn *= COHESION_WEIGHT;

                moveAway *= 1.0f / numFlockMates;   // calculate average distance from
                // scale to reduce distance moved away
                moveAway *= SEPERATION_WEIGHT;

                avgVel *= 1.0f / numFlockMates;
                // scale to reduce velocity change towards the average
                avgVel *= ALIGNMENT_WEIGHT;

            }
            return avgVel + moveAway + avgPosn;
        }

        public void DoSimulation(long ticks)
        {
            for (int index = 0; index < boidsList.Count; index++)
            {
                boidsList[index].UpdateEntity(ticks);
            }
        }

        public void OnDiscoverNewBoid(object proxy)
        {
            if (proxy is Boid)
            {
                Boid boid = proxy as Boid;
                knownBoidsList.Add(boid);
                if (OnNewBoid != null)
                    OnNewBoid(boid);
            }
        }

    }
}
