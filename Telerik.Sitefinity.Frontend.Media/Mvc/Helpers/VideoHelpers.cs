using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper method for working with videos.
    /// </summary>
    public static class VideoHelpers
    {
        /// <summary>
        /// Generates image height attributes as html by given video gallery view model for object that is image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="model">The Video Thumbnail View Model.</param>
        /// <returns>The generated image height attribute as html content.</returns>
        public static string GetImageHeightAttributeForVideoThumbnail(this HtmlHelper helper, VideoThumbnailViewModel item)
        {
            var height = item.Fields.Thumbnail != null ? item.Fields.Thumbnail.Height : null;

            var thumbnailList = item.Fields.Thumbnails as System.Collections.Generic.IList<Thumbnail>;
            var thumbnail = thumbnailList?.ToList().FirstOrDefault(tp => tp.Name == item.ProfileName);

            if (thumbnail != null)
            {
                height = thumbnail.Height;
            }
            else
            {
                if (item.Height != null)
                {
                    height = item.Height.Value;
                }

                if (item.MaxHeight != null)
                {
                    height = height != null ? height > item.MaxHeight.Value ? item.MaxHeight.Value : height : item.MaxHeight.Value;
                }
            }

            return string.Format(@"height={0}", height);
        }

        /// <summary>
        /// Generates image width attributes as html by given video gallery view model for object that is image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="model">The Video Thumbnail View Model.</param>
        /// <returns>The generated image width attribute as html content.</returns>
        public static string GetImageWidthAttributeForVideoThumbnail(this HtmlHelper helper, VideoThumbnailViewModel item)
        {
            var width = item.Fields.Thumbnail != null ? item.Fields.Thumbnail.Width : null;

            var thumbnailList = item.Fields.Thumbnails as System.Collections.Generic.IList<Thumbnail>;
            var thumbnail = thumbnailList?.ToList().FirstOrDefault(tp => tp.Name == item.ProfileName);

            if (thumbnail != null)
            { 
                width = thumbnail.Width;
            } 
            else
            {
                if (item.Width != null)
                {
                    width = item.Width.Value;
                }

                if (item.MaxWidth != null)
                {
                    width = width != null ? width > item.MaxWidth.Value ? item.MaxWidth.Value : width : item.MaxWidth.Value;
                }
            }         

            return string.Format(@"width={0}", width);
        }
    }
}
