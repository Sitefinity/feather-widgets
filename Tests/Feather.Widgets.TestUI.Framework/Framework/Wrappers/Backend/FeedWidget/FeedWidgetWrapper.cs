using System;
using System.Linq;
using System.Threading;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.MS.TestUI;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.FeedWidget
{
    /// <summary>
    /// This is an entry point for FeedWidgetWrapper.
    /// </summary>
    public class FeedWidgetWrapper : BaseWrapper
    {
        public void CreateBlog(string blogName)
        {
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().ClickCreateBlogButton();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            var frame = Manager.Current.ActiveBrowser.WaitForFrame(new FrameInfo() { Name = "create" });
            Assert.IsNotNull(frame, "There is no create frame");
            frame.WaitForAsyncOperations();
            frame.WaitForAsyncRequests();
            var inp = frame.Find.ByIdEndingWith<HtmlInputText>("_textBox_write_0");
            if (inp != null)
                inp.SimulateTextTyping(blogName);

            HtmlAnchor publishBtn = frame.Find.ByExpression<HtmlAnchor>("class=sfLinkBtn sfSave").AssertIsPresent("Publish button");
            publishBtn.Click();
            ActiveBrowser.RefreshDomTree();
        }
    }
}
