using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.MediaGallery
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather media selectors and widgets screens.
    /// </summary>
    public class MediaGalleryMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public MediaGalleryMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the image gallery frontend.
        /// </summary>
        /// <value>The image gallery frontend.</value>
        public MediaGalleryFrontend MediaGalleryFrontend
        {
            get
            {
                return new MediaGalleryFrontend(this.find);
            }
        }

        private Find find;
    }
}
