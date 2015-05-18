using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Select Sorted Videos With Changed Thumbnails arrangement class.
    /// </summary>
    public class SelectSortedVideosWithChangedThumbnails : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(page1Id);

            ServerSideUpload.CreateVideoLibrary(VideoLibraryTitle);
            ServerSideUpload.CreateVideoLibrary(AnotherVideoLibraryTitle);

            ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 1, VideoResource1);
            ServerSideUpload.UploadVideo(AnotherVideoLibraryTitle, VideoTitle + 2, VideoResource2);
            ServerSideUpload.UploadVideo(AnotherVideoLibraryTitle, VideoTitle + 3, VideoResource3);
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
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string AnotherVideoLibraryTitle = "AnotherVideoLibrary";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
    }
}