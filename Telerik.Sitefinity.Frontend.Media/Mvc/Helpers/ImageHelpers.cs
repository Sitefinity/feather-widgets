using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;

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

            var itemViewModel = new
            {
                Title = image.Title.Value,
                AlternativeText = image.AlternativeText.Value,
                Description = image.Description.Value,
                MediaUrl = image.MediaUrl,
                DateCreated = image.DateCreated,
                Author = image.Author.Value
            };

            var serializedItemViewModel = new JavaScriptSerializer().Serialize(itemViewModel);

            return serializedItemViewModel;
        }
    }
}
