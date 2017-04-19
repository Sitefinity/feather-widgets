using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    public class VerifySvgImageInImageGallery : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Images().EnableSvgFileExtension(enableSvgSupport);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(pageId);

            var parentId = ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            var childId = ServerOperations.Images().CreateFolder(ChildLibraryTitle, parentId);

            ServerOperationsFeather.MediaOperations().UploadImageInFolder(childId, ImageTitle + 1, ImageResource1);
            ServerOperationsFeather.MediaOperations().UploadImageInFolder(childId, ImageTitle + 2, ImageResource2);
            ServerOperationsFeather.MediaOperations().UploadImageInFolder(childId, ImageTitle + 3, ImageResource3);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();

            ////Disable Svg Support
            ServerOperations.Images().EnableSvgFileExtension();
        }

        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        private static bool enableSvgSupport = true;
        private const string PageName = "PageWithSvgImage";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ChildLibraryTitle = "ChildImageLibrary";
        private const string ImageTitle = "Image";
        private const string ImageResource1 = "FeatherWidgets.TestUtilities.Data.MediaFiles.test1.svg";
        private const string ImageResource2 = "FeatherWidgets.TestUtilities.Data.MediaFiles.test2.svg";
        private const string ImageResource3 = "FeatherWidgets.TestUtilities.Data.MediaFiles.test3.svg";
    }
}
