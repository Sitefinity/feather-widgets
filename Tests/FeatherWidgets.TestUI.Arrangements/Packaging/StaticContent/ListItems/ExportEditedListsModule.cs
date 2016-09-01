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
    /// Export Edited Lists Module.
    /// </summary>
    public class ExportEditedListsModule : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            Guid listId = ServerOperations.Lists().CreateList("TestList");
            ServerOperations.Lists().CreateListItem(listId, "TestListItem", "TestListItem");

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

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.Lists().DeleteAllLists();
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
            ServerOperations.ModuleBuilder().DeleteDirectory(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Export");
            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            for (int i = 0; i < this.widgetTemplatesNames.Length; i++)
            {
                ServerOperations.Widgets().DeleteWidgetTemplate(this.widgetTemplatesNames[i]);
            }

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ListsType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ListsType, flatClassification);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ListsType, hierarchicalClassification);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassification);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassification);
        }

        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.ListsStructure.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.ListsEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export";
        private const string ListsType = "Telerik.Sitefinity.Lists.Model.ListItem";
        private const string Path = "App_Data";
        private static string flatClassification = "l1";
        private static string hierarchicalClassification = "l2";
        private const string File1 = @"App_Data\Sitefinity\Export\Lists\Structure\Lists.sf";
        private const string File2 = @"App_Data\Export\Lists\Structure\Lists.sf";
        private const string Widgets1 = @"App_Data\Sitefinity\Export\Lists\Structure\widgetTemplates.sf";
        private const string Widgets2 = @"App_Data\Export\Lists\Structure\widgetTemplates.sf";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "Detail.DetailPageNewList", "List.AnchorListNew", "List.ExpandableListNew", 
                                                        "List.ExpandedListNew", "List.PagesListNew", "List.SimpleListNew"
                                                    };
    }
}
