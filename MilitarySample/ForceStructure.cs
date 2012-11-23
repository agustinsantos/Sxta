using System;
using System.Collections.Generic;
using System.Text;

using Sxta.Core.Plugins;

namespace Sxta.Rti1516.MilitarySample
{
    public class ForceStructure
    {
        [XmlMemberGenericListAttribute("ForceSides", "ForceSide", IsRequired = true)]
        protected List<ForceSide> forceSides;


        public List<ForceSide> ForceSides
        {
            get { return forceSides; }
            set { forceSides = value; }
        }
    }
}
