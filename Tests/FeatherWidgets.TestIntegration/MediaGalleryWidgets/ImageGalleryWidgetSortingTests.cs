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
    /// This is a class with Image gallery tests for Sorting options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Image gallery sorting tests.")]
    public class ImageGalleryWidgetSortingTests
    {
        /// <summary>
        /// Create library with images
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ServerSideUpload.CreateAlbum(LibraryTitle);
            ServerSideUpload.UploadImage(LibraryTitle, ImageTitle + 1, ImageResource1);
            ServerSideUpload.UploadImage(LibraryTitle, ImageTitle + 3, ImageResource3);
            ServerSideUpload.UploadImage(LibraryTitle, ImageTitle + 2, ImageResource2);
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
        /// Verify Last published sorting and All Images in Image gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifySortingLastPublished()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.SortExpression = "PublicationDate DESC";       
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(3), "Number of images is not correct");

            //// expected: Image2, Image3, Image1
            Assert.AreEqual(ImageTitle + 2, images[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 3, images[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 1, images[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title A-Z sorting and All Images in Image gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifySortingTitleAZ()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.SortExpression = "Title ASC";
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(3), "Number of images is not correct");

            //// expected: Image1, Image2, Image3
            Assert.AreEqual(ImageTitle + 1, images[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 2, images[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 3, images[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title Z-A sorting and All Images in Image gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifySortingTitleZA()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.SortExpression = "Title DESC";
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(3), "Number of images is not correct");

            //// expected: Image3, Image2, Image1
            Assert.AreEqual(ImageTitle + 3, images[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 2, images[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 1, images[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Last modified sorting and All Images in Image gallery widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.FeatherTeam)]
        public void ImageGallery_VerifySortingLastModified()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ImageGalleryController).FullName;
            var imageGalleryController = new ImageGalleryController();
            imageGalleryController.Model.SelectionMode = SelectionMode.AllItems;
            imageGalleryController.Model.SortExpression = "LastModified DESC";
            mvcProxy.Settings = new ControllerSettings(imageGalleryController);

            this.ChangeModifiedDateOfImage3();

            var images = imageGalleryController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(images.Length.Equals(3), "Number of images is not correct");

            //// expected: Image31, Image2, Image1
            Assert.AreEqual(ImageTitle + 3 + 1, images[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 2, images[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(ImageTitle + 1, images[2].Fields.Title.Value, "Wrong title");
        }
 
        private void ChangeModifiedDateOfImage3()
        {
            var librariesManager = LibrariesManager.GetManager();
            Image modified = librariesManager.GetImages().Where<Image>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == ImageTitle + 3).FirstOrDefault();
            Image temp = librariesManager.Lifecycle.CheckOut(modified) as Image;

            temp.Title = ImageTitle + 3 + 1;

            modified = librariesManager.Lifecycle.CheckIn(temp) as Image;

            var image = librariesManager.GetImage(modified.Id);
            librariesManager.Lifecycle.Publish(image);
            librariesManager.SaveChanges();
        }

        private const string LibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}