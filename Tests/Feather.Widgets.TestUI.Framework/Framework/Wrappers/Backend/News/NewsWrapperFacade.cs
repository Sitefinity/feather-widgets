using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for news widget backend wrappers.
    /// </summary>
    public class NewsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the NewsWidgetEditContentScreenWrapper 
        /// </summary>
        /// <returns>Returns the NewsWidgetEditContentScreenWrapper</returns>
        public NewsWidgetEditContentScreenWrapper NewsWidgetEditContentScreenWrapper()
        {
            return new NewsWidgetEditContentScreenWrapper();
        }
    }
}
