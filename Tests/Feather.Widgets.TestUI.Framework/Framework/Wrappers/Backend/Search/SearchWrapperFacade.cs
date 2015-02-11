using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Search;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class SearchWrapperFacade
    {
        /// <summary>
        /// Search box widget wrapper
        /// </summary>
        /// <returns></returns>
        public SearchBoxWidgetWrapper SearchBoxWrapper()
        {
            return new SearchBoxWidgetWrapper();
        }

        /// <summary>
        /// Search results widget wrapper
        /// </summary>
        /// <returns></returns>
        public SearchResultsWidgetWrapper SearchResultsWrapper()
        {
            return new SearchResultsWidgetWrapper();
        }
    }
}
