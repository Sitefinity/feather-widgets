using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules.Libraries;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

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

        public string CssClass { get; set; }

        /// <inheritDoc/>
        public ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel()
            {
                Markup = this.Markup,
                CssClass = this.CssClass
            };

            if (this.Id != Guid.Empty)
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
                viewModel.Item = new ItemViewModel(librariesManager.GetImages().Where(i => i.Id == this.Id).FirstOrDefault());
            }
            else
            {
                viewModel.Item = new ItemViewModel(new SfImage());
            }

            return viewModel;
        }
    }
}