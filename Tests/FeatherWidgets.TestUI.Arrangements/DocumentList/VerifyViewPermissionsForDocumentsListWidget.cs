using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyViewPermissionsForDocumentsListWidget arrangement class.
    /// </summary>
    public class VerifyViewPermissionsForDocumentsListWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Roles().CreateRole(RoleName1);
            ServerOperations.Roles().CreateRole(RoleName2);
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(pageId, "Contentplaceholder1");
            ServerOperationsFeather.Pages().AddLoginStatusWidgetToPage(pageId, "Contentplaceholder1");
            ServerOperationsFeather.Pages().AddLoginFormWidgetToPage(pageId, "Contentplaceholder1");

            ServerOperations.Documents().CreateLibrary(LibraryTitle);
            ServerOperations.Documents().CreateLibrary(AnotherDocumentLibraryTitle);

            ServerOperations.Documents().Upload(LibraryTitle, DocumentTitle + 1, ImageResource1);
            ServerOperations.Documents().Upload(AnotherDocumentLibraryTitle, DocumentTitle + 2, ImageResource2);

            Guid userId1 = ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail1, AuthorPassword1, AuthorFirstName1, AuthorLastName1, new List<string> { RoleName1 });
            Guid userId2 = ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorEmail2, AuthorPassword2, AuthorFirstName2, AuthorLastName2, new List<string> { RoleName2 });
            ServerOperations.Roles().AssignRoleToUser(RoleName1, userId1);
            ServerOperations.Roles().AssignRoleToUser(RoleName2, userId2);
        }

        /// <summary>
        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Users().LogoutUser();
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass); 
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Documents().DeleteAllLibrariesExceptDefaultOne();
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail1);
            ServerOperations.Users().DeleteUserAndProfile(AuthorEmail2);
            ServerOperations.Roles().DeleteRoles(new string[] { RoleName1, RoleName2 });
        }

        private const string PageName = "PageWithDocument";
        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string AnotherDocumentLibraryTitle = "AnotherDocumentLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string PageTemplateName = "Bootstrap.default";
        private const string AuthorPassword1 = "admin@2";
        private const string AuthorFirstName1 = "fname1";
        private const string AuthorLastName1 = "lname1";
        private const string AuthorEmail1 = "user1@test.test";
        private const string AuthorPassword2 = "admin@2";
        private const string AuthorFirstName2 = "fname2";
        private const string AuthorLastName2 = "lname2";
        private const string AuthorEmail2 = "user2@test.test";
        private const string RoleName1 = "TestRole1";
        private const string RoleName2 = "TestRole2";
    }
}