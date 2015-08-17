using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

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

            ServerOperations.Users().CreateUser(UserName, Password, "mymail12345@mail.com", "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

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
                ServerOperations.Users().DeleteUsers(new[] { UserName });
            }
        }
    }
}
