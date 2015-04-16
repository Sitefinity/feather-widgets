using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

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
            var filterExpression = string.Format("(Id = {0} OR OriginalContentId = {1})", id.ToString("D"), mediaItem.OriginalContentId);
            location.Filters.Add(new BasicContentLocationFilter(filterExpression));

            return new[] { location };
        }

        /// <inheritDoc/>
        public static void HandlePreview<T>(HttpRequestBase request, Guid itemId, string providerName) 
            where T : MediaContent
        {
            string contentAction = request.QueryString["sf-content-action"];

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
