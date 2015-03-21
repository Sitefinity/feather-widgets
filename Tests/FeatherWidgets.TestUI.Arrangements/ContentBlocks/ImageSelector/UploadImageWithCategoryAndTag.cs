using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Upload Image With Category And Tag arrangement class.
    /// </summary>
    public class UploadImageWithCategoryAndTag : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerSideUpload.CreateAlbum(ImageLibraryTitle);
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
            ServerOperations.Libraries().DeleteLibraries(false, "Image");
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
        }

        private const string PageName = "PageWithImage";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ChildLibraryTitle = "ChildImageLibrary";
        private const string TaxonTitle = "Category";
    }
}