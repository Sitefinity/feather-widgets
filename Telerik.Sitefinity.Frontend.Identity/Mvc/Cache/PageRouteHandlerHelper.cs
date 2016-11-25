using System;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Cache
{
    /// <summary>
    /// PageRouteHandler helper class.
    /// </summary>
    public class PageRouteHandlerHelper
    {
        /// <summary>
        /// Methods invokes RegisterCustomOutputCacheVariation of <see cref="PageRouteHandler"/>.
        /// </summary>
        /// <param name="outputCacheVariation">The output cache variation.</param>
        public static void RegisterCustomOutputCacheVariation(ICustomOutputCacheVariation outputCacheVariation)
        {
            var pageRouteHandlerType = typeof(PageRouteHandler);

            pageRouteHandlerType.GetMethod(PageRouteHandlerHelper.RegisterCustomOutputCacheVariationMethodName, BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(ICustomOutputCacheVariation) }, null)
                                .Invoke(null, new object[] { outputCacheVariation });
        }

        /// <summary>
        /// Name of Register custom output cache variation method in <see cref="PageRouteHandler"/>.
        /// </summary>
        public const string RegisterCustomOutputCacheVariationMethodName = "RegisterCustomOutputCacheVariation";
    }
}