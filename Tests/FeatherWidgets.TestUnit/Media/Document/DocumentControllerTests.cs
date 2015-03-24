using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Media.Document;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace FeatherWidgets.TestUnit.Media.Document
{
    /// <summary>
    /// Tests for DocumentController
    /// </summary>
    [TestClass]
    public class DocumentControllerTests
    {
        [TestMethod]
        [Owner("GeorgiMateev")]
        public void CreateDocumentController_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DocumentController())
            {
                // Act
                var view = controller.Index() as EmptyResult;

                // Assert
                Assert.IsNotNull(view);
            }
        }

        [TestMethod]
        [Owner("GeorgiMateev")]
        public void CreateDocument_CallIndexAction_EnsuresImageWasNotSelectedOrHasBeenDeletedMessageDisplayed()
        {
            var testModel = new DummyDocumentModel(null)
            {
                Id = new Guid("D4110267-C59C-4816-A080-64F59D9425DC"),
            };

            // Arrange
            using (var controller = new DummyDocumentController(testModel))
            {
                // Act
                var view = controller.Index() as ContentResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.Content == "DocumentWasNotSelectedOrHasBeenDeletedMessage");
            }
        }

        [TestMethod]
        [Owner("GeorgiMateev")]
        public void CreateDocumentControllerWithProperties_CallTheIndexAction_EnsuresViewPropertiesArePresented()
        {
            var doc = new SfDocument("App", new Guid("D4110267-C59C-4816-A080-64F59D9425DC"));

            var testModel = new DummyDocumentModel(doc)
            {
                CssClass = "CssClass",
                Id = new Guid("D4110267-C59C-4816-A080-64F59D9425DC"),
                ProviderName = "OpenAccessDefaultProvider"                
            };

            // Arrange
            using (var controller = new DummyDocumentController(testModel))
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view.Model);
                var docVM = view.Model as DocumentViewModel;
                Assert.IsNotNull(docVM);
                Assert.IsTrue(docVM.CssClass == testModel.CssClass);
                Assert.IsTrue(docVM.Title == "title");
                Assert.IsTrue(docVM.FileSize == 1);
                Assert.IsTrue(docVM.Extension == "pdf");
                Assert.IsTrue(docVM.MediaUrl == "http://mysite.com/file.doc");
            }
        }
    }
}
