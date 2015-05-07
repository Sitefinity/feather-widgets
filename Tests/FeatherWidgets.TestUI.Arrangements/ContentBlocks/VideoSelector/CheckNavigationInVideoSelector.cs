using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Pages;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Check Navigation in Video Selector arrangement class.
    /// </summary>
    public class CheckNavigationInVideoSelector : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerSideUpload.CreateVideoLibrary(VideoLibraryTitle);
            var childId = ServerSideUpload.CreateFolder(ChildLibraryTitle, parentId);
            var nextChildId = ServerSideUpload.CreateFolder(NextChildLibraryTitle, childId);
            ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 1, VideoResource);
            ServerOperationsFeather.MediaOperations().UploadVideoInFolder(childId, VideoTitle + 2, VideoResourceChild);
            ServerOperations.Users().CreateUserWithProfileAndRoles("administrator", "password", "Administrator", "User", "administrator@test.test", new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.AuthenticateUser("administrator", "password", true);
            ServerOperationsFeather.MediaOperations().UploadVideoInFolder(nextChildId, VideoTitle + 3, VideoResourceNextChild);
        }

        /// <summary>
        /// Tears down.
        /// 
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile("administrator");
            ServerOperations.Libraries().DeleteAllVideoLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithVideo";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string ChildLibraryTitle = "ChildVideoLibrary";
        private const string NextChildLibraryTitle = "NextChildVideoLibrary";
        private const string VideoTitle = "Video";
        private const string VideoResource = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny.mp4";
        private const string VideoResourceChild = "FeatherWidgets.TestUtilities.Data.MediaFiles.WebM File.webm";
        private const string VideoResourceNextChild = "FeatherWidgets.TestUtilities.Data.MediaFiles.Ogv File.ogv";
    }
}