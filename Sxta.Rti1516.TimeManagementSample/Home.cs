using System;
using System.Collections.Generic;
using System.Text;

using Hla.Rti1516;
using Sxta.Rti1516.Reflection;

namespace Sxta.Rti1516.TimeManagementSample
{
    [HLAObjectClassAttribute(Name = "Home",
                         Sharing = HLAsharingType.PublishSubscribe,
                         Semantics = "A home.")]
    public class Home : HLAobjectRoot
    {
        protected int posX = -1;
        protected int posY = -1;
        protected int boxesCount;

        [HLAAttribute(Name = "PosX", Semantics = "")]
        public virtual int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        [HLAAttribute(Name = "PosY", Semantics = "")]
        public virtual int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        [HLAAttribute(Name = "BoxesCount", Semantics = "The number of boxes in home")]
        public virtual int BoxesCount
        {
            get
            {
                lock (this)
                {
                    return boxesCount;
                }
            }

            set
            {
                lock (this)
                {
                    boxesCount = value;
                }
            }
        }

        protected Home() { }

        protected Home(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        protected Home(int posX, int posY, int nrOfBoxes) 
            : this(posX, posY)
        {
            this.BoxesCount = nrOfBoxes;
        }

        // Create an instance of Home
        static Type myCallType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

        public static Home NewHome()
        {
            return (Home)NewInstance(myCallType);
        }

        public static Home NewHome(int posX, int posY)
        {
            return (Home)NewInstance(myCallType, posX, posY);
        }

        public static Home NewHome(int posX, int posY, int nrOfBoxes)
        {
            return (Home)NewInstance(myCallType, posX, posY, nrOfBoxes);
        }

        public override String ToString()
        {
            return "Home(" + posX + ", " + posY + ")";
        }
    }
}
