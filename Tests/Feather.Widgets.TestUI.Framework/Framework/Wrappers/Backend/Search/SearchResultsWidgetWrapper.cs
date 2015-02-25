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
            this.EM.Search.SearchResultsWidgetEditScreen.SortResultsLabel.AssertIsPresent("Sorting label");
            HtmlSelect sortingOptionDropdown = this.EM.Search.SearchResultsWidgetEditScreen.SortingOptionsDropdown.AssertIsPresent("Sorting option dropdown");

            sortingOptionDropdown.SelectByText(sortingOption);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Check Allow users to sort results checkbox
        /// </summary>
        public void AllowUsersToSortResults()
        {
            this.EM.Search.SearchResultsWidgetEditScreen.AllowSortingLabel.AssertIsPresent("Allow sorting label");
            HtmlInputCheckBox allowUsersToSortResultCheckbox = this.EM.Search.SearchResultsWidgetEditScreen.AllowUsersToSortResultsCheckbox.AssertIsPresent("Frontend sorting checkbox");

            allowUsersToSortResultCheckbox.Click();
        }

        /// <summary>
        /// Verifies Use paging
        /// </summary>
        public void VerifyUsePaging()
        {
            this.EM.Search.SearchResultsWidgetEditScreen.UsePagingRadioButton.AssertIsPresent("Use paging radio button");
            this.EM.Search.SearchResultsWidgetEditScreen.UsePagingSpan.AssertIsPresent("Use paging span");
        }

        /// <summary>
        /// Verifies Use limit
        /// </summary>
        public void VerifyUseLimit()
        {
            this.EM.Search.SearchResultsWidgetEditScreen.UseLimitRadioButton.AssertIsPresent("Use limit radio button");
            this.EM.Search.SearchResultsWidgetEditScreen.UseLimitSpan.AssertIsPresent("Use limit span");
        }

        /// <summary>
        /// Verifies No limit
        /// </summary>
        public void VerifyNoLimit()
        {
            this.EM.Search.SearchResultsWidgetEditScreen.NoLimitRadioButton.AssertIsPresent("No limit radio button");
            this.EM.Search.SearchResultsWidgetEditScreen.NoLimitSpan.AssertIsPresent("No limit span");
        }

        /// <summary>
        /// Selects template in dropdown
        /// </summary>
        /// <param name="templateName">template name to select</param>
        public void SelectTemplate(string templateName)
        {
            this.EM.Search.SearchResultsWidgetEditScreen.TemplateLabel.AssertIsPresent("Template label");
            HtmlSelect templateDropdown = this.EM.Search.SearchResultsWidgetEditScreen.TemplateDropdown.AssertIsPresent("Template dropdown");

            templateDropdown.SelectByText(templateName);
            templateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies More options
        /// </summary>
        public void ExpandMoreOptions()
        {
            HtmlSpan moreOptions = this.EM.Search.SearchResultsWidgetEditScreen.MoreOptionsSpan.AssertIsPresent("More options span");
            moreOptions.Click();
        }

        /// <summary>
        /// Apply css class to search results
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            this.EM.Search.SearchResultsWidgetEditScreen.CssClassesLabel.AssertIsPresent("Css classes label");
            HtmlInputText CssClassesTextbox = this.EM.Search.SearchResultsWidgetEditScreen.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            CssClassesTextbox.Text = cssClassName;
            CssClassesTextbox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
