using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Pages;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Check Navigation in Document Selector arrangement class.
    /// </summary>
    public class CheckNavigationInDocumentSelector : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerSideUpload.CreateDocumentLibrary(DocumentLibraryTitle);
            var childId = ServerSideUpload.CreateFolder(ChildLibraryTitle, parentId);
            var nextChildId = ServerSideUpload.CreateFolder(NextChildLibraryTitle, childId);
            ServerSideUpload.UploadDocument(DocumentLibraryTitle, DocumentTitle + 1, DocumentResource);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(childId, DocumentTitle + 2, DocumentResourceChild);
            //// ServerSideUpload.UploadImageInFolder(childId, ImageTitle + 2, ImageResourceChild);

            ServerOperations.Users().CreateUserWithProfileAndRoles("administrator", "password", "Administrator", "User", "administrator@test.test", new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.AuthenticateUser("administrator", "password", true);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(nextChildId, DocumentTitle + 3, DocumentResourceNextChild);
            //// ServerSideUpload.UploadImageInFolder(nextChildId, DocumentTitle + 3, DocumentResourceNextChild);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile("administrator");
            ServerOperations.Libraries().DeleteLibraries(false, "Image");
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