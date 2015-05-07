using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// EditAllPropertiesOfSelectedVideoAndSearch arrangement class.
    /// </summary>
    public class EditAllPropertiesOfSelectedVideoAndSearch : ITestArrangement
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

            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, string.Empty, PlaceHolderId);

            ServerSideUpload.CreateVideoLibrary(LibraryTitle);
            ServerSideUpload.UploadVideo(LibraryTitle, VideoTitle1, VideoResource1);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Libraries().DeleteAllVideoLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithVideo";
        private const string LibraryTitle = "TestVideoLibrary";
        private const string VideoTitle1 = "big_buck_bunny1";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}