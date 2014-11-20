using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangement methods for DeactivateAndActivateDynamicModuleVerifyPageToolbox
    /// </summary>
    public class DeactivateAndActivateDynamicModuleVerifyPageToolbox : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
            ServerOperations.Pages().CreatePage(PageName);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void DeactivateModule()
        {
            ServerOperations.ModuleBuilder().DeactivateModule(ModuleName, string.Empty, TransactionName);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void ActivateModule()
        {
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void VerifyToolboxConfigBeforeDeactivate()
        {
            string dynamicWidgetSection = ModuleName;

            Assert.IsTrue(ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(dynamicWidgetSection).Tools.Count.Equals(2), "Widgets count is unexpected.");
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, DynamicWidgetMVCTitle), "Widget not found: " + DynamicWidgetMVCTitle);
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, DynamicWidgetTitle), "Widget not found: " + DynamicWidgetTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);          
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";
        private const string DynamicWidgetMVCTitle = "Press Articles MVC";
        private const string DynamicWidgetTitle = "Press Articles";
        private const string PageName = "TestPage";
    }
}
