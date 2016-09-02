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
    /// Import edited widget template for news structure
    /// </summary>
    public class ImportEditedWidgetTemplateNewsStructure : TestArrangementBase
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
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            if (System.IO.Directory.Exists(this.tempFolderPath))
                ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);

            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            for (int i = 0; i < this.widgetTemplatesNames.Length; i++)
            {
                ServerOperations.Widgets().DeleteWidgetTemplate(this.widgetTemplatesNames[i]);
            }

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, flatClassification);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(NewsType, hierarchicalClassification);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassification);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassification);
        }
     
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.NewsStructure.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "List.NewsListNew", "Detail.DetailPageNewNews"
                                                    };

        private static string flatClassification = "n1";
        private static string hierarchicalClassification = "n2";
        private const string NewsType = "Telerik.Sitefinity.News.Model.NewsItem";
    }
}
