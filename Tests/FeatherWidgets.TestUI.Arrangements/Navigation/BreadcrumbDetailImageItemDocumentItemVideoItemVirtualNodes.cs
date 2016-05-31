using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a class with arrangement methods related to UI test BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes
    /// </summary>
    public class BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            
            ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle, ImageResource1);

            ServerOperations.Documents().CreateLibrary(DocLibraryTitle);
            ServerOperations.Documents().Upload(DocLibraryTitle, DocumentTitle, ImageResource2);

            ServerOperations.Videos().CreateLibrary(VideoLibraryTitle);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle, VideoResource1);

            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddBreadcrumbWidgetToPage(pageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Videos().DeleteAllLibrariesExceptDefaultOne();
            ServerOperations.Documents().DeleteAllLibrariesExceptDefaultOne();
            ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "TestPageMediaWidgets";
        private const string PlaceHolderId = "Body";
        private const string ImageTitle = "TestImage1";
        private const string DocumentTitle = "TestDocument1";
        private const string VideoTitle = "TestVideo1";
        private const string DocLibraryTitle = "TestDocumentLibrary";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
    }
}
