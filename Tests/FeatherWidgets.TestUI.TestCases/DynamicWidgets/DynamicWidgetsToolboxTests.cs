﻿using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class DynamicWidgetsToolboxTests : FeatherTestCase
    {
        /// <summary>
        /// UI test DynamicModuleAddNewContentTypeVerifyPageToolbox
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void DynamicModuleAddNewContentTypeVerifyPageToolbox()
        {
            moduleName = "Press Release";

            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(moduleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenAddNewContentTypeWizardFromModuleDashboard();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().EnterContentTypeName(ContentTypeName, DevName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ClickFinishEditButton();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().WaitForSystemRestart();

            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyToolboxConfig");
        }

        /// <summary>
        /// UI test DynamicModuleRemoveContentTypeVerifyPageToolbox
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void DynamicModuleRemoveContentTypeVerifyPageToolbox()
        {
            moduleName = "Music Collection";

            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyToolboxConfigBeforeDeleteContentType");

            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(moduleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenFieldsEditor(moduleName, ContentTypeToDelete);
            BATFeather.Wrappers().Backend().ModuleBuilder().ModuleBuilderEditContentTypeWrapper().DeleteContentType();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().WaitForSystemRestart();

            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyToolboxConfig");
        }

        /// <summary>
        /// UI test DeactivateAndActivateDynamicModuleVerifyPageToolbox
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void DeactivateAndActivateDynamicModuleVerifyPageToolbox()
        {
            moduleName = "Press Release";

            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyToolboxConfigBeforeDeactivate");
            BAT.Arrange(this.TestName).ExecuteArrangement("DeactivateModule");

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().IsWidgetPresent(WidgetName, false);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Arrange(this.TestName).ExecuteArrangement("ActivateModule");
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().IsWidgetPresent(WidgetName, true);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private string moduleName;
        private const string ContentTypeName = "Test Type";
        private const string DevName = "TestType";
        private const string PageName = "TestPage";
        private const string ContentTypeToDelete = "Songs";
        private string WidgetName = "Press Articles MVC";
    }
}
