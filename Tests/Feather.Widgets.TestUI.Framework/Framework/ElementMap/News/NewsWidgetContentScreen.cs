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
        public HtmlUnorderedList WhichNewsToDisplayList
        {
            get
            {
                return this.Get<HtmlUnorderedList>("tagname=ul", "class=form-group ng-scope");
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
                return this.Get<HtmlDiv>("tagname=div", "class=list-group s-items-list-wrp endlessScroll");
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
                return this.Find.AllByExpression<HtmlButton>("class=btn btn-xs btn-default openSelectorBtn");
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
    }
}
