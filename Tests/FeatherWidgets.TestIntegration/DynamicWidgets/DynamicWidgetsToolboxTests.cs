using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web.UI;

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
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicWidgets_ImportDynamicModule_VerifyGeneratedWidgetInPageToolbox()
        {
            string[] widgets = new string[] 
            { 
                DynamicWidgetMVCTitle, 
                DynamicWidgetTitle, 
                DynamicChild1WidgetTitle, 
                DynamicChild1WidgetMVCTitle,
                DynamicChild2WidgetTitle,
                DynamicChild2WidgetMVCTitle
            };

            Assert.IsTrue(ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(DynamicWidgetSection).Tools.Count.Equals(6), "Widgets count is unexpected.");

            foreach (var widget in widgets)
            {
                Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(DynamicWidgetSection, widget), "Widget not found: " + widget);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private const string ModuleName = "Hierarchical Module";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.HierarchicalModule.zip";
        private const string DynamicWidgetSection = "Hierarchical Module";
        private const string DynamicWidgetMVCTitle = "Root Content Types MVC";
        private const string DynamicWidgetTitle = "Root Content Types";
        private const string DynamicChild1WidgetTitle = "Child 1 Types";
        private const string DynamicChild1WidgetMVCTitle = "Child 1 Types MVC";
        private const string DynamicChild2WidgetTitle = "Child 2 Types";
        private const string DynamicChild2WidgetMVCTitle = "Child 2 Types MVC";
        private const string TransactionName = "Module Installations";
    }        
}
