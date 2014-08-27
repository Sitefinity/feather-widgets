using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for content block backend wrappers.
    /// </summary>
    public class ContentBlocksWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ContentBlockWidgetEditWrapper
        /// </summary>
        /// <returns>Returns the ContentBlockWidgetEditWrapper</returns>
        public ContentBlockWidgetEditWrapper ContentBlocksWrapper()
        {
            return new ContentBlockWidgetEditWrapper();
        }

        /// <summary>
        /// Provides unified access to the ContentBlockWidgetShareWrapper
        /// </summary>
        /// <returns>Returns the ContentBlockWidgetShareWrapper</returns>
        public ContentBlockWidgetShareWrapper ContentBlocksShareWrapper()
        {
            return new ContentBlockWidgetShareWrapper();
        }
    }
}
