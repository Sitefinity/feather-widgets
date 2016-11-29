using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifyUseLimitListSettingsOnFrontendPageForUsersListWidget
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForUsersListWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid listPageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddUsersListWidgetToPage(listPageId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail, AuthorPassword, AuthorFirstName, AuthorLastName, AuthorEmail, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail1, AuthorPassword1, AuthorFirstName1, AuthorLastName1, AuthorEmail1, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(EditorEmail, EditorPassword, EditorFirstName, EditorLastName, EditorEmail, new List<string> { "BackendUsers", "Editors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AdministratorEmail1, AdministratorPassword1, AdministratorFirstName1, AdministratorLastName1, AdministratorEmail1, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail);
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail1);
            ServerOperations.Users().DeleteUserAndProfile(EditorEmail);
            ServerOperations.Users().DeleteUserAndProfile(AdministratorEmail1);
        }

        private const string PageName = "UsersListPage";

        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "fname";
        private const string AuthorLastName = "lname";
        private const string AuthorEmail = "author@test.test";

        private const string AuthorPassword1 = "password";
        private const string AuthorFirstName1 = "fname1";
        private const string AuthorLastName1 = "lname1";
        private const string AuthorEmail1 = "author1@test.test";

        private const string EditorPassword = "password";
        private const string EditorFirstName = "fn";
        private const string EditorLastName = "ln";
        private const string EditorEmail = "editor@test.test";

        private const string AdministratorPassword1 = "passoword";
        private const string AdministratorFirstName1 = "admin1";
        private const string AdministratorLastName1 = "admin1";
        private const string AdministratorEmail1 = "admin1@admin.com";
    }
}
