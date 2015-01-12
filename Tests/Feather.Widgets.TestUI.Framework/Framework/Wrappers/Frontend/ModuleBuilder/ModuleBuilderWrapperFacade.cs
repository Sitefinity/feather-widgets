using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.ModuleBuilder
{
    /// <summary>
    /// This is the entry point class for module builder frontend wrappers.
    /// </summary>
    public class ModuleBuilderWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ModuleBuilderWrapper 
        /// </summary>
        /// <returns>Returns the ModuleBuilderWrapper</returns>
        public ModuleBuilderWrapper ModuleBuilderWrapper()
        {
            return new ModuleBuilderWrapper();
        }

        /// <summary>
        /// Provides unified access to the InlineEditingWrapper 
        /// </summary>
        /// <returns>Returns the InlineEditingWrapper</returns>
        public InlineEditingWrapper InlineEditingWrapper()
        {
            return new InlineEditingWrapper();
        }
    }
}
