using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for all frontend wrappers.
    /// </summary>
    public class FrontendWrappesFacade
    {
        /// <summary>
        /// Provides unified access to the ContentBlockWrapperFacade 
        /// </summary>
        /// <returns>Returns the ContentBlockWrapperFacade</returns>
        public ContentBlockWrapperFacade ContentBlock()
        {
            return new ContentBlockWrapperFacade();
        }

        /// <summary>
        /// Provides unified access to the NavigationWrapperFacade 
        /// </summary>
        /// <returns>Returns the NavigationWrapperFacade</returns>
        public NavigationWrapperFacade Navigation()
        {
            return new NavigationWrapperFacade();
        }
    }
}
