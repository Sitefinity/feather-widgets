using System.Collections.Generic;
using System.Web;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models
{
    /// <summary>
    /// Classes that implement this interface could be used as model with can handle cache dependencies for the Navigation widget.
    /// </summary>
    public interface ICacheDependentNavigationModel : INavigationModel
    {
        /// <summary>
        /// Gets the cache dependency objects.
        /// </summary>
        /// <returns></returns>
        IList<CacheDependencyKey> GetCacheDependencyObjects();
    }
}
