using System;

namespace Sxta.Core.Plugins
{
	/// <summary>
	/// Is thrown when the PluginTree could not find the requested path.
	/// </summary>
	public class PluginLoadException : Exception
	{
		/// <summary>
		/// Constructs a new <see cref="PluginLoadException"/>
		/// </summary>
		public PluginLoadException(string reason) : base(reason)
		{
		}
	}
}
