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
    /// Provides access to ImageGalleryFrontend
    /// </summary>
    public class ImageGalleryFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageGalleryFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImageGalleryFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the previous button overlay template.
        /// </summary>
        /// <value>The previous button overlay template.</value>
        public HtmlButton PreviousButtonOverlayTemplate
        {
            get
            {
                return this.Get<HtmlButton>("type=button", "title=Previous (Left arrow key)", "class=mfp-arrow mfp-arrow-left mfp-prevent-close");
            }
        }

        /// <summary>
        /// Gets the next button overlay template.
        /// </summary>
        /// <value>The next button overlay template.</value>
        public HtmlButton NextButtonOverlayTemplate
        {
            get
            {
                return this.Get<HtmlButton>("type=button", "title=Next (Right arrow key)", "class=mfp-arrow mfp-arrow-right mfp-prevent-close");
            }
        }

        /// <summary>
        /// Gets the close button overlay template.
        /// </summary>
        /// <value>The close button overlay template.</value>
        public HtmlButton CloseButtonOverlayTemplate
        {
            get
            {
                return this.Get<HtmlButton>("type=button", "title=Close (Esc)", "class=mfp-close");
            }
        }
    }
}
