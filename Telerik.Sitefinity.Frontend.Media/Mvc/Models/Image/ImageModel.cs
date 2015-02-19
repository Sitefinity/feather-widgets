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

        public Guid Id { get; set; }

        public string Markup { get; set; }

        public string ProviderName { get; set; }

        /// <inheritDoc/>
        public ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel(this.Id, this.ProviderName);
            viewModel.Markup = this.Markup;

            return viewModel;
        }
    }
}