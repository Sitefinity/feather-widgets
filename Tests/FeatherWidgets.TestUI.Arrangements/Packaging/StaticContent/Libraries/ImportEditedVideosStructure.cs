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
    /// Import edited videos module.
    /// </summary>
    public class ImportEditedVideosStructure : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(page1Id);
            ServerOperations.Videos().CreateLibrary(AlbumName);
            ServerOperations.Videos().Upload(AlbumName, VideoTitle, FilePath, ".wmv");

            ServerOperationsFeather.DynamicModules().ExtractStructureZip(PackageResource, InstallationPath);
            ServerOperations.SystemManager().RestartApplication(false);
            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) 
                + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
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
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.Videos().DeleteLibrary(AlbumName, false);
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
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, flatClassificationVideo);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, hierarchicalClassificationVideo);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationVideo);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationVideo);
        }

        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.Video.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.VideoEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private const string VideosType = "Telerik.Sitefinity.Libraries.Model.Video";
        private const string AlbumName = "myTestAlbum2";
        private const string VideoTitle = "Wildlife1";
        private const string FilePath = "Telerik.Sitefinity.TestUtilities.Data.Videos.Wildlife1.wmv";
        private static string flatClassificationVideo = "v1";
        private static string hierarchicalClassificationVideo = "v2";       
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
