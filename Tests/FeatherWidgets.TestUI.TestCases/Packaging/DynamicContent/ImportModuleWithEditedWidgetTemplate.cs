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

        private const string ModuleName = "FlatModuleAllFields";
        private const string DynamicContentName = "Some title";
        private const string ContentTypeName = "AllTypes";
        private const string MVCWidgetTemplate = "Detail.AllTypes";
        private const string MVCWidgetTemplate2 = "List.AllTypes";
        private const string PageTitle = "myTestPage";
        private const string EditedWidgetTemplate = "EDITED";
    }
}
