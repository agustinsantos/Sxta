namespace Sxta.RPR2D17
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;
    
    /// <summary>
    /// A base class of all discrete platform scenario domain participants.
    /// <code>
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "PhysicalEntity",
                         NameNotes = "25",
                        Sharing = HLAsharingType.Subscribe,
                         Semantics = "A base class of all discrete platform scenario domain participants.")]
    public class PhysicalEntity : BaseEntity
    {
        /// <summary>
        /// The state of damage of the entity
        /// <code>
        ///     <attribute
        ///           name="DamageState"
        ///           dataType="DamageStatusEnum32"
        ///           updateType="Conditional"
        ///           updateCondition="On change"
        ///           ownership="NoTransfer"
        ///           sharing="PublishSubscribe"
        ///           dimensions="NA"
        ///           transportation="HLAbestEffort"
        ///           order="Receive"
        ///           semantics="The state of damage of the entity."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "DamageState",
                   Sharing = HLAsharingType.PublishSubscribe,
                    Transportation = "HLAbestEffort",
                    Semantics = "The state of damage of the entity.")]
        public DamageStatusEnum32 DamageState
        {
            get { return damageState.DamageStatusEnum; }
            //set { damageState = value; }
        }

        /// <summary>
        /// The state of damage of the entity
        /// </summary>
        protected DamageStatus damageState;

    }
}
