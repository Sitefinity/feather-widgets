using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Routing;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestIntegration.Helpers.OpenAccessPerformanceTests;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Performance
{
    /// <summary>
    /// Validated the number of SQL queries required to display a content widget.
    /// </summary>
    [TestFixture]
    [Description("Validated the number of SQL queries required to display a content widget.")]
    public class ContentModelSqlProfilingTests
    {
        /// <summary>
        /// Validated the number of SQL queries required to display a News widget.
        /// </summary>
        [Test]
        [Category(TestCategories.Performance)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Validated the number of SQL queries required to display a News widget.")]
        public void NewsWidget_Index_ValidateQueriesCount()
        {
            Func<int, string> newsTitle = (int i) => "NewsWidget_Index_ValidateQueriesCount-{0}".Arrange(i);
            var newsOperations = new NewsOperations();
            var createdNewsItems = new List<Guid>(50);
            for (int i = 0; i < 50; i++)
            {
                var id = Guid.NewGuid();
                newsOperations.CreateNewsItem(newsTitle(i), id);
                createdNewsItems.Add(id);
            }

            try
            {
                SqlProfiler.EnableProfiling();

                this.NewsWidgetAction();

                TracedSqlStatements tracedData = null;
                OpenAccessTraceListener.ExecuteWithSqlTracing(
                                        this.NewsWidgetAction,
                                        out tracedData);
                SqlProfiler.DisableSqlProfiling();

                ContentModelSqlProfilingTests.AssertSelects(tracedData, 2, "NewsController.Index");
            }
            finally
            {
                for (int i = 0; i < createdNewsItems.Count; i++)
                    newsOperations.DeleteNewsItem(newsTitle(i));
            }
        }

        /// <summary>
        /// Validated the number of SQL queries required to display a Blogs widget.
        /// </summary>
        [Test]
        [Category(TestCategories.Performance)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Validated the number of SQL queries required to display a Blogs widget.")]
        public void BlogsWidget_Index_ValidateQueriesCount()
        {
            var blogOperations = new BlogOperations();
            List<Guid> blogs = null;
            try
            {
                blogs = blogOperations.BatchCreateBlog(50, "Blog");
                foreach (Guid blogID in blogs)
                    blogOperations.BatchCreatePublishedBlogPost(10, "post", string.Empty, string.Empty, blogID);

                SqlProfiler.EnableProfiling();

                this.BlogsWidgetAction();

                TracedSqlStatements tracedData = null;
                OpenAccessTraceListener.ExecuteWithSqlTracing(
                                        this.BlogsWidgetAction,
                                        out tracedData);
                SqlProfiler.DisableSqlProfiling();

                ContentModelSqlProfilingTests.AssertSelects(tracedData, 3, "BlogController.Index");
            }
            finally
            {
                if (blogs != null)
                {
                    for (int i = 0; i < blogs.Count; i++)
                        blogOperations.DeleteBlog(blogs[i]);
                }
            }
        }

        private static void AssertSelects(TracedSqlStatements tracedData, int expectedSelects, string operationName)
        {
            const string MessageFormat = "{0} operation does not produce the expected SQL select statements. Actual statements: {1}";
            Assert.AreEqual(expectedSelects, tracedData.StatementsCountsByType.Selects, MessageFormat, operationName, tracedData.CombinedStatements);
        }

        private void NewsWidgetAction()
        {
            var writer = new StringWriter(CultureInfo.InvariantCulture);
            var fakeContext = new HttpContextWrapper(new HttpContext(new HttpRequest(string.Empty, "http://irrelevant.uri.com", string.Empty), new HttpResponse(writer)));

            var controller = new NewsController();
            FrontendControllerFactory.EnhanceViewEngines(controller);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(fakeContext, new RouteData(), controller);
            controller.ControllerContext.RouteData.Values["controller"] = "News";
            controller.Index(1).ExecuteResult(controller.ControllerContext);
        }

        private void BlogsWidgetAction()
        {
            var writer = new StringWriter(CultureInfo.InvariantCulture);
            var fakeContext = new HttpContextWrapper(new HttpContext(new HttpRequest(string.Empty, "http://irrelevant.uri.com", string.Empty), new HttpResponse(writer)));

            var controller = new BlogController();
            FrontendControllerFactory.EnhanceViewEngines(controller);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(fakeContext, new RouteData(), controller);
            controller.ControllerContext.RouteData.Values["controller"] = "Blog";
            controller.Index(1).ExecuteResult(controller.ControllerContext);
        }
    }
}
