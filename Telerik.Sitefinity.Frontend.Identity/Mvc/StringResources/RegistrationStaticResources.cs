using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// This class is used as a resource class for localization of strings rendered by the MVC framework. It should not be used directly.
    /// </summary>
    public static class RegistrationStaticResources
    {
        public static string Email
        {
            get
            {
                return Res.Get<RegistrationResources>().Email;
            }
        }

        public static string Username
        {
            get
            {
                return Res.Get<RegistrationResources>().Username;
            }
        }

        public static string Password
        {
            get
            {
                return Res.Get<RegistrationResources>().Password;
            }
        }

        public static string ReTypePassword
        {
            get
            {
                return Res.Get<RegistrationResources>().ReTypePassword;
            }
        }
    }
}
