namespace Sxta.RPR2D17
{
    using System;

    using Hla.Rti1516;
    using Sxta.Rti1516.Reflection;

    /// <summary>
    /// A base class of aggregate and discrete scenaro domain participants. The BaseEntity class is 
    /// characterised by being located at a particular location in space and independently movable, 
    /// if capable of movement at all. It specifically excludes elements normally considered to be a 
    /// component of another element. The BaseEntity class is intended to be a container for common 
    /// attributes for entities of this type.  Since it lacks sufficient class specific attributes that 
    /// are required for simulation purposes it is not expected that any federate shall publish objects
    /// of this class.  Certain simulation management federates, e.g. viewers, may subscribe to this 
    /// class.  Simulation federates will normally subscribe to one of the subclasses, to gain the extra 
    /// information required to properly simulate the entity. (See section 6.1 of the GRIM)
    /// <code>
    ///    <objectClass
    ///         name="BaseEntity"
    ///         nameNotes="25"
    ///         sharing="Subscribe"
    ///         semantics="A base class of aggregate and discrete scenaro domain participants. The BaseEntity class is characterised by being located at a particular location in space and independently movable, if capable of movement at all. It specifically excludes elements normally considered to be a component of another element. The BaseEntity class is intended to be a container for common attributes for entities of this type.  Since it lacks sufficient class specific attributes that are required for simulation purposes it is not expected that any federate shall publish objects of this class.  Certain simulation management federates, e.g. viewers, may subscribe to this class.  Simulation federates will normally subscribe to one of the subclasses, to gain the extra information required to properly simulate the entity. (See section 6.1 of the GRIM)">
    ///        <attribute
    ///            name="EntityType"
    ///            dataType="EntityTypeStruct"
    ///            updateType="Static"
    ///            updateCondition="N/A"
    ///            ownership="NoTransfer"
    ///            sharing="PublishSubscribe"
    ///            dimensions="NA"
    ///            transportation="HLAbestEffort"
    ///            order="Receive"
    ///            semantics="The category of the entity."/>
    ///         <attribute
    ///            name="EntityIdentifier"
    ///            dataType="EntityIdentifierStruct"
    ///            updateType="Static"
    ///            updateCondition="N/A"
    ///            ownership="NoTransfer"
    ///            sharing="PublishSubscribe"
    ///            dimensions="NA"
    ///            transportation="HLAbestEffort"
    ///            order="Receive"
    ///            semantics="The unique identifier for the entity instance."/>
    ///         <attribute
    ///            name="IsPartOf"
    ///            dataType="IsPartOfStruct"
    ///            updateType="Conditional"
    ///            updateCondition="On change"
    ///            ownership="NoTransfer"
    ///            sharing="PublishSubscribe"
    ///            dimensions="NA"
    ///            transportation="HLAbestEffort"
    ///            order="Receive"
    ///            semantics="Defines if the entity if a constituent part of another entity (denoted the host entity). If the entity is a constituent part of another entity then the ParentEntityID shall be set to the EntityID of the host entity and the ParentRTIObjectID shall be set to the RTO object ID of the host entity. If the entity is not a constituent part of another entity then the ParentEntityID shall being set to 0.0.0 and the ParentRTIObjectID shall be set to the empty string."/>
    ///         <attribute
    ///            name="Spatial"
    ///            nameNotes="77"
    ///            dataType="SpatialStruct"
    ///            updateType="Conditional"
    ///            ownership="NoTransfer"
    ///            sharing="PublishSubscribe"
    ///            dimensions="NA"
    ///            transportation="HLAbestEffort"
    ///            order="Receive"
    ///            semantics="Spatial state stored in one variant record attribute"/>
    ///         <attribute
    ///            name="RelativeSpatial"
    ///            nameNotes="77"
    ///            dataType="SpatialStruct"
    ///            updateType="Conditional"
    ///            ownership="NoTransfer"
    ///            sharing="PublishSubscribe"
    ///            dimensions="NA"
    ///            transportation="HLAbestEffort"
    ///            order="Receive"
    ///            semantics="Relative spatial state stored in one variant record attribute"/>
    /// </code>
    /// </summary>
    [HLAObjectClassAttribute(Name = "BaseEntity",
                             NameNotes = "25",
                            Sharing = HLAsharingType.Subscribe,
                             Semantics = "A base class of aggregate and discrete scenaro domain participants. The BaseEntity class is characterised by being located at a particular location in space and independently movable, if capable of movement at all. It specifically excludes elements normally considered to be a component of another element. The BaseEntity class is intended to be a container for common attributes for entities of this type.  Since it lacks sufficient class specific attributes that are required for simulation purposes it is not expected that any federate shall publish objects of this class.  Certain simulation management federates, e.g. viewers, may subscribe to this class.  Simulation federates will normally subscribe to one of the subclasses, to gain the extra information required to properly simulate the entity. (See section 6.1 of the GRIM)")]
    public class BaseEntity : HLAobjectRoot
    {


