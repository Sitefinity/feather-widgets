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

        /// <summary>
        /// Gets Where to search label.
        /// </summary>
        public HtmlContainerControl WhereToSearchLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("innerText=Where to search?");
            }
        }

        /// <summary>
        /// Gets What is this link.
        /// </summary>
        public HtmlAnchor WhatsThisLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tag=a", "class=Tooltip");
            }
        }

        /// <summary>
        /// Gets What is this information div.
        /// </summary>
        public HtmlDiv WhatsThisInfo
        {
            get
            {
                return this.Get<HtmlDiv>("tag=div", "class=Tooltip-info Tooltip-info--bottom");
            }
        }

        /// <summary>
        /// Gets Where to display search results label.
        /// </summary>
        public HtmlContainerControl WhereToDisplaySearchResultsLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tag=label", "innerText=Where to display search results?");
            }
        }

        /// <summary>
        /// Gets Template label.
        /// </summary>
        public HtmlContainerControl TemplateLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tag=label", "innerText=Template");
            }
        }

        /// <summary>
        /// Gets Templates dropdown.
        /// </summary>
        public HtmlSelect TemplatesDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=searchTemplateName");
            }
        }

        /// <summary>
        /// Gets More options div.
        /// </summary>
        public HtmlDiv MoreOptionsDiv
        {
            get
            {
                return this.Get<HtmlDiv>("class=Options ng-isolate-scope", "expander-title=More options");
            }
        }

        /// <summary>
        /// Gets CSS classes label.
        /// </summary>
        public HtmlContainerControl CssClassesLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tag=label", "innerText=CSS classes");
            }
        }

        /// <summary>
        /// Gets CSS classes label.
        /// </summary>
        public HtmlInputText CssClassesTextbox
        {
            get
            {
                return this.Get<HtmlInputText>("id=searchCssClass");
            }
        }
    }
}
