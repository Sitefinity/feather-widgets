using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for DragAndDropProfileWidgetAndSetReadMode
    /// </summary>
    public class DragAndDropProfileWidgetAndSetReadMode : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(ProfilePage, templateId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(NewUserEmail, NewUserPassword, NewUserFirstName, NewUserLastName, NewUserEmail, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.LogoutUser(NewUserEmail);
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(NewUserEmail);
        }

        private const string ProfilePage = "ProfilePage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string NewUserPassword = "password";
        private const string NewUserFirstName = "First name";
        private const string NewUserLastName = "Last name";
        private const string NewUserEmail = "newuser@test.test";
    }
}
