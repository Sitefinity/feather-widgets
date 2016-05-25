using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget arrangement class.
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            
            ServerOperations.Documents().CreateLibrary(LibraryTitle);
            ServerOperations.Documents().CreateLibrary(AnotherDocumentLibraryTitle);

            ServerOperations.Documents().Upload(LibraryTitle, DocumentTitle + 1, ImageResource1);
            ServerOperations.Documents().Upload(LibraryTitle, DocumentTitle + 2, ImageResource2);
            ServerOperations.Documents().Upload(AnotherDocumentLibraryTitle, DocumentTitle + 3, ImageResource3);
            ServerOperations.Documents().Upload(AnotherDocumentLibraryTitle, DocumentTitle + 4, ImageResource4);
            ServerOperations.Documents().Upload(AnotherDocumentLibraryTitle, DocumentTitle + 5, ImageResource5);

            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(pageId, "Body");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Documents().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "DocumentsPage";
        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string AnotherDocumentLibraryTitle = "AnotherDocumentLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string ImageResource4 = "Telerik.Sitefinity.TestUtilities.Data.Images.4.jpg";
        private const string ImageResource5 = "Telerik.Sitefinity.TestUtilities.Data.Images.5.jpg";
    }
}
