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
    /// <summary>
    /// SelectCssFileInCssWidget arrangement class.
    /// </summary>
    public class CssWidgetAndNewsWidgetOnTheSamePage : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName);

            ServerOperations.News().CreatePublishedNewsItem(NewsTitle, NewsContent, NewsProvider);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
            ServerOperationsFeather.Pages().AddCssWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "PageWithCssWidget";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string NewsProvider = "Default News";
    }
}
