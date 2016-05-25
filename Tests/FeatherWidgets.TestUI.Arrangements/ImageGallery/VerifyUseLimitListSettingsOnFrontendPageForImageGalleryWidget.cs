using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget arrangement class.
    /// </summary>
    public class VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            ServerOperations.Images().CreateLibrary(LibraryTitle);
            ServerOperations.Images().CreateLibrary(AnotherImageLibraryTitle);

            ServerOperations.Images().Upload(LibraryTitle, ImageTitle + 1, ImageResource1);
            ServerOperations.Images().Upload(LibraryTitle, ImageTitle + 2, ImageResource2);
            ServerOperations.Images().Upload(AnotherImageLibraryTitle, ImageTitle + 3, ImageResource3);
            ServerOperations.Images().Upload(AnotherImageLibraryTitle, ImageTitle + 4, ImageResource4);
            ServerOperations.Images().Upload(AnotherImageLibraryTitle, ImageTitle + 5, ImageResource5);

            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(pageId, "Body");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "ImagesPage";
        private const string LibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string AnotherImageLibraryTitle = "AnotherImageLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string ImageResource4 = "Telerik.Sitefinity.TestUtilities.Data.Images.4.jpg";
        private const string ImageResource5 = "Telerik.Sitefinity.TestUtilities.Data.Images.5.jpg";
    }
}
