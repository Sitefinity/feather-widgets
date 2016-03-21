using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.TestUI.Core.Navigation;

namespace FeatherWidgets.TestUI.TestCases.Packaging.DynamicContent
{
    /// <summary>
    ///  Export and import Flat Module and verify front end
    /// </summary>
    [TestClass]
    public class ExportImportFlatModuleAllFieldsFrontendCheck_ : FeatherTestCase
    {
        /// <summary>
        ///  Export and import Flat Module and verify front end
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ExportImportFlatModuleAllFieldsFrontendCheck()
        {
            BAT.Wrappers().Backend().Packaging().PackagingWrapper().ExportStructure();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().DeleteModule(ModuleName);
            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyExportedFiles");
            BAT.Arrange(this.TestName).ExecuteArrangement("DeletePackageFromDB");
            BAT.Arrange(this.TestName).ExecuteArrangement("RestartApplication");
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().WaitForSystemRestart();
            ActiveBrowser.Refresh();

            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().AssertModulePresentInContentMenu(ModuleName, true);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(ModuleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().AssertContentIsPresent(ContentTypeName, true);
            BAT.Macros().NavigateTo().Modules().ParticularModule(ModuleName, this.Culture);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenCreateItemWizard(this.title);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().SetTitle(this.dynamicItemNameAllTypes);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().SetNumber(this.dynamicItemNumber);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().PublishItem();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(ContentTypeName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            this.VerifyDynamicItemsOnFrontEnd(this.dynamicItemNameAllTypes);
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Forces cleanup of the test data. This method is thrown if test setup fails. This method must be overridden in your test case.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verifies the dynamic items on front end.
        /// </summary>
        /// <param name="dynamicItem">The dynamic item.</param>
        private void VerifyDynamicItemsOnFrontEnd(string dynamicItem)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            Assert.IsTrue(frontendPageMainDiv.InnerText.Contains(dynamicItem));
        }

        private const string ModuleName = "FlatModuleAllFields";
        private const string ContentTypeName = "AllTypes";
        private string dynamicItemNameAllTypes = "Test dynamic item all types";
        private string dynamicItemNumber = "123";
        private string title = @"id=?_0_textBox_write_0";
        private const string PageName = "TestPage";
    }
}
