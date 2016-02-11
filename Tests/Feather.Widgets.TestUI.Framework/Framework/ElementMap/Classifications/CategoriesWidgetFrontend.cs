using System;
using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Classifications
{
    /// <summary>
    /// Provides access to Categories widget frontend elements.
    /// </summary>
    public class CategoriesWidgetFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesWidgetFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CategoriesWidgetFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the Categories list.
        /// </summary>
        public HtmlUnorderedList CategoryList
        {
            get
            {
                return this.Get<HtmlUnorderedList>("class=Test");
            }
        }
    }
}
