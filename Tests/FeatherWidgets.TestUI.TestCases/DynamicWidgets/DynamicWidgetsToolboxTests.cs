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
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().NavigateToModuleBuilderPage();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenModuleDashboard(ModuleName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().OpenAddNewContentTypeWizardFromModuleDashboard();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().EnterContentTypeName(ContentTypeName, DevName);
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().ClickFinishEditButton();
            BAT.Wrappers().Backend().ModuleBuilder().ModuleInitializerWrapper().WaitForSystemRestart();

            BAT.Arrange(this.TestName).ExecuteArrangement(this.ArrangementMethodTitle);
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

        private string ArrangementMethod
        {
            get
            {
                return ArrangementMethodTitle;
            }
        }

        private const string ModuleName = "Press Release";
        private const string ContentTypeName = "Test Type";
        private const string DevName = "TestType";
        private string ArrangementMethodTitle = "VerifyToolboxConfig";
    }
}
