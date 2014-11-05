using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for EditNewsWidgetOnPageBasedOnPackageTemplate
    /// </summary>
    public class EditNewsWidgetOnPageBasedOnPackageTemplate : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle1, NewsContent1, NewsProvider);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle2, NewsContent2, NewsProvider);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "News";
        private const string NewsContent1 = "News content1";
        private const string NewsTitle1 = "NewsTitle1";
        private const string NewsContent2 = "News content2";
        private const string NewsTitle2 = "NewsTitle2";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string NewsProvider = "Default News";
    }
}
