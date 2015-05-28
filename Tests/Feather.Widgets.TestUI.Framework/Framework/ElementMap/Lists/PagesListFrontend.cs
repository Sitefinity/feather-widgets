using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Lists
{
    /// <summary>
    /// Provides access to frontend elements of pages list template.
    /// </summary>
    public class PagesListFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagesListFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public PagesListFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets list title.
        /// </summary>
        public HtmlContainerControl ListTitleLabel
        {
            get
            {
                return this.Get<HtmlContainerControl>("tagname=h3");
            }
        }

        /// <summary>
        /// Gets unordered list with list items.
        /// </summary>
        public HtmlUnorderedList ListItemsUnorderedList
        {
            get
            {
                return this.Get<HtmlUnorderedList>("tagname=ul");
            }
        }

        /// <summary>
        /// Gets div element containing list item on Bootstrap.
        /// </summary>
        public HtmlDiv ListItemsDivOnBootstrap
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 3
                                                                        && d.ChildNodes[0].TagName.Equals("h3")
                                                                        && d.ChildNodes[0].ChildNodes[0].TagName.Equals("span")
                                                                        && d.ChildNodes[1].TagName.Equals("div")
                                                                        && d.ChildNodes[2].TagName.Equals("div")).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets div element containing list item.
        /// </summary>
        public HtmlDiv ListItemsDiv
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 2
                                                                        && d.ChildNodes[0].TagName.Equals("h3")
                                                                        && d.ChildNodes[0].ChildNodes[0].TagName.Equals("span")
                                                                        && d.ChildNodes[1].TagName.Equals("div")).SingleOrDefault();
            }
        }
    }
}
