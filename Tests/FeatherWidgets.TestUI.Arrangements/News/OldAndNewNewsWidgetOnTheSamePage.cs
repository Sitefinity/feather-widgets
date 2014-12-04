using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    public class OldAndNewNewsWidgetOnTheSamePage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddControlToPage(System.Guid,System.String,System.String,System.String,System.String,System.String)"), ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitleOld, NewsContentOld, NewsProvider);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitleNew, NewsContentOld, NewsProvider);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
            ServerOperations.Widgets().AddControlToPage(pageId, ControlTypes.NewsView, "Body", "News");
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

        private const string PageName = "NewsPage";
        private const string NewsContentOld = "News content old";
        private const string NewsTitleOld = "NewsTitleOld";
        private const string NewsContentNew = "News content new";
        private const string NewsTitleNew = "NewsTitleNew";
        private const string NewsProvider = "Default News";
    }
}
