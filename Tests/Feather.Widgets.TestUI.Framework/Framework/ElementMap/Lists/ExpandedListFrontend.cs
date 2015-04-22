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
    /// Provides access to frontend elements of expanded list template.
    /// </summary>
    public class ExpandedListFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandedListFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ExpandedListFrontend(Find find)
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
                return this.Get<HtmlContainerControl>("tagname=h1");
            }
        }

        /// <summary>
        /// Gets list of div elements containing list items.
        /// </summary>
        public List<HtmlDiv> ListItemsDivs
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => (d.ChildNodes.Count == 2) 
                                                                        && (d.ChildNodes[0].TagName.Equals("h2"))
                                                                        && (d.ChildNodes[1].TagName.Equals("div"))).ToList();
            }
        }
    }
}
