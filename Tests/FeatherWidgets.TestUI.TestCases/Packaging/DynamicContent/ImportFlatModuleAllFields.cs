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
    /// Import module FlatModuleAllFields
    /// </summary>
    [TestClass]
    public class ImportFlatModuleAllFields_ : FeatherTestCase
    {
        /// <summary>
        /// Import module FlatModuleAllFields
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportFlatModuleAllFields()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage());                     
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(ModuleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenFieldsEditor(ModuleName, ContentTypeName);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().VerifyIfFieldExists(this.fieldNames);

            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().OpenEditFieldScreen(this.fieldNames[2]);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SelectLimitations();
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().VerifyMinAndMaxLenght("1", "10");
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SetMaxLenght(15);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SaveField();

            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ClickFinishEditButton();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ConfirmWidgetTemplateUpdate(true);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenFieldsEditor(ModuleName, ContentTypeName);

            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().OpenEditFieldScreen(this.fieldNames[2]);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SelectLimitations();
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().VerifyMinAndMaxLenght("1", "15");
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().CancelFieldEditScreen();

            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().OpenEditFieldScreen(this.fieldNames[6]);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().AssertRequiredCheckboxIsSelected(true);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().CancelFieldEditScreen();

            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().OpenCreateFieldWizard();
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SetFieldName(FieldName);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SelectFieldType(FieldTypeNames.LongText);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SelectInterfaceWidgetForEnteringData(WidgetNames.TextArea);
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().ClickContinueButton();
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().SaveField();
            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().WaitForFieldPresent(FieldName);

            BAT.Wrappers().Backend().ModuleBuilder().FieldActionsWrapper().DeleteField(this.fieldNames[1]);

            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ClickFinishEditButton();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ConfirmWidgetTemplateUpdate(true);

            BAT.Macros().NavigateTo().Dashboard();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().AssertModulePresentInContentMenu(ModuleName, true);
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
        private const string FieldName = "NewField";
        private string[] fieldNames = new string[] 
                                                   { 
                                                        "Title", "LongText", "ShortTextLimitation", "Choices", "YesNo",
                                                        "DateTime", "NumberRequired", "Address", "Images", "Video", "Document"
                                                    };
    }
}
