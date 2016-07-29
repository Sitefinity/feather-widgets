using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Routing;
using MbUnit.Framework;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.TestIntegration.Helpers;
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
                ((IOpenAccessDataProvider)NewsManager.GetManager().Provider).GetContext().Cache.ReleaseAll();
                ((IOpenAccessDataProvider)NewsManager.GetManager().Provider).GetContext().LevelTwoCache.EvictAll();

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
                ((IOpenAccessDataProvider)BlogsManager.GetManager().Provider).GetContext().Cache.ReleaseAll();
                ((IOpenAccessDataProvider)BlogsManager.GetManager().Provider).GetContext().LevelTwoCache.EvictAll();

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

        /// <summary>
        /// Validated the number of SQL queries required to display a Events widget.
        /// </summary>
        [Test]
        [Category(TestCategories.Performance)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Validated the number of SQL queries required to display a Events widget.")]
        public void EventsWidget_Index_ValidateQueriesCount()
        {
            const int SampleSize = 450;
            var eventOperations = new EventOperations();
            List<Guid> calendars = new List<Guid>(SampleSize);
            try
            {
                eventOperations.DeleteAllEvents();
                for (int i = 0; i < SampleSize; i++)
                {
                    var calId = eventOperations.CreateCalendar(Guid.Empty, "EventsWidget_Index_ValidateQueriesCount_" + i);
                    calendars.Add(calId);
                    eventOperations.CreateEvent("EventsWidget_Index_ValidateQueriesCount_" + i.ToString(CultureInfo.InvariantCulture), "no content", false, DateTime.UtcNow, DateTime.UtcNow, calId);
                }

                SqlProfiler.EnableProfiling();

                this.EventsWidgetAction();

                TracedSqlStatements tracedData = null;

                ((IOpenAccessDataProvider)EventsManager.GetManager().Provider).GetContext().Cache.ReleaseAll();
                ((IOpenAccessDataProvider)EventsManager.GetManager().Provider).GetContext().LevelTwoCache.EvictAll();

                OpenAccessTraceListener.ExecuteWithSqlTracing(
                                        this.EventsWidgetAction,
                                        out tracedData);

                SqlProfiler.DisableSqlProfiling();
                ContentModelSqlProfilingTests.AssertSelects(tracedData, 3, "EventController.Index");
            }
            finally
            {
                if (calendars != null)
                {
                    for (int i = 0; i < calendars.Count; i++)
                        eventOperations.DeleteCalendar(calendars[i]);
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

        private void EventsWidgetAction()
        {
            var writer = new StringWriter(CultureInfo.InvariantCulture);
            var fakeContext = new HttpContextWrapper(new HttpContext(new HttpRequest(string.Empty, "http://irrelevant.uri.com", string.Empty), new HttpResponse(writer)));

            var controller = new EventController();
            FrontendControllerFactory.EnhanceViewEngines(controller);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(fakeContext, new RouteData(), controller);
            controller.ControllerContext.RouteData.Values["controller"] = "Event";
            controller.Index(1).ExecuteResult(controller.ControllerContext);
        }
    }
}
