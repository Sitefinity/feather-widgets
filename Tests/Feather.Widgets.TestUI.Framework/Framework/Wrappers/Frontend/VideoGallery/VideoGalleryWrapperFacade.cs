using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class VideoGalleryWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the VideoGalleryWrapper
        /// </summary>
        /// <returns>Returns the VideoGalleryWrapper</returns>
        public VideoGalleryWrapper VideoGalleryWrapper()
        {
            return new VideoGalleryWrapper();
        }
    }
}
