using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// This class is used as a resource class for localization of strings rendered by the MVC framework. It should not be used directly.
    /// </summary>
    public static class LoginFormStaticResources
    {
        public static string Username
        {
            get
            {
                return Res.Get<LoginFormResources>().Username;
            }
        }

        public static string Password
        {
            get
            {
                return Res.Get<LoginFormResources>().Password;
            }
        }

        public static string RememberMe
        {
            get
            {
                return Res.Get<LoginFormResources>().RememberMe;
            }
        }
    }
}
