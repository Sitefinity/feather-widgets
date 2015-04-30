using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for DynamicModuleRemoveContentTypeVerifyPageToolbox
    /// </summary>
    public class DynamicModuleRemoveContentTypeVerifyPageToolbox : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void VerifyToolboxConfigBeforeDeleteContentType()
        {
            string dynamicWidgetSection = ModuleName;
            var section = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(dynamicWidgetSection);
            int expectedCount = 6;

            Assert.IsTrue(section.Tools.Count.Equals(expectedCount), "Widgets count is unexpected.");
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, DynamicWidgetTitle, isMvcWidget: true), "Mvc Widget not found: " + DynamicWidgetTitle);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void VerifyToolboxConfig()
        {
            string dynamicWidgetSection = ModuleName;
            var section = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(dynamicWidgetSection);
            int expectedCount = 4;

            Assert.IsTrue(section.Tools.Count.Equals(expectedCount), "Widgets count is unexpected.");
            Assert.IsFalse(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, DynamicWidgetTitle, isMvcWidget: true), "Mvc Widget was found: " + DynamicWidgetTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private const string ModuleName = "Music Collection";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.MusicCollectionAdvanced.zip";
        private const string TransactionName = "Module Installations";
        private const string DynamicWidgetTitle = "Songs";
    }
}
