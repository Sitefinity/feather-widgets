using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.MediaGalleryWidgets
{
    /// <summary>
    /// This is a class with Video gallery tests for Sorting options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Video gallery sorting tests.")]
    public class VideoGalleryWidgetSortingTests
    {
        /// <summary>
        /// Create library with Videos
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ServerSideUpload.CreateVideoLibrary(LibraryTitle);
            ServerSideUpload.UploadVideo(LibraryTitle, VideoTitle + 1, VideoResource1);
            ServerSideUpload.UploadVideo(LibraryTitle, VideoTitle + 3, VideoResource3);
            ServerSideUpload.UploadVideo(LibraryTitle, VideoTitle + 2, VideoResource2);
        }

        /// <summary>
        /// Delete library
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            ServerOperations.Libraries().DeleteLibraries(false, "Video");
        }

        /// <summary>
        /// Verify Last published sorting and All Videos in Video gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void VideoGallery_VerifySortingLastPublished()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.SortExpression = "PublicationDate DESC";       
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(3), "Number of Videos is not correct");

            //// expected: Video2, Video3, Video1
            Assert.AreEqual(VideoTitle + 2, videos[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 3, videos[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 1, videos[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title A-Z sorting and All Videos in Video gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void VideoGallery_VerifySortingTitleAZ()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.SortExpression = "Title ASC";
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(3), "Number of Videos is not correct");

            //// expected: Video1, Video2, Video3
            Assert.AreEqual(VideoTitle + 1, videos[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 2, videos[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 3, videos[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title Z-A sorting and All Videos in Video gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void VideoGallery_VerifySortingTitleZA()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.SortExpression = "Title DESC";
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(3), "Number of Videos is not correct");

            //// expected: Video3, Video2, Video1
            Assert.AreEqual(VideoTitle + 3, videos[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 2, videos[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 1, videos[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Last modified sorting and All Videos in Video gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void VideoGallery_VerifySortingLastModified()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.SortExpression = "LastModified DESC";
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            this.ChangeModifiedDateOfVideo3();

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(3), "Number of Videos is not correct");

            //// expected: Video31, Video2, Video1
            Assert.AreEqual(VideoTitle + 3 + 1, videos[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 2, videos[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideoTitle + 1, videos[2].Fields.Title.Value, "Wrong title");
        }
 
        private void ChangeModifiedDateOfVideo3()
        {
            var librariesManager = LibrariesManager.GetManager();
            Video modified = librariesManager.GetVideos().Where<Video>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == VideoTitle + 3).FirstOrDefault();
            Video temp = librariesManager.Lifecycle.CheckOut(modified) as Video;

            temp.Title = VideoTitle + 3 + 1;

            modified = librariesManager.Lifecycle.CheckIn(temp) as Video;

            var video = librariesManager.GetVideo(modified.Id);
            librariesManager.Lifecycle.Publish(video);
            librariesManager.SaveChanges();
        }

        private const string LibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
    }
}