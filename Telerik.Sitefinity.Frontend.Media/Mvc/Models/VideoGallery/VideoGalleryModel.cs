using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SfVideo = Telerik.Sitefinity.Libraries.Model.Video;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery
{
    /// <summary>
    /// A model for the Video Gallery MVC widget.
    /// </summary>
    public class VideoGalleryModel : MediaGalleryModelBase<SfVideo>, IVideoGalleryModel
    {
        protected override IQueryable<Model.IDataItem> GetItemsQuery()
        {
            throw new NotImplementedException();
        }
    }
}

