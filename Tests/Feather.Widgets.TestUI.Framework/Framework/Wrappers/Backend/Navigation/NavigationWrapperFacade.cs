using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for navigation widget backend wrappers.
    /// </summary>
    public class NavigationWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the NavigationWidgetEditWrapper 
        /// </summary>
        /// <returns>Returns the NavigationWidgetEditWrapper</returns>
        public NavigationWidgetEditWrapper NavigationWidgetEditWrapper()
        {
            return new NavigationWidgetEditWrapper();
        }
    }
}
