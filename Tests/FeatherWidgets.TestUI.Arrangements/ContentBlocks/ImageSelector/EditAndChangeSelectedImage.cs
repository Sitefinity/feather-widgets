using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Edit And Change Selected Image arrangement class.
    /// </summary>
    public class EditAndChangeSelectedImage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            ServerSideUpload.CreateAlbum(ImageLibraryTitle);
            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 1, ImageResource);

            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 2, ImageResourceChild);
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
        private const string ImageResource = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResourceChild = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
    }
}