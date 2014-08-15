using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class ContentBlockWidgetShareWrapper : BaseWrapper
    {
        public void FillContentBlockTitle(string title)
        {
            HtmlInputText input = EM.GenericContent.ContentBlockWidget.ShareContentTitle
                .AssertIsPresent("Title field");

            input.ScrollToVisible();
            input.Focus();
            input.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(title);
        }

        public void ShareButton()
        {
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.ShareButton
            .AssertIsPresent("Share button");
            shareButton.Click();
        }

        public void UnshareButton()
        {
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.UnshareButton
            .AssertIsPresent("Unshare button");
            shareButton.Click();
        }

        public void DoneSelectingButton()
        {
            HtmlButton shareButton = EM.GenericContent.ContentBlockWidget.DoneSelectingButton
            .AssertIsPresent("Done selecting button");
            shareButton.Click();
        }

        public void SelectContentBlock(string sharedContentTitle)
        {
            HtmlDiv sharedContentBlockList = EM.GenericContent.ContentBlockWidget.ContentBlockList
            .AssertIsPresent("Shared content list");
            var itemSpan = sharedContentBlockList.Find.ByExpression<HtmlSpan>("class=ng-binding", "InnerText=" + sharedContentTitle);
            itemSpan.ScrollToVisible();
            itemSpan.MouseClick();
            this.DoneSelectingButton();
        }
    }
}
