using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class ContentBlockWrapper : BaseWrapper
    {
        public void VerifyContentOfContentBlockOnThePageFrontend(string contentBlockContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> contentBlockCount = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "data-sf-field=Content").ToList<HtmlDiv>();

            for (int i = 0; i < contentBlockCount.Count; i++)
            {
                var isContained = contentBlockCount[i].InnerText.Contains(contentBlockContent);
                Assert.IsTrue(isContained, String.Concat("Expected ", contentBlockContent, " but found [", contentBlockCount[i].InnerText, "]"));
            }
        }
    }
}
