using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls;

namespace FeatherWidgets.TestUI.TestCases.Common
{
    /// <summary>
    /// UninstallFeatherAndVerifyFormsMvcOption test class.
    /// </summary>
    [TestClass]
    public class UninstallFeatherAndVerifyFormsMvcOption_ : FeatherTestCase
    {
        /// <summary>
        /// UI test UninstallFeatherAndVerifyFormsMvcOption
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.IgnoredInReadOnly)]
        public void UninstallFeatherAndVerifyFormsMvcOption()
        {
            BAT.Macros().NavigateTo().System().ModulesAndServices();
            ActiveBrowser.WaitUntilReady();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().WaitForRestart();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().DeactivateModule(ModuleName);
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().UninstallModule(ModuleName);

            this.VerifyPageBackend(PageName, WidgetName, FormsContentDisabled, false);

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAdvancedButton();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyWebFrameworkOptions(false);
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickBackToForms();

            BAT.Macros().NavigateTo().System().ModulesAndServices();
            ActiveBrowser.WaitUntilReady();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().WaitForRestart();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().InstallModule(ModuleName);

            this.VerifyPageBackend(PageName, WidgetName, FormsContentEnabled, true);

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAdvancedButton();
            BATFeather.Wrappers().Backend().Forms().FormsWrapper().VerifyWebFrameworkOptions(true);
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickBackToForms();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verify page backend
        /// </summary>
        /// <param name="pageName">Page name</param>
        /// <param name="widgetName">Widget name</param>
        /// <param name="widgetContent">Widget content</param>
        /// <param name="isModuleActvie">if set to <c>true</c> if module is actvie.</param>
        private void VerifyPageBackend(string pageName, string widgetName, string widgetContent, bool isModuleActvie)
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageName);
            Assert.AreEqual(isModuleActvie, BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().IsAnyMvcWidgetPersentInToolbox());

            if (isModuleActvie)
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(widgetName, widgetContent);

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        private const string PageName = "FormPage";
        private const string FormsContentEnabled = "Untitled";
        private const string FormsContentDisabled = "This widget doesn't work, because Feather module has been deactivated.";
        private const string WidgetName = "Form";
        private const string ModuleName = "Feather";
    }
}
