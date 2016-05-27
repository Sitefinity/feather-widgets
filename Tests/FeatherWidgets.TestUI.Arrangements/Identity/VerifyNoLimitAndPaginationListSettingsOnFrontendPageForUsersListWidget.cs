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
    /// Arrangement methods for VerifyNoLimitAndPaginationListSettingsOnFrontendPageForUsersListWidget
    /// </summary>
    public class VerifyNoLimitAndPaginationListSettingsOnFrontendPageForUsersListWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid listPageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddUsersListWidgetToPage(listPageId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorUserName, AuthorPassword, AuthorFirstName, AuthorLastName, AuthorEmail, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorUserName1, AuthorPassword1, AuthorFirstName1, AuthorLastName1, AuthorEmail1, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(EditorUserName, EditorPassword, EditorFirstName, EditorLastName, EditorEmail, new List<string> { "BackendUsers", "Editors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AdministratorUserName1, AdministratorPassword1, AdministratorFirstName1, AdministratorLastName1, AdministratorEmail1, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(AuthorUserName);
            ServerOperations.Users().DeleteUserAndProfile(AuthorUserName1);
            ServerOperations.Users().DeleteUserAndProfile(EditorUserName);
            ServerOperations.Users().DeleteUserAndProfile(AdministratorUserName1);
        }

        private const string PageName = "UsersListPage";

        private const string AuthorUserName = "author";
        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "fname";
        private const string AuthorLastName = "lname";
        private const string AuthorEmail = "author@test.com";

        private const string AuthorUserName1 = "author1";
        private const string AuthorPassword1 = "password";
        private const string AuthorFirstName1 = "fname1";
        private const string AuthorLastName1 = "lname1";
        private const string AuthorEmail1 = "author1@test.com";

        private const string EditorUserName = "editor";
        private const string EditorPassword = "password";
        private const string EditorFirstName = "fn";
        private const string EditorLastName = "ln";
        private const string EditorEmail = "editor@test.com";

        private const string AdministratorUserName1 = "admin1";
        private const string AdministratorPassword1 = "passoword";
        private const string AdministratorFirstName1 = "admin1";
        private const string AdministratorLastName1 = "admin1";
        private const string AdministratorEmail1 = "admin1@admin.com";
    }
}
