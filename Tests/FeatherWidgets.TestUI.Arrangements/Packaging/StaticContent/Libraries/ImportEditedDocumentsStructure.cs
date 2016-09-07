using System;
using System.Web;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.TestConfig;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Import edited documents module.
    /// </summary>
    public class ImportEditedDocumentsStructure : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(page1Id);
            Guid albumId = ServerOperations.Documents().CreateLibrary(AlbumName);
            MultilingualTestConfig config = MultilingualTestConfig.Get();
            config.DocumentBgTitle = DocumentTitle;
            ServerOperations.Multilingual().Documents().CreateDocumentMultilingual(config, null, albumId, false, 0, "en");

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
            ServerOperations.Documents().DeleteLibrary(AlbumName);
            ServerOperations.Pages().DeleteAllPages();

            if (System.IO.Directory.Exists(this.tempFolderPath))
                ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);

            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            for (int i = 0; i < this.widgetTemplatesNames.Length; i++)
            {
                ServerOperations.Widgets().DeleteWidgetTemplate(this.widgetTemplatesNames[i]);
            }

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(DocumentsType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(DocumentsType, flatClassificationDoc);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(DocumentsType, hierarchicalClassificationDoc);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationDoc);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationDoc);
        }

        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.Documents.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.DocumentsEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private const string DocumentsType = "Telerik.Sitefinity.Libraries.Model.Document";
        private const string AlbumName = "myTestAlbum1";
        private const string DocumentTitle = "Document1";
        private static string flatClassificationDoc = "d1";
        private static string hierarchicalClassificationDoc = "d2";    
        private const string PageName = "TestPage";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "ImageNew", "Detail.DetailPageNewImageGallery", "List.ImageGalleryNew", 
                                                        "List.OverlayGalleryNew", "List.SimpleListNew", "List.ThumbnailStripNew", "DocumentLinkNew",
                                                        "Detail.DocumentDetailsNewDocument", "List.DocumentsListNew", "List.DocumentsTableNew", 
                                                        "VideoNew", "Detail.DefaultNewVideoGallery", "List.OverlayGalleryNewVideoGallery", "List.VideoGalleryNew"
                                                    };
    }
}
