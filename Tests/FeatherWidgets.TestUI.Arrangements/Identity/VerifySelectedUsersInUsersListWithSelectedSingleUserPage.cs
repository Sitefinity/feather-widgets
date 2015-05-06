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
    /// Arrangement methods for VerifySelectedUsersInUsersListWithSelectedSingleUserPage
    /// </summary>
    public class VerifySelectedUsersInUsersListWithSelectedSingleUserPage : ITestArrangement
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

            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorUserName, AuthorPassword, AuthorFirstName, AuthorLastName, AuthorEmail, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(EditorUserName, EditorPassword, EditorFirstName, EditorLastName, EditorEmail, new List<string> { "BackendUsers", "Editors" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(AuthorUserName);
            ServerOperations.Users().DeleteUserAndProfile(EditorUserName);
        }

        private const string PageName = "UsersListPage";
        private const string SingleUserPage = "UserPage";

        private const string AuthorUserName = "author";
        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "fname";
        private const string AuthorLastName = "lname";
        private const string AuthorEmail = "author@test.com";

        private const string EditorUserName = "editor";
        private const string EditorPassword = "password";
        private const string EditorFirstName = "fn";
        private const string EditorLastName = "ln";
        private const string EditorEmail = "editor@test.com";
    }
}
