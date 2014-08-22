using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for navigation widget frontend wrappers.
    /// </summary>
    public class NavigationWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the NavigationWrapper 
        /// </summary>
        /// <returns>Returns the NavigationWrapper</returns>
        public NavigationWrapper NavigationWrapper()
        {
            return new NavigationWrapper();
        }
    }
}
