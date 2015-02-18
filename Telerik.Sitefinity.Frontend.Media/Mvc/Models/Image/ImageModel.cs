using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    ///     This class is used as a model for the image controller.
    /// </summary>
    public class ImageModel : IImageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageModel" /> class.
        /// </summary>
        public ImageModel()
        {
            
        }

        /// <inheritDoc/>
        public ImageViewModel GetViewModel(){
            return new ImageViewModel();
        }
    }
}