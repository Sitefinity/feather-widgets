using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper method for working with images.
    /// </summary>
    public static class ImageHelpers
    {
        /// <summary>
        /// Gets the serialized image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static string GetSerializedImage(this HtmlHelper helper, ItemViewModel item)
        {
            var image = (Image)item.DataItem;
            var thumbnailViewModel = item as ThumbnailViewModel;

            var itemViewModel = new
            {
                Title = image.Title.Value,
                AlternativeText = image.AlternativeText.Value,
                Description = image.Description.Value,
                MediaUrl = thumbnailViewModel.MediaUrl,
                DateCreated = image.DateCreated,
                Author = image.Author.Value,
                Width = thumbnailViewModel.DetailsImageWidth.HasValue ? (int?)thumbnailViewModel.DetailsImageWidth.Value : (int?)null,
                Height = thumbnailViewModel.DetailsImageHeight.HasValue ? (int?)thumbnailViewModel.DetailsImageHeight.Value : (int?)null
            };

            var serializedItemViewModel = new JavaScriptSerializer().Serialize(itemViewModel);

            return serializedItemViewModel;
        }

        /// <summary>
        /// Generates image width attribute as html if the given width value is not nullable.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="width">The value to check.</param>
        /// <returns>The generated image width attribute as html content.</returns>
        public static string GetWidthAttributeIfExists(this HtmlHelper helper, int? width)
        {
            var html = width.HasValue ? string.Format(@"width={0}", width.Value) : string.Empty;

            return html;
        }

        /// <summary>
        /// Generates image height attribute as html if the given height value is not nullable.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="height">The value to check.</param>
        /// <returns>The generated image height attribute as html content.</returns>
        public static string GetHeightAttributeIfExists(this HtmlHelper helper, int? height)
        {
            var html = height.HasValue ? string.Format(@"height={0}", height.Value) : string.Empty;

            return html;
        }

        /// <summary>
        /// Generates details image width attribute as html if the given width value is not nullable.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="width">The value to check.</param>
        /// <returns>The generated details image width attribute as html content.</returns>
        public static string GetDetailsImageWidthAttributeIfExists(this HtmlHelper helper, int? width)
        {
            var html = width.HasValue ? string.Format(@"data-width={0}", width.Value) : string.Empty;

            return html;
        }

        /// <summary>
        /// Generates image width attributes as html by given image view model for object that is image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="model">The image view model.</param>
        /// <returns>The generated image width attribute as html content.</returns>
        public static string GetWidthAttributeForImage(this HtmlHelper helper, ImageViewModel model)
        {
            CustomSizeModel customSize = model.CustomSize;
            IDataItem dataItem = model.Item.DataItem;
            string thumbnailName = model.ThumbnailName;
            int? thumbnailWidth = model.ThumbnailWidth;
            int? thumbnailHeight = model.ThumbnailHeight;

            var html = string.Empty;
            var image = dataItem as Image;

            if (image != null)
            {
                double height = image.Height;
                double width = image.Width;
                double originalHeight = height;
                double originalWidth = width;

                double widthToHeightRatio = originalWidth / originalHeight;

                if (customSize != null && thumbnailName == null)
                {
                    if (customSize.Width.HasValue && customSize.Method.Equals(CropCropArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        width = customSize.Width.Value;
                    }
                    else if (customSize.MaxWidth.HasValue && customSize.Method.Equals(ResizeFitToAreaArguments, StringComparison.InvariantCultureIgnoreCase))
                    {

                        if (image.Height > customSize.MaxHeight.Value)
                        {
                            width = Math.Round(widthToHeightRatio * customSize.MaxHeight.Value);
                        }
                        else if (image.Width > customSize.MaxWidth.Value || image.IsVectorGraphics())
                        {
                            width = customSize.MaxWidth.Value;
                        }
                    }
                }
                else if (thumbnailName != null && thumbnailWidth.HasValue)
                {
                    width = thumbnailWidth.Value;
                }

                if (width > 0) 
                {
                    html = string.Format(@"width={0}", width);
                }
            }

            return html;
        }

        /// <summary>
        /// Generates image width attribute as html by given custom size model for object that is vector graphics.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="customSize">The custom size model.</param>
        /// <param name="dataItem">The data item represents an image object.</param>
        /// <returns>The generated image width attribute as html content.</returns>
        public static string GetWidthAttributeForVectorGraphics(this HtmlHelper helper, CustomSizeModel customSize, IDataItem dataItem)
        {
            var html = string.Empty;

            if (customSize != null)
            {
                var image = dataItem as Image;
                if (image != null && image.IsVectorGraphics())
                {
                    if (customSize.Width.HasValue && customSize.Method.Equals(CropCropArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        html = string.Format(@"width={0}", customSize.Width.Value);
                    }
                    else if (customSize.MaxWidth.HasValue && customSize.Method.Equals(ResizeFitToAreaArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        html = string.Format(@"width={0}", customSize.MaxWidth.Value);
                    }
                }
            }

            return html;
        }

        /// <summary>
        /// Generates image height attributes as html by given image view model for object that is image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="model">The image view model.</param>
        /// <returns>The generated image width attribute as html content.</returns>
        public static string GetHeightAttributeForImage(this HtmlHelper helper, ImageViewModel model)
        {
            CustomSizeModel customSize = model.CustomSize;
            IDataItem dataItem = model.Item.DataItem;
            string thumbnailName = model.ThumbnailName;
            int? thumbnailWidth = model.ThumbnailWidth;
            int? thumbnailHeight = model.ThumbnailHeight;

            var html = string.Empty;
            var image = dataItem as Image;

            if (image != null)
            {
                double height = image.Height;
                double width = image.Width;
                double originalHeight = height;
                double originalWidth = width;

                double heightToWidthRatio = originalHeight / originalWidth;

                if (customSize != null && thumbnailName == null)
                {
                    if (customSize.Height.HasValue && customSize.Method.Equals(CropCropArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        height = customSize.Height.Value;
                    }
                    else if (customSize.MaxHeight.HasValue && customSize.Method.Equals(ResizeFitToAreaArguments, StringComparison.InvariantCultureIgnoreCase))
                    {

                        if (image.Height > customSize.MaxHeight.Value || image.IsVectorGraphics())
                        {
                            height = customSize.MaxHeight.Value;
                        }
                        else if (image.Width > customSize.MaxWidth.Value)
                        {
                            height = Math.Round(heightToWidthRatio * customSize.MaxWidth.Value);
                        }
                    }
                }
                else if (thumbnailName != null && thumbnailHeight.HasValue)
                {
                    height = thumbnailHeight.Value;
                }

                if (height > 0) 
                {
                    html = string.Format(@"height={0}", height);
                }

            }

            return html;
        }

        /// <summary>
        /// Generates image height attribute as html by given custom size model for object that is vector graphics.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="customSize">The custom size model.</param>
        /// <param name="dataItem">The data item represents an image object.</param>
        /// <returns>The generated image height attribute as html content.</returns>
        public static string GetHeightAttributeForVectorGraphics(this HtmlHelper helper, CustomSizeModel customSize, IDataItem dataItem)
        {
            var html = string.Empty;

            if (customSize != null)
            {
                var image = dataItem as Image;
                if (image != null && image.IsVectorGraphics())
                {
                    if (customSize.Height.HasValue && customSize.Method.Equals(CropCropArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        html = string.Format(@"height={0}", customSize.Height.Value);
                    }
                    else if (customSize.MaxHeight.HasValue && customSize.Method.Equals(ResizeFitToAreaArguments, StringComparison.InvariantCultureIgnoreCase))
                    {
                        html = string.Format(@"height={0}", customSize.MaxHeight.Value);
                    }
                }
            }

            return html;
        }

        /// <summary>
        /// Gets the image thumbnails and sorts them by width in descending order
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="customSize">The image view model.</param>
        /// <returns>The thumbnails of the image, sorted by width in descending order</returns>

        public static List<Thumbnail> GetImageThumbnailsSortedByWidthDesc(this HtmlHelper helper, ImageViewModel model) 
        {
            List<Thumbnail> sortedList = new List<Thumbnail>();

            if (model.Item.DataItem != null)
            {
                var thumbnails = (model.Item.DataItem as MediaContent).Thumbnails;

                if (thumbnails != null)
                {
                    sortedList = thumbnails.OrderBy(t => t.Width).ToList();
                }
            }

            return sortedList;
        }

        private static readonly string ResizeFitToAreaArguments = "ResizeFitToAreaArguments";
        private static readonly string CropCropArguments = "CropCropArguments";
    }
}
