using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in toolbox section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets in toolbox section.")]
    public class DynamicWidgetsToolboxTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicWidgets_ImportDynamicModule_VerifyGeneratedWidgetInPageToolbox()
        {
            Assert.IsTrue(ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(DynamicWidgetSection).Tools.Count.Equals(2), "Widgets count is unexpected.");
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(DynamicWidgetSection, DynamicWidgetMVCTitle), "Widget not found: " + DynamicWidgetMVCTitle);
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(DynamicWidgetSection, DynamicWidgetTitle), "Widget not found: " + DynamicWidgetTitle);
        }      

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string DynamicWidgetSection = "Press Release";
        private const string DynamicWidgetMVCTitle = "Press Articles MVC";
        private const string DynamicWidgetTitle = "Press Articles";
        private const string TransactionName = "Module Installations";
    }        
}
