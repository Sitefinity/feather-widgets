using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.FeedWidget
{
    public class FeedWidgetWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify feed link is visible
        /// </summary>
        public void VerifyFeedLImageIsVisible()
        {
            Assert.IsTrue(this.EM.FeedWidget.FeedWidgetFrontend.FeedImage.IsVisible());
        }

        /// <summary>
        /// Verify the feed link in head tag.
        /// </summary>
        public void VerifyFeedLinkInHeadTag(string feedTitle, string feedlink)
        {
            var head = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=head");
            Assert.IsTrue(head.ChildNodes[5].Attributes[0].RawValue.Contains(feedTitle));
            Assert.IsTrue(head.ChildNodes[5].Attributes[1].RawValue.Contains(feedlink));
        }
    }
}
