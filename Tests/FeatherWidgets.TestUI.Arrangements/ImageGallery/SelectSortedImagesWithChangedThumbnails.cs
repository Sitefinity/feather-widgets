using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Select Sorted Images With Changed Thumbnails arrangement class.
    /// </summary>
    public class SelectSortedImagesWithChangedThumbnails : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(page1Id);

            ServerSideUpload.CreateAlbum(ImageLibraryTitle);
            ServerSideUpload.CreateAlbum(AnotherImageLibraryTitle);

            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 1, ImageResource1);
            ServerSideUpload.UploadImage(AnotherImageLibraryTitle, ImageTitle + 2, ImageResource2);
            ServerSideUpload.UploadImage(AnotherImageLibraryTitle, ImageTitle + 3, ImageResource3);
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
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string AnotherImageLibraryTitle = "AnotherImageLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}