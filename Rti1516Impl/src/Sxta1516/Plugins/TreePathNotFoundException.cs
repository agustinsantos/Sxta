using System;

namespace Sxta.Core.Plugins
{
	/// <summary>
	/// Is thrown when the PluginTree could not find the requested path.
	/// </summary>
	public class TreePathNotFoundException : Exception
	{
		/// <summary>
		/// Constructs a new <see cref="TreePathNotFoundException"/>
		/// </summary>
		public TreePathNotFoundException(string path) : base("Treepath not found : " + path)
		{
		}
	}
}
