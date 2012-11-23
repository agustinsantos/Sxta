namespace Sxta.Rti1516.ObjectManagement
{

    using Hla.Rti1516;
    using System;
    using System.Collections.Generic;

    public interface IHLACreateObjectRootListener
    {
        void OnReceiveCreatedNewObject(object newObject);
    }
}
