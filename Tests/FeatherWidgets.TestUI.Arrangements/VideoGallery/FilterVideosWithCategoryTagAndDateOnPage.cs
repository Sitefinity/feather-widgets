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
    /// FilterVideosWithCategoryTagAndDateOnPage arrangement class.
    /// </summary>
    public class FilterVideosWithCategoryTagAndDateOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(page1Id);

            ServerSideUpload.CreateVideoLibrary(VideoLibraryTitle);

            List<Guid> listOfIds = new List<Guid>();
            var guidVideo1 = ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 1, VideoResource1);
            listOfIds.Add(guidVideo1);
            var guidVideo2 = ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 2, VideoResource2);
            listOfIds.Add(guidVideo2);
            var guidVideo3 = ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 3, VideoResource3);
            listOfIds.Add(guidVideo3);
            var guidVideo4 = ServerSideUpload.UploadVideo(VideoLibraryTitle, VideoTitle + 4, VideoResource4);
            listOfIds.Add(guidVideo4);
 
            this.AssignTaxonomiesToVideos(listOfIds);     

            this.ChangeThePublicationDateOfAnVideo();
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

        private void ChangeThePublicationDateOfAnVideo()
        {
            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            var librariesManager = LibrariesManager.GetManager();
            var modified = librariesManager.GetVideos().Where(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == VideoTitle + 1).FirstOrDefault();
            librariesManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            librariesManager.SaveChanges();
        }
 
        private void AssignTaxonomiesToVideos(List<Guid> listOfIds)
        {
            ServerOperations.Taxonomies().CreateCategory(CategoryTitle + "0");
            var taxonomyManager = TaxonomyManager.GetManager();
            for (int i = 1; i < 5; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(CategoryTitle + i, CategoryTitle + (i - 1));
                var category = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == CategoryTitle + i);
                ServerOperations.Taxonomies().CreateTag(TagTitle + i);
                var tag = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == TagTitle + i);
                ServerOperations.Videos().AssignTaxonToVideo(listOfIds[i - 1], "Category", category.Id);
                ServerOperations.Videos().AssignTaxonToVideo(listOfIds[i - 1], "Tags", tag.Id);                                         
            }

            var category3 = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == CategoryTitle + 3);
            var tag3 = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == TagTitle + 3);
            ServerOperations.Videos().AssignTaxonToVideo(listOfIds[0], "Category", category3.Id);
            ServerOperations.Videos().AssignTaxonToVideo(listOfIds[0], "Tags", tag3.Id);
        }

        private const string PageName = "PageWithVideo";
        private const string CategoryTitle = "Category";
        private const string TagTitle = "Tag";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
        private const string VideoResource4 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny4.mp4";
    }
}
