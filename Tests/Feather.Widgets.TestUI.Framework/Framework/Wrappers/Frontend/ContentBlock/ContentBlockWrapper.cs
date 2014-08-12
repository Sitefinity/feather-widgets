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
        public void VerifyContentOnTheFrontend(string content)
        {
            HtmlDiv publicWrapper = ActiveBrowser.Find.ByExpression<HtmlDiv>("tagname=div", "class=sfPublicWrapper")
            .AssertIsPresent("Public wrapper");

            Assert.IsTrue(publicWrapper.InnerText.Equals(content), "Unexpected content");
        }
    }
}
