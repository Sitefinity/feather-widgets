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
    /// This class contains ensures TextField functionalities work correctly.
    /// </summary>
    [TestFixture]
    public class TextFieldTests
    {
        /// <summary>
        /// Ensures that when a text field widget is added to form the default value is presented in the page markup.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a text field widget is added to form the default value is presented in the page markup.")]
        public void TextField_EditDefaultValue_MarkupIsCorrect()
        {
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(TextFieldController).FullName;
            var controller = new TextFieldController();
            controller.MetaField.DefaultValue = "My default text";
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "TextFieldValueTest", "text-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("My default text"), "Form did not render the default text in the text field.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a text field widget is submitted with certain value then the response is correct.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a text field widget is submitted with certain value then the response is correct.")]
        public void TextField_SubmitValue_ResponseIsCorrect()
        {
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(TextFieldController).FullName;
            var controller = new TextFieldController();
            controller.MetaField.DefaultValue = "My default text";
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "TextFieldValueTest", "text-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
                var pageDataId = pageManager.GetPageNode(pageId).GetPageData().Id;

                var textFieldName = this.GetTextFieldName(formManager, form);
                this.FindAndSubmitForm(pageManager, pageDataId, textFieldName);
                var formEntry = formManager.GetFormEntries(form).LastOrDefault();

                Assert.IsNotNull(formEntry, "Form entry has not been submitted.");
                var submittedValue = formEntry.GetValue(textFieldName) as string;

                Assert.IsTrue(submittedValue == "Submitted value", "Form did not persisted the submitted text value correctly.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        private void FindAndSubmitForm(PageManager pageManager, System.Guid pageDataId, string textFieldName)
        {
            var formCollection = new FormCollection();
            formCollection.Add(textFieldName, "Submitted value");

            var formControllerProxy = pageManager.LoadPageControls<MvcControllerProxy>(pageDataId).Where(contr => contr.Controller.GetType() == typeof(FormController)).FirstOrDefault();
            var formController = formControllerProxy.Controller as FormController;
            formController.ControllerContext = new ControllerContext();
            formController.ControllerContext.HttpContext = new System.Web.HttpContextWrapper(HttpContext.Current);

            formController.Submit(formCollection);
        }

        private string GetTextFieldName(FormsManager formManager, FormDescription form)
        {
            var textFieldControlData = form.Controls.Where(c => c.PlaceHolder == "Body" && c.IsLayoutControl == false).FirstOrDefault();
            var mvcfieldProxy = formManager.LoadControl(textFieldControlData) as MvcWidgetProxy;
            var textField = mvcfieldProxy.Controller as TextFieldController;

            Assert.IsNotNull(textField, "The text field was not found.");

            var textFieldName = textField.MetaField.FieldName;
            return textFieldName;
        }
    }
}
