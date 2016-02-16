using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterCategoriesByContentTypeImages arrangement class.
    /// </summary>
    public class FilterCategoriesByContentTypeImages : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerSideUpload.CreateAlbum(ImageLibraryTitle);
            Guid imageId = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle, ImageResource1);
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleImages);
            Guid categoryId = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == this.taxonTitleImages).Id;
            ServerOperations.Images().AssignTaxonToImage(imageId, "Category", categoryId);
            
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + "0");

            for (int i = 1; i < 4; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + i, this.taxonTitleNews + (i - 1));
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, AuthorName, SourceName, new List<string> { this.taxonTitleNews + i }, null, null);
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageNodeId, Placeholder);
            ServerOperationsFeather.Pages().AddCategoriesWidgetToPage(pageNodeId, Placeholder);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(pageNodeId, Placeholder);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Taxonomies().DeleteCategories(this.taxonTitleImages);

            for (int i = 0; i <= 4; i++)
            {
                ServerOperations.Taxonomies().DeleteCategories(this.taxonTitleNews + i);
            }

            ServerOperations.Libraries().DeleteLibraries(false, "Image");
            ServerOperations.Images().DeleteAllImages(ContentLifecycleStatus.Master);
        }

        private const string PageName = "CategoriesPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string Placeholder = "Contentplaceholder1";
        private string taxonTitleNews = "CategoryNews";
        private string taxonTitleImages = "CategoryImages";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "TestImage";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
    }
}
