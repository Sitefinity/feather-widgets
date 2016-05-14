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
    /// Import module with edited widget template
    /// </summary>
    public class ImportModuleWithEditedWidgetTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, InstallationPath);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) 
                + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Creates a page and content.
        /// </summary>
        [ServerArrangement]
        public void CreatePageAndContent()
        {
            Guid pageID = Guid.NewGuid();
            ServerOperations.Pages().CreateTestPage(pageID, PageTitle);
            ServerOperations.DynamicTypes().PublishAllTypesInFlatModule();
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, "Module Installations");
            ServerOperations.Packaging().DeleteAllPackagesFromDB();
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Widgets().DeleteWidgetTemplate("NewWidgetTemplate");
        }

        private const string ModuleName = "FlatModuleAllFields";        
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.FlatModuleAllFieldsWidgetTemplate.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export";
        private const string PageTitle = "myTestPage";
    }
}
