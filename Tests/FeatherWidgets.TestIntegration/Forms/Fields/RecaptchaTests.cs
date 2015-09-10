using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class contains ensures Recaptcha functionalities work correctly.
    /// </summary>
    [TestFixture]
    public class RecaptchaTests
    {
        /// <summary>
        /// Ensures that when a recaptcha field widget is added to form the default value is presented in the page markup.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that when a recaptcha field widget is added to form the default value is presented in the page markup.")]
        public void Recaptcha_MarkupIsCorrect()
        {
            var controller = new RecaptchaFieldController();
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(RecaptchaFieldController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "RecaptchaFieldValueTest", "recaptcha-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                Assert.IsNotNull(pageContent.Contains("g-recaptcha"), "Form did not render recaptcha");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
