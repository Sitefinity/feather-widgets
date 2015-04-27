using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Lists
{
    /// <summary>
    /// This is the entry point class for lists widget frontend wrappers.
    /// </summary>
    public class ListsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ListsWrapper 
        /// </summary>
        /// <returns>Returns the ListsWrapper</returns>
        public ListsWrapper ListsWidgetWrapper()
        {
            return new ListsWrapper();
        }
    }
}
