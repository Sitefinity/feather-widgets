using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ImageGallery
{
    /// <summary>
    /// This is an netry point for media wrappers.
    /// </summary>
    public class ImageGalleryWrapperFacade
    {
        /// <summary>
        /// Images galleryr wrapper.
        /// </summary>
        /// <returns></returns>
        public ImageGalleryWidgetWrapper ImageGalleryWrapper()
        {
            return new ImageGalleryWidgetWrapper();
        }
    }
}
