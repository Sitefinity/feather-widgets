using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ModuleBuilder
{
    /// <summary>
    /// This is the entry point class for module builder backend wrappers.
    /// </summary>
    public class ModuleBuilderWrapperFacade
    {
        /// <summary>
        /// Provides access to ModuleBuilderEditContentTypeWrapper.
        /// </summary>
        /// <returns>Return the ModuleBuilderEditContentTypeWrapper.</returns>
        public ModuleBuilderEditContentTypeWrapper ModuleBuilderEditContentTypeWrapper()
        {
            return new ModuleBuilderEditContentTypeWrapper();
        }
    }
}
