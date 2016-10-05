﻿using System;
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
    /// Import edited widget template for libraries structure
    /// </summary>
    public class ImportEditedWidgetTemplateLibrariesStructure : TestArrangementBase
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

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, flatClassificationIm);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(ImagesType, hierarchicalClassificationIm);
            }

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, flatClassificationVideo);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(VideosType, hierarchicalClassificationVideo);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationDoc);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationDoc);
            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationIm);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationIm);
            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationVideo);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationVideo);
        }

        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.LibrariesStructure.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Deployment";
        private static string flatClassificationDoc = "d1";
        private static string hierarchicalClassificationDoc = "d2";
        private static string flatClassificationIm = "i1";
        private static string hierarchicalClassificationIm = "i2";
        private static string flatClassificationVideo = "v1";
        private static string hierarchicalClassificationVideo = "v2";
        private const string DocumentsType = "Telerik.Sitefinity.Libraries.Model.Document";
        private const string ImagesType = "Telerik.Sitefinity.Libraries.Model.Image";
        private const string VideosType = "Telerik.Sitefinity.Libraries.Model.Video";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "ImageNew", "Detail.DetailPageNewImageGallery", "List.ImageGalleryNew", 
                                                        "List.OverlayGalleryNew", "List.SimpleListNew", "List.ThumbnailStripNew", "DocumentLinkNew",
                                                        "Detail.DocumentDetailsNewDocument", "List.DocumentsListNew", "List.DocumentsTableNew", 
                                                        "VideoNew", "Detail.DefaultNewVideoGallery", "List.OverlayGalleryNewVideoGallery", "List.VideoGalleryNew"
                                                    };
    }
}
