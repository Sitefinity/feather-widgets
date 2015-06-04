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
    /// This is the entry point class for search box widget wrapper.
    /// </summary>
    public class SearchBoxWidgetWrapper : BaseWrapper
    {
        /// <summary>
        /// Selects search index in dropdown
        /// </summary>
        /// <param name="searchIndex">search index name</param>
        public void SelectSearchIndex(string searchIndex)
        {
            this.EM.Search.SearchBoxWidgetEditScreen.WhereToSearchLabel.AssertIsPresent("Where to search label");
            HtmlSelect searchIndexesDropdown = this.EM.Search.SearchBoxWidgetEditScreen.SearchIndexesDropdown.AssertIsPresent("Search indexes dropdown");
            
            searchIndexesDropdown.SelectByText(searchIndex);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies Where to display search results label
        /// </summary>
        public void VerifyWhereToDisplaySearchResultsLabel()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.WhereToDisplaySearchResultsLabel.AssertIsPresent("Where to display search results label");
        }

        /// <summary>
        /// Selects template in dropdown
        /// </summary>
        /// <param name="templateName">template name to select</param>
        public void SelectTemplate(string templateName)
        {
            this.EM.Search.SearchBoxWidgetEditScreen.TemplateLabel.AssertIsPresent("Template label");
            HtmlSelect templateDropdown = this.EM.Search.SearchBoxWidgetEditScreen.TemplateDropdown.AssertIsPresent("Template dropdown");

            templateDropdown.SelectByText(templateName);
            templateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            templateDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies More options
        /// </summary>
        public void ExpandMoreOptions()
        {
            HtmlAnchor moreOptions = this.EM.Search.SearchBoxWidgetEditScreen.MoreOptions.AssertIsPresent("More options span");
            moreOptions.Click();
        }

        /// <summary>
        /// Verifies What is this link and information
        /// </summary>
        public void VerifyWhatsThis()
        {
            ActiveBrowser.RefreshDomTree();
            HtmlAnchor whatsThisLink = this.EM.Search.SearchBoxWidgetEditScreen.WhatsThisLink;
            whatsThisLink.AssertIsPresent("What's this link");
            whatsThisLink.MouseHover();
            this.EM.Search.SearchBoxWidgetEditScreen.WhatsThisInfo.AssertIsPresent("What's this div");
        }

        /// <summary>
        /// Apply css class to search box
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            this.EM.Search.SearchBoxWidgetEditScreen.CssClassesLabel.AssertIsPresent("Css classes label");
            HtmlInputText cssClassesTextbox = this.EM.Search.SearchBoxWidgetEditScreen.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            cssClassesTextbox.Text = cssClassName;
            cssClassesTextbox.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
