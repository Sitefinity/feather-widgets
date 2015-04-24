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
    public class SearchFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationWidgetFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SearchFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Search box for Sitefinity template and Semantic UI template
        /// </summary>
        public HtmlInputText SearchTextBox
        {
            get
            {
                return this.Get<HtmlInputText>("tagname=input", "class=~ui-autocomplete-input");
            }
        }

        /// <summary>
        /// Gets Search box for Bootstrap and Foundation template
        /// </summary>
        public HtmlInputSearch SearchInput
        {
            get
            {
                return this.Get<HtmlInputSearch>("tagname=input", "class=~ui-autocomplete-input");
            }
        }

        /// <summary>
        /// Gets Search button for Sitefinity template, Bootstrap and Foundation template.
        /// </summary>
        public HtmlButton SearchButton
        {
            get
            {
                return this.Get<HtmlButton>("tagname=button", "innerText=Search");
            }
        }

        /// <summary>
        /// Gets Search link for SemanticUI template.
        /// </summary>
        public HtmlAnchor SearchLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "innerText=Search");
            }
        }

        /// <summary>
        /// Gets Sorting options dropdown.
        /// </summary>
        public HtmlSelect SortingOptionsDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("class=userSortDropdown");
            }
        }

        /// <summary>
        /// Gets Search results list.
        /// </summary>
        public IList<HtmlDiv> ResultsDivList
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 3
                                                                        && d.ChildNodes[0].TagName.Equals("h3")
                                                                        && d.ChildNodes[1].TagName.Equals("p")
                                                                        && d.ChildNodes[2].TagName.Equals("a")).ToList();

            }
        }

        /// <summary>
        /// Gets Search results label.
        /// </summary>
        public HtmlContainerControl ResultsLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=h1");
            }
        }
    }
}
