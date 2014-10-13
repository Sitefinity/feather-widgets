using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.ContentBlock
{
    /// <summary>
    /// This is a class with tests for Content Block widget on page and search indexing.
    /// </summary>
    [TestFixture]
    [Description("This is a class with test related to content block widget on page and searching.")]
    public class ContentBlockSearchIndex
    {
        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author("FeatherTeam")]
        [Description("Creates a pure MVC template and page, based on the template, with content block widget, verifies the search result is correct")]
        public void ContentBlock_PureMvcPageWithContentBlockWidget_VerifySearch()
        {
            string searchIndex = "TestSearchIndex_" + Guid.NewGuid().ToString();

            try
            {
                var searchIndexId = ServerOperations.Search().CreateSearchIndex(searchIndex);

                var templateId = FeatherServerOperations.Pages().CreatePureMvcTemplate(PageTemplate);
                var pageId = ServerOperations.Pages().CreatePage(PageTitle, templateId);
                pageId = ServerOperations.Pages().GetPageNodeId(pageId);
                ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);

                this.Reindex(searchIndexId);

                IResultSet result = this.Search(searchIndex, ContentBlockText);
                Assert.IsTrue(result.HitCount == 1, "Search result is not correct");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Templates().DeletePageTemplate(PageTemplate);
                ServerOperations.Search().DeleteAllIndexes();
            }
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author("FeatherTeam")]
        [Description("Creates normal page (hybrid mode - web forms + MVC) with content block widget and verifies the search result is correct ")]
        public void ContentBlock_PageWithContentBlockWidget_VerifySearch()
        {
            string searchIndex = "TestSearchIndex_" + Guid.NewGuid().ToString();

            try
            {
                var searchIndexId = ServerOperations.Search().CreateSearchIndex(searchIndex);

                var pageId = ServerOperations.Pages().CreatePage(PageTitle);
                ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);

                this.Reindex(searchIndexId);

                IResultSet result = this.Search(searchIndex, ContentBlockText);

                Assert.IsTrue(result.HitCount == 1, "Search result is not correct");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Search().DeleteAllIndexes();
            }
        }

        private IResultSet Search(string catalogue, string query)
        {
            var service = this.GetService();
            var searchQuery = service.BuildQuery(query, new[] { "Title", "Content" }, false);
            return service.Search(catalogue, searchQuery, null, null);
        }

        private ISearchService GetService()
        {
            return Telerik.Sitefinity.Services.ServiceBus.ResolveService<ISearchService>();
        }

        private void Reindex(Guid searchIndexId)
        {
            PublishingAdminService service = new PublishingAdminService();
            service.ReindexSearchContent(PublishingConfig.SearchProviderName, searchIndexId.ToString());
        }

        private const string PageTemplate = "TestTemplate";
        private const string PageTitle = "PageWithContentBlock";
        private const string ContentBlockText = "TestSearching";
    }
}
