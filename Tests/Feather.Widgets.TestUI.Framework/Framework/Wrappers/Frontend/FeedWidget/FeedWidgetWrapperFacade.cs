using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.FeedWidget
{
    public class FeedWidgetWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the FeedWidgetWrapper
        /// </summary>
        /// <returns>Returns the FeedWidgetWrapper</returns>
        public FeedWidgetWrapper FeedWidget()
        {
            return new FeedWidgetWrapper();
        }
    }
}
