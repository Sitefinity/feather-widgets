using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for news widget on the frontend.
    /// </summary>
    public class NewsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify title in news widget on the frontend
        /// </summary>
        /// <param name="contentBlockContent">The content value</param>
        public void VerifyNewsTitlesOnThePageFrontend(string[] newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlListItem> newsLists = frontendPageMainDiv.Find.AllByExpression<HtmlListItem>("tagname=li", "data-sf-type=Telerik.Sitefinity.News.Model.NewsItem").ToList<HtmlListItem>();

            for (int i = 0; i < newsLists.Count; i++)
            {
                var isContained = newsLists[i].InnerText.Contains(newsTitle[i]);
                Assert.IsTrue(isContained, string.Concat("Expected ", newsTitle[i], " but found [", newsLists[i].InnerText, "]"));
            }
        }

        /// <summary>
        /// Checks if a news title is present on the frontend.
        /// </summary>
        /// <param name="newsTitle">The news title.</param>
        /// <returns>True or False depending on the news item presense.</returns>
        public bool IsNewsTitlePresentOnTheFrontend(string newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlListItem> newsLists = frontendPageMainDiv.Find.AllByExpression<HtmlListItem>("tagname=li", "data-sf-type=Telerik.Sitefinity.News.Model.NewsItem").ToList<HtmlListItem>();

            for (int i = 0; i < newsLists.Count; i++)
            {
                if (newsLists[i].InnerText.Contains(newsTitle))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Click news title on the frontend
        /// </summary>
        /// <param name="newsTitle">News title</param>
        public void ClickNewsTitle(string newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlAnchor newsAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + newsTitle)
                      .AssertIsPresent("News with this title was not found");

            newsAnchor.Wait.ForVisible();
            newsAnchor.ScrollToVisible();
            newsAnchor.MouseClick();
            ActiveBrowser.WaitForUrl(newsTitle.ToLower());
        }

        /// <summary>
        /// Verify related news on the frontend
        /// </summary>
        /// <param name="newsTitle">News title</param>
        public void VerifyRelatedNews(string newsTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlDiv relatedNews = frontendPageMainDiv.Find.ByExpression<HtmlDiv>("tagname=div", "class=sfMultiRelatedItmsWrp")
                      .AssertIsPresent("News with this title was not found");

            var isContained = relatedNews.InnerText.Contains(newsTitle);
            Assert.IsTrue(isContained, string.Concat("Expected ", newsTitle, " but found [", relatedNews.InnerText, "]"));
        }

        /// <summary>
        /// Checks if a news title is present on the frontend.
        /// </summary>
        /// <param name="newsTitle">The news title.</param>
        /// <returns>True or False depending on the news item presense.</returns>
        public bool IsNewsTitlePresentOnDetailMasterPage(string newsTitle)
        {
            ActiveBrowser.RefreshDomTree();
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            if (frontendPageMainDiv.InnerText.Contains(newsTitle))
            {
               return true;
            }

            return false;
        }

        /// <summary>
        /// Verify title in news widget on the frontend
        /// </summary>
        /// <param name="contentBlockContent">The content value</param>
        public void VerifyNewsTitlesOnThePageFrontendOLDNewsWidget(string[] newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlUnorderedList newsList = frontendPageMainDiv.Find.ByExpression<HtmlUnorderedList>("tagname=ul", "class=sfnewsList sfnewsListTitleDate sflist");

            List<HtmlListItem> listItem = newsList.Find.AllByExpression<HtmlListItem>("tagname=li", "class=sfnewsListItem sflistitem").ToList<HtmlListItem>();

            for (int i = 0; i < listItem.Count; i++)
            {
                var isContained = listItem[i].InnerText.Contains(newsTitle[i]);
                Assert.IsTrue(isContained, string.Concat("Expected ", newsTitle[i], " but found [", listItem[i].InnerText, "]"));
            }
        }
    }
}
