using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{
    //representation="HLAoctet"
    //semantics="-NULL-">
    public enum ForceIdentifierEnum8
    {
        Other = 0,
        Friendly = 1,
        Opposing = 2,
        Neutral = 3,
        Friendly_2 = 4,
        Opposing_2 = 5,
        Neutral_2 = 6,
        Friendly_3 = 7,
        Opposing_3 = 8,
        Neutral_3 = 9,
        Friendly_4 = 10,
        Opposing_4 = 11,
        Neutral_4 = 12,
        Friendly_5 = 13,
        Opposing_5 = 14,
        Neutral_5 = 15,
        Friendly_6 = 16,
        Opposing_6 = 17,
        Neutral_6 = 18,
        Friendly_7 = 19,
        Opposing_7 = 20,
        Neutral_7 = 21,
        Friendly_8 = 22,
        Opposing_8 = 23,
        Neutral_8 = 24,
        Friendly_9 = 25,
        Opposing_9 = 26,
        Neutral_9 = 27,
        Friendly_10 = 28,
        Opposing_10 = 29,
        Neutral_10 = 30
    }

    public class ForceIdentifierSupport
    {
        /// <summary>
        /// returns true if the force is friendly, false otherwise
        /// </summary>
        /// <param name="force">the force</param>
        /// <returns>true if the force is friendly</returns>
        public static bool IsFriendly(ForceIdentifierEnum8 force)
        {
            return (((int)force - 1) % 3 == 0);
        }

        /// <summary>
        /// returns true if the force is opposing, false otherwise
        /// </summary>
        /// <param name="force">the force</param>
        /// <returns>true if the force is opposing</returns>
        public static bool IsOpposing(ForceIdentifierEnum8 force)
        {
            return (((int)force - 1) % 3 == 1);
        }

        public static bool IsNeutral(ForceIdentifierEnum8 force)
        {
            return (((int)force - 1) % 3 == 2);
        }

        public static bool IsOther(ForceIdentifierEnum8 force)
        {
            return (force == ForceIdentifierEnum8.Other);
        }
    }

    internal class ForceIndentifierTest
    {
        public void Test1()
        {
            foreach (RPR2D17.ForceIdentifierEnum8 force in Enum.GetValues(typeof(RPR2D17.ForceIdentifierEnum8)))
            {
                if (RPR2D17.ForceIdentifierSupport.IsFriendly(force))
                {
                    Console.WriteLine(force + " is Friendly");
                }
                else if (RPR2D17.ForceIdentifierSupport.IsOpposing(force))
                {
                    Console.WriteLine(force + " is Opposing");
                }
                else if (RPR2D17.ForceIdentifierSupport.IsNeutral(force))
                {
                    Console.WriteLine(force + " is Neutral");
                }
                else if (RPR2D17.ForceIdentifierSupport.IsOther(force))
                {
                    Console.WriteLine(force + " is Other");
                }
                else Console.WriteLine("Oops, what is " + force + "?");
            }
        }
    }
}

