using System;
using System.Collections.Generic;
using System.Text;

using Sxta.Core.Plugins;

namespace Sxta.Rti1516.MilitarySample
{
    public class MilitaryScenario
    {
        [XmlMemberAttribute("ForceStructure")]
        protected ForceStructure forceStructure;

        public ForceStructure ForceStructure
        {
            set { forceStructure = value; }
            get { return forceStructure ; }
        }
    }
}
