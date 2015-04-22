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
    /// Provides access to frontend elements of anchor list template.
    /// </summary>
    public class AnchorListFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorListFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public AnchorListFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets list title anchor.
        /// </summary>
        public HtmlContainerControl ListTitleWithAnchor
        {
            get
            {
                return this.Find.AllByTagName<HtmlContainerControl>("h1").Where(d => (d.ChildNodes.Count == 1)
                                                                                    && (d.ChildNodes[0].TagName.Equals("a"))).SingleOrDefault();
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
        /// Gets list of div elements containing list items.
        /// </summary>
        public List<HtmlDiv> ListItemsDivs
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => (d.ChildNodes.Count == 3)
                                                                        && (d.ChildNodes[0].TagName.Equals("h3"))
                                                                        && (d.ChildNodes[1].TagName.Equals("div"))
                                                                        && (d.ChildNodes[2].TagName.Equals("p"))
                                                                        && (d.ChildNodes[2].ChildNodes[0].TagName.Equals("a"))).ToList();
            }
        }
    }
}
