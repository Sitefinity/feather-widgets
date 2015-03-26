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
        /// <value>The narrow selection by arrow.</value>
        public HtmlSpan NarrowSelectionByArrow
        {
            get
            {
                return this.Get<HtmlSpan>("tagName=span", "ng-click=toggle()", "innertext=~Narrow selection by...");
            }
        }

        /// <summary>
        /// Gets the image thumbnail selector.
        /// </summary>
        public ICollection<HtmlSelect> ThumbnailSelector
        {
            get
            {
                return this.Find.AllByExpression<HtmlSelect>("tagName=select", "ng-model=sizeSelection");
            }
        }

        /// <summary>
        /// Gets the sort images selector.
        /// </summary>
        /// <value>The sort images selector.</value>
        public HtmlSelect SortImagesSelector
        {
            get
            {
                return this.Get<HtmlSelect>("tagName=select", "ng-model=selectedSortOption");
            }
        }
    }
}
