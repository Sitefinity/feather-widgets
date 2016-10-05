using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Packaging.DynamicContent
{
    /// <summary>
    /// Import module with edited widget template
    /// </summary>
    [TestClass]
    public class ImportModuleWithEditedWidgetTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// Import module with edited widget template
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportModuleWithEditedWidgetTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Design().WidgetTemplates());
            this.SelectAllTemplatesFromTheSidebar();     
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplate(MVCWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().EditFrame.WaitForAsyncOperations();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(EditedWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplate(MVCWidgetTemplate2);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().EditFrame.WaitForAsyncOperations();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(EditedWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("LoadApplication");
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
        /// Selects all templates from the sidebar.
        /// </summary>
        private void SelectAllTemplatesFromTheSidebar()
        {
            BAT.Utilities().InMultiSiteMode(() =>
            {
                ActiveBrowser.WaitUntilReady();
                ActiveBrowser.WaitForAsyncOperations();
                ActiveBrowser.WaitForBinding();

                HtmlAnchor allTemplatesFilter = ActiveBrowser.Find.ByExpression<HtmlAnchor>("id=?_controlTemplatesBackendList_ctl00_ctl00_sidebar_allTemplates_ctl00_ctl00_allTemplates")
                    .AssertIsPresent("all templates filter");
                allTemplatesFilter.Click();

                ActiveBrowser.WaitForAsyncOperations();
                ActiveBrowser.WaitForBinding();
            });
        }

        private const string ModuleName = "FlatModuleAllFields";
        private const string DynamicContentName = "Some title";
        private const string ContentTypeName = "AllTypes";
        private const string MVCWidgetTemplate = "Detail.AllTypes";
        private const string MVCWidgetTemplate2 = "List.AllTypes";
        private const string PageTitle = "myTestPage";
        private const string EditedWidgetTemplate = "EDITED";
    }
}
