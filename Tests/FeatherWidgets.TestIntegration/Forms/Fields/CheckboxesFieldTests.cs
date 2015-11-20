using System;
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
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class ensures checkboxes field functionalities work correctly.
    /// </summary>
    [TestFixture]
    [Description("This class ensures checkboxes field functionalities work correctly.")]
    public class CheckboxesFieldTests
    {
        /// <summary>
        /// Ensures that when a checkboxes field is added to form the choices are presented in the page markup.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Multilingual(MultilingualExecutionMode.MultiAndMonoLingual)]
        [Description("Ensures that when a checkboxes field is added to form the choices are presented in the page markup.")]
        public void CheckboxesFieldTests_EditDefaultChoices_MarkupIsCorrect()
        {
            var choice1 = "Choice1";
            var choice2 = "Choice2";
            var choice3 = "Choice3";

            var controller = new CheckboxesFieldController();
            controller.Model.SerializedChoices = "[\"{0}\",\"{1}\",\"{2}\"]".Arrange(choice1, choice2, choice3);
            controller.Model.HasOtherChoice = true;

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(CheckboxesFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "CheckboxesFieldDefaultValueTest", "checkboxes-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);
                Assert.IsTrue(pageContent.Contains(choice1), "Form did not render the first choice in the checkboxes field.");
                Assert.IsTrue(pageContent.Contains(choice2), "Form did not render the second choice in the checkboxes field.");
                Assert.IsTrue(pageContent.Contains(choice3), "Form did not render the third choice in the checkboxes field.");
                Assert.IsTrue(pageContent.Contains(Res.Get<FieldResources>().Other), "Form did not render the 'Other' choice in the checkboxes field.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Multilingual(MultilingualExecutionMode.MultiAndMonoLingual)]
        [Description("Ensures that when a checkboxes field widget is submitted with certain value then the response is correct.")]
        public void CheckboxesFieldTests_SubmitValue_ResponseIsCorrect()
        {
            var submitedCheckboxesValue = Res.Get<FieldResources>().OptionSecondChoice;

            var controller = new CheckboxesFieldController();

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(CheckboxesFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "CheckboxesListFieldValueTest", "checkboxes-field-submit-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                
                var checkboxesFieldName = ServerOperationsFeather.Forms().GetFirstFieldName(formManager, form);
                ServerOperationsFeather.Forms().SubmitField(checkboxesFieldName, submitedCheckboxesValue, pageManager, pageId);

                var formEntry = formManager.GetFormEntries(form).LastOrDefault();
                Assert.IsNotNull(formEntry, "Form entry has not been submitted.");

                var submittedValue = formEntry.GetValue(checkboxesFieldName) as string;
                Assert.AreEqual(submitedCheckboxesValue, submittedValue, "Form did not persisted the submitted checkboxes value correctly.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Multilingual(MultilingualExecutionMode.MultiAndMonoLingual)]
        [Description("Ensures that when a checkboxes field is submitted with empty value then the validation fails.")]
        public void CheckboxesFieldTests_SubmitIncorrectValue_ServerValidationFails()
        {
            var controller = new CheckboxesFieldController();
            controller.Model.ValidatorDefinition.Required = true;

            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(CheckboxesFieldController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "CheckboxesFieldValidationTest", "checkboxes-field-validation-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                
                var checkboxesFieldName = ServerOperationsFeather.Forms().GetFirstFieldName(formManager, form);
                var result = ServerOperationsFeather.Forms().SubmitField(checkboxesFieldName, string.Empty, pageManager, pageId);
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
