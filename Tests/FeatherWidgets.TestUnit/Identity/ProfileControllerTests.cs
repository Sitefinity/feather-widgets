using System.Web.Mvc;

using FeatherWidgets.TestUnit.DummyClasses.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
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
    }
}
