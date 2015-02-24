using System.Linq;
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
    }
}
