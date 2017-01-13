using System.Web;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper method for working with users.
    /// </summary>
    public static class UsersHelper
    {
        /// <summary>
        /// Encode username in the url
        /// </summary>
        /// <param name="url">Input URL</param>
        /// <param name="user">User profile data</param>
        /// <returns></returns>
        public static string EncodeUrlUsername(string url, SitefinityProfileItemViewModel user)
        {
            if (user == null || user.DataItem == null || !(user.DataItem is SitefinityProfile))
            {
                return url;
            }

            var username = (user.DataItem as SitefinityProfile).User.UserName;
            return url.Replace(username, HttpUtility.UrlEncode(username));
        }
    }
}
