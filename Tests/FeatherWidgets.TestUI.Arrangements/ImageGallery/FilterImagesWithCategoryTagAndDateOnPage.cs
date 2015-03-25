using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterImagesWithCategoryTagAndDateOnPage arrangement class.
    /// </summary>
    public class FilterImagesWithCategoryTagAndDateOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(page1Id);

            ServerSideUpload.CreateAlbum(ImageLibraryTitle);

            List<Guid> listOfIds = new List<Guid>();
            var guidImage1 = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 1, ImageResource1);
            listOfIds.Add(guidImage1);
            var guidImage2 = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 2, ImageResource2);
            listOfIds.Add(guidImage2);
            var guidImage3 = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 3, ImageResource3);
            listOfIds.Add(guidImage3);
            var guidImage4 = ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 4, ImageResource4);
            listOfIds.Add(guidImage4);
 
            this.AssignTaxonomiesToImages(listOfIds);     

            this.ChangeThePublicationDateOfAnImage();
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
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private void ChangeThePublicationDateOfAnImage()
        {
            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            var librariesManager = LibrariesManager.GetManager();
            var modified = librariesManager.GetImages().Where(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == ImageTitle + 1).FirstOrDefault();
            librariesManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            librariesManager.SaveChanges();
        }
 
        private void AssignTaxonomiesToImages(List<Guid> listOfIds)
        {
            ServerOperations.Taxonomies().CreateCategory(CategoryTitle + "0");
            var taxonomyManager = TaxonomyManager.GetManager();
            for (int i = 1; i < 5; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(CategoryTitle + i, CategoryTitle + (i - 1));
                var category = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == CategoryTitle + i);
                ServerOperations.Taxonomies().CreateTag(TagTitle + i);
                var tag = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == TagTitle + i);
                ServerOperations.Images().AssignTaxonToImage(listOfIds[i - 1], "Category", category.Id);
                ServerOperations.Images().AssignTaxonToImage(listOfIds[i - 1], "Tags", tag.Id);                                         
            }

            var category3 = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == CategoryTitle + 3);
            var tag3 = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == TagTitle + 3);
            ServerOperations.Images().AssignTaxonToImage(listOfIds[0], "Category", category3.Id);
            ServerOperations.Images().AssignTaxonToImage(listOfIds[0], "Tags", tag3.Id);
        }

        private const string PageName = "PageWithImage";
        private const string CategoryTitle = "Category";
        private const string TagTitle = "Tag";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string ImageResource4 = "Telerik.Sitefinity.TestUtilities.Data.Images.4.jpg";
    }
}
