using System;
using System.Web;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Export Edited News Module.
    /// </summary>
    public class ExportEditedNewsModule : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            ServerOperations.News().CreateNewsItem("TestNews");
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, InstallationPath);
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResourceEdited, Path);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) 
                + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Verifies the exported files.
        /// </summary>
        [ServerArrangement]
        public void VerifyExportedFiles()
        {
            ServerOperations.Packaging().VerifyExportedStaticModule(File1, File2);
            ServerOperations.Packaging().VerifyExportedWidgetTemplates(Widgets1, Widgets2);
        }

        /// Load the application.
        /// </summary>
        [ServerArrangement]
        public void LoadApplication()
        {
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url
                .GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.News().DeleteAllNews();

            if (System.IO.Directory.Exists(this.tempFolderPath))
                ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);

            if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Deployment"))
                ServerOperations.ModuleBuilder().DeleteDirectory(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Deployment");

            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            for (int i = 0; i < this.widgetTemplatesNames.Length; i++)
            {
                ServerOperations.Widgets().DeleteWidgetTemplate(this.widgetTemplatesNames[i]);
            }

            using (new UnrestrictedModeRegion())
            {
                for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
                {
                    ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                    ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, flatClassification);
                    ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, hierarchicalClassification);
                }
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassification);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassification);
        }

        private const string PageName = "TestPage";
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.NewsStructure.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.NewsEditedStructure.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private const string NewsType = "Telerik.Sitefinity.News.Model.NewsItem";
        private static string flatClassification = "n1";
        private static string hierarchicalClassification = "n2";
        private const string File1 = @"App_Data\Sitefinity\Deployment\News\Structure\News.sf";
        private const string File2 = @"App_Data\Deployment\News\Structure\News.sf";
        private const string Widgets1 = @"App_Data\Sitefinity\Deployment\News\Structure\widgetTemplates.sf";
        private const string Widgets2 = @"App_Data\Deployment\News\Structure\widgetTemplates.sf";
        private const string Path = "App_Data";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "List.NewsListNew", "Detail.DetailPageNewNews"
                                                    };
    }
}
