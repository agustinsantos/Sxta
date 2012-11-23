using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.RPR2D17
{
    /// <summary>
    /// The state of damage of the entity.
    /// <code>
    /// <enumeratedData
    ///         name="DamageStatusEnum32"
    ///         nameNotes="10"
    ///         representation="HLAinteger32BE"
    ///         semantics="-NULL-">
    ///         <enumerator
    ///            name="NoDamage"
    ///            values="0"/>
    ///         <enumerator
    ///            name="SlightDamage"
    ///            values="1"/>
    ///         <enumerator
    ///            name="ModerateDamage"
    ///            values="2"/>
    ///         <enumerator
    ///            name="Destroyed"
    ///            values="3"/>
    ///      </enumeratedData>
    /// </code>
    /// </summary>
    public enum DamageStatusEnum32
    {
        NoDamage = 0,
        SlightDamage = 1,
        ModerateDamage = 2,
        Destroyed =3
    }

    /// <summary>
    /// A generic damage model using values from UInt32
    /// </summary>
    public struct DamageStatus
    {
        public UInt32 OveralDamage;

        public UInt32 MinDamageValue
        {
            get { return UInt32.MinValue; }
        }

        public UInt32 MaxDamageValue
        {
            get { return UInt32.MaxValue; }
        }

        public DamageStatusEnum32 DamageStatusEnum
        {
            get
            {
                if (OveralDamage <= NoDamageLimit)
                    return DamageStatusEnum32.NoDamage;
                else if (OveralDamage <= SlightDamageLimit)
                    return DamageStatusEnum32.SlightDamage;
                else if (OveralDamage <= ModerateDamageLimit)
                    return DamageStatusEnum32.ModerateDamage;
                else
                    return DamageStatusEnum32.Destroyed;

            }
        }

        private const UInt32 NoDamageLimit = (UInt32)((UInt32.MaxValue - UInt32.MinValue) * 0.05);
        private const UInt32 SlightDamageLimit = (UInt32)((UInt32.MaxValue - UInt32.MinValue) * 0.50);
        private const UInt32 ModerateDamageLimit = (UInt32)((UInt32.MaxValue - UInt32.MinValue) * 0.95);
    }
}
