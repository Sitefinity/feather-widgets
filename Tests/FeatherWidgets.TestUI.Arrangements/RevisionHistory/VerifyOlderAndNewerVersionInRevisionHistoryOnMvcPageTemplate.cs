using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate arrangement class.
    /// </summary>
    public class VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var templateId = ServerOperations.Templates().CreatePureMVCPageTemplate(TemplateTitle);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle, NewsContent, "AuthorName", "SourceName", null, null, null);
            ServerOperations.Events().CreateEvent(EventsTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Events().DeleteAllEvents();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
        }

        private const string TemplateTitle = "TestLayout";
        private const string PageName = "TestPage";
        private const string NewsContent = "News content";
        private const string NewsTitle = "TestNewsTitle";
        private const string EventsTitle = "TestEventTitle";
    }
}
