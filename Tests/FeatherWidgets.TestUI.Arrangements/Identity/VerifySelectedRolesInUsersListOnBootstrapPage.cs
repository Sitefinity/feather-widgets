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
    /// Arrangement methods for VerifySelectedRolesInUsersListOnBootstrapPage
    /// </summary>
    public class VerifySelectedRolesInUsersListOnBootstrapPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddUsersListWidgetToPage(pageNodeId, PlaceHolderId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail, AuthorPassword, AuthorFirstName, AuthorLastName, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AdministratorEmail, AdministratorPassword, AdministratorFirstName, AdministratorLastName, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail);
            ServerOperations.Users().DeleteUserAndProfile(AdministratorEmail);
        }

        private const string PageTemplateName = "Bootstrap.default";
        private const string PageName = "UsersListPage";
        private const string PlaceHolderId = "Contentplaceholder1";

        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "fname";
        private const string AuthorLastName = "lname";
        private const string AuthorEmail = "author@test.test";

        private const string AdministratorPassword = "passoword";
        private const string AdministratorFirstName = "admin2";
        private const string AdministratorLastName = "admin2";
        private const string AdministratorEmail = "admin2@admin.com";
    }
}
