using System;
using System.Collections;
using System.Xml;

namespace Sxta.Core.Plugins
{
	public class ModuleNotFoundException : System.Exception
	{
		public ModuleNotFoundException(string message) : base(message)
		{
		}
	}
}
