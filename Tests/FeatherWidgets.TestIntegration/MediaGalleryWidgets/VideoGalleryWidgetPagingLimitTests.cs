using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.MediaGalleryWidgets
{
    /// <summary>
    /// This is a class with Videos gallery tests for paging and limit options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Videos gallery paging and limit tests.")]
    public class VideoGalleryWidgetPagingLimitTests
    {
        /// <summary>
        /// Create library with Videos
        /// </summary>
        [SetUp]
        public void Setup()
        {     
            ServerSideUpload.CreateVideoLibrary(LibraryTitle);
            ServerSideUpload.UploadVideo(LibraryTitle, VideotTitle + 1, VideoResource1);
            ServerSideUpload.UploadVideo(LibraryTitle, VideotTitle + 2, VideoResource2);
            ServerSideUpload.UploadVideo(LibraryTitle, VideotTitle + 3, VideoResource3);
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
        /// Verify Paging (1 item per page) and All Videos in Videos gallery widget
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void VideoGallery_VerifyPaging()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.DisplayMode = ListDisplayMode.Paging;
            videoGalleryController.Model.SortExpression = "Title ASC";
            videoGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videosPage1 = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videosPage1.Length.Equals(1), "Number of Videos is not correct");
            Assert.AreEqual(VideotTitle + 1, videosPage1[0].Fields.Title.Value, "Wrong title");

            var videosPage2 = videoGalleryController.Model.CreateListViewModel(null, 2).Items.ToArray();
            Assert.IsTrue(videosPage2.Length.Equals(1), "Number of Videos is not correct");
            Assert.AreEqual(VideotTitle + 2, videosPage2[0].Fields.Title.Value, "Wrong title");

            var videosPage3 = videoGalleryController.Model.CreateListViewModel(null, 3).Items.ToArray();
            Assert.IsTrue(videosPage3.Length.Equals(1), "Number of Videos is not correct");
            Assert.AreEqual(VideotTitle + 3, videosPage3[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Limit to 1 and All Videos in Videos gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void VideoGallery_VerifyLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.DisplayMode = ListDisplayMode.Limit;
            videoGalleryController.Model.SortExpression = "Title ASC";
            videoGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(1), "Number of Videos is not correct");
            Assert.AreEqual(VideotTitle + 1, videos[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify No limit and All Videos in Videos gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void VideoGallery_VerifyNoLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(VideoGalleryController).FullName;
            var videoGalleryController = new VideoGalleryController();
            videoGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            videoGalleryController.Model.DisplayMode = ListDisplayMode.All;
            videoGalleryController.Model.SortExpression = "Title ASC";
            videoGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(videoGalleryController);

            var videos = videoGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(videos.Length.Equals(3), "Number of Videos is not correct");
            Assert.AreEqual(VideotTitle + 1, videos[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideotTitle + 2, videos[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(VideotTitle + 3, videos[2].Fields.Title.Value, "Wrong title");
        }

        private const string LibraryTitle = "TestVideoLibrary";
        private const string VideotTitle = "Video";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
    }
}