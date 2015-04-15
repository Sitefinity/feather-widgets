using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Select Sorted Documents In Table View arrangement class.
    /// </summary>
    public class SelectSortedDocumentsInTableView : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(pageId, "Contentplaceholder1");

            ServerSideUpload.CreateDocumentLibrary(LibraryTitle);
            ServerSideUpload.CreateDocumentLibrary(AnotherDocumentLibraryTitle);

            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 1, ImageResource1);
            ServerSideUpload.UploadDocument(AnotherDocumentLibraryTitle, DocumentTitle + 2, ImageResource2);
            ServerSideUpload.UploadDocument(AnotherDocumentLibraryTitle, DocumentTitle + 3, ImageResource3);
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
        private const string DocumentTitle = "Document";
        private const string AnotherDocumentLibraryTitle = "AnotherDocumentLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string PageTemplateName = "Bootstrap.default";
    }
}