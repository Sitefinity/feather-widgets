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
            HtmlSelect searchIndexesDropdown = this.EM.Search.SearchBoxWidgetEditScreen.SearchIndexesDropdown.AssertIsPresent("Search indexes dropdown");
            
            searchIndexesDropdown.SelectByText(searchIndex);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies Where to search label
        /// </summary>
        public void VerifyWhereToSearchLabel()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.WhereToSearchLabel.AssertIsPresent("Where to search label");
        }

        /// <summary>
        /// Verifies Where to display search results label
        /// </summary>
        public void VerifyWhereToDisplaySearchResultsLabel()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.WhereToDisplaySearchResultsLabel.AssertIsPresent("Where to display search results label");
        }

        /// <summary>
        /// Verifies Templates label
        /// </summary>
        public void VerifyTemplateLabel()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.TemplateLabel.AssertIsPresent("Template label");
        }

        /// <summary>
        /// Verifies More options
        /// </summary>
        public void ExpandMoreOptions()
        {
            HtmlDiv moreOptions = this.EM.Search.SearchBoxWidgetEditScreen.MoreOptionsDiv.AssertIsPresent("More options div");
            moreOptions.Click();
        }

        /// <summary>
        /// Verifies Css classes label
        /// </summary>
        public void VerifyCssClassesLabel()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.CssClassesLabel.AssertIsPresent("Css classes label");
        }

        /// <summary>
        /// Verifies What is this link and information
        /// </summary>
        public void VerifyWhatsThis()
        {
            this.EM.Search.SearchBoxWidgetEditScreen.WhatsThisLink.AssertIsPresent("What's this link");
            this.EM.Search.SearchBoxWidgetEditScreen.WhatsThisInfo.AssertIsPresent("What's this div");
        }

        /// <summary>
        /// Apply css class to search box
        /// </summary>
        /// <param name="cssClassName">css class name</param>
        public void ApplyCssClasses(string cssClassName)
        {
            HtmlInputText CssClassesTextbox = this.EM.Search.SearchBoxWidgetEditScreen.CssClassesTextbox.AssertIsPresent("Css classes textbox");
            CssClassesTextbox.Text = cssClassName;
        }
    }
}
