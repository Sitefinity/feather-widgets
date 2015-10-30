using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class ensures dropdown list field functionalities work correctly.
    /// </summary>
    [TestFixture]
    [Description("This class ensures dropdown list field functionalities work correctly.")]
    public class DropdownListFieldTests
    {
        /// <summary>
        /// Ensures that when a dropdown list field widget is added to form the default value is presented in the page markup.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a dropdown list field widget is added to form the default value is presented in the page markup.")]
        public void DropdownListFieldTests_EditDefaultValue_MarkupIsCorrect()
        {
            var controller = new DropdownListFieldController();

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(DropdownListFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "DropdownListFieldSubmitValueTest", "dropdown-list-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);
                Assert.IsTrue(pageContent.Contains(Res.Get<FieldResources>().OptionSelect), "Form did not render the select default choice in the dropdown list field.");
                Assert.IsTrue(pageContent.Contains(Res.Get<FieldResources>().OptionFirstChoice), "Form did not render the first default choice in the dropdown list field.");
                Assert.IsTrue(pageContent.Contains(Res.Get<FieldResources>().OptionSecondChoice), "Form did not render the second default choice in the dropdown list field.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a dropdown list field widget is submitted with certain value then the response is correct.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a dropdown list field widget is submitted with certain value then the response is correct.")]
        public void DropdownListFieldTests_SubmitValue_ResponseIsCorrect()
        {
            var submitedDropdownValue = Res.Get<FieldResources>().OptionFirstChoice;

            var controller = new DropdownListFieldController();

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(DropdownListFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "Bootstrap.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "DropdownListFieldValueTest", "dropdown-list-field-submit-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                
                var dropdownListFieldName = ServerOperationsFeather.Forms().GetFirstFieldName(formManager, form);
                ServerOperationsFeather.Forms().SubmitField(dropdownListFieldName, submitedDropdownValue, pageManager, pageId);

                var formEntry = formManager.GetFormEntries(form).LastOrDefault();
                Assert.IsNotNull(formEntry, "Form entry has not been submitted.");

                var submittedValue = formEntry.GetValue(dropdownListFieldName) as string;
                Assert.AreEqual(submitedDropdownValue, submittedValue, "Form did not persisted the submitted dropdown list value correctly.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a dropdown list field widget with URL type is submitted with incorrect value then the validation fails.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a dropdown list field widget with URL type is submitted with incorrect value then the validation fails.")]
        public void DropdownListFieldTests_SubmitIncorrectValue_ServerValidationFails()
        {
            var controller = new DropdownListFieldController();
            controller.Model.ValidatorDefinition.Required = true;

            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(DropdownListFieldController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "DropdownListFieldValidationTest", "dropdown-list-field-validation-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var dropdownListFieldName = ServerOperationsFeather.Forms().GetFirstFieldName(formManager, form);
                var result = ServerOperationsFeather.Forms().SubmitField(dropdownListFieldName, string.Empty, pageManager, pageId);
                var contentResult = result as ContentResult;
                Assert.IsNotNull(contentResult, "Submit should return content result.");
                Assert.AreEqual(Res.Get<FormResources>().UnsuccessfullySubmittedMessage, contentResult.Content, "The Submit didn't result in error as expected!");

                var formEntry = formManager.GetFormEntries(form).LastOrDefault();
                Assert.IsNull(formEntry, "Form entry has been submitted even when the form is not valid.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
