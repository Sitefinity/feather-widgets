using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ImageGallery
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather media selectors and widgets screens.
    /// </summary>
    public class ImageGalleryMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ImageGalleryMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the image gallery widget edit screen.
        /// </summary>
        /// <value>The image gallery widget edit screen.</value>
        public ImageGalleryWidgetEditScreen ImageGalleryWidgetEditScreen
        {
            get
            {
                return new ImageGalleryWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the image gallery frontend.
        /// </summary>
        /// <value>The image gallery frontend.</value>
        public ImageGalleryFrontend ImageGalleryFrontend
        {
            get
            {
                return new ImageGalleryFrontend(this.find);
            }
        }

        private Find find;
    }
}
