using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Search
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Search box and results widget back-end screens.
    /// </summary>
    public class SearchMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SearchMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the search box widget backend
        /// </summary>
        public SearchBoxWidgetEditScreen SearchBoxWidgetEditScreen
        {
            get
            {
                return new SearchBoxWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the search box widget backend
        /// </summary>
        public SearchResultsWidgetEditScreen SearchResultsWidgetEditScreen
        {
            get
            {
                return new SearchResultsWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the search box on frontend
        /// </summary>
        public SearchFrontend SearchFrontend
        {
            get
            {
                return new SearchFrontend(this.find);
            }
        }

        private Find find;
    }
}
