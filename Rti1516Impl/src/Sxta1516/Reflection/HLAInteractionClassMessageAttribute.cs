namespace Sxta.Rti1516.Reflection
{
    using System;

    using Hla.Rti1516;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class HLAInteractionClassMessageAttribute : HLAReflectionAttribute
    {
    }
}
