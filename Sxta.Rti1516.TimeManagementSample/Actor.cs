using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.TimeManagementSample
{
    [HLAObjectClassAttribute(Name = "Actor",
                         Sharing = HLAsharingType.PublishSubscribe,
                         Semantics = "An actor.")]
    public class Actor : HLAobjectRoot
    {
        /// <summary>
        /// Enum for the possible directions of Actor
        /// </summary>
        public enum MoveDirection
        {
            Right,
            Up,
            Down,
            Left
        }

        protected String name;
        protected String color;
        protected int posX;
        protected int posY;
        protected MoveDirection direction;
        protected Home home;

        //[HLAAttribute(Name = "Name", Semantics = "actor's name")]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Color
        {
            get { return color; }
            set { color = value; }
        }

        [HLAAttribute(Name = "PosX", Semantics = "The actor's X-coordinate")]
        public virtual int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        [HLAAttribute(Name = "PosY", Semantics = "The actor's Y-coordinate")]
        public virtual int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        [HLAAttribute(Name = "Direction", Semantics = "The direction's move of the actor")]
        public virtual MoveDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Home Home
        {
            get { return home; }
            set { home = value; }
        }

        #region Constructor

        // Create an instance of Actor
        static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

        public static Actor NewActor()
        {
            return (Actor)NewInstance(myCallType);
        }

        public static Actor NewActor(String name, String color,
            MoveDirection initialDirection, Home homeActor)
        {
            return (Actor)NewInstance(myCallType, name, color, initialDirection, homeActor);
        }
      
        protected Actor() { }

        protected Actor(String name, String color, 
            MoveDirection initialDirection, Home homeActor)
        {
            this.Name = name;
            this.color = color;
            this.PosX = homeActor.PosX;
            this.PosY = homeActor.PosY;
            this.Direction = initialDirection;
            this.home = homeActor;
        }

        #endregion

        public void Move(MoveDirection aDirection)
        {
            Direction = aDirection;

            switch (aDirection)
            {
                case MoveDirection.Up:
                    PosY--;
                    break;
                case MoveDirection.Down:
                    PosY++;
                    break;
                case MoveDirection.Right:
                    PosX++;
                    break;
                case MoveDirection.Left:
                    PosX--;
                    break;
            }
        }

        public override String ToString()
        {
            return "Actor(" + posX + ", " + posY + ", " + direction + ")";
        }
    }
}
