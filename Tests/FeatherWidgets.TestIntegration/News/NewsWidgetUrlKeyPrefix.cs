using System;
using System.Globalization;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.News
{
    [TestFixture]
    [Description("This class contains tests for UrlKeyPrefix")]
    public class NewsWidgetUrlKeyPrefix
    {
        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that correct news are displayed when UrlKeyPrefix is set.")]
        public void NewsWidget_UrlKeyPrefix_VerifyContent()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "NewsPage";
            string urlKeyPrefix = "prefix1";
            int index = 1;
            string allNewsUrl = UrlPath.ResolveAbsoluteUrl("~/" + pageNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.UrlKeyPrefix = urlKeyPrefix;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(newsController);

            try
            {
                var templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
                var pageId = ServerOperations.Pages().CreatePage(pageNamePrefix + index, templateId);
                Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
                this.AddNewsWidgetToPage(pageNodeId, mvcProxy);

                int newsCount = 5;

                for (int i = 1; i <= newsCount; i++)
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(NewsTitle + i);

                var newsManager = NewsManager.GetManager();
                var selectedNewsItemIndex = 3;
                var newsItem3 = newsManager.GetNewsItems().FirstOrDefault(n => n.Title == NewsTitle + selectedNewsItemIndex);

                var allResponseContent = PageInvoker.ExecuteWebRequest(allNewsUrl);
                var noPrefixNewsUrl = UrlPath.ResolveAbsoluteUrl("~/" + pageNamePrefix + index + newsItem3.ItemDefaultUrl);
                var noPrefixResponseContent = PageInvoker.ExecuteWebRequest(noPrefixNewsUrl);
                var withPrefixNewsUrl = UrlPath.ResolveAbsoluteUrl(string.Format("~/{0}{1}/!{2}{3}", pageNamePrefix, index, urlKeyPrefix, newsItem3.ItemDefaultUrl));
                var withPrefixResponseContent = PageInvoker.ExecuteWebRequest(withPrefixNewsUrl);

                Assert.IsTrue(allResponseContent.Contains(withPrefixNewsUrl), "The detail url is not correctly generated with the urlKeyPrefxi!");

                for (int i = 1; i <= newsCount; i++)
                {
                    Assert.IsTrue(noPrefixResponseContent.Contains(NewsTitle + i), "The news with this title was not found!");

                    if (i == selectedNewsItemIndex)
                    {
                        Assert.IsTrue(withPrefixResponseContent.Contains(NewsTitle + i), "The news with this title was found!");
                    }
                    else
                    {
                        Assert.IsFalse(withPrefixResponseContent.Contains(NewsTitle + i), "The news with this title was found!");
                    }
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.Pages.Model.ControlData.set_Caption(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        private void AddNewsWidgetToPage(Guid pageId, MvcControllerProxy mvcWidget)
        {
            PageManager pageManager = PageManager.GetManager();
            pageManager.Provider.SuppressSecurityChecks = true;
            var pageDataId = pageManager.GetPageNode(pageId).PageId;
            var page = pageManager.EditPage(pageDataId, CultureInfo.CurrentUICulture);

            var draftControlDefault = pageManager.CreateControl<PageDraftControl>(mvcWidget, "Contentplaceholder1");
            draftControlDefault.Caption = "News";
            pageManager.SetControlDefaultPermissions(draftControlDefault);
            page.Controls.Add(draftControlDefault);

            pageManager.PublishPageDraft(page, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();
        }

        #region Fields and constants

        private const string NewsTitle = "Title";
        private const string PageTemplateName = "Bootstrap.default";

        #endregion
    }
}
