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
    /// Provides access to Search box widget designer elements.
    /// </summary>
    public class SearchBoxWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SearchBoxWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Select page button.
        /// </summary>
        public HtmlButton SelectPageButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "class=btn btn-xs btn-default openSelectorBtn");
            }
        }

        /// <summary>
        /// Gets Search indexes dropdown.
        /// </summary>
        public HtmlSelect SearchIndexesDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=searchIndexes");
            }
        }
    }
}
