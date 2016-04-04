using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.FeedWidget
{
    /// <summary>
    /// This is the entry point class for feed widget backend wrappers.
    /// </summary>
    public class FeedWidgetWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the FeedWidgetWrapper 
        /// </summary>
        /// <returns>Returns the FeedWidgetWrapper</returns>
        public FeedWidgetWrapper FeedWidgetWrapper()
        {
            return new FeedWidgetWrapper();
        }
    }
}
