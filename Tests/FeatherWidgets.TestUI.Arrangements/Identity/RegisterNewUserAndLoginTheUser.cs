using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for RegisterNewUserAndLoginTheUser
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
    public class RegisterNewUserAndLoginTheUser : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(RegistrationPage, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddRegistrationWidgetToPage(pageId, PlaceHolderId);

            Guid loginPageId = ServerOperations.Pages().CreatePage(LoginPage, templateId);
            loginPageId = ServerOperations.Pages().GetPageNodeId(loginPageId);
            ServerOperationsFeather.Pages().AddLoginFormWidgetToPage(loginPageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(UserName);
        }

        private const string RegistrationPage = "RegistrationPage";
        private const string LoginPage = "LoginPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string UserName = "newUser";
    }
}
