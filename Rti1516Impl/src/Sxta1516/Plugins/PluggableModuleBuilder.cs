namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections;

    [ModuleNameAttribute("Module")]
    public class PluggableModuleBuilder : AbstractPluggableModule
    {
        /// <summary>
        /// Creates an item with the specified sub items. And the current
        /// Condition status for this item.
        /// </summary>
        public object BuildItem(object owner, ArrayList subItems)
        {
            return Plugin.CreateObject(Class);
        }

    }
}
