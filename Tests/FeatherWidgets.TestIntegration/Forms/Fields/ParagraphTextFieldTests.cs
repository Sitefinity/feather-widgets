using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class contains ensures ParagraphTextField functionalities work correctly.
    /// </summary>
    [TestFixture]
    public class ParagraphTextFieldTests
    {
        /// <summary>
        /// Ensures that when a paragraph text field widget is added to form the default value is presented in the page markup.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a paragraph text field widget is added to form the default value is presented in the page markup.")]
        public void ParagraphTextFieldTests_EditDefaultValue_MarkupIsCorrect()
        {
            const string DefaultText = "My default text";

            var controller = new ParagraphTextFieldController();
            controller.MetaField.DefaultValue = DefaultText;

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(ParagraphTextFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "ParagraphTextFieldSubmitValueTest", "paragraph-text-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);
                Assert.IsTrue(pageContent.Contains(DefaultText), "Form did not render the default text in the paragraph text field.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a paragraph text field widget is submitted with certain value then the response is correct.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a paragraph text field widget is submitted with certain value then the response is correct.")]
        public void ParagraphTextField_SubmitValue_ResponseIsCorrect()
        {
            const string SubmitedParagraphValue = "Submitted paragraph value";

            var controller = new ParagraphTextFieldController();

            var control = new MvcWidgetProxy();
            control.Settings = new ControllerSettings(controller);
            control.ControllerName = typeof(ParagraphTextFieldController).FullName;

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "ParagraphTextFieldValueTest", "paragraph-text-field-submit-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                var pageDataId = pageManager.GetPageNode(pageId).GetPageData().Id;
                var paragraphTextFieldControlData = form.Controls.Where(c => c.PlaceHolder == "Body" && !c.IsLayoutControl).FirstOrDefault();
                var mvcFieldProxy = formManager.LoadControl(paragraphTextFieldControlData) as MvcWidgetProxy;

                var paragraphTextField = mvcFieldProxy.Controller as ParagraphTextFieldController;
                Assert.IsNotNull(paragraphTextField, "The text field was not found.");

                var paragraphTextFieldName = paragraphTextField.MetaField.FieldName;
                var formCollection = new FormCollection();
                formCollection.Add(paragraphTextFieldName, SubmitedParagraphValue);
                var formControllerProxy = pageManager.LoadPageControls<MvcControllerProxy>(pageDataId).Where(contr => contr.Controller.GetType() == typeof(FormController)).FirstOrDefault();
                var formController = formControllerProxy.Controller as FormController;
                formController.ControllerContext = new ControllerContext();
                formController.ControllerContext.HttpContext = new HttpContextWrapper(HttpContext.Current);
                formController.Index(formCollection);

                var formEntry = formManager.GetFormEntries(form).LastOrDefault();
                Assert.IsNotNull(formEntry, "Form entry has not been submitted.");

                var submittedValue = formEntry.GetValue(paragraphTextFieldName) as string;
                Assert.AreEqual(SubmitedParagraphValue, submittedValue, "Form did not persisted the submitted paragraph text value correctly.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a paragraph text field widget with URL type is submitted with incorrect value then the validation fails.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a paragraph text field widget with URL type is submitted with incorrect value then the validation fails.")]
        public void ParagraphTextFieldUrl_SubmitIncorrectValue_ServerValidationFails()
        {
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(ParagraphTextFieldController).FullName;
            var controller = new ParagraphTextFieldController();
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "ParagraphTextFieldValidationTest", "paragraph-text-field-validation-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                var pageDataId = pageManager.GetPageNode(pageId).GetPageData().Id;
                var paragraphTextFieldControlData = form.Controls.Where(c => c.PlaceHolder == "Body" && !c.IsLayoutControl).FirstOrDefault();
                var mvcFieldProxy = formManager.LoadControl(paragraphTextFieldControlData) as MvcWidgetProxy;

                var paragraphTextField = mvcFieldProxy.Controller as ParagraphTextFieldController;
                Assert.IsNotNull(paragraphTextField, "The text field was not found.");

                var paragraphTextFieldName = paragraphTextField.MetaField.FieldName;
                var formCollection = new FormCollection();
                formCollection.Add(paragraphTextFieldName, "Submitted paragraph value");
                var formControllerProxy = pageManager.LoadPageControls<MvcControllerProxy>(pageDataId).Where(contr => contr.Controller.GetType() == typeof(FormController)).FirstOrDefault();
                var formController = formControllerProxy.Controller as FormController;
                formController.ControllerContext = new ControllerContext();
                formController.ControllerContext.HttpContext = new HttpContextWrapper(HttpContext.Current);

                formController.Index(formCollection);
                Assert.IsFalse((bool)formController.TempData["sfSubmitSuccess"], "The Submit result was not correct");
                
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
