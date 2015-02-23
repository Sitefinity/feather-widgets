using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace FeatherWidgets.TestUnit.Media.Image
{
    /// <summary>
    /// Tests methods for the ImageControllerTests
    /// </summary>
    [TestClass]
    public class ImageControllerTests
    {
        [TestMethod]
        [Owner("Manev")]
        public void CreateImage_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new ImageController())
            {
                // Act
                var view = controller.Index() as EmptyResult;

                // Assert
                Assert.IsNotNull(view);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateImage_CallIndexAction_EnsuresImageWasNotSelectedOrHasBeenDeletedMessageDisplayed()
        {
            var testModel = new DummyImageModel(null)
            {
                Id = new Guid("D4110267-C59C-4816-A080-64F59D9425DC"),
            };

            // Arrange
            using (var controller = new DummyImageController(testModel))
            {
                // Act
                var view = controller.Index() as ContentResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.Content == "ImageWasNotSelectedOrHasBeenDeletedMessage");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateImageControllerWithProperties_CallTheIndexAction_EnsuresViewPropertiesArePresented()
        {
            var image = new SfImage("App", new Guid("D4110267-C59C-4816-A080-64F59D9425DC"));

            var testModel = new DummyImageModel(image)
            {
                AlternativeText = "AlternativeText",
                CssClass = "CssClass",
                CustomSize = "{'MaxWidth':11,'MaxHeight':11,'Width':null,'Height':null,'ScaleUp':true,'Quality':'Medium','Method':'ResizeFitToAreaArguments'}",
                DisplayMode = ImageDisplayMode.Thumbnail,
                Id = new Guid("D4110267-C59C-4816-A080-64F59D9425DC"),
                LinkedPageId = Guid.Empty,
                ProviderName = "OpenAccessDefaultProvider",
                ThumbnailName = "ThumbnailName",
                ThumbnailUrl = "ThumbnailUrl",
                Title = "Title",
                UseAsLink = false
            };

            // Arrange
            using (var controller = new DummyImageController(testModel))
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view.Model);
                var imageModel = view.Model as ImageViewModel;
                Assert.IsNotNull(imageModel);
                Assert.IsTrue(imageModel.AlternativeText == testModel.AlternativeText);
                Assert.IsTrue(imageModel.CssClass == testModel.CssClass);
                Assert.IsTrue(imageModel.CustomSize.Equals(new JavaScriptSerializer().Deserialize<ImageViewModel.CustomSizeModel>(testModel.CustomSize)));
                Assert.IsTrue(imageModel.DisplayMode == testModel.DisplayMode);
                Assert.IsTrue(imageModel.ThumbnailName == testModel.ThumbnailName);
                Assert.IsTrue(imageModel.ThumbnailUrl == testModel.ThumbnailUrl);
                Assert.IsTrue(imageModel.Title == testModel.Title);
            }
        }
        
        [TestMethod]
        [Owner("Manev")]
        public void CreateImageControllerWithPropertiesAndImageItemPresented()
        {
            var image = new SfImage("App", new Guid("D4110267-C59C-4816-A080-64F59D9425DC"));

            var testModel = new DummyImageModel(image)
            {
                DisplayMode = ImageDisplayMode.Thumbnail,
                Id = new Guid("D4110267-C59C-4816-A080-64F59D9425DC"),
                UseAsLink = false
            };

            // Arrange
            using (var controller = new DummyImageController(testModel))
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view.Model);
                var imageModel = view.Model as ImageViewModel;

                Assert.IsNotNull(imageModel.Item);
                Assert.IsTrue(imageModel.SelectedSizeUrl == "GetSelectedSizeUrl");
                Assert.IsTrue(imageModel.Item.DataItem.Id == image.Id);
                Assert.IsTrue(imageModel.Item.DataItem.ApplicationName == image.ApplicationName);
            }
        }
    }
}
