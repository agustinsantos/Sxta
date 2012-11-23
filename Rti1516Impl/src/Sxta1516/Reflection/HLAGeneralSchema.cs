namespace Sxta.Rti1516.Reflection
{
    using Hla.Rti1516;


    /// <summary>
    /// Enumerated object model type.
    /// </summary>
    public enum HLAObjectModelType
    {
        FOM,
        SOM
    }


    /// <summary>
    /// Enumerated ownership type.
    /// </summary>
    public enum OwnershipType
    {
        Divest,
        Acquire,
        DivestAcquire,
        NoTransfer
    }


    /// <summary>
    /// Enumerated capability type.
    /// </summary>
    public enum CapabilityType
    {
        Register,
        Achieve,
        RegisterAchieve,
        NoSynch,
        NA
    }

    /// <summary>
    /// Enumerated switch type.
    /// </summary>
    /// TODO ANGEL: Esta clase o la que se encuentra en Sxta.Rti1516.Interactions.HLAswitch pueden estar duplicadas REVISAR
    public enum HLASwitchType
    {
        Enabled,
        Disabled,
        NA
    }
}
