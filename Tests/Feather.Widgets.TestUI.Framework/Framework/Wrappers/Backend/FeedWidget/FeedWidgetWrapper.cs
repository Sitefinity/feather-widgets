using System;
using System.Linq;
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
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().ClickCreateBlogButton();
            var frame = Manager.Current.ActiveBrowser.WaitForFrame(new FrameInfo() { Name = "create" });
            Assert.IsNotNull(frame, "There is no create frame");
            frame.WaitForAsyncOperations();
            frame.WaitForAsyncRequests();
            var inp = frame.Find.ByIdEndingWith<HtmlInputText>("_textBox_write_0");
            if (inp != null)
                inp.SimulateTextTyping(blogName);

            Manager.Current.Log.CaptureBrowser(ActiveBrowser);
            var createFrame = ActiveBrowser.WaitForFrame(new FrameInfo() { Name = "create" });
            Assert.IsNotNull(createFrame, "The blogs create frame was not found");
            var submit = createFrame.Find.ByExpression<HtmlAnchor>("id=?_createThisBlog", "TagName=a");
            Assert.IsNotNull(submit, "The create blog button was not found");
            submit.Click();
        }
    }
}
