using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
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
        [FixtureSetUp]
        public void FixtureSetUp()
        {
            foreach (var taxon in this.taxaNames)
            {
                var id = this.taxonomyOperations.CreateFlatTaxon(TaxonomiesConstants.TagsTaxonomyId, taxon);
                this.taxaIds.Add(id);
            }

            this.CreateNewsWithTaxons("news1", this.taxaNames.Take(1));
            this.CreateNewsWithTaxons("news2");
            this.CreateNewsWithTaxons("news3", this.taxaNames.Take(2));
        }

        [FixtureTearDown]
        public void FixtureTearDown()
        {
            this.newsOperations.DeleteAllNews();
            this.taxonomyOperations.DeleteTags(this.taxaNames.ToArray());
        }

        [TearDown]
        public void TearDown()
        {
            this.pagesOperations.DeletePages();
        }

        /// <summary>
        /// Verifies that the default settings are applied.
        /// </summary>
        [Test]
        public void FlatTaxonomy_DefaultSettings()
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, 1);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + 1);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = "-in-tags/tags/";
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
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowItemCount = false;
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, 1);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + 1);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = "-in-tags/tags/";
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
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowEmptyTaxa = true;
            flatTaxonomyController.Model.SortExpression = "Title DESC";
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, 1);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + 1);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = "-in-tags/tags/";
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
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(FlatTaxonomyController).FullName;
            var flatTaxonomyController = new FlatTaxonomyController();
            flatTaxonomyController.Model.ShowEmptyTaxa = true;
            flatTaxonomyController.Model.SerializedSelectedTaxaIds = string.Format(
                        CultureInfo.InvariantCulture, 
                        @"[""{0}"",""{1}""]", 
                        this.taxaIds[2], 
                        this.taxaIds[1]);
            flatTaxonomyController.Model.SortExpression = "AsSetManually";
            mvcProxy.Settings = new ControllerSettings(flatTaxonomyController);

            this.pagesOperations.CreatePageWithControl(mvcProxy, PageName, PageName, PageUrl, 1);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + 1);
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            Assert.IsNotNull(responseContent);

            var urlPrefix = "-in-tags/tags/";
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

        #region Private methods
        private void CreateNewsWithTaxons(string newsTitle, IEnumerable<string> tags = null)
        {
            this.newsOperations.CreatePublishedNewsItem(newsTitle, string.Empty, string.Empty, string.Empty, new List<string>(), tags != null ? tags : new List<string>());
        }

        private void AssertTagsLinks(string pageContent, List<Tag> expectedTags, List<Tag> notExpectedTags, bool showCount)
        {
            pageContent = pageContent.Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty);

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
                        foreach (var tag in notExpectedTags)
                        {
                            if (chunk.GetParamValue("href") != null)
                            {
                                Assert.IsFalse(
                                    chunk.GetParamValue("href").EndsWith(tag.Url, StringComparison.Ordinal),
                                    string.Format(CultureInfo.InvariantCulture, "The tag {0} is found but is not expected.", chunk.GetParamValue("href")));
                            }
                        }

                        var currentTag = expectedTags.First();

                        if (chunk.GetParamValue("href").EndsWith(currentTag.Url, StringComparison.Ordinal))
                        {
                            var contentChunk = parser.ParseNext();
                            Assert.AreEqual(
                                currentTag.Name,
                                contentChunk.Html,
                                string.Format(CultureInfo.InvariantCulture, "The tag {0} is not correct.", contentChunk.Html));

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
                                var listChunk = parser.ParseNext(); // gets the closing </li>
                                Assert.IsTrue(listChunk.TagName.Equals("li"));
                                Assert.IsTrue(listChunk.IsClosure);
                            }
                        }

                        expectedTags.RemoveAt(0);
                    }
                }
            }
        }
        #endregion

        #region Private fields and constants
        private readonly List<string> taxaNames = new List<string>() { "tag1", "tag2", "tag3" };
        private List<Guid> taxaIds = new List<Guid>();
        private readonly TaxonomiesOperations taxonomyOperations = new TaxonomiesOperations();
        private readonly NewsOperations newsOperations = new NewsOperations();
        private readonly FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pagesOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();
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
