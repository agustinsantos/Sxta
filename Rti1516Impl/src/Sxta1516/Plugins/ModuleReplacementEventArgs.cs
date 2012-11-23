namespace Sxta.Core.Plugins
{
	using System;
	
	public delegate void ModuleReplacementEventHandler(object sender, ModuleReplacementEventArgs e);


	/// <summary> 
	/// An event fired when a IModule is replaced (e.g., upgraded).
	/// 
	/// </summary>
	[Serializable]
	public class ModuleReplacementEventArgs : ModuleEventArgs
	{
		/// <summary>
		/// Returns a reference to the retired Module.
		/// </summary>
		/// <returns> a reference to the retired Module
		/// </returns>
		virtual public IModule RetiredModule
		{
			get
			{
				return retired;
			}
			
		}
		/// <summary> Returns a reference to the replacement Module.
		/// 
		/// </summary>
		/// <returns> a reference to the replacement Module
		/// </returns>
		virtual public IModule ReplacementModule
		{
			get
			{
				return replacement;
			}
			
		}
		/// <summary> The retired Module.</summary>
		protected internal IModule retired;
		
		/// <summary> The replacement Module.</summary>
		protected internal IModule replacement;
		
		
		/// <summary> Constructor.
		/// 
		/// </summary>
		/// <param name="pRetiredModule">the retired Module
		/// </param>
		/// <param name="pReplacementModule">the replacement Module
		/// </param>
		public ModuleReplacementEventArgs(IModule pRetired, IModule pReplacement):base(pRetired)
		{
			
			retired = pRetired;
			
			replacement = pReplacement;
		}
	}
}