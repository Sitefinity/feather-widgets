using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Upload Video With Category And Tag arrangement class.
    /// </summary>
    public class UploadVideoWithCategoryAndTag : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerSideUpload.CreateVideoLibrary(VideoLibraryTitle);
            ServerSideUpload.CreateFolder(ChildLibraryTitle, parentId);
            ServerOperations.Taxonomies().CreateCategory(TaxonTitle + 0);
            ServerOperations.Taxonomies().CreateCategory(TaxonTitle + 1, TaxonTitle + 0);               
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Libraries().DeleteAllVideoLibrariesExceptDefaultOne();
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        /// <summary>
        /// Verifies the created tag.
        /// </summary>
        [ServerArrangement]
        public void VerifyCreatedTag()
        {
            var taxonomyManager = TaxonomyManager.GetManager();
            var tag = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == "Tag0");
            Assert.IsNotNull(tag);
        }

        private const string PageName = "PageWithVideo";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string ChildLibraryTitle = "ChildVideoLibrary";
        private const string TaxonTitle = "Category";
    }
}