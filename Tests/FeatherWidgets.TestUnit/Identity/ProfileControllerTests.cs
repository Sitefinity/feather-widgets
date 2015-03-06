using System;
using System.Web.Mvc;

using FeatherWidgets.TestUnit.DummyClasses.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;

namespace FeatherWidgets.TestUnit.Identity
{
    /// <summary>
    /// Tests the profile controller.
    /// </summary>
    [TestClass]
    public class ProfileControllerTests
    {
        [TestMethod]
        [Description("Tests whether Index action returns the Read view when the mode is ReadOnly.")]
        [Owner("Boyko-Karadzhov")]
        public void Index_ReadOnlyMode_ReturnsReadView()
        {
            var controller = new DummyProfileController();
            controller.ReadModeTemplateName = "TestTemplate";
            controller.Mode = ViewMode.ReadOnly;

            var result = controller.Index();

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Read.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfilePreviewViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether Index action returns the Edit view when the mode is EditOnly.")]
        [Owner("Boyko-Karadzhov")]
        public void Index_EditOnlyMode_ReturnsEditView()
        {
            var controller = new DummyProfileController();
            controller.EditModeTemplateName = "TestTemplate";
            controller.Mode = ViewMode.EditOnly;

            var result = controller.Index();

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Edit.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfileEditViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether Index action returns the Read view when the mode is Both.")]
        [Owner("Boyko-Karadzhov")]
        public void Index_BothMode_ReturnsReadView()
        {
            var controller = new DummyProfileController();
            controller.ReadModeTemplateName = "TestTemplate";
            controller.Mode = ViewMode.Both;

            var result = controller.Index();

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Read.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfilePreviewViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether EditPRofile action returns the Edit view.")]
        [Owner("Boyko-Karadzhov")]
        public void EditProfile_ReturnsEditView()
        {
            var controller = new DummyProfileController();
            controller.EditModeTemplateName = "TestTemplate";

            var result = controller.EditProfile();

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Edit.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfileEditViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with invalid POST data will return the edit view with the same model.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_InvalidModel_ReturnsEditViewWithSameModel()
        {
            var controller = new DummyProfileController();
            controller.EditModeTemplateName = "TestTemplate";
            var viewModel = new ProfileEditViewModel();
            controller.ModelState.AddModelError("TestError", "The model is invalid");

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Edit.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfileEditViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with POST data will return the read view when Save Action is SwitchToReadMode.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_SwitchToReadMode_ReturnsReadView()
        {
            var controller = new DummyProfileController();
            controller.ReadModeTemplateName = "TestTemplate";
            controller.Model.SaveChangesAction = SaveAction.SwitchToReadMode;
            var viewModel = new ProfileEditViewModel();

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            Assert.AreEqual("Read.TestTemplate", viewResult.ViewName, "The Index did not return the configured view according to its convention.");
            Assert.IsNotNull(viewResult.Model, "The Index action did not assign a view model.");
            Assert.IsInstanceOfType(viewResult.Model, typeof(ProfilePreviewViewModel), "The Index action did not assign a view model of the expected type.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with POST data will return a message when Save Action is ShowMessage.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_ShowMessage_ReturnsMessage()
        {
            var controller = new DummyProfileController();
            controller.Model.SaveChangesAction = SaveAction.ShowMessage;
            var viewModel = new ProfileEditViewModel();

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(ContentResult), "The action result is not of the expected type.");

            var viewResult = (ViewResult)result;
            var model = viewResult.Model as ProfileEditViewModel;
            Assert.IsNotNull(model, "The action result should return view wiht ProfileEditViewModel.");
            Assert.AreEqual(true, model.ShowProfileChangedMsg, "The action did not return the expected success message.");
        }

        [TestMethod]
        [Description("Tests whether the Index action that is invoked with POST data will redirect when Save Action is ShowPage.")]
        [Owner("Boyko-Karadzhov")]
        public void IndexPostAction_ShowPage_Redirects()
        {
            var controller = new DummyProfileController();
            controller.Model.SaveChangesAction = SaveAction.ShowPage;
            controller.Model.ProfileSavedPageId = new Guid("3bf29da0-1074-4a71-bb23-7ae43f36d8f9");
            var viewModel = new ProfileEditViewModel();

            var result = controller.Index(viewModel);

            Assert.IsNotNull(result, "The action result is null.");
            Assert.IsInstanceOfType(result, typeof(RedirectResult), "The action result is not of the expected type.");

            var redirectResult = (RedirectResult)result;
            Assert.AreEqual("http://3bf29da0-1074-4a71-bb23-7ae43f36d8f9", redirectResult.Url, "The action did not return the expected message.");
        }
    }
}
