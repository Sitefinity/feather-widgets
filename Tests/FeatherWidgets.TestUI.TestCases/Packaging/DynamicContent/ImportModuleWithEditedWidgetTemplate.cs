using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.ModuleBuilder.Framework;

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
            BAT.Macros().NavigateTo().Dashboard();
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(ModuleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenFieldsEditor(ModuleName, ContentTypeName);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().VerifyIfFieldExists(this.fieldNames);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ClickCancelButton();           
            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplate(MVCWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(EditedWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplate(MVCWidgetTemplate2);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(EditedWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplate(MVCWidgetTemplate3);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(EditedWidgetTemplate);
            BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();  
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
        private const string MVCWidgetTemplate3 = "NewWidgetTemplate";
        private const string PageTitle = "myTestPage";
        private const string EditedWidgetTemplate = "EDITED";
        private string[] fieldNames = new string[] 
                                                   { 
                                                        "Title", "LongText", "ShortTextLimitation", "Choices", "YesNo",
                                                        "DateTime", "NumberRequired", "Address", "Images", "Video", "Document"
                                                    };
    }
}
