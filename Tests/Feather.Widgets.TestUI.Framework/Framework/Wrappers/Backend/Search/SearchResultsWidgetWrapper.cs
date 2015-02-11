using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Search
{
    /// <summary>
    /// This is the entry point class for search results widget wrapper.
    /// </summary>
    public class SearchResultsWidgetWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects sorting option in dropdown
        /// </summary>
        /// <param name="sortingOption">sorting option name</param>
        public void SelectSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionDropdown = EM.Search.SearchResultsWidgetEditScreen.SortingOptionsDropdown.AssertIsPresent("Sorting option dropdown");

            sortingOptionDropdown.SelectByText(sortingOption);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Check Allow users to sort results checkbox
        /// </summary>
        public void AllowUsersToSortResults()
        {
            HtmlInputCheckBox allowUsersToSortResultCheckbox = EM.Search.SearchResultsWidgetEditScreen.AllowUsersToSortResultsCheckbox.AssertIsPresent("Frontend sorting checkbox");

            allowUsersToSortResultCheckbox.Click();
        }
    }
}
