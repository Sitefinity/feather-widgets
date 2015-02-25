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
        public Frontend.Mvc.Models.ContentDetailsViewModel CreateDetailsViewModel(IDataItem item, int? itemIndex)
        {
            var viewModel = (ImageDetailsViewModel) base.CreateDetailsViewModel(item);

            int? totalCount = 0;
            IDataItem next = null;
            IDataItem prev = null;

            if (itemIndex != null)
            {
                var query = this.GetItemsQuery();
                query = this.UpdateExpression(query, null, null, ref totalCount);

                if (itemIndex == 1)
                {
                    next = query.Skip(itemIndex.Value).FirstOrDefault();
                    prev = query.LastOrDefault();
                }
                else if (itemIndex == totalCount)
                {
                    next = query.FirstOrDefault();
                    prev = query.Skip(itemIndex.Value - 2).FirstOrDefault();
                }
                else
                {
                    next = query.Skip(itemIndex.Value).FirstOrDefault();
                    prev = query.Skip(itemIndex.Value - 2).FirstOrDefault();
                }
            }

            viewModel.PreviousItem = prev != null ? new ItemViewModel(prev) : null;
            viewModel.NextItem = next != null ? new ItemViewModel(next) : null;
            viewModel.TotalItemsCount = totalCount.Value;

            return viewModel;
        }
    }
}
