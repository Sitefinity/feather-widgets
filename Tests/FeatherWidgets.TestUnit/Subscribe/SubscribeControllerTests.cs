using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;
using Telerik.Sitefinity.Services;

namespace FeatherWidgets.TestUnit.Media.DocumentsList
{
    /// <summary>
    /// Tests for SubscribeControllerTests
    /// </summary>
    [TestClass]
    public class SubscribeControllerTests
    {
        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "SubscribeForm.SubscribeForm");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.Model is SubscribeFormViewModel);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallTheIndexAction_EnsuresViewNameIsChanged()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                controller.TemplateName = "CustomName";

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "SubscribeForm.CustomName");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.Model is SubscribeFormViewModel);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallTheIndexAction_EnsuresMessageWhenNoLicense()
        {
            var httpContext = new MyDummyHttpContext { ApplicationInstance = new System.Web.HttpApplication() };

            httpContext.Items[SystemManager.PageDesignModeKey] = true;

            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummySubscribeFormController(isLicensed: false))
                              {
                                  // Act
                                  var view = controller.Index() as ContentResult;

                                  // Assert
                                  Assert.IsNotNull(view);
                                  Assert.IsTrue(view.Content == "No valid license");
                              }
                          });
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallTheIndexAction_EnsuresMessageWhenModuleIsDeactivated()
        {
            var httpContext = new MyDummyHttpContext { ApplicationInstance = new System.Web.HttpApplication() };

            httpContext.Items[SystemManager.PageDesignModeKey] = true;

            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummySubscribeFormController(isModuleActivated: false))
                              {
                                  // Act
                                  var view = controller.Index() as ContentResult;

                                  // Assert
                                  Assert.IsNotNull(view);
                                  Assert.IsTrue(view.Content == "NewslettersModuleDeactivatedMessage");
                              }
                          });
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallTheIndexAction_EnsuresIsEmpty()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                controller.MockModel = new DummySubscribeFormModel();

                // Act
                var view = controller.Index();

                // Assert
                Assert.IsNull(view);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallThePostIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                // Act
                var view = controller.Index(new SubscribeFormViewModel { Email = "my email" }) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "SubscribeForm.SubscribeForm");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewBag.Error is string);
                Assert.IsTrue(view.ViewBag.IsSucceeded);
                Assert.IsTrue(view.ViewBag.Email == "my email");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallThePostIndexAction_EnsuresRedirectIsCalled()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                controller.MockModel = new DummySubscribeFormModel { SuccessfullySubmittedForm = SuccessfullySubmittedForm.OpenSpecificPage };

                // Act
                var view = controller.Index(new SubscribeFormViewModel
                {
                    Email = "my email",
                    RedirectPageUrl = "www.sitefinity.com"
                }) as RedirectResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.Url == "www.sitefinity.com");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallThePostIndexAction_EnsuresViewNameIsChanged()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                controller.TemplateName = "CustomName";

                // Act
                var view = controller.Index(new SubscribeFormViewModel()) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "SubscribeForm.CustomName");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.Model is SubscribeFormViewModel);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallThePostIndexAction_EnsuresMessageWhenNoLicense()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController(isLicensed: false))
            {
                // Act
                var view = controller.Index(new SubscribeFormViewModel()) as ContentResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.Content == "No valid license");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateSubscribeController_CallThePostIndexAction_EnsuresMessageWithInvalidState()
        {
            // Arrange
            using (var controller = new DummySubscribeFormController())
            {
                controller.ModelState.AddModelError("Email", "Email is empty");

                // Act
                var view = controller.Index(new SubscribeFormViewModel()) as ViewResult;

                // Assert
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewBag.Error == null);
                Assert.IsTrue(view.ViewBag.IsSucceeded == null);
                Assert.IsTrue(view.ViewBag.Email == null);
            }
        }

        private class MyDummyHttpContext : DummyHttpContext
        {
            public override HttpApplication ApplicationInstance { get; set; }
        }
    }
}
