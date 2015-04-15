using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an netry point for media wrappers.
    /// </summary>
    public class MediaWrapperFacade
    {
        /// <summary>
        /// Provides access to ImageSelectorWrapper
        /// </summary>
        /// <returns>New instance of ImageSelectorWrapper</returns>
        public ImageSelectorWrapper ImageSelectorWrapper()
        {
            return new ImageSelectorWrapper();
        }

        /// <summary>
        /// Provides access to DocumentSelectorWrapper
        /// </summary>
        /// <returns>New instance of DocumentSelectorWrapper</returns>
        public DocumentSelectorWrapper DocumentSelectorWrapper()
        {
            return new DocumentSelectorWrapper();
        }

        /// <summary>
        /// Provides access to ImagePropertiesWrapper
        /// </summary>
        /// <returns>New instance of ImagePropertiesWrapper</returns>
        public ImagePropertiesWrapper ImagePropertiesWrapper()
        {
            return new ImagePropertiesWrapper();
        }

        /// <summary>
        /// Provides access to DocumentPropertiesWrapper
        /// </summary>
        /// <returns>New instance of DocumentPropertiesWrapper</returns>
        public DocumentPropertiesWrapper DocumentPropertiesWrapper()
        {
            return new DocumentPropertiesWrapper();
        }

        /// <summary>
        /// Provides access to MediaUploadPropertiesWrapper
        /// </summary>
        /// <returns>New instance of MediaUploadPropertiesWrapper</returns>
        public MediaUploadPropertiesWrapper MediaUploadPropertiesWrapper()
        {
            return new MediaUploadPropertiesWrapper();
        }
    }
}
