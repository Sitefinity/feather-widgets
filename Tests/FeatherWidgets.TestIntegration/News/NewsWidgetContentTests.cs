using System;
using System.Collections.Generic;
using System.Linq;

using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using NewsOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations.NewsOperations;
using TaxonomiesOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesOperations;

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
            this.serverOperationsNews.DeleteAllNews();
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
            newsController.Model.SelectionMode = SelectionMode.AllItems;
            mvcProxy.Settings = new ControllerSettings(newsController);

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    this.serverOperationsNews.CreateNewsItem(NewsTitle + i);

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
        /// News widget - test content functionality - Selected news
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_VerifySelectedItemsFunctionality()
        {
            int newsCount = 5;
         
            for (int i = 0; i < newsCount; i++)
            {
                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
            }

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.SelectedItems;                   

            var newsManager = NewsManager.GetManager();
            var selectedNewsItem = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == "Title2" && n.OriginalContentId != Guid.Empty);
            newsController.Model.SerializedSelectedItemsIds = "[\"" + selectedNewsItem.Id.ToString() + "\"]";

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.AreEqual(1, items.Length, "The count of news is not as expected");

            Assert.IsTrue(items[0].Fields.Title.Equals("Title2", StringComparison.CurrentCulture), "The news with this title was not found!");                          
        }

        /// <summary>
        /// News widget - test content functionality - Selected news with Sort News Descending
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_VerifySelectedItemsFunctionalityWithSortNewsDescending()
        {
            int newsCount = 10;
            string sortExpession = "Title DESC";
            string[] selectedNewsTitles = { "Title2", "Title7", "Title5" };
            string[] descendingNewsTitles = { "Title7", "Title5", "Title2" };
            var selectedNewsItems = new NewsItem[3];

            for (int i = 0; i < newsCount; i++)
            {
                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
            }

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.SelectedItems;
            newsController.Model.SortExpression = sortExpession;

            var newsManager = NewsManager.GetManager();

            for (int i = 0; i < selectedNewsTitles.Count(); i++)
            { 
                selectedNewsItems[i] = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == selectedNewsTitles[i] && n.OriginalContentId != Guid.Empty);                           
            }

            //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
            newsController.Model.SerializedSelectedItemsIds =
                                                             "[\"" + selectedNewsItems[0].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[1].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[2].Id.ToString() + "\"]";

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();

            Assert.AreEqual(3, items.Length, "The count of news is not as expected");

            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsTrue(items[i].Fields.Title.Equals(descendingNewsTitles[i]), "The news with this title was not found!");
            }

            newsController.Model.SelectionMode = SelectionMode.AllItems;

            items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();

            int lastIndex = 9;
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(items[i].Fields.Title.Equals(NewsTitle + lastIndex), "The news with this title was not found!");
                lastIndex--;
            }
        }

        /// <summary>
        /// News widget - test content functionality -  Selected news with Paging
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_VerifySelectedItemsFunctionalityWithPaging()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string index2 = "/2";
            string index3 = "/3";
            int itemsPerPage = 3;
            string sortExpession = "AsSetManually";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);
            string url3 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index3);

            int newsCount = 20;
            string[] selectedNewsTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
            var selectedNewsItems = new NewsItem[9];

            for (int i = 0; i < newsCount; i++)
            {
                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
            }

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();

            newsController.Model.SelectionMode = SelectionMode.SelectedItems;
            newsController.Model.SortExpression = sortExpession;
            newsController.Model.ItemsPerPage = itemsPerPage;

            var newsManager = NewsManager.GetManager();

            for (int i = 0; i < selectedNewsTitles.Count(); i++)
            {
                selectedNewsItems[i] = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == selectedNewsTitles[i] && n.OriginalContentId != Guid.Empty);
            }

            //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
            newsController.Model.SerializedSelectedItemsIds =
                                                             "[\"" + selectedNewsItems[0].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[1].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[2].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[3].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[4].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[5].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[6].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[7].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[8].Id.ToString() + "\"]";

            mvcProxy.Settings = new ControllerSettings(newsController);

            this.VerifyCorrectNewsOnPages(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, url2, url3, selectedNewsTitles);
        }

        /// <summary>
        /// News widget - test content functionality -  Selected news with Use Limits
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_VerifySelectedItemsFunctionalityWithUseLimit()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string sortExpession = "AsSetManually";
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            int newsCount = 20;
            string[] selectedNewsTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
            var selectedNewsItems = new NewsItem[9];

            for (int i = 0; i < newsCount; i++)
            {
                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
            }

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.SelectedItems;
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.SortExpression = sortExpession;
            newsController.Model.ItemsPerPage = 5;

            var newsManager = NewsManager.GetManager();

            for (int i = 0; i < selectedNewsTitles.Count(); i++)
            {
                selectedNewsItems[i] = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == selectedNewsTitles[i] && n.OriginalContentId != Guid.Empty);
            }

            //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
            newsController.Model.SerializedSelectedItemsIds =
                                                             "[\"" + selectedNewsItems[0].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[1].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[2].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[3].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[4].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[5].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[6].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[7].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[8].Id.ToString() + "\"]";

            mvcProxy.Settings = new ControllerSettings(newsController);

            this.VerifyCorrectNewsOnPageWithUseLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, selectedNewsTitles);
        }

        /// <summary>
        /// News widget - test content functionality -  Selected news with No Limits
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_VerifySelectedItemsFunctionalityWithNoLimit()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string pageTitlePrefix = testName + "News Page";
            string urlNamePrefix = testName + "news-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            int newsCount = 25;
            string[] selectedNewsTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
            var selectedNewsItems = new NewsItem[9];
            string[] newsNames = new string[newsCount];

            for (int i = 0; i < newsCount; i++)
            {
                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle: NewsTitle + i, newsContent: NewsTitle + i, author: NewsTitle + i);
                newsNames[i] = NewsTitle + i;
            }

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.SelectedItems;
            newsController.Model.DisplayMode = ListDisplayMode.All;

            var newsManager = NewsManager.GetManager();

            for (int i = 0; i < selectedNewsTitles.Count(); i++)
            {
                selectedNewsItems[i] = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == selectedNewsTitles[i] && n.OriginalContentId != Guid.Empty);
            }

            //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
            newsController.Model.SerializedSelectedItemsIds =
                                                             "[\"" + selectedNewsItems[0].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[1].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[2].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[3].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[4].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[5].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[6].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[7].Id.ToString() + "\"," +
                                                             "\"" + selectedNewsItems[8].Id.ToString() + "\"]";

            mvcProxy.Settings = new ControllerSettings(newsController);

            this.VerifyCorrectNewsOnPageWithNoLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, selectedNewsTitles);

            newsController.Model.SelectionMode = SelectionMode.AllItems;

            this.VerifyCorrectNewsOnPageWithNoLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, newsNames);
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
                    taxonId[i] = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle + i);
                    tagTitles[i] = tagTitle + i;
                    newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + i);
                    this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Tags", taxonId[i]);
                }

                for (int i = 0; i < newsCount; i++)
                {
                    ITaxon taxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId[i]);
                    var items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                    for (int j = 0; j < items.Length; j++)
                        Assert.IsTrue(items[j].Fields.Title.Equals(newsTitle + i, StringComparison.CurrentCulture), "The news with this title was not found!");
                }
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitles);
            }
        }

        /// <summary>
        /// News widget - test select by category functionality 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByCategoryNewsFunctionality()
        {
            int newsCount = 4;
            string newsTitle = "Title";
            string categoryTitle = "Category ";
            var newsController = new NewsController();
            string[] categoryTitles = new string[newsCount + 1];            

            try
            {
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "0");

                for (int i = 0; i <= newsCount; i++)
                {
                    categoryTitles[i] = categoryTitle + i;
                    this.serverOperationsTaxonomies.CreateCategory(categoryTitle + (i + 1), categoryTitle + i);
                    ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, "Content", "AuthorName", "SourceName", new List<string> { categoryTitle + i }, null, null);
                }

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                for (int i = 0; i < newsCount; i++)
                {
                    var category = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + i);
                    ITaxon taxonomy = taxonomyManager.GetTaxon(category.Id);
                    var items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                    for (int j = 0; j < items.Length; j++)
                        Assert.IsTrue(items[j].Fields.Title.Equals(newsTitle + i, StringComparison.CurrentCulture), "The news with this title was not found!");
                }
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteCategories(categoryTitles);
            }
        }

        /// <summary>
        /// News widget - test select by tag and sort news functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByTagAndSortNewsFunctionality()
        {
            int tagsCount = 3;
            Guid[] taxonId = new Guid[tagsCount];
            string tagTitle = "Tag ";
            string sortExpession = "Title ASC";
            string[] newsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            string[] sortedTitles = { "Angel", "Boat", "Cat" };
            Guid[] newsId = new Guid[newsTitles.Count()];
            var newsController = new NewsController();
            newsController.Model.SortExpression = sortExpession;
            string[] tagTitles = new string[tagsCount];

            try
            {
                for (int i = 0; i < tagsCount; i++)
                {
                    taxonId[i] = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle + i);
                    tagTitles[i] = tagTitle + i;                  
                }

                for (int i = 0; i < newsTitles.Count(); i++)
                {
                    if (i <= 2)
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Tags", taxonId[0]);
                    }
                    else
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Tags", taxonId[i - 2]);
                    }
                }

                newsController.Index(null);
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                for (int i = 0; i < tagsCount; i++)
                {                    
                    ITaxon taxonomy = taxonomyManager.GetTaxon(taxonId[i]);
                    var items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                    if (i == 0)
                    {
                        for (int j = 0; j < items.Length; j++)
                            Assert.IsTrue(items[j].Fields.Title.Equals(sortedTitles[j], StringComparison.CurrentCulture), "The news with this title was not found!");
                    }
                    else
                    {
                        for (int j = 0; j < items.Length; j++)
                            Assert.IsTrue(items[j].Fields.Title.Equals(newsTitles[i + 2], StringComparison.CurrentCulture), "The news with this title was not found!");
                    }
                }
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitles);
            }
        }

        /// <summary>
        /// News widget - test select by category and paging functionality 
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByCategoryNewsFunctionalityAndPaging()
        {       
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Paging;
            int itemsPerPage = 3;
            newsController.Model.ItemsPerPage = itemsPerPage; 
            string categoryTitle = "Category";
            string[] newsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            Guid[] newsId = new Guid[newsTitles.Count()];

            try
            {
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "0");
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "1", categoryTitle + "0");

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var category0 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "0");
                var category1 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "1");

                for (int i = 0; i < newsTitles.Count(); i++)
                {
                    if (i == 0)
                    { 
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category0.Id);
                    }
                    else if (i > 0 && i <= 3)
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category0.Id);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category1.Id);
                    }
                    else
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category1.Id);
                    }
                }
                 
                this.VerifyCorrectNewsOnPageWithCategoryFilterAndPaging(category0, category1, newsController, newsTitles);
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteCategories("Category0", "Category1");
            }
        }

        /// <summary>
        /// News widget - test select by category and limits functionality 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByCategoryNewsFunctionalityAndLimits()
        {
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            int itemsPerPage = 3;
            newsController.Model.ItemsPerPage = itemsPerPage;
            string categoryTitle = "Category";
            string[] newsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            Guid[] newsId = new Guid[newsTitles.Count()];

            try
            {
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "0");
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "1", categoryTitle + "0");

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var category0 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "0");
                var category1 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "1");

                for (int i = 0; i < newsTitles.Count(); i++)
                { 
                    if (i <= 3)
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category0.Id);
                    }
                    else
                    {
                        newsId[i] = this.serverOperationsNews.CreatePublishedNewsItem(newsTitles[i]);
                        this.serverOperationsNews.AssignTaxonToNewsItem(newsId[i], "Category", category1.Id);
                    }
                }

                ITaxon taxonomy = taxonomyManager.GetTaxon(category0.Id);
                var items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(3), "Number of news items is not correct");
                for (int i = 0; i <= 2; i++)
                {
                    Assert.IsTrue(items[i].Fields.Title.Equals(newsTitles[3 - i], StringComparison.CurrentCulture), "The news with this title was found!");
                }

                taxonomy = taxonomyManager.GetTaxon(category1.Id);
                items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(1), "Number of news items is not correct");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitles[4], StringComparison.CurrentCulture), "The news with this title was found!");

                newsController.Model.DisplayMode = ListDisplayMode.All;
                taxonomy = taxonomyManager.GetTaxon(category0.Id);
                items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(4), "Number of news items is not correct");
                for (int i = 0; i <= 3; i++)
                {
                    Assert.IsTrue(items[i].Fields.Title.Equals(newsTitles[3 - i], StringComparison.CurrentCulture), "The news with this title was found!");
                }

                taxonomy = taxonomyManager.GetTaxon(category1.Id);
                items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(1), "Number of news items is not correct");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitles[4], StringComparison.CurrentCulture), "The news with this title was found!");               
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteCategories("Category0", "Category1");
            }
        }
 
        /// <summary>
        /// News widget - test select by category and tag functionality 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByCategoryAndTagNewsFunctionality()
        {
            string newsTitle = "News ";
            string tagTitle = "Tag1";
            string categoryTitle = "Category1";

            var newsController = new NewsController();

            try
            {
                Guid tagId = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle);

                this.serverOperationsTaxonomies.CreateCategory(categoryTitle);
                Guid categoryId = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle).Id;

                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(newsTitle + 0, "Content", "AuthorName", "SourceName", null, new List<string> { tagTitle }, null);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(newsTitle + 1, "Content", "AuthorName", "SourceName", new List<string> { categoryTitle }, new List<string> { tagTitle }, null);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(newsTitle + 2, "Content", "AuthorName", "SourceName", new List<string> { categoryTitle }, null, null);

                newsController.Model.SelectionMode = SelectionMode.FilteredItems;
                newsController.Model.SerializedAdditionalFilters = "{\"__msdisposeindex\":205,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\"," + 
                                                                   "\"QueryItems\":[{\"IsGroup\":true,\"Ordinal\":0,\"Join\":\"AND\",\"ItemPath\":\"_0\",\"Value\":null,\"Condition\":null," +
                                                                   "\"Name\":\"Category\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":206},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_0_0\",\"Value\":" + categoryId + ",\"Condition\":" +
                                                                   "{\"FieldName\":\"Category\",\"FieldType\":\"System.Guid\",\"Operator\":\"Contains\",\"__msdisposeindex\":207},\"Name\":" + categoryTitle + ",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":208}," + 
                                                                   "{\"IsGroup\":true,\"Ordinal\":1,\"Join\":\"AND\",\"ItemPath\":\"_1\",\"Value\":null,\"Condition\":null," +
                                                                   "\"Name\":\"Tags\",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":209},{\"IsGroup\":false,\"Ordinal\":0,\"Join\":\"OR\",\"ItemPath\":\"_1_0\",\"Value\":" + tagId + ",\"Condition\":" +
                                                                   "{\"FieldName\":\"Tags\",\"FieldType\":\"System.Guid\",\"Operator\":\"Contains\",\"__msdisposeindex\":210},\"Name\":" + tagTitle + ",\"_itemPathSeparator\":\"_\",\"__msdisposeindex\":211}]," + 
                                                                   "\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";

                var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(1), "The count of news items is not correct");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was not found!");
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitle);
                this.serverOperationsTaxonomies.DeleteCategories(categoryTitle);
            }
        }

        /// <summary>
        /// News widget - test select by tag functionality and limit to 1
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByTagNewsFunctionalityAndLimits()
        {
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Limit;
            newsController.Model.ItemsPerPage = 1;

            string tagTitle = "Tag1";
            string newsTitle = "News ";

            try
            {
                Guid taxonId = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle);

                Guid newsId0 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 0);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId0, "Tags", taxonId);

                Guid newsId1 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 1);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId1, "Tags", taxonId);

                ITaxon tagTaxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId);
                var items = newsController.Model.CreateListViewModel(tagTaxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(1), "The count of news items is not correct - limit applied");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");

                newsController.Model.DisplayMode = ListDisplayMode.Paging;
                newsController.Model.ItemsPerPage = 20;
                items = newsController.Model.CreateListViewModel(tagTaxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(2), "The count of news items is not correct - all items");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[1].Fields.Title.Equals(newsTitle + 0, StringComparison.CurrentCulture), "The news with this title was found!");
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitle);
            }
        }

        /// <summary>
        /// News widget - test select by tag functionality and paging (2 items per page)
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByTagNewsFunctionalityAndPaging()
        {
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.Paging;
            newsController.Model.ItemsPerPage = 2;

            string tagTitle = "Tag1";
            string newsTitle = "News ";

            try
            {
                Guid taxonId = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle);

                Guid newsId0 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 0);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId0, "Tags", taxonId);

                Guid newsId1 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 1);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId1, "Tags", taxonId);

                Guid newsId2 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 2);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId2, "Tags", taxonId);

                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 3);

                ITaxon tagTaxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId);
                var items = newsController.Model.CreateListViewModel(tagTaxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(2), "The count of news items is not correct - first page");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 2, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[1].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");

                items = newsController.Model.CreateListViewModel(tagTaxonomy, 2).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(1), "The count of news items is not correct - second page");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 0, StringComparison.CurrentCulture), "The news with this title was found!");

                newsController.Model.DisplayMode = ListDisplayMode.Paging;
                newsController.Model.ItemsPerPage = 20;
                items = newsController.Model.CreateListViewModel(tagTaxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(3), "The count of news items is not correct - all items");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 2, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[1].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[2].Fields.Title.Equals(newsTitle + 0, StringComparison.CurrentCulture), "The news with this title was found!");
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitle);
            }
        }

        /// <summary>
        /// News widget - test select by tag functionality and no limit
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("Sitefinity Team 7")]
        public void NewsWidget_SelectByTagNewsFunctionalityAndNoLimit()
        {
            var newsController = new NewsController();
            newsController.Model.DisplayMode = ListDisplayMode.All;
            newsController.Model.ItemsPerPage = 1;

            string tagTitle = "Tag1";
            string newsTitle = "News ";

            try
            {
                Guid taxonId = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, tagTitle);

                Guid newsId0 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 0);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId0, "Tags", taxonId);

                Guid newsId1 = this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 1);
                this.serverOperationsNews.AssignTaxonToNewsItem(newsId1, "Tags", taxonId);

                this.serverOperationsNews.CreatePublishedNewsItem(newsTitle + 2);

                ITaxon tagTaxonomy = TaxonomyManager.GetManager().GetTaxon(taxonId);
                var items = newsController.Model.CreateListViewModel(tagTaxonomy, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(2), "The count of news items is not correct - no limit");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[1].Fields.Title.Equals(newsTitle + 0, StringComparison.CurrentCulture), "The news with this title was found!");

                newsController.Model.DisplayMode = ListDisplayMode.Paging;
                newsController.Model.ItemsPerPage = 20;
                items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();

                Assert.IsTrue(items.Length.Equals(3), "The count of news items is not correct - all items");
                Assert.IsTrue(items[0].Fields.Title.Equals(newsTitle + 2, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[1].Fields.Title.Equals(newsTitle + 1, StringComparison.CurrentCulture), "The news with this title was found!");
                Assert.IsTrue(items[2].Fields.Title.Equals(newsTitle + 0, StringComparison.CurrentCulture), "The news with this title was found!");
            }
            finally
            {
                this.serverOperationsTaxonomies.DeleteTags(tagTitle);
            }
        }

        private void VerifyCorrectNewsOnPageWithCategoryFilterAndPaging(HierarchicalTaxon category0, HierarchicalTaxon category1, NewsController newsController, string[] newsTitles)
        {
            ITaxon taxonomy = TaxonomyManager.GetManager().GetTaxon(category0.Id);
            var items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

            Assert.IsTrue(items.Length.Equals(3), "Number of news items is not correct");
            for (int i = 0; i <= 2; i++)
            {
                Assert.IsTrue(items[i].Fields.Title.Equals(newsTitles[3 - i], StringComparison.CurrentCulture), "The news with this title was found!");
            }

            items = newsController.Model.CreateListViewModel(taxonomy, 2).Items.ToArray();

            Assert.IsTrue(items.Length.Equals(1), "Number of news items is not correct");          
            Assert.IsTrue(items[0].Fields.Title.Equals(newsTitles[0], StringComparison.CurrentCulture), "The news with this title was found!");

            taxonomy = TaxonomyManager.GetManager().GetTaxon(category1.Id);
            items = newsController.Model.CreateListViewModel(taxonomy, 1).Items.ToArray();

            Assert.IsTrue(items.Length.Equals(3), "Number of news items is not correct");
            for (int i = 0; i <= 2; i++)
            {
                Assert.IsTrue(items[i].Fields.Title.Equals(newsTitles[4 - i], StringComparison.CurrentCulture), "The news with this title was found!");
            }

            items = newsController.Model.CreateListViewModel(taxonomy, 2).Items.ToArray();

            Assert.IsTrue(items.Length.Equals(1), "Number of news items is not correct");
            Assert.IsTrue(items[0].Fields.Title.Equals(newsTitles[1], StringComparison.CurrentCulture), "The news with this title was found!");    
        }

        private void VerifyCorrectNewsOnPages(MvcControllerProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string url2, string url3, string[] selectedNewsTitles)
        {
            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string responseContent2 = PageInvoker.ExecuteWebRequest(url2);
                string responseContent3 = PageInvoker.ExecuteWebRequest(url3);

                for (int i = 0; i < selectedNewsTitles.Count(); i++)
                {
                    if (i <= 2)
                    {
                        Assert.IsTrue(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was not found!");
                        Assert.IsFalse(responseContent2.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                        Assert.IsFalse(responseContent3.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                    }
                    else if (i > 2 && i <= selectedNewsTitles.Count() - 4)
                    {
                        Assert.IsTrue(responseContent2.Contains(selectedNewsTitles[i]), "The news with this title was not found!");
                        Assert.IsFalse(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                        Assert.IsFalse(responseContent3.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                    }
                    else
                    {
                        Assert.IsTrue(responseContent3.Contains(selectedNewsTitles[i]), "The news with this title was not found!");
                        Assert.IsFalse(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                        Assert.IsFalse(responseContent2.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                    }
                }
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        private void VerifyCorrectNewsOnPageWithUseLimitsOption(MvcControllerProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string[] selectedNewsTitles)
        {
            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < selectedNewsTitles.Count(); i++)
                {
                    if (i <= 4)
                    {
                        Assert.IsTrue(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was not found!");
                    }
                    else
                    {
                        Assert.IsFalse(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was found!");
                    }
                }
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        private void VerifyCorrectNewsOnPageWithNoLimitsOption(MvcControllerProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string[] selectedNewsTitles)
        {
            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < selectedNewsTitles.Count(); i++)
                { 
                    Assert.IsTrue(responseContent.Contains(selectedNewsTitles[i]), "The news with this title was not found!");                 
                }
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }
  
        #region Fields and constants

        private const string NewsTitle = "Title";
        private PagesOperations pageOperations;

        private readonly TaxonomiesOperationsContext serverOperationsTaxonomies = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies();
        private readonly NewsOperationsContext serverOperationsNews = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News();
        
        #endregion
    }
}