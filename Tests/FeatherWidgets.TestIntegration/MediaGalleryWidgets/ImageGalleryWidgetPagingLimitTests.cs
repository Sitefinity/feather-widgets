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
    /// This is a class with images gallery tests for paging and limit options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with images gallery paging and limit tests.")]
    public class ImageGalleryWidgetPagingLimitTests
    {
        /// <summary>
        /// Create library with images
        /// </summary>
        [SetUp]
        public void Setup()
        {     
            ServerSideUpload.CreateAlbum(LibraryTitle);
            ServerSideUpload.UploadImage(LibraryTitle, ImagetTitle + 1, ImageResource1);
            ServerSideUpload.UploadImage(LibraryTitle, ImagetTitle + 2, ImageResource2);
            ServerSideUpload.UploadImage(LibraryTitle, ImagetTitle + 3, ImageResource3);
        }

        /// <summary>
        /// Delete library
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            ServerOperations.Libraries().DeleteLibraries(false, "Image");
        }

        /// <summary>
        /// Verify Paging (1 item per page) and All images in images gallery widget
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifyPaging()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.DisplayMode = ListDisplayMode.Paging;
            imageGalleryController.Model.SortExpression = "Title ASC";
            imageGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var imagesPage1 = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(imagesPage1.Length.Equals(1), "Number of images is not correct");
            Assert.AreEqual(ImagetTitle + 1, imagesPage1[0].Fields.Title.Value, "Wrong title");

            var imagesPage2 = imageGalleryController.Model.CreateListViewModel(null, 2).Items.ToArray();
            Assert.IsTrue(imagesPage2.Length.Equals(1), "Number of images is not correct");
            Assert.AreEqual(ImagetTitle + 2, imagesPage2[0].Fields.Title.Value, "Wrong title");

            var imagesPage3 = imageGalleryController.Model.CreateListViewModel(null, 3).Items.ToArray();
            Assert.IsTrue(imagesPage3.Length.Equals(1), "Number of images is not correct");
            Assert.AreEqual(ImagetTitle + 3, imagesPage3[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Limit to 1 and All images in images gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifyLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.DisplayMode = ListDisplayMode.Limit;
            imageGalleryController.Model.SortExpression = "Title ASC";
            imageGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(1), "Number of images is not correct");
            Assert.AreEqual(ImagetTitle + 1, images[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify No limit and All images in images gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifyNoLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.DisplayMode = ListDisplayMode.All;
            imageGalleryController.Model.SortExpression = "Title ASC";
            imageGalleryController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(3), "Number of images is not correct");
            Assert.AreEqual(ImagetTitle + 1, images[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImagetTitle + 2, images[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImagetTitle + 3, images[2].Fields.Title.Value, "Wrong title");
        }

        private const string LibraryTitle = "TestimageLibrary";
        private const string ImagetTitle = "Image";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}