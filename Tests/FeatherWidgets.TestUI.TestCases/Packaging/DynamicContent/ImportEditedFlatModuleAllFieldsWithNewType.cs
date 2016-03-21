using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.ModuleBuilder.Framework;

namespace FeatherWidgets.TestUI.TestCases.Packaging.DynamicContent
{
    /// <summary>
    /// Import edited module FlatModuleAllFields
    /// </summary>
    [TestClass]
    public class ImportEditedFlatModuleAllFieldsWithNewType_ : FeatherTestCase
    {
        /// <summary>
        /// Import module FlatModuleAllFields
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedFlatModuleAllFieldsWithNewType()
        {
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(ModuleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().AssertContentIsPresent(ContentTypeName, true);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().AssertContentIsPresent(ContentTypeNameNew, true);
            BAT.Macros().NavigateTo().Modules().ParticularModule(ModuleNameNewType, this.Culture);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenCreateItemWizard(this.title);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().SetTitle(this.dynamicItemNameNewType);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().PublishItem();
            BAT.Macros().NavigateTo().Modules().ParticularModule(ModuleName, this.Culture);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenCreateItemWizard(this.title);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().SetTitle(this.dynamicItemNameAllTypes);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().SetNumber(this.dynamicItemNumber);
            BAT.Wrappers().Backend().ModuleBuilderWrapper().ModuleBuilderItemsCreateScreenFrameWrapper().PublishItem();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(ContentTypeName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(ContentTypeNameNew);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            this.VerifyDynamicItemsOnFrontend(this.dynamicItemNameAllTypes);
            this.VerifyDynamicItemsOnFrontend(this.dynamicItemNameNewType);
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Arrange(this.TestName).ExecuteSetUp();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().WaitForSystemRestart();
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
        private void VerifyDynamicItemsOnFrontend(string dynamicItem)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            Assert.IsTrue(frontendPageMainDiv.InnerText.Contains(dynamicItem));
        }

        private const string ModuleName = "FlatModuleAllFields";
        private const string ContentTypeName = "AllTypes";
        private const string ContentTypeNameNew = "NewTypes";
        private const string ModuleNameNewType = "FlatModuleAllFields-NewType-dashboard";
        private string dynamicItemNameAllTypes = "Test dynamic item all types";
        private string dynamicItemNameNewType = "Test dynamic item new types";
        private string dynamicItemNumber = "123";
        private string title = @"id=?_0_textBox_write_0";
        private const string PageName = "TestPage";
    }
}
