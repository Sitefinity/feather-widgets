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
        public void VerifyContentOfContentBlockOnThePageFrontend(string[] newsTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> newsBlock = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "data-sf-type=Telerik.Sitefinity.News.Model.NewsItem").ToList<HtmlDiv>();

            for (int i = 0; i < newsBlock.Count; i++)
            {
                var isContained = newsBlock[i].InnerText.Contains(newsTitle[i]);
                Assert.IsTrue(isContained, string.Concat("Expected ", newsTitle, " but found [", newsBlock[i].InnerText, "]"));
            }
        }
    }
}
