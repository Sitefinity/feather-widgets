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

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Markup { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string AlternativeText { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public bool UseAsLink { get; set; }

        /// <inheritdoc />
        public Guid PageIdToUseAsLink { get; set; }

        /// <inheritDoc/>
        public ImageViewModel GetViewModel()
        {
            var viewModel = new ImageViewModel()
            {
                Markup = this.Markup,
                Title = this.Title
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