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
    /// Arrangement methods for VerifySelectedUsersInUsersListWithSelectedSingleUserPage
    /// </summary>
    public class VerifySelectedUsersInUsersListWithSelectedSingleUserPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid listPageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddUsersListWidgetToPage(listPageId);
            Guid detailsPageId = ServerOperations.Pages().CreatePage(SingleUserPage);
            ServerOperationsFeather.Pages().AddUsersListWidgetToPage(detailsPageId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail, AuthorPassword, AuthorFirstName, AuthorLastName, AuthorEmail, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(EditorEmail, EditorPassword, EditorFirstName, EditorLastName, EditorEmail, new List<string> { "BackendUsers", "Editors" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail);
            ServerOperations.Users().DeleteUserAndProfile(EditorEmail);
        }

        private const string PageName = "UsersListPage";
        private const string SingleUserPage = "UserPage";

        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "fname";
        private const string AuthorLastName = "lname";
        private const string AuthorEmail = "author@test.test";

        private const string EditorPassword = "password";
        private const string EditorFirstName = "fn";
        private const string EditorLastName = "ln";
        private const string EditorEmail = "editor@test.test";
    }
}
