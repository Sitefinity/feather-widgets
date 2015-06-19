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
using Telerik.Sitefinity.Frontend.TestUtilities;
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
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicWidgets_ImportDynamicModule_VerifyGeneratedWidgetInPageToolbox()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            var section = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(DynamicWidgetSection);
            int expectedCount = 6;

            string[] widgets = new string[] 
            { 
                DynamicWidgetTitle, 
                DynamicChild1WidgetTitle, 
                DynamicChild2WidgetTitle,
            };

            Assert.AreEqual(expectedCount, section.Tools.Count);

            foreach (var widget in widgets)
            {
                Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, widget, isMvcWidget: true), "Mvc widget not found: " + widget);
                Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, widget, isMvcWidget: false), "Web Forms widget not found: " + widget);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicWidgets_ImportDynamicModule_VerifyOldDynamicWidgetsNotDuplicated()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(RelatedModuleName, RelatedModuleResource);

            var dynamicWidgetSection = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(RelatedModuleName);
            var contentSection = ServerOperationsFeather.Pages().GetContentToolboxSection();

            string[] widgets = new string[] 
            {
                RelatedModuleWidgetTitle1,
                RelatedModuleWidgetTitle2,
                RelatedModuleWidgetTitle3
            };

            foreach (var widget in widgets)
            {
                if (ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(contentSection, widget, isMvcWidget: false))
                {
                    Assert.IsFalse(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, widget, isMvcWidget: false), "Widget " + widget + " is duplicated");
                }
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private const string ModuleName = "Hierarchical Module";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.HierarchicalModule.zip";
        private const string DynamicWidgetSection = "Hierarchical Module";
        private const string DynamicWidgetTitle = "Root Content Types";
        private const string DynamicChild1WidgetTitle = "Child 1 Types";
        private const string DynamicChild2WidgetTitle = "Child 2 Types";
        private const string TransactionName = "Module Installations";
        private const string RelatedModuleName = "RelatedModule";
        private const string RelatedModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.RelatedModule.zip";
        private const string RelatedModuleWidgetTitle1 = "SingleRelatedDatas";
        private const string RelatedModuleWidgetTitle2 = "MultipleRelatedDatas";
        private const string RelatedModuleWidgetTitle3 = "SelfRelatings";
        private const string MvcWidgetClass = "sfMvcIcn";
        private const string WebFormsWidgetClass = "sfMvcIcn";
    }        
}
