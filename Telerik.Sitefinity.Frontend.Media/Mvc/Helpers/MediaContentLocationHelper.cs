using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Helpers
{
    /// <summary>
    /// This class contains functionality for working with content locations and media items.
    /// </summary>
    internal class ContentLocationHelper
    {
        public static IEnumerable<IContentLocationInfo> GetLocations(Guid id, string providerName, Type mediaType)
        {
            var location = new ContentLocationInfo();
            location.ContentType = mediaType;
            location.ProviderName = providerName;

            var mediaItem = LibrariesManager.GetManager(providerName).GetItem(mediaType, id) as MediaContent;
            var itemsList = new List<string>();
            itemsList.Add(mediaItem.Id.ToString());
            if (mediaItem.OriginalContentId != Guid.Empty)
            {
                itemsList.Add(mediaItem.OriginalContentId.ToString());
            }

            var filter = new ContentLocationSingleItemFilter(itemsList);
            location.Filters.Add(filter);

            return new[] { location };
        }

        /// <inheritDoc/>
        public static void HandlePreview<T>(HttpRequestBase request, Guid itemId, string providerName) 
            where T : MediaContent
        {
            string contentAction = request.QueryStringGet("sf-content-action");

            if (contentAction != null && contentAction == "preview")
            {
                var mediaItem = Telerik.Sitefinity.Modules.Libraries.LibrariesManager.GetManager(providerName).GetItems<T>().SingleOrDefault(i => i.Id == itemId);

                if (mediaItem != null)
                {
                    var routeDataParams = request.RequestContext.RouteData.Values["params"] as string[];

                    if (routeDataParams != null && routeDataParams.Contains(mediaItem.UrlName.Value))
                        Telerik.Sitefinity.Web.RouteHelper.SetUrlParametersResolved();
                }
            }
        }
    }
}
