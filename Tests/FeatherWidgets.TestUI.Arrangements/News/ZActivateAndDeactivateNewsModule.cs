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
    /// ActivateAndDeactivateNewsModule arragement.
    /// </summary>
    public class ZActivateAndDeactivateNewsModule : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle1, NewsContent1, NewsProvider);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        [ServerArrangement]
        public void DeactivateNewsModule()
        {
            ServerOperations.StaticModules().DeactivateModule(ModuleName);
        }

        [ServerArrangement]
        public void ActivateNewsModule()
        {
            ServerOperations.StaticModules().ActivateModule(ModuleName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.StaticModules().ActivateModule(ModuleName);
        }

        private const string PageName = "NewsPage";
        private const string NewsContent1 = "News content1";
        private const string NewsTitle1 = "NewsTitle1";
        private const string NewsProvider = "Default News";
        private const string ModuleName = "News";
    }
}
