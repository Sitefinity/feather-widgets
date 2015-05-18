using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.MediaGallery
{
    /// <summary>
    /// Provides access to MediaGalleryFrontend
    /// </summary>
    public class MediaGalleryFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaGalleryFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public MediaGalleryFrontend(Find find)
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

        /// <summary>
        /// Gets the previous link.
        /// </summary>
        /// <value>The previous link.</value>
        public HtmlAnchor PreviousLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "innertext=~Previous");
            }
        }

        /// <summary>
        /// Gets the next link.
        /// </summary>
        /// <value>The next link.</value>
        public HtmlAnchor NextLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "innertext=~Next");
            }
        }

        /// <summary>
        /// Gets the beck to all media files link.
        /// </summary>
        /// <value>The beck to all media files link.</value>
        public HtmlAnchor BeckToAllMediaFilesLink
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "innertext=~Back to all");
            }
        }

        /// <summary>
        /// Gets the thubnail stripe prev.
        /// </summary>
        /// <value>The thubnail stripe prev.</value>
        public HtmlAnchor ThubnailStripePrev
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "class=~js-Gallery-prev");
            }
        }

        /// <summary>
        /// Gets the thubnail stripe next.
        /// </summary>
        /// <value>The thubnail stripe next.</value>
        public HtmlAnchor ThubnailStripeNext
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "class=~js-Gallery-next");
            }
        }
    }
}
