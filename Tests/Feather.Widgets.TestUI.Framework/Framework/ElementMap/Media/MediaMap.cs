using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Media
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather media selectors and widgets screens.
    /// </summary>
    public class MediaMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public MediaMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the media selector main screen.
        /// </summary>
        public MediaSelectorScreen MediaSelectorScreen
        {
            get
            {
                return new MediaSelectorScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the image properties main screen.
        /// </summary>
        public ImagePropertiesScreen ImagePropertiesScreen
        {
            get
            {
                return new ImagePropertiesScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the image properties main screen.
        /// </summary>
        public MediaPropertiesBaseScreen MediaPropertiesBaseScreen
        {
            get
            {
                return new MediaPropertiesBaseScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the media upload properties main screen.
        /// </summary>
        public MediaUploadPropertiesScreen MediaUploadPropertiesScreen
        {
            get
            {
                return new MediaUploadPropertiesScreen(this.find);
            }
        }

        private Find find;
    }
}
