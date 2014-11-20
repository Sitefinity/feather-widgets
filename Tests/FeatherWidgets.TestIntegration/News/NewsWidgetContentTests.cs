using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using News.Mvc.Controllers;
using News.Mvc.Models;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.News
{
    /// <summary>
    /// This is a class with News tests.
    /// </summary>
    [TestFixture]
    [Description("This is a class with News tests for content settings.")]
    public class NewsWidgetContentTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
        }

        /// <summary>
        /// News widget - test content functionality - All news
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifyAllNewsFunctionality()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.AllItems;
            mvcProxy.Settings = new ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= newsCount; i++)
                    Assert.IsTrue(responseContent.Contains(NewsTitle + i), "The news with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        /// <summary>
        /// News widget - test content functionality - All news
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_VerifySelectedItemsFunctionality()
        {
            int newsCount = 5;
         
            for (int i = 0; i < newsCount; i++)
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
            }

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.SelectedItems;                   

            var newsManager = NewsManager.GetManager();
            var selectedNewsItem = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == "Title2" && n.OriginalContentId != Guid.Empty);
            newsController.Model.SerializedSelectedItemsIds = "[\"" + selectedNewsItem.Id.ToString() + "\"]";
            mvcProxy.Settings = new ControllerSettings(newsController);

            newsController.Index(null);

            Assert.AreEqual(1, newsController.Model.Items.Count, "The count of news is not as expected");

            Assert.IsTrue(newsController.Model.Items[0].Title.Equals("Title2", StringComparison.CurrentCulture), "The news with this title was not found!");                          
        }

        /// <summary>
        /// News widget - test select by tag functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        public void NewsWidget_SelectByTagNewsFunctionality()
        {
            int newsCount = 2;
            Guid[] taxonId = new Guid[newsCount];
            Guid[] newsId = new Guid[newsCount];
            string newsTitle = "News ";
            string tagTitle = "Tag ";
            var newsController = new NewsController();
            string[] tagTitles = new string[newsCount];

            try
            {
                for (int i = 0; i < newsCount; i++)
                {
                    taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle + i);
                    tagTitles[i] = tagTitle + i;
                    newsId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreatePublishedNewsItem(newsTitle + i);
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().AssignTaxonToNewsItem(newsId[i], "Tags", taxonId[i]);
                }

                for (int i = 0; i < newsCount; i++)
                {
                    ITaxon taxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId[i]);
                    newsController.ListByTaxon(taxonomy, null);

                    for (int j = 0; j < newsController.Model.Items.Count; j++)
                        Assert.IsTrue(newsController.Model.Items[j].Title.Equals(newsTitle + i, StringComparison.CurrentCulture), "The news with this title was not found!");
                }
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(tagTitles);
            }
        }

        #region Fields and constants

        private const string NewsTitle = "Title";
        private PagesOperations pageOperations;
        
        #endregion
    }
}