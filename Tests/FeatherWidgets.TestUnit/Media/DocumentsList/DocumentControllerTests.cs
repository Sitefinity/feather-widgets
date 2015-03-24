using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;

namespace FeatherWidgets.TestUnit.Media.DocumentsList
{
    /// <summary>
    /// Tests for DocumentControllerTests
    /// </summary>
    [TestClass]
    public class DocumentsListControllerTests
    {
        [TestMethod]
        [Owner("Manev")]
        public void CreateDocumentsListController_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DummyDocumentsListController())
            {
                // Act
                var view = controller.Index(null) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewName == "List.DocumentsList");
                Assert.IsTrue(view.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(view.ViewBag.RedirectPageUrlTemplate == "/{0}");
                Assert.IsTrue(view.ViewBag.DetailsPageId == Guid.Empty);
                Assert.IsTrue(view.ViewBag.OpenInSamePage);
                Assert.IsTrue(view.ViewBag.ItemsPerPage == 20);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateDocumentsListController_CallTheIndexAction_EnsuresDefinedPropertiesAreSetCorrectly()
        {
            // Arrange
            using (var controller = new DummyDocumentsListController())
            {
                controller.DetailsPageId = new Guid("C8420FD7-2AD0-4D34-B8CD-C0636DE5AD09");
                controller.OpenInSamePage = false;
                controller.ListTemplateName = "ListTemplateName";

                // Act
                var view = controller.Index(null) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewName == "List.ListTemplateName");
                Assert.IsTrue(view.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(view.ViewBag.RedirectPageUrlTemplate == "/{0}");
                Assert.IsTrue(view.ViewBag.DetailsPageId == controller.DetailsPageId);
                Assert.IsFalse(view.ViewBag.OpenInSamePage);
                Assert.IsTrue(view.ViewBag.ItemsPerPage == 20);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateDocumentsListController_CallTheDetailsAction_EnsuresDefaultModelPropertiesArePresented()
        {
            var docGuid = new Guid("C8420FD7-2AD0-4D34-B8CD-C0636DE5AD09");

            // Arrange
            using (var controller = new DummyDocumentsListController())
            {
                // Act
                var doc = new DummyDocument("App", docGuid)
                {
                    Title = "Doc title"
                };

                var view = controller.Details(doc) as ViewResult;

                var viewModel = view.Model as ContentDetailsViewModel;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsNotNull(viewModel);
                Assert.IsNotNull(viewModel.ContentType is DummyDocument);
                Assert.IsNotNull(viewModel.Item.DataItem == doc);
                Assert.IsTrue(view.ViewBag.Title == doc.Title);
                Assert.IsTrue(view.ViewName == "Detail.DocumentDetails");
                Assert.IsTrue(view.ViewBag.DetailsPageId == Guid.Empty);
                Assert.IsTrue(view.ViewBag.OpenInSamePage);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateDocumentsListController_CallSuccessorsAction_EnsuresModelProperties()
        {
            var id = new Guid("C8420FD7-2AD0-4D34-B8CD-C0636DE5AD09");

            // Arrange
            using (var controller = new DummyDocumentsListController())
            {
                // Act
                var lib = new DummyLibrary { ItemDefaultUrl = "ItemDefaultUrl" };

                var view = controller.Successors(lib, null) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "List.DocumentsList");
                Assert.IsTrue(view.ViewBag.RedirectPageUrlTemplate == "ItemDefaultUrl?page={0}");
                Assert.IsTrue(view.ViewBag.DetailsPageId == Guid.Empty);
                Assert.IsTrue(view.ViewBag.OpenInSamePage);
            }
        }

        private class DummyLibrary : Library
        {
            public override Lstring ItemDefaultUrl { get; set; }
        }
    }
}
