namespace Sxta.Rti1516.Tests.Rti1516
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.IO;

    using NUnit.Framework;

    // Import log4net classes.
    using log4net;

    using Nini.Config;

    using Hla.Rti1516;
    using Sxta.Rti1516;
    using Sxta.Rti1516.Ambassadors;
    using Sxta.Rti1516.BaseApplication;

    /// <summary>
    /// This class represents a federate that can be used for testing purposes. 
    /// </summary>
    public class TestFederate : XrtiFederateAmbassador
    {
        public TestFederate(IRTIambassador rtiAmbassador)
            : base(rtiAmbassador)
        {
        }
    }
}
