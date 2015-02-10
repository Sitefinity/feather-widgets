using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Search
{
    /// <summary>
    /// Provides access to Search results widget designer elements.
    /// </summary>
    public class SearchResultsWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SearchResultsWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Sorting options dropdown.
        /// </summary>
        public HtmlSelect SortingOptionsDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=sortOptions");
            }
        }

        /// <summary>
        /// Gets Allow users to sort results checkbox.
        /// </summary>
        public HtmlInputCheckBox AllowUsersToSortResultsCheckbox
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("id=allowSorting");
            }
        }
    }
}
