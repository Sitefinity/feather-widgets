using System;
using System.IO;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

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
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Creates a pure MVC template and page, based on the template, with content block widget, verifies the search result is correct"), Ignore]
        public void ContentBlock_PureMvcPageWithContentBlockWidget_VerifySearch()
        {
            string searchIndex = "TestSearchIndex";
            var searchIndexId = Guid.NewGuid();

            try
            {
                searchIndexId = ServerOperations.Search().CreateSearchIndex(searchIndex);

                var templateId = FeatherServerOperations.Pages().CreatePureMvcTemplate(PageTemplate);
                var pageId = ServerOperations.Pages().CreatePage(PageTitle, templateId);
                pageId = ServerOperations.Pages().GetPageNodeId(pageId);
                ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);

                this.Reindex(searchIndexId);
                this.SearchWithWaitOperation(searchIndex, ContentBlockText, 1);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperations.Templates().DeletePageTemplate(PageTemplate);
                this.DeleteSearchIndex(searchIndex, searchIndexId);
                this.EmptySearchFoler();
            }
        }

        [Test]
        [Category(TestCategories.ContentBlock)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Creates normal page (hybrid mode - web forms + MVC) with content block widget and verifies the search result is correct "), Ignore]
        public void ContentBlock_PageWithContentBlockWidget_VerifySearch()
        {
            string searchIndex = "TestSearchIndex";
            var searchIndexId = Guid.NewGuid();

            try
            {
                searchIndexId = ServerOperations.Search().CreateSearchIndex(searchIndex);

                var pageId = ServerOperations.Pages().CreatePage(PageTitle);
                ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);

                this.Reindex(searchIndexId);
                this.SearchWithWaitOperation(searchIndex, ContentBlockText, 1);
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                this.DeleteSearchIndex(searchIndex, searchIndexId);
                this.EmptySearchFoler();               
            }
        }

        protected void DeleteSearchIndex(string searchIndexName, Guid publishingPointId)
        {
            var transaction = Guid.NewGuid().ToString();
            var manager = PublishingManager.GetManager(PublishingConfig.SearchProviderName, transaction);
            var pp = manager.GetPublishingPoint(publishingPointId);
            foreach (var settings in pp.PipeSettings)
                manager.DeletePipeSettings(settings);
            manager.DeletePublishingPoint(pp);
            TransactionManager.CommitTransaction(transaction);

            var service = ServiceBus.ResolveService<ISearchService>();
            service.DeleteIndex(searchIndexName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        protected int ServiceSearchHitCount(string catalogue, string query)
        {
            var resultSet = this.Search(catalogue, query);
            if (resultSet is IDisposable)
            {
                using (resultSet as IDisposable)
                {
                    return resultSet.HitCount;
                }
            }
            else
            {
                return resultSet.HitCount;
            }
        }

        private IResultSet Search(string catalogue, string query)
        {
            var service = ServiceBus.ResolveService<ISearchService>();
            var queryBuilder = ObjectFactory.Resolve<IQueryBuilder>();
            var searchQuery = queryBuilder.BuildQuery(query, new[] { "Title", "Content" });
            searchQuery.IndexName = catalogue;
            return service.Search(searchQuery);
        }

        private void Reindex(Guid searchIndexId)
        {
            PublishingAdminService service = new PublishingAdminService();
            service.ReindexSearchContent(PublishingConfig.SearchProviderName, searchIndexId.ToString());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        private void SearchWithWaitOperation(string searchIndexName, string searchedText, int expectedCount, int waitMilliseconds = 5000, int sleepTimeMilliseconds = 1000)
        {
            int actualResult = 0;
            WaitUtils.WaitOperationUntil(
                    () =>
                    {
                        int result = this.ServiceSearchHitCount(searchIndexName, searchedText);
                        actualResult = result;
                        return result == expectedCount;
                    },
                    waitMilliseconds,
                    () =>
                    {
                        Assert.Fail(string.Format("Searched text \"{0}\". Expected count: {1}. Actual: {2}", searchedText, expectedCount, actualResult));
                    },
                    sleepTimeMilliseconds);
        }

        private void EmptySearchFoler()
        {
            var path = Path.Combine(FeatherServerOperations.ResourcePackages().SfPath, "App_Data", "Sitefinity", "Search");

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private const string PageTemplate = "TestTemplate";
        private const string PageTitle = "PageWithContentBlock";
        private const string ContentBlockText = "TestSearching";
    }
}
