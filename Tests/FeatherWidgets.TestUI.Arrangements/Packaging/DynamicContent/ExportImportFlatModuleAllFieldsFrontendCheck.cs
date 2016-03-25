using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Export and import Flat Module and verify front end
    /// </summary>
    public class ExportImportFlatModuleAllFieldsFrontendCheck : TestArrangementBase
    {
        /// <summary>
        /// Set Up
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url
                .GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, Path);
        }

        /// <summary>
        /// Delete the package from the db.
        /// </summary>
        [ServerArrangement]
        public void DeletePackageFromDB()
        {
            ServerOperations.Packaging().DeleteAllPackagesFromDB();
        }

        /// <summary>
        /// Restarts the application.
        /// </summary>
        [ServerArrangement]
        public void RestartApplication()
        {
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url
                .GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Verifies the exported files.
        /// </summary>
        [ServerArrangement]
        public void VerifyExportedFiles()
        {
            ServerOperations.Packaging().VerifyExportedDynamicModules(File1, File2);
            ServerOperations.Packaging().VerifyExportedWidgetTemplates(Widgets1, Widgets2);
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, "Module Installations");
            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteDirectory(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export");
            ServerOperations.ModuleBuilder().DeleteDirectory(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Export");
        }

        private const string File1 = @"App_Data\Sitefinity\Export\Dynamic modules\FlatModuleAllFields\Structure\FlatModuleAllFields.sf";
        private const string File2 = @"App_Data\Export\Dynamic modules\FlatModuleAllFields\Structure\FlatModuleAllFields.sf";
        private const string Widgets1 = @"App_Data\Sitefinity\Export\Dynamic modules\FlatModuleAllFields\Structure\widgetTemplates.sf";
        private const string Widgets2 = @"App_Data\Export\Dynamic modules\FlatModuleAllFields\Structure\widgetTemplates.sf";
        private const string PageName = "TestPage";
        private const string ModuleName = "FlatModuleAllFields";
        private const string Path = "App_Data";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.Packaging.Modules.FlatModuleAllFields.zip";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.FlatModuleAllFieldsStructure.zip";
    }
}