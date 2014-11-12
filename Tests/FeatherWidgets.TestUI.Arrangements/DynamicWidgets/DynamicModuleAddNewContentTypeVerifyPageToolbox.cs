using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for DynamicModuleAddNewContentTypeVerifyPageToolbox
    /// </summary>
    public class DynamicModuleAddNewContentTypeVerifyPageToolbox : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void VerifyToolboxConfig()
        {
            string dynamicWidgetSection = ModuleName;

            Assert.IsTrue(ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(dynamicWidgetSection).Tools.Count.Equals(4), "Widgets count is unexpected.");
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, DynamicWidgetMVCTitle), "Widget not found: " + DynamicWidgetMVCTitle);
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, DynamicWidgetTitle), "Widget not found: " + DynamicWidgetTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";
        private const string DynamicWidgetMVCTitle = "Test Types MVC";
        private const string DynamicWidgetTitle = "Test Types";
    }
}
