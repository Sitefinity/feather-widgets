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
    /// Provides access to Lists widget designer elements.
    /// </summary>
    public class ListsWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ListsWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Sort list item drop down.
        /// </summary>
        /// <value>Sorting drop down</value>
        public HtmlSelect SortDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=sortOptions");
            }
        }

        /// <summary>
        /// Gets List items template drop down.
        /// </summary>
        /// <value>List items template drop down</value>
        public HtmlSelect ListItemsTemplateDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=listTemplateName");
            }
        }

        /// <summary>
        /// Gets List details template drop down.
        /// </summary>
        /// <value>List details template drop down</value>
        public HtmlSelect ListDetailsTemplateDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=listDetailTemplateName");
            }
        }
    }
}
