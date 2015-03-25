using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for DragAndDropProfileWidgetAndSetReadMode
    /// </summary>
    public class DragAndDropProfileWidgetAndSetReadMode : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(ProfilePage, templateId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(NewUserName, NewUserPassword, NewUserFirstName, NewUserLastName, NewUserEmail, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.LogoutUser(NewUserName);
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(NewUserName);
        }

        private const string ProfilePage = "ProfilePage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string NewUserName = "newUser";
        private const string NewUserPassword = "password";
        private const string NewUserFirstName = "First name";
        private const string NewUserLastName = "Last name";
        private const string NewUserEmail = "newuser@test.com";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
    }
}
