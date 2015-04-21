using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Lists
{
    /// <summary>
    /// This is an entry point for ListsWrapper.
    /// </summary>
    public class ListsWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects option in sorting dropdown
        /// </summary>
        /// <param name="sortingOption">sorting option</param>
        public void SelectSortingOption(string sortingOption)
        {
            HtmlSelect sortingOptionDropdown = this.EM.Lists.ListsWidgetEditScreen.SortDropdown.AssertIsPresent("Sorting option dropdown");

            sortingOptionDropdown.SelectByText(sortingOption);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects option in list template
        /// </summary>
        /// <param name="templateName">template name</param>
        public void SelectListTemplate(string templateName)
        {
            HtmlSelect sortingOptionDropdown = this.EM.Lists.ListsWidgetEditScreen.ListItemsTemplateDropdown.AssertIsPresent("List template dropdown");

            sortingOptionDropdown.SelectByText(templateName);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects option in list details template
        /// </summary>
        /// <param name="templateName">template name</param>
        public void SelectListDetailsTemplate(string templateName)
        {
            HtmlSelect sortingOptionDropdown = this.EM.Lists.ListsWidgetEditScreen.ListDetailsTemplateDropdown.AssertIsPresent("List details template dropdown");

            sortingOptionDropdown.SelectByText(templateName);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            sortingOptionDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
