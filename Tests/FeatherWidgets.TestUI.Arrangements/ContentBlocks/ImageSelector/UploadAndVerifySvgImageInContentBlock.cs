using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Upload Svg Image, create MVC page with Content Block widget
    /// </summary>
    public class UploadAndVerifySvgImageInContentBlock : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Images().EnableSvgFileExtension(enableSvgSupport);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId);

            var parentId = ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            ServerOperations.Images().CreateFolder(ChildLibraryTitle, parentId); 
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
    }
}
