using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for content block on the frontend.
    /// </summary>
    public class ContentBlockWrapper : BaseWrapper
    {
        private Manager Manager
        {
            get
            {
                return Manager.Current;
            }
        }

        /// <summary>
        /// Verify content in content block widget on the frontend
        /// </summary>
        /// <param name="contentBlockContent">The content value</param>
        public void VerifyContentOfContentBlockOnThePageFrontend(string contentBlockContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            List<HtmlDiv> contentBlockCount = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("tagname=div", "data-sf-field=Content").ToList<HtmlDiv>();

            for (int i = 0; i < contentBlockCount.Count; i++)
            {
                var isContained = contentBlockCount[i].InnerText.Contains(contentBlockContent);
                Assert.IsTrue(isContained, string.Concat("Expected ", contentBlockContent, " but found [", contentBlockCount[i].InnerText, "]"));
            }
        }

        /// <summary>
        /// Verify aocial share buttons on the frontend
        /// </summary>
        public void VerifySocialShareButtonsOnThePageFrontend()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            HtmlUnorderedList socialShareButtons = frontendPageMainDiv.Find.ByExpression<HtmlUnorderedList>("tagname=ul", "class=list-inline s-social-share-list").
                AssertIsPresent("Social share buttons");

            Assert.IsNotNull(socialShareButtons, "Social share buttons");
        }

        /// <summary>
        /// Edit content block
        /// </summary>
        public void EditContentBlock()
        {
            HtmlDiv cb = ActiveBrowser.Find.ByCustom<HtmlDiv>(x => x.CssClass.Contains("sfFieldEditable"))
                 .AssertIsPresent("ContentBlock");
            cb.Focus();
            cb.MouseClick();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            ActiveBrowser.RefreshDomTree();
            cb.MouseClick();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            var element = ActiveBrowser.Find.ByCustom<HtmlUnorderedList>(e => e.CssClass.Contains("k-editor-toolbar") && e.IsVisible() == true)
             .AssertIsPresent("toolbar");

            Manager.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.TypeText("edited content block", 20);
        }
    }
}
