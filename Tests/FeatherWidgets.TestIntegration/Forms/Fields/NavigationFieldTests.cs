using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class contains ensures Navigation field functionalities work correctly.
    /// </summary>
    [TestFixture]
    public class NavigationFieldTests
    {
        /// <summary>
        /// Ensures that when a Navigation field widget is added to form, page titles are presented in the page markup.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when a Navigation field widget is added to form, page titles are presented in the page markup.")]
        public void Navigation_MarkupIsCorrect()
        {
            var controller = new NavigationFieldController();

            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(NavigationFieldController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);
            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "NavigationFieldValueTest", "section-header-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                var pageName = Res.Get<FieldResources>().PageName + "1";
                Assert.IsTrue(pageContent.Contains(pageName), "Form did not render navigation field pages");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a Navigation field widget is added to form with page break widget, page titles are presented in the page markup.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when a Navigation field widget is added to form with page break widget, page titles are presented in the page markup.")]
        public void Navigation_FieldIsCorrectlyInitialized()
        {
            var controller = new PageBreakController();
            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(PageBreakController).FullName;
            control.Settings = new ControllerSettings(controller);

            var navigationController = new NavigationFieldController();
            var navigationControl = new MvcWidgetProxy();
            navigationControl.ControllerName = typeof(NavigationFieldController).FullName;
            navigationControl.Settings = new ControllerSettings(navigationController);

            var controls = new List<Control>() { control, navigationControl };

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(controls);

            var pageManager = PageManager.GetManager();
            var formsManager = FormsManager.GetManager();
            try
            {
                var culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
                var tempForm = formsManager.EditForm(formId, culture);
                var controlInForm = tempForm.Controls.FirstOrDefault(c => c.GetType() == typeof(NavigationFieldController));
                var zoneEditorService = new ZoneEditorService();
                var parameters = new Dictionary<string, string>();
                parameters["ControllerName"] = typeof(NavigationFieldController).FullName;

                zoneEditorService.UpdateControlState(
                    new Telerik.Sitefinity.Web.UI.ZoneEditorWebServiceArgs()
                    {
                        Id = controlInForm.Id,
                        ControlType = typeof(MvcControllerProxy).FullName,
                        PageId = tempForm.Id,
                        PlaceholderId = "Body",
                        Parameters = parameters,
                        MediaType = DesignMediaType.Form,
                        Attributes = new Dictionary<string, string>(),
                        CommandName = "reload"
                    });

                var masterForm = formsManager.Lifecycle.CheckIn(tempForm, culture);
                formsManager.Lifecycle.Publish(masterForm, culture);

                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "NavigationFieldInitializationTest", "section-header-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = FeatherServerOperations.Pages().GetPageContent(pageId);

                var pageName1 = Res.Get<FieldResources>().PageName + "1";
                var pageName2 = Res.Get<FieldResources>().PageName + "2";
                Assert.IsTrue(pageContent.Contains(pageName1), "Form did not render navigation field pages");
                Assert.IsTrue(pageContent.Contains(pageName2), "Form did not render navigation field pages");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
