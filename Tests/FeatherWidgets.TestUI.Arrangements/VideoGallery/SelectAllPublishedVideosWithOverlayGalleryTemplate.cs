using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectAllPublishedVideosWithOverlayGalleryTemplate arrangement class.
    /// </summary>
    public class SelectAllPublishedVideosWithOverlayGalleryTemplate : ITestArrangement
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

            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(pageId, PlaceHolderId);

            ServerSideUpload.CreateVideoLibrary(VideoLibraryTitle);
            ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 1, VideoResource1);

            ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 2, VideoResource2);

            ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 3, VideoResource3);
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

        private const string PageTemplateName = "Bootstrap.default";
        private const string PageName = "PageWithVideo";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}