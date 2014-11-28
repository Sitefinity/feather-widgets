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
    /// SelectSingleNewsPageAndVerifyFrontendPage arrangement class.
    /// </summary>
    public class SelectSingleNewsPageAndVerifyFrontendPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.News().CreateNewsItem(NewsTitle);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);

            Guid singleItemPageId = ServerOperations.Pages().CreatePage(SingleItemPage);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(singleItemPageId);
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
        private const string SingleItemPage = "Test Page";
        private const string NewsTitle = "News 1";
    }
}
