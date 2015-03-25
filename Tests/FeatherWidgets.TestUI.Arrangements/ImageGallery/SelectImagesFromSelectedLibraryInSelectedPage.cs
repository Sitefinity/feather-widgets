using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Select Images From Selected Library In Selected Page arrangement class.
    /// </summary>
    public class SelectImagesFromSelectedLibraryInSelectedPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(page1Id);

            Guid singleItemPageId = ServerOperations.Pages().CreatePage(SingleItemPage);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(singleItemPageId);

            var parentId = ServerSideUpload.CreateAlbum(ImageLibraryTitle);
            var childId = ServerSideUpload.CreateFolder(ChildLibraryTitle, parentId);

            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 1, ImageResource1);
            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 2, ImageResource2);           
            ServerSideUpload.UploadImageInFolder(childId, ImageTitle + 3, ImageResource3);
            ServerSideUpload.UploadImageInFolder(childId, ImageTitle + 4, ImageResource4);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Libraries().DeleteLibraries(false, "Image");
        }

        private const string PageName = "PageWithImage";
        private const string SingleItemPage = "Test Page";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string ChildLibraryTitle = "ChildImageLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string ImageResource4 = "Telerik.Sitefinity.TestUtilities.Data.Images.4.jpg";
    }
}