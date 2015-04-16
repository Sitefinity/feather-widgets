using System;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfVideo = Telerik.Sitefinity.Libraries.Model.Video;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery
{
    /// <summary>
    /// This class is used as a model for the <see cref="VideoGalleryController"/>.
    /// </summary>
    public class VideoGalleryModel : MediaGalleryModelBase<SfVideo>, IVideoGalleryModel
    {
        /// <inheritdoc />
        public string SerializedThumbnailSizeModel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public string SerializedVideoSizeModel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public ContentDetailsViewModel CreateDetailsViewModel(IDataItem item, int? itemIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            throw new NotImplementedException();
        }
    }
}
