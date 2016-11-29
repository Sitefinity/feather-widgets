using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Pages;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Check Navigation in Document Selector arrangement class.
    /// </summary>
    public class CheckNavigationInDocumentSelector : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerOperations.Documents().CreateLibrary(DocumentLibraryTitle);
            var childId = ServerOperations.Documents().CreateFolder(ChildLibraryTitle, parentId);
            var nextChildId = ServerOperations.Documents().CreateFolder(NextChildLibraryTitle, childId);
            ServerOperations.Documents().Upload(DocumentLibraryTitle, DocumentTitle + 1, DocumentResource);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(childId, DocumentTitle + 2, DocumentResourceChild);

            ServerOperations.Users().CreateUserWithProfileAndRoles("administrator", "password", "Administrator", "User", "administrator@test.test", new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.AuthenticateUser("administrator", "password", true);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(nextChildId, DocumentTitle + 3, DocumentResourceNextChild);
        }

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
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile("administrator");
            ServerOperations.Documents().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithDocument";
        private const string DocumentLibraryTitle = "TestDocumentLibrary";
        private const string ChildLibraryTitle = "ChildDocumentLibrary";
        private const string NextChildLibraryTitle = "NextChildDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string DocumentResource = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string DocumentResourceChild = "FeatherWidgets.TestUtilities.Data.MediaFiles.2.jpg";
        private const string DocumentResourceNextChild = "FeatherWidgets.TestUtilities.Data.MediaFiles.3.jpg";
    }
}