        /// <summary>
        /// The category of the entity.
        /// <code>
        /// <attribute
        ///     name="EntityType"
        ///     dataType="EntityTypeStruct"
        ///     updateType="Static"
        ///     updateCondition="N/A"
        ///     ownership="NoTransfer"
        ///     sharing="PublishSubscribe"
        ///     dimensions="NA"
        ///     transportation="HLAbestEffort"
        ///     order="Receive"
        ///     semantics="The category of the entity."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "EntityType",
                     Sharing = HLAsharingType.PublishSubscribe,
                      Transportation = "HLAbestEffort",
                      Semantics = "The category of the entity.")]
        public EntityTypeStruct EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }



        /// <summary>
        /// The unique identifier for the entity instance.
        /// <code>
        ///         <attribute
        ///            name="EntityIdentifier"
        ///            dataType="EntityIdentifierStruct"
        ///            updateType="Static"
        ///            updateCondition="N/A"
        ///            ownership="NoTransfer"
        ///            sharing="PublishSubscribe"
        ///            dimensions="NA"
        ///            transportation="HLAbestEffort"
        ///            order="Receive"
        ///            semantics="The unique identifier for the entity instance."/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "EntityIdentifier",
                       Sharing = HLAsharingType.PublishSubscribe,
                        Transportation = "HLAbestEffort",
                        Semantics = "The unique identifier for the entity instance.")]
        public EntityIdentifierStruct EntityIdentifier
        {
            get { return entityIdentifier; }
            set { entityIdentifier = value; }
        }

        /// <summary>
        /// Spatial state stored in one variant record attribute
        /// <code>
        ///         <attribute
        ///            name="Spatial"
        ///            nameNotes="77"
        ///            dataType="SpatialStruct"
        ///            updateType="Conditional"
        ///            ownership="NoTransfer"
        ///            sharing="PublishSubscribe"
        ///            dimensions="NA"
        ///            transportation="HLAbestEffort"
        ///            order="Receive"
        ///            semantics="Spatial state stored in one variant record attribute"/>
        /// </code>
        /// </summary>
        [HLAAttribute(Name = "Spatial",
                      NameNotes = "77",
                     Sharing = HLAsharingType.PublishSubscribe,
                      Transportation = "HLAbestEffort",
                      Semantics = "Spatial state stored in one variant record attribute")]
        public SpatialStruct Spatial
        {
            get { return spatial; }
            set { spatial = value; }
        }


        #region Protected and Private

        /// <summary>
        /// The category of the entity.
        /// </summary>
        protected EntityTypeStruct entityType;

        /// <summary>
        /// The unique identifier for the entity instance.
        /// </summary>
        protected EntityIdentifierStruct entityIdentifier;

        /// <summary>
        /// Spatial state stored in one variant record attribute
        /// </summary>
        public SpatialStruct spatial;

        #endregion
    }
}
