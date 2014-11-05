using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
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

            List<HtmlDiv> newsDiv = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "data-sf-type=Telerik.Sitefinity.News.Model.NewsItem").ToList<HtmlDiv>();

            for (int i = 0; i < newsDiv.Count; i++)
            {
                var isContained = newsDiv[i].InnerText.Contains(newsTitle[i]);
                Assert.IsTrue(isContained, string.Concat("Expected ", newsTitle[i], " but found [", newsDiv[i].InnerText, "]"));
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

            List<HtmlDiv> newsDiv = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "data-sf-type=Telerik.Sitefinity.News.Model.NewsItem").ToList<HtmlDiv>();

            for (int i = 0; i < newsDiv.Count; i++)
            {
                if (newsDiv[i].InnerText.Contains(newsTitle))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
