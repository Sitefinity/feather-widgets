using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.News
{
    /// <summary>
    /// Provides access to news widget Content screen
    /// </summary>
    public class NewsWidgetContentScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetContentScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NewsWidgetContentScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets which news to display.
        /// </summary>
        public HtmlDiv WhichNewsToDisplayList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=tab-pane ng-scope active");
            }
        }

        /// <summary>
        /// Gets Select button.
        /// </summary>
        public HtmlButton SelectButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "class=btn btn-xs btn-default openSelectorBtn");
            }
        }

        /// <summary>
        /// Gets news list with news items.
        /// </summary>
        public HtmlDiv NewsList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=list-group list-group-endless ng-isolate-scope");
            }
        }

        /// <summary>
        /// Gets Done selecting button.
        /// </summary>
        public HtmlButton DoneSelectingButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Done selecting");
            }
        }

        /// <summary>
        /// Gets Save changes button.
        /// </summary>
        public HtmlButton SaveChangesButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "InnerText=Save");
            }
        }

         /// <summary>
        /// Gets Save changes button.
        /// </summary>
        public ICollection<HtmlButton> SelectButtons
        {
            get
            {
                return this.Find.AllByExpression<HtmlButton>("class=btn btn-xs btn-default openSelectorBtn ng-scope");
            }
        }

        /// <summary>
        /// Gets Save changes button date.
        /// </summary>
        public ICollection<HtmlButton> SelectButtonsDate
        {
            get
            {
                return this.Find.AllByExpression<HtmlButton>("class=btn btn-xs btn-default openSelectorBtn");
            }
        }

        /// <summary>
        /// Gets search div.
        /// </summary>
        public HtmlDiv SearchByTypingDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=input-group m-bottom-sm");
            }
        }

        /// <summary>
        /// Gets no items div.
        /// </summary>
        public HtmlDiv NoItemsFoundDiv
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=No items found");
            }
        }

        /// <summary>
        /// Gets the selector items.
        /// </summary>
        /// <value>The selector items.</value>
        public ICollection<HtmlAnchor> SelectorItems
        {
            get
            {
                return this.Find.AllByExpression<HtmlAnchor>("ng-repeat=item in items");
            }
        }

        /// <summary>
        /// Gets the search input.
        /// </summary>
        /// <value>The search input.</value>
        public HtmlInputText SearchInput
        {
            get
            {
                return this.Get<HtmlInputText>("ng-model=filter.search");
            }
        }

        /// <summary>
        /// Gets which news to display.
        /// </summary>
        public HtmlForm DisplayItemsPublishedIn
        {
            get
            {
                return this.Get<HtmlForm>("tagname=form", "name=periodSelection");
            }
        }

        /// <summary>
        /// Gets news selected items
        /// </summary>
        public HtmlSpan SelectedItemsSpan
        {
            get
            {
                return this.Get<HtmlSpan>("tagname=span", "class=label label-taxon label-full ng-binding");
            }
        }

        /// <summary>
        /// Gets the active tab.
        /// </summary>
        /// <value>The active tab.</value>
        public HtmlDiv ActiveTab
        {
            get
            {
                return this.Get<HtmlDiv>("class=~k-content k-state-active");
            }
        }

        /// <summary>
        /// Gets the all tab.
        /// </summary>
        /// <value>The all tab.</value>
        public HtmlAnchor AllTab
        {
            get
            {
                return this.Get<HtmlAnchor>("class=k-link", "innertext=~All");
            }
        }

        /// <summary>
        /// Gets the selected tab.
        /// </summary>
        /// <value>The selected tab.</value>
        public HtmlAnchor SelectedTab
        {
            get
            {
                return this.Get<HtmlAnchor>("class=k-link", "innertext=~Selected");
            }
        }

        /// <summary>
        /// Gets the inactive widget.
        /// </summary>
        /// <value>The inactive widget.</value>
        public HtmlDiv InactiveWidget
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=sf_inactiveWidget");
            }
        }
    }
}
