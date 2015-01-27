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
            int countContentBlocks = this.GetCountOfContentBlocksOnFrontend(contentBlockContent);
            Assert.AreNotEqual(0, countContentBlocks, "Count should not be 0!");
        }
 
        /// <summary>
        /// Gets count of content blocks on frontend
        /// </summary>
        /// <param name="contentBlockContent">Content of the content block.</param>
        /// <returns></returns>
        public int GetCountOfContentBlocksOnFrontend(string contentBlockContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            var divList = frontendPageMainDiv.Find.AllByTagName("div");
            Assert.AreNotEqual(0, divList.Count, "Count should not be 0!");
            int countContentBlocks = 0;

            foreach (var div in divList)
            {
                if (div.ChildNodes.Count > 0)
                {
                    if (div.ChildNodes.First().TagName.Equals("div") && div.Attributes.Count.Equals(0))
                    {
                        var isContained = div.ChildNodes.First().TextContent.Contains(contentBlockContent);
                        Assert.IsTrue(isContained, "Expected " + contentBlockContent + " ,but found" + div.ChildNodes.First().TextContent);
                        countContentBlocks++;
                    }
                }
            }

            return countContentBlocks;
        }

        /// <summary>
        /// Verify aocial share buttons on the frontend
        /// </summary>
        public void VerifySocialShareButtonsOnThePageFrontend()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            frontendPageMainDiv.Find.ByExpression<HtmlUnorderedList>("tagname=ul", "class=list-inline sf-social-share").
                AssertIsPresent("Social share buttons");
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
            ActiveBrowser.Find.ByCustom<HtmlUnorderedList>(e => e.CssClass.Contains("k-editor-toolbar") && e.IsVisible() == true)
             .AssertIsPresent("toolbar");

            Manager.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.LControlKey);
            Manager.Desktop.KeyBoard.TypeText("edited content block", 20);
        }
    }
}
