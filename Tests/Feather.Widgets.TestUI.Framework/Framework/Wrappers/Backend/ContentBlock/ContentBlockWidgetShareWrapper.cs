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
    }
}
