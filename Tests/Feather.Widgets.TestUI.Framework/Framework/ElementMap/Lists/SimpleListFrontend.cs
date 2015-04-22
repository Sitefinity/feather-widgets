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
    /// Provides access to frontend elements of simple list template.
    /// </summary>
    public class SimpleListFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleListFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SimpleListFrontend(Find find)
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
    }
}
