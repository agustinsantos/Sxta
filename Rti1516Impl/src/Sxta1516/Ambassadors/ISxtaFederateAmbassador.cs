namespace Sxta.Rti1516.Ambassadors
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.BoostrapProtocol;

    public interface ISxtaFederateAmbassador : IFederateAmbassador
    {
        void ReflectAttributeValuesExt(IObjectInstanceHandle theObject, HLAattributeHandleValuePair[] theAttributes, byte[] userSuppliedTag, OrderType sentOrdering, TransportationType theTransport);
        void RegisterObjectInstance(object obj);
        void DumpObjects();
    }
}
