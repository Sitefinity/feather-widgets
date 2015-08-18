using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SitefinityTestUtilities = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Identity.LoginForm
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), TestFixture]
    public class LoginFormTests
    {
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether authenticating with login form's model will result in current identity with correctly set claim type properties.")]
        public void AuthenticateUser_IdentityHasClaimTypes()
        {
            const string UserName = "AuthenticateUser_IdentityHasClaimTypes";
            const string Password = "admin@2";

            SecurityManager.Logout();

            SitefinityTestUtilities.ServerOperations.Users().CreateUser(UserName, Password, "mymail12345@mail.com", "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

            try
            {
                var model = new LoginFormModel();
                model.Authenticate(new LoginFormViewModel() { UserName = UserName, Password = Password }, SystemManager.CurrentHttpContext);

                var currentIdentity = ClaimsManager.GetCurrentIdentity();

                Assert.AreEqual(UserName, currentIdentity.Name, "The name of the current identity did not match the user.");
                Assert.IsNotNull(currentIdentity.NameClaimType, "NameClaimType was not set in the current identity.");
                Assert.IsNotNull(currentIdentity.RoleClaimType, "RoleClaimType was not set in the current identity.");
                Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", currentIdentity.NameClaimType, "NameClaimType did not have the expected value.");
                Assert.AreEqual("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", currentIdentity.RoleClaimType, "RoleClaimType did not have the expected value.");
            }
            finally
            {
                SecurityManager.Logout();
                SitefinityTestUtilities.ServerOperations.Users().DeleteUsers(new[] { UserName });
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether redirect url is preserved in form action attribute.")]
        public void PostForm_RedirectUrlIsPreserved()
        {
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            var redirectQuery = "?RedirectUrl=myRedirectUrl";
            this.pageOperations = new PagesOperations();

            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginFormController).FullName;
                var loginFormController = new LoginFormController();
                mvcProxy.Settings = new ControllerSettings(loginFormController);

                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                SecurityManager.Logout();
                var responseContent = PageInvoker.ExecuteWebRequest(loginFormPageUrl + redirectQuery);

                var expectedActionUrl = this.urlNamePrefix + this.pageIndex + redirectQuery;
                Assert.IsTrue(responseContent.Contains(string.Format("action=\"{0}\"", expectedActionUrl)), "The action URL no longer contains redirect URL");
            }
            finally
            {
                SecurityManager.Logout();
                SitefinityTestUtilities.ServerOperations.Users().AuthenticateAdminUser();
                this.pageOperations.DeletePages();
            }
        }

        private string pageNamePrefix = "LoginFormPage";
        private string pageTitlePrefix = "Login Form";
        private string urlNamePrefix = "login-form";
        private int pageIndex = 1;

        private PagesOperations pageOperations;
    }
}
