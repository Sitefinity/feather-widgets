using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// A model for the Image Gallery MVC widget.
    /// </summary>
    public class ImageGalleryModel : MediaGalleryModelBase<SfImage>, IImageGalleryModel
    {
        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((LibrariesManager)this.GetManager()).GetImages();
        }

        /// <inheritdoc />
        protected override Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new ImageDetailsViewModel();
        }

        /// <inheritdoc />
        public override Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = (ImageDetailsViewModel) base.CreateDetailsViewModel(item);

            var query = this.GetItemsQuery();
            int? totalCount = 0;
            query = this.UpdateExpression(query, null, null, ref totalCount);

            viewModel.PreviousItem = null;
            viewModel.NextItem = null;
            return viewModel;
        }
    }
}
