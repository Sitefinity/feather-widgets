using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Taxonomies
{
    [TestFixture]
    [AssemblyFixture]
    [Description("Integration tests for the Flat taxonomy model.")]
    [Author(TestAuthor.Team7)]
    public class FlatTaxonomyTests
    {
        [TearDown]
        public void TearDown()
        {
            this.newsOperations.DeleteAllNews();
            this.taxonomyOperations.DeleteTags(this.taxaNames.ToArray());
            this.blogOperations.DeleteAllBlogs();
            this.serverPagesOperations.DeleteAllPages();
        }

        /// <summary>
        /// Verifies that the default settings are applied.
        /// </summary>
        [Test]
        public void FlatTaxonomy_DefaultSettings()
        {
            this.CreateTestData();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            var index = 1;

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = PageUrl + index + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 2),
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1),
            };

            var notExpectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
            };

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        /// <summary>
        /// Verifies that tags count is not shown
        /// </summary>
        [Test]
        public void FlatTaxonomy_DoNotShowCount()
        {
            this.CreateTestData();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowItemCount = false;
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            var index = 1;

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = PageUrl + index + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 2),
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1),
            };

            var notExpectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
            };

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        /// <summary>
        /// Verifies that empty tags are shown and the tags are sorted by Title DESC
        /// </summary>
        [Test]
        public void FlatTaxonomy_ShowEmptyTags_SortByTitleDescending()
        {
            this.CreateTestData();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowEmptyTaxa = true;
            flatTaxonomyController.Model.SortExpression = "Title DESC";
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            var index = 1;

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = PageUrl + index + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1),
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 2)
            };

            var notExpectedTags = new List<Tag>();

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        /// <summary>
        /// Verifies that the selected tags are shown and sorted as manually.
        /// </summary>
        [Test]
        public void FlatTaxonomy_SelectTags_ShowEmptyTags_SortManually()
        {
            this.CreateTestData();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowEmptyTaxa = true;
            flatTaxonomyController.Model.TaxaToDisplay = FlatTaxaToDisplay.Selected;
            flatTaxonomyController.Model.SerializedSelectedTaxaIds = string.Format(
                        CultureInfo.InvariantCulture, 
                        @"[""{0}"",""{1}""]", 
                        this.taxaIds[2], 
                        this.taxaIds[1]);
            flatTaxonomyController.Model.SortExpression = "AsSetManually";
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            var index = 1;

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = PageUrl + index + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1)
            };

            var notExpectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 2)
            };

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        /// <summary>
        /// Verifies that the tag assigned to a blog post is shown when 'Only tags used by content type...' option is selected with 'Blog posts' value
        /// </summary>
        [Test]
        public void FlatTaxonomy_SelectTagsByContentType()
        {
            this.CreateTestData();
            this.CreateBlogPostWithTag("testBlog", "testBlogPost");

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.TaxaToDisplay = FlatTaxaToDisplay.UsedByContentType;
            flatTaxonomyController.Model.ContentTypeName = "Telerik.Sitefinity.Blogs.Model.BlogPost";
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            var index = 1;

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = PageUrl + index + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                // the count will show how many blog posts has assigned tag1 (not news) -> only one
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 1),   
            };

            var notExpectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1),
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
            };

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        /// <summary>
        /// Verifies that only tags assigned to selected ContentId will be shown and that the filter url will open the page specified in BaseUrl.
        /// </summary>
        [Test]
        public void FlatTaxonomy_ContentId_BaseUrl()
        {
            this.CreateTestData();

            var index = 1;
            var filterPageName = "filterPage";
            var filterPageUrl = "filter-page";

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.TaxaToDisplay = FlatTaxaToDisplay.UsedByContentType;
            flatTaxonomyController.Model.ContentTypeName = "Telerik.Sitefinity.News.Model.NewsItem";
            flatTaxonomyController.Model.ContentId = this.newsIds[0]; // news1 id
            flatTaxonomyController.Model.BaseUrl = "~/" + filterPageUrl;
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, index);
            this.serverPagesOperations.CreatePage(filterPageName, filterPageUrl);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = filterPageUrl + "/-in-tags/tags/";
            var expectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[0], urlPrefix + this.taxaNames[0], 2)
            };

            var notExpectedTags = new List<Tag>()
            {
                new Tag(this.taxaNames[1], urlPrefix + this.taxaNames[1], 1),
                new Tag(this.taxaNames[2], urlPrefix + this.taxaNames[2], 0),
            };

            this.AssertTagsLinks(responseContent, expectedTags, notExpectedTags, flatTaxonomyController.Model.ShowItemCount);
        }

        #region Private methods
        /// <summary>
        /// Creates tags and news with assigned tags.
        /// </summary>
        private void CreateTestData()
        {
            foreach (var taxon in this.taxaNames)
            {
                var id = this.CreateTaxon(TaxonomiesConstants.TagsTaxonomyId, taxon);
                this.taxaIds.Add(id);
            }

            var news1Id = this.CreateNewsWithTags("news1", this.taxaNames.Take(1));
            var news2Id = this.CreateNewsWithTags("news2");
            var news3Id = this.CreateNewsWithTags("news3", this.taxaNames.Take(2));
            this.newsIds.Add(news1Id);
            this.newsIds.Add(news2Id);
            this.newsIds.Add(news3Id);
        }

        /// <summary>
        /// Creates the news with tags.
        /// </summary>
        /// <param name="newsTitle">The news title.</param>
        /// <param name="tags">The tags.</param>
        private Guid CreateNewsWithTags(string newsTitle, IEnumerable<string> tags = null)
        {
            return this.newsOperations.CreatePublishedNewsItem(newsTitle, string.Empty, string.Empty, string.Empty, new List<string>(), tags != null ? tags : new List<string>());
        }

        /// <summary>
        /// Creates the taxon.
        /// </summary>
        /// <param name="taxonomyId">The taxonomy id.</param>
        /// <param name="taxonTitle">The taxon title.</param>
        /// <returns></returns>
        private Guid CreateTaxon(Guid taxonomyId, string taxonTitle)
        {
            var taxonomyManager = new TaxonomyManager();
            var taxon = taxonomyManager.CreateTaxon<FlatTaxon>();
            taxon.Taxonomy = taxonomyManager.GetTaxonomy<FlatTaxonomy>(taxonomyId);
            taxon.Title = taxonTitle;
            taxon.UrlName = new Lstring(Regex.Replace(taxonTitle, ArrangementConstants.UrlNameCharsToReplace, ArrangementConstants.UrlNameReplaceString).ToLower());
            taxonomyManager.SaveChanges();
            return taxon.Id;
        }

        /// <summary>
        /// Creates the blog post with the first found tag.
        /// </summary>
        /// <param name="blogPostName">Name of the blog post.</param>
        /// <returns></returns>
        private Guid CreateBlogPostWithTag(string blogName, string blogPostName)
        {
            var blogId = this.blogOperations.CreateBlog(blogName);
            return this.blogOperations.CreateBlogPostWithTag(blogPostName, blogId);
        }

        /// <summary>
        /// Asserts that the expected tags exists on the page and that not expected tags do not exists on the page.
        /// </summary>
        /// <param name="pageContent">Content of the page.</param>
        /// <param name="expectedTags">The expected tags.</param>
        /// <param name="notExpectedTags">The not expected tags.</param>
        /// <param name="showCount">The show count.</param>
        private void AssertTagsLinks(string pageContent, List<Tag> expectedTags, List<Tag> notExpectedTags, bool showCount)
        {
            pageContent = pageContent.Replace(Environment.NewLine, string.Empty);

            using (HtmlParser parser = new HtmlParser(pageContent))
            {
                HtmlChunk chunk = null;
                parser.SetChunkHashMode(false);
                parser.AutoExtractBetweenTagsOnly = true;
                parser.CompressWhiteSpaceBeforeTag = true;
                parser.KeepRawHTML = true;

                while ((chunk = parser.ParseNext()) != null)
                {
                    if (chunk.TagName.Equals("a") && !chunk.IsClosure)
                    {
                        var linkHref = chunk.GetParamValue("href");
                        chunk = parser.ParseNext();
                        var linkText = chunk.Html;

                        foreach (var tag in notExpectedTags)
                        {
                            if (linkHref != null)
                            {
                                Assert.IsFalse(
                                    linkHref.EndsWith(tag.Url, StringComparison.Ordinal),
                                    string.Format(CultureInfo.InvariantCulture, "The tag's url {0} is found but is not expected.", linkHref));

                                Assert.AreNotEqual(
                                    tag.Name,
                                    linkText,
                                    string.Format(CultureInfo.InvariantCulture, "The tag's name {0} is found but is not expected.", linkText));
                            }
                        }

                        var currentTag = expectedTags.First();

                        Assert.IsTrue(
                            linkHref.EndsWith(currentTag.Url, StringComparison.Ordinal),
                            string.Format(CultureInfo.InvariantCulture, "The expected tag url {0} is not found.", currentTag.Url));
                        
                        Assert.AreEqual(
                            currentTag.Name,
                            linkText,
                            string.Format(CultureInfo.InvariantCulture, "The tag's name {0} is not correct.", linkText));

                        if (showCount)
                        {
                            parser.ParseNext(); // gets the closing tag </a>
                            var countChunk = parser.ParseNext(); // gets the chunk that contains the tag's count

                            var count = countChunk.Html.Trim();
                            Assert.AreEqual(
                                "(" + currentTag.Count + ")",
                                count,
                                string.Format(CultureInfo.InstalledUICulture, "The tag's count {0} is not correct.", count));
                        }
                        else
                        {
                            parser.ParseNext(); // gets the closing tag </a>
                            parser.ParseNext(); // gets an empty line
                            var listChunk = parser.ParseNext(); // gets the closing </li>

                            // Verifies there is no count before the closing li tag
                            Assert.IsTrue(listChunk.TagName.Equals("li"));
                            Assert.IsTrue(listChunk.IsClosure);
                        }

                        expectedTags.RemoveAt(0);
                    }
                }
            }
        }

        #endregion

        #region Private fields and constants
        private readonly List<string> taxaNames = new List<string>() { "tag1", "tag2", "tag3" };
        private readonly List<Guid> taxaIds = new List<Guid>();
        private readonly List<Guid> newsIds = new List<Guid>();
        private readonly TaxonomiesOperations taxonomyOperations = new TaxonomiesOperations();
        private readonly NewsOperations newsOperations = new NewsOperations();
        private readonly FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pagesOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();
        private readonly Telerik.Sitefinity.TestUtilities.CommonOperations.PagesOperations serverPagesOperations = ServerOperations.Pages();
        private readonly BlogOperations blogOperations = new BlogOperations();
        private const string PageName = "TestPage";
        private const string PageUrl = "tests-page";
        #endregion

        private class Tag
        {
            public Tag(string name, string url, int count)
            {
                this.Name = name;
                this.Url = url;
                this.Count = count;
            }

            public string Name { get; set; }

            public string Url { get; set; }

            public int Count { get; set; }
        }
    }
}
