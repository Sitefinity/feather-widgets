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
            HtmlSelect searchIndexesDropdown = EM.Search.SearchBoxWidgetEditScreen.SearchIndexesDropdown.AssertIsPresent("Search indexes dropdown");
            
            searchIndexesDropdown.SelectByText(searchIndex);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            searchIndexesDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }
    }
}
