using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class MediaWidgetsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ImageGalleryWrapper
        /// </summary>
        /// <returns>Returns the NewsWrapper</returns>
        public MediaWidgetsWrapper MediaWidgetsWrapper()
        {
            return new MediaWidgetsWrapper();
        }
    }
}
