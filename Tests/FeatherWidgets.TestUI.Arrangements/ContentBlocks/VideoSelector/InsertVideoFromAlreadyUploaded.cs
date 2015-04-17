using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// InsertVideoFromAlreadyUploaded class
    /// </summary>
    public class InsertVideoFromAlreadyUploaded : ITestArrangement
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
        public void UploadVideo()
        {
            ServerSideUpload.CreateVideoLibrary(LibraryTitle);
            Guid id = ServerSideUpload.UploadVideo(LibraryTitle, VideoTitle, VideoResource);

            var manager = LibrariesManager.GetManager();
            var master = manager.GetVideo(id);
            var live = manager.Lifecycle.GetLive(master);

            ServerArrangementContext.GetCurrent().Values.Add("videoId", live.Id.ToString());
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
        private const string VideoTitle = "big_buck_bunny";
        private const string VideoResource = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny.mp4";
    }
}
