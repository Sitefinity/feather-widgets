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
    /// Import Edited Lists Module.
    /// </summary>
    public class ImportEditedListsModule : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            Guid listId = ServerOperations.Lists().CreateList("TestList");
            ServerOperations.Lists().CreateListItem(listId, "TestListItem", "TestListItem");
            ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, InstallationPath);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
                + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
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
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
                + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Lists().DeleteAllLists();
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
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

        private const string PageName = "TestPage";
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.ListsStructure.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.ListsEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export";
        private const string ListsType = "Telerik.Sitefinity.Lists.Model.ListItem";
        private static string flatClassification = "l1";
        private static string hierarchicalClassification = "l2";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "Detail.DetailPageNewList", "List.AnchorListNew", "List.ExpandableListNew", 
                                                        "List.ExpandedListNew", "List.PagesListNew", "List.SimpleListNew"
                                                    };
    }
}
