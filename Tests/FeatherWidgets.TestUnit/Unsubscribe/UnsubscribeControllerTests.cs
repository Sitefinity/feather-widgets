using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;
using Telerik.Sitefinity.Services;

namespace FeatherWidgets.TestUnit.Media.DocumentsList
{
    /// <summary>
    /// Tests for UnsubscribeControllerTests
    /// </summary>
    [TestClass]
    public class UnsubscribeControllerTests
    {
        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallThePostIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DummyUnsubscribeFormController())
            {
                // Act
                var view = controller.Index(new UnsubscribeFormViewModel { }) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "UnsubscribeFormByEmailAddress.UnsubscribeForm");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewBag.Error is string);
                Assert.IsTrue(view.ViewBag.IsSucceded);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUnit.DummyClasses.Media.DummyUnsubscribeFormModel.#ctor(System.String,System.String,System.String)"), TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallThePostIndexAction_EnsuresRedirectIsCalled()
        {
            // Arrange
            using (var controller = new DummyUnsubscribeFormController())
            {
                controller.MockModel = new DummyUnsubscribeFormModel
                {
                    SuccessfullySubmittedForm = SuccessfullySubmittedForm.OpenSpecificPage,
                    ListId = new Guid("51C6563A-C165-40B0-B950-E3F99CF1ED98")
                };

                // Act
                var view = controller.Index(new UnsubscribeFormViewModel
                {
                    RedirectPageUrl = "www.sitefinity.com"
                }) as RedirectResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.Url == "www.sitefinity.com");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallThePostIndexAction_EnsuresViewNameIsChanged()
        {
            // Arrange
            using (var controller = new DummyUnsubscribeFormController())
            {
                controller.EmailAddressTemplateName = "CustomName";

                // Act
                var view = controller.Index(new UnsubscribeFormViewModel()) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "UnsubscribeFormByEmailAddress.CustomName");
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.Model is UnsubscribeFormViewModel);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallTheIndexAction_EnsuresEmptyResult_NoLicense_NotEdit()
        {
            // Arrange
            using (var controller = new DummyUnsubscribeFormController(isLicensed: false))
            {
                // Act
                var view = controller.Index() as EmptyResult;

                // Assert
                Assert.IsNotNull(view);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallThePostIndexAction_EnsuresMessageWithInvalidState()
        {
            // Arrange
            using (var controller = new DummyUnsubscribeFormController())
            {
                controller.ModelState.AddModelError("Email", "Email is empty");

                // Act
                var view = controller.Index(new UnsubscribeFormViewModel()) as ViewResult;

                // Assert
                Assert.IsNotNull(view.Model);
                Assert.IsTrue(view.ViewBag.Error == null);
                Assert.IsTrue(view.ViewBag.IsSucceded == null);
                Assert.IsTrue(view.ViewBag.Email == null);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallThePostIndexAction_EnsuresCorrectMessage_NoLicense()
        {
            var httpContext = new DummyHttpContext();
            httpContext.Items[SystemManager.PageDesignModeKey] = true;
            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummyUnsubscribeFormController(isLicensed: false))
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
        public void CreateUnsubscribeController_CallTheIndexAction_EnsuresViewNameIsChanged()
        {
            var httpContext = new DummyHttpContext();
            httpContext.Items[SystemManager.PageDesignModeKey] = true;
            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummyUnsubscribeFormController())
                              {
                                  controller.LinkTemplateName = "CustomName";

                                  // Act
                                  var view = controller.Index() as ViewResult;

                                  // Assert
                                  Assert.IsNotNull(view);
                                  Assert.IsTrue(view.ViewName == "UnsubscribeFormByLink.CustomName");
                                  Assert.IsNotNull(view.Model);
                                  Assert.IsTrue(view.Model is UnsubscribeFormViewModel);
                              }
                          });
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallTheIndexAction_EnsuresMessageIsDisplayedWhenModuleIsNotActive()
        {
            var httpContext = new MyDummyHttpContext(new Page());
            httpContext.Items[SystemManager.PageDesignModeKey] = true;
            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummyUnsubscribeFormController(isModuleActivated: false))
                              {
                                  // Act
                                  var view = controller.Index() as ContentResult;

                                  // Assert
                                  Assert.IsNotNull(view);
                                  Assert.IsTrue(view.Content == "Not installed module");
                              }
                          });
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateUnsubscribeController_CallIndexAction_EnsuresMessageWithNoLicensed()
        {
            var httpContext = new DummyHttpContext();
            httpContext.Items[SystemManager.PageDesignModeKey] = true;
            SystemManager.RunWithHttpContext(
                          httpContext,
                          () =>
                          {
                              // Arrange
                              using (var controller = new DummyUnsubscribeFormController(isLicensed: false))
                              {
                                  // Act
                                  var view = controller.Index() as ContentResult;

                                  // Assert
                                  Assert.IsTrue(view.Content == "No valid license");
                              }
                          });
        }

        private class MyDummyHttpContext : DummyHttpContext
        {
            private readonly IHttpHandler handler;

            public MyDummyHttpContext(IHttpHandler handler)
            {
                this.handler = handler;
            }

            public override IHttpHandler CurrentHandler
            {
                get
                {
                    return this.handler;
                }
            }
        }
    }
}
