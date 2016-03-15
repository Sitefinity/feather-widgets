using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestIntegration.Packaging
{
    /// <summary>
    /// This is a test class with tests for dynamic structure export and import.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests for dynamic structure export and import.")]
    public class ImportExportDynamicStructureTests
    {
        [Test]
        [Category(TestCategories.Packaging)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicStructureTransfer_ImportDynamicModule_VerifyMvcWidgetsExist()
        {
            var exportModulePath = HostingEnvironment.MapPath(ImportExportDynamicStructureTests.ExportModulePath);
            if (!Directory.Exists(exportModulePath))
                Directory.CreateDirectory(exportModulePath);

            var assembly = ServerOperationsFeather.DynamicModules().GetTestUtilitiesAssembly();

            Dictionary<string, string> files = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "PackedModule.sf", ImportExportDynamicStructureTests.DynamicModuleFile },
                { "widgetTemplates.sf", ImportExportDynamicStructureTests.DynamicModuleWidgetTemplatesFile },
                { "configs.sf", ImportExportDynamicStructureTests.DynamicModuleConfigurationsFile }
            };

            foreach (var file in files)
            {
                using (Stream embeddedFileStream = assembly.GetManifestResourceStream(file.Value))
                {
                    var filePath = string.Concat(exportModulePath, Path.DirectorySeparatorChar, file.Key);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        embeddedFileStream.CopyTo(fileStream);
                    }
                }
            }

            // Restart the application
            SystemManager.ClearCurrentTransactions();
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                               (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));

            var dynamicWidgetSection = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(ModuleName);

            string[] widgets = new string[] 
            {
                ModuleWidgetTitle1,
                ModuleWidgetTitle2,
                ModuleWidgetTitle3
            };

            foreach (var widget in widgets)
            {
                Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, widget, isMvcWidget: true), "Widget " + widget + " is missing!");
                Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(dynamicWidgetSection, widget, isMvcWidget: false), "Widget " + widget + " is missing!");
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, Guid.NewGuid().ToString());
            var exportPath = HostingEnvironment.MapPath(ImportExportDynamicStructureTests.ExportPath);
            if (Directory.Exists(exportPath))
                ServerOperations.ModuleBuilder().DeleteDirectory(exportPath);
        }

        private const string ModuleName = "PackedModule";
        private const string DynamicModuleFile = "FeatherWidgets.TestUtilities.Data.DynamicModules.PackedModule.Structure.PackedModule.sf";
        private const string DynamicModuleWidgetTemplatesFile = "FeatherWidgets.TestUtilities.Data.DynamicModules.PackedModule.Structure.widgetTemplates.sf";
        private const string DynamicModuleConfigurationsFile = "FeatherWidgets.TestUtilities.Data.DynamicModules.PackedModule.Structure.configs.sf";
        private const string ModuleWidgetTitle1 = "T1";
        private const string ModuleWidgetTitle2 = "T2";
        private const string ModuleWidgetTitle3 = "T3";

        private const string ExportModulePath = "~/App_Data\\Sitefinity\\Export\\Dynamic modules\\PackedModule\\Structure";
        private const string ExportPath = "~/App_Data\\Sitefinity\\Export";
    }
}
