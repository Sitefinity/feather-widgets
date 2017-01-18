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
    /// Import edited images module.
    /// </summary>
    public class ImportEditedImagesStructure : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(page1Id);
            Guid albumImageId = ServerOperations.Images().CreateLibrary(AlbumName);
            MultilingualTestConfig config = MultilingualTestConfig.Get();
            config.ImageBgTitle = Name;
            ServerOperations.Multilingual().Images().CreateImageMultilingual(config, null, albumImageId, false, 0, config.BgCultureInfo);

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
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Images().DeleteLibrary(AlbumName, false);
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
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, flatClassificationIm);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, hierarchicalClassificationIm);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationIm);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationIm);
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.Images.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.ImagesEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private const string ImagesType = "Telerik.Sitefinity.Libraries.Model.Image";
        private const string Name = "Test_Image_GIF";
        private const string AlbumName = "myTestAlbum";
        private static string flatClassificationIm = "i1";
        private static string hierarchicalClassificationIm = "i2";
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
