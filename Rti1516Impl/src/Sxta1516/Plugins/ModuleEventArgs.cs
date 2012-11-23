namespace Sxta.Core.Plugins
{
	using System;
	
	public delegate void ModuleEventHandler(object sender, ModuleEventArgs e);

	/// <summary>
	///  The abstract superclass of all module events.
	/// </summary>
	[Serializable]
	public class ModuleEventArgs : System.EventArgs
	{
		/// <summary> 
		/// Sets/gets a reference to the module that generated the
		/// event.
		/// </summary>
		virtual public IModule Source
		{
			get
			{
				return source;
			}
			
		}

		/// <summary> The <code>IModule</code> that generated the event.</summary>
		protected internal IModule source;
		
		/// <summary>
		///  Constructor.
		/// </summary>
		/// <param name="pSource">the module that generated the event
		/// </param>
		public ModuleEventArgs(IModule pSource) : base()
		{
			
			source = pSource;
		}
	}
}