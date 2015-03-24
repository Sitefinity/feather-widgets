using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ImageGallery
{
    /// <summary>
    /// Provides access to ImageGalleryWidgetEditScreen
    /// </summary>
    public class ImageGalleryWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertiesScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImageGalleryWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the narro selection by arrow.
        /// </summary>
        /// <value>The narro selection by arrow.</value>
        public HtmlSpan NarroSelectionByArrow
        {
            get
            {
                return this.Get<HtmlSpan>("tagName=span", "ng-click=toggle()", "innertext=~Narrow selection by...");
            }
        }
    }
}
