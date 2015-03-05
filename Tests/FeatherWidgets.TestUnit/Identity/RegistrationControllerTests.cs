using System;
using System.Web.Mvc;
using System.Web.Security;

using FeatherWidgets.TestUnit.DummyClasses.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;

namespace FeatherWidgets.TestUnit.Identity
{
    /// <summary>
    /// Unit tests for the Registration controller.
    /// </summary>
    [TestClass]
    public class RegistrationControllerTests
    {
        [TestMethod]
        [Description("Invokes the Index action and ispects whether the view result is constructed correctly.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexAction_ReturnsValidViewResult()
        {
            var controller = new DummyRegistrationController();
            controller.TemplateName = "TestTemplate";

            var result = controller.Index();

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Registration.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(RegistrationViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with invalid POST data will return the configured view with the same model.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_InvalidModel_ReturnsConfiguredViewWithSameModel()
        {
            var controller = new DummyRegistrationController();
            controller.TemplateName = "TestTemplate";
            var viewModel = new RegistrationViewModel();
            controller.ModelState.AddModelError("TestError", "Test error message");

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Registration.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(RegistrationViewModel), "The Index action did not assign a view model of the expected type.");
            Assert.AreSame(viewModel, viewResult.Model, "The model in the result is not the same as the one passed to the action.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with POST data will return a success message on valid model and ShowMessage success action.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_ValidModelShowMessageAction_ShowsSuccessMessage()
        {
            var controller = new DummyRegistrationController();
            controller.TemplateName = "TestTemplate";
            var viewModel = new RegistrationViewModel();
            controller.Model.SuccessfulRegistrationMsg = "Registered successfully!";
            controller.Model.SuccessfulRegistrationAction = SuccessfulRegistrationAction.ShowMessage;

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ContentResult), "The action result is not of the expected type.");

            var contentResult = (ContentResult)result;
            Assert.AreEqual(controller.Model.SuccessfulRegistrationMsg, contentResult.Content, "The result did not contain the expected message.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with POST data will redirect on valid model and Redirect success action.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_ValidModelRedirectAction_Redirects()
        {
            var controller = new DummyRegistrationController();
            controller.TemplateName = "TestTemplate";
            var viewModel = new RegistrationViewModel();
            controller.Model.SuccessfulRegistrationPageId = new Guid("3bf29da0-1074-4a71-bb23-7ae43f36d8f9");
            controller.Model.SuccessfulRegistrationAction = SuccessfulRegistrationAction.RedirectToPage;

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(RedirectResult), "The action result is not of the expected type.");

            var redirectResult = (RedirectResult)result;
            Assert.AreEqual("http://3bf29da0-1074-4a71-bb23-7ae43f36d8f9", redirectResult.Url, true, "The action did not redirect to the expected URL.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with valid POST data but fails registration will return the configured view with the same model and set error message in ViewBag.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_ValidModelFailedRegistration_ReturnsConfiguredViewWithSameModelAndErrorMessage()
        {
            var controller = new DummyRegistrationController();
            controller.TemplateName = "TestTemplate";
            var viewModel = new RegistrationViewModel();

            // "Fail" is a magical UserName that tells the Dummy Model to fail the registration.
            viewModel.UserName = "Fail";

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            Assert.AreEqual(MembershipCreateStatus.InvalidUserName.ToString(), controller.ViewBag.Error, "ViewBag did not containg the expected error message.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Registration.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(RegistrationViewModel), "The Index action did not assign a view model of the expected type.");
            Assert.AreSame(viewModel, viewResult.Model, "The model in the result is not the same as the one passed to the action.");
        }
    }
}
