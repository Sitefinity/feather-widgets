using System.Collections.Generic;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.ContentLocations;

namespace FeatherWidgets.TestUnit.Media.ImageGallery
{
    /// <summary>
    /// Tests for the ImageGalleryController.
    /// </summary>
    [TestClass]
    public class ImageGalleryControllerTests
    {
        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether Index action returns a view result for the configured template.")]
        public void Index_ReturnsConfiguredView()
        {
            var model = new DummyImageGalleryModel();
            model.ItemsPerPage = 20;

            using (var controller = new DummyImageGalleryController(model))
            {
                controller.ListTemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.Index(null);

                Assert.AreEqual("List.MyTestTemplate", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether Successors action returns a view result for the configured template.")]
        public void Successors_ReturnsConfiguredView()
        {
            var model = new DummyImageGalleryModel();

            using (var controller = new DummyImageGalleryController(model))
            {
                controller.ListTemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.Successors(null, null);

                Assert.AreEqual("List.MyTestTemplate", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether ListByTaxon action returns a view result for the configured template.")]
        public void ListByTaxon_ReturnsConfiguredView()
        {
            var model = new DummyImageGalleryModel();

            using (var controller = new DummyImageGalleryController(model))
            {
                controller.ListTemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.ListByTaxon(null, null);

                Assert.AreEqual("List.MyTestTemplate", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether Details action returns a view result for the configured template.")]
        public void Details_ReturnsConfiguredView()
        {
            var model = new DummyImageGalleryModel();

            using (var controller = new DummyImageGalleryController(model))
            {
                controller.DetailTemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.Details(null);

                Assert.AreEqual("Detail.MyTestTemplate", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("Tests whether GetLocations returns the location items given by the model.")]
        public void GetLocations_ReturnsModelLocations()
        {
            var expectedLocations = new List<IContentLocationInfo>();
            var model = new DummyImageGalleryModel();
            model.DummyLocations = expectedLocations;

            using (var controller = new DummyImageGalleryController(model))
            {
                var result = controller.GetLocations();

                Assert.AreSame(expectedLocations, result);
            }
        }
    }
}
