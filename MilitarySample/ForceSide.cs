using System;
using System.Collections.Generic;
using System.Text;

using Sxta.Core.Plugins;

namespace Sxta.Rti1516.MilitarySample
{
    public class ForceSide
    {
        [XmlMemberAttribute("ObjectHandle")]
        protected short objectHandle;

        [XmlMemberAttribute("ForceSideName")]
        protected string forceSideName;

        public short ObjectHandle
        {
            get { return objectHandle; }
        }

        public string ForceSideName
        {
            get { return forceSideName; }
        }
    }
}
