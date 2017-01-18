using System.Web.Mvc;
using FeatherWidgets.TestUnit.Forms.FormControllerTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUnit.Forms.FormControllerTests
{
    /// <summary>
    /// Tests methods for the Content Block
    /// </summary>
    [TestClass]
    public class FormControllerTests
    {
        [TestMethod]
        [Owner(FeatherTeams.SitefinityTeam3)]
        public void PostIndexAction_OnSuccessFormSubmit_SelectsProperView()
        {
            using (var controller = new FormControllerStub())
            {
                // Arrange
                var model = new FormModelStub();
                model.NeedsRedirect = false;
                model.RaiseBeforeFormActionEventValue = true;
                model.SubmitResultStatus = SubmitStatus.Success;
                controller.Model = model;

                // Act
                var view = controller.Index(new object() as FormCollection) as ViewResult;

                // Asserts
                var expectedViewName = FormController.TemplateNamePrefix + FormController.SubmitResultTemplateName;
                Assert.AreEqual(expectedViewName, view.ViewName);
            }
        }
    }
}