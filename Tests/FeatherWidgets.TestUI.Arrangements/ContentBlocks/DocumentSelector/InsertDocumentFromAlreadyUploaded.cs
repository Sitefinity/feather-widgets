using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// InsertDocumentFromAlreadyUploaded class
    /// </summary>
    public class InsertDocumentFromAlreadyUploaded : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Libraries().DeleteAllDocumentLibrariesExceptDefaultOne();
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);
        }

        /// <summary>
        /// Server arrangement method
        /// </summary>
        [ServerArrangement]
        public void UploadDocument()
        {
            ServerOperations.Documents().CreateDocumentLibrary(LibraryTitle);
            Guid id = ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle, DocumentResource);

            var manager = LibrariesManager.GetManager();
            var master = manager.GetDocument(id);
            var live = manager.Lifecycle.GetLive(master);

            ServerArrangementContext.GetCurrent().Values.Add("documentId", live.Id.ToString());
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Libraries().DeleteAllDocumentLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithDocument";
        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Image1";
        private const string DocumentResource = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
    }
}
