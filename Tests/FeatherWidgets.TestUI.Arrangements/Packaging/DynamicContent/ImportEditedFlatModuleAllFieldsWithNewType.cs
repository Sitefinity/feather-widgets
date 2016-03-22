using System;
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
    /// Import module edited FlatModuleAllFields
    /// </summary>
    public class ImportEditedFlatModuleAllFieldsWithNewType : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, InstallationPath);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Import new package
        /// </summary>
        [ServerArrangement]
        public void ImportNewPackage()
        {
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResourceEdited, InstallationPath);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, "Module Installations");
            ServerOperations.Packaging().DeletePackageFromDB(ModuleName);
        }

        private const string ModuleName = "FlatModuleAllFields";
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.FlatModuleAllFieldsStructure.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.FlatModuleAllFieldsAddedNewType.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export";
        private const string PageName = "TestPage";
    }
}
