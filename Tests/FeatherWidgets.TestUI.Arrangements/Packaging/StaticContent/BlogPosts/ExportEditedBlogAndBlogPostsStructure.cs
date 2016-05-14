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
    /// Export Edited Blog and Posts Structure.
    /// </summary>
    public class ExportEditedBlogAndBlogPostsStructure : TestArrangementBase
    {
        /// <summary>
        /// Server side setup.
        /// </summary>
        [ServerSetUp]
        public void ServerSetUp()
        {
            var blogId = ServerOperations.Blogs().CreateBlog("TestBlog");
            ServerOperations.Blogs().CreatePublishedBlogPost("TestBlogPost", blogId);

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
            ServerOperations.Packaging().VerifyExportedTaxonomies(Taxonomies1, Taxonomies2);
        }

        /// <summary>
        /// Cleans up the resources on the server used for this arrangement
        /// </summary>
        [ServerTearDown]
        public void ClearUp()
        {
            ServerOperations.Blogs().DeleteAllBlogs();
            ServerOperations.ModuleBuilder().DeleteDirectory(this.tempFolderPath);
            ServerOperations.Packaging().DeleteAllPackagesFromDB();

            for (int i = 0; i < this.widgetTemplatesNames.Length; i++)
            {
                ServerOperations.Widgets().DeleteWidgetTemplate(this.widgetTemplatesNames[i]);
            }

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogPostsType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogPostsType, flatClassification);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogPostsType, hierarchicalClassification);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassification);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassification);

            for (int i = 0; i < ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited.Length; i++)
            {
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogType, ServerOperations.CustomFieldsNames().FieldNamesWithoutClassificationsEdited[i]);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogType, flatClassificationBlog);
                ServerOperations.CustomFields().RemoveCustomFieldsFromContent(BlogType, hierarchicalClassificationBlog);
            }

            ServerOperations.Taxonomies().DeleteHierarchicalTaxonomy(hierarchicalClassificationBlog);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(flatClassificationBlog);
        }

        private const string InstallationPath = @"App_Data\Sitefinity";
        private const string PackageResource = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.BlogsStructure.zip";
        private const string PackageResourceEdited = "FeatherWidgets.TestUtilities.Data.Packaging.Structure.BlogsEdited.zip";
        private string tempFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Sitefinity\Export";
        private const string BlogPostsType = "Telerik.Sitefinity.Blogs.Model.BlogPost,Telerik.Sitefinity.ContentModules";
        private const string BlogType = "Telerik.Sitefinity.Blogs.Model.Blog,Telerik.Sitefinity.ContentModules";
        private const string File1 = @"App_Data\Sitefinity\Export\Blogs\Structure\Blogs.sf";
        private const string File2 = @"App_Data\Export\Blogs\Structure\Blogs.sf";
        private const string Widgets1 = @"App_Data\Sitefinity\Export\Blogs\Structure\widgetTemplates.sf";
        private const string Widgets2 = @"App_Data\Export\Blogs\Structure\widgetTemplates.sf";
        private const string Taxonomies1 = @"App_Data\Sitefinity\Export\Blogs\Structure\taxonomies.sf";
        private const string Taxonomies2 = @"App_Data\Export\Blogs\Structure\taxonomies.sf";
        private const string Path = "App_Data";
        private static string flatClassification = "post1";
        private static string hierarchicalClassification = "post2";
        private static string flatClassificationBlog = "b1";
        private static string hierarchicalClassificationBlog = "b2";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "Detail.DetailPageNewBlog", "List.BlogListNew", "Detail.DetailPageNewBlogPost", "List.BlogPostListNew"
                                                    };
    }
}
