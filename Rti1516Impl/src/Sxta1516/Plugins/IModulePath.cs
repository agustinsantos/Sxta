
namespace Sxta.Core.Plugins
{
	using System;
	using System.Reflection;

	/// <summary>
	/// This is the basic interface to module tree path. 
	/// </summary>
	public interface IModulePath
	{
		/// <summary>
        /// Returns a IModulePathNode corresponding to <paramref name="path"/>.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
        IModulePathNode ResolvePath(string path);

        /// <summary>
        /// Returns a IModule instance by its path <see cref="Resolve"/>
        /// </summary>
        IModule this[String id]
        {
            get;
        }

        IModulePathNode CreatePath(string path);
	}
}
