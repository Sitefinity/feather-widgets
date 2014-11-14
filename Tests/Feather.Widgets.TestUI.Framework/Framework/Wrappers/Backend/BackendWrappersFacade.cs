using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ModuleBuilder;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for all backend wrappers.
    /// </summary>
    public class BackendWrappersFacade
    {
        /// <summary>
        /// Provides unified access to the pages
        /// </summary>
        /// <returns>Returns the PagesWrapperFacade</returns>
        public PagesWrapperFacade Pages()
        {
            return new PagesWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the content block
        /// </summary>
        /// <returns>Returns the ContentBlocksWrapperFacade</returns>
        public ContentBlocksWrapperFacade ContentBlocks()
        {
            return new ContentBlocksWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the navigation
        /// </summary>
        /// <returns>Returns the NavigationWidgetEditWrapper</returns>
        public NavigationWrapperFacade Navigation()
        {
            return new NavigationWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the navigation
        /// </summary>
        /// <returns>Returns the NavigationWidgetEditWrapper</returns>
        public NewsWrapperFacade News()
        {
            return new NewsWrapperFacade();
        }

        /// <summary>
        /// Provides access to module builder.
        /// </summary>
        /// <returns>Returns the ModuleBuilderWrapperFacade.</returns>
        public ModuleBuilderWrapperFacade ModuleBuilder()
        {
            return new ModuleBuilderWrapperFacade();
        }
    }
}
