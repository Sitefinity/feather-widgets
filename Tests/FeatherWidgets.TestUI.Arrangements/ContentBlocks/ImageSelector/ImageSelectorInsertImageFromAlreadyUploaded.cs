using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    public class ImageSelectorInsertImageFromAlreadyUploaded : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);
        }

        /// <summary>
        /// Server arrangement method
        /// </summary>
        [ServerArrangement]
        public void UploadImage()
        {
            ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            Guid id = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle, ImageResource);

            var manager = LibrariesManager.GetManager();
            var master = manager.GetImage(id);
            var live = manager.Lifecycle.GetLive(master);

            ServerArrangementContext.GetCurrent().Values.Add("imageId", live.Id.ToString());
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
        private const string ImageTitle = "Image1";
        private const string ImageResource = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
    }
}
