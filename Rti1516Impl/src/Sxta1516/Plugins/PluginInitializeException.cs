using System;

namespace Sxta.Core.Plugins
{
	/// <summary>
	/// Is thrown when the PluginTree could not find the requested path.
	/// </summary>
	public class PluginInitializeException : Exception
	{
		/// <summary>
		/// Constructs a new <see cref="PluginInitializeException"/>
		/// </summary>
		public PluginInitializeException(string fileName, Exception e) :
			base("Could not load add-in file : " + fileName + "\n exception got :" + e.ToString())
		{
		}
	}
}
