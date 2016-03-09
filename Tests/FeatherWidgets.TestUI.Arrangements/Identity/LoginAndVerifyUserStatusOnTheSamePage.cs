using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
    public class LoginAndVerifyUserStatusOnTheSamePage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperations.Users().CreateUserWithProfileAndRoles(TestAdmin, TestAdminPass, TestAdminFirstName, TestAdminLastName, TestAdminEmail, this.roles);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "LoginPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string TestAdmin = "admin2";
        private const string TestAdminUserName = "admin2";
        private const string TestAdminPass = "password";
        private const string TestAdminFirstName = "admin2";
        private const string TestAdminLastName = "admin2";
        private const string TestAdminEmail = "admin2@test.com";
        private readonly List<string> roles = new List<string>() { "Administrators", "BackendUsers" };
    }
}
