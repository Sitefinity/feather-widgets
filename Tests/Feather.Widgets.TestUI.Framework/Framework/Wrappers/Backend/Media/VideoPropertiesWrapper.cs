using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Media
{
    /// <summary>
    /// This is an netry point for VideoPropertiesWrapper.cs.
    /// </summary>
    public class VideoPropertiesWrapper : MediaPropertiesBaseWrapper
    {
        /// <summary>
        /// Verifies the selected option in aspect ratio selector
        /// </summary>
        /// <param name="option">The aspect ratio option.</param>
        public void VerifySelectedOptionAspectRatioSelector(string option)
        {
            HtmlFindExpression expression = new HtmlFindExpression("tagName=select", "ng-model=model.aspectRatio");
            ActiveBrowser.WaitForElement(expression, 60000, false);

            HtmlSelect selector = this.EM.Media.VideoPropertiesScreen.AspectRatioSelector
                                      .AssertIsPresent("aspect ration selector");

            Assert.AreEqual(option, selector.SelectedOption.Text);
        }

        /// <summary>
        /// Selects option from aspect ratio selector.
        /// </summary>
        /// <param name="optionValue">Option value.</param>
        public void SelectOptionAspectRatioSelector(string optionValue)
        {
            HtmlSelect selector = this.EM.Media.VideoPropertiesScreen.AspectRatioSelector
                                      .AssertIsPresent("aspect ration selector");

            selector.SelectByText(optionValue);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            selector.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Plays the video.
        /// </summary>
        public void PlayVideo()
        {
            HtmlDiv playBtn = this.EM.Media.VideoPropertiesScreen.PlayButton.AssertIsPresent("Play button");

            playBtn.Click();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
        }

        /// <summary>
        /// Verifies the small video properites.
        /// </summary>
        /// <param name="src">The SRC.</param>
        public void VerifySmallVideoProperites(string src)
        {
            HtmlDiv holder = this.EM.Media.VideoPropertiesScreen.SmallVideoHolder.AssertIsPresent("video holder");
            holder.Find.ByExpression<HtmlVideo>("src=~" + src).AssertIsPresent("video");
        }

        /// <summary>
        /// Verifies the video info.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        public void VerifyVideoInfo(string title, string type, string size)
        {
            DateTime dateTime = DateTime.Now;
            var date = dateTime.Date;
            var dateString = date.ToString("M/d/yyyy");
            HtmlFindExpression expression = new HtmlFindExpression("ng-bind=sfMedia.Title.Value");
            ActiveBrowser.WaitForElement(expression, 60000, false);
            ActiveBrowser.Find.ByExpression<HtmlSpan>("ng-bind=sfMedia.Title.Value", "innertext=" + title).AssertIsPresent("title");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("ng-bind=sfMedia.Extension", "innertext=" + type).AssertIsPresent("type");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("ng-bind=mediaSize", "innertext=" + size).AssertIsPresent("size");
            ActiveBrowser.Find.ByExpression<HtmlSpan>("ng-bind=uploaded | date : 'M/d/yyyy h:mm'", "innertext=~" + dateString).AssertIsPresent("date");
        }

        /// <summary>
        /// Verifies the big video properites.
        /// </summary>
        /// <param name="src">The SRC.</param>
        public void VerifyBigVideoProperites(string src)
        {
            HtmlDiv holder = this.EM.Media.VideoPropertiesScreen.BigVideoHolder.AssertIsPresent("video holder");
            holder.Find.ByExpression<HtmlVideo>("id=sfVideoPlayer", "src=~" + src).AssertIsPresent("video");
        }

        /// <summary>
        /// Enters the width of the max.
        /// </summary>
        /// <param name="number">The number.</param>
        public void EnterWidth(string number)
        {
            HtmlInputText numberField = this.EM.Media.VideoPropertiesScreen.WidthNumber
                                              .AssertIsPresent("width");

            numberField.Text = number;
            numberField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Enters the height of the max.
        /// </summary>
        /// <param name="number">The number.</param>
        public void EnterHeight(string number)
        {
            HtmlInputText numberField = this.EM.Media.VideoPropertiesScreen.HeightNumber
                                              .AssertIsPresent("height");

            numberField.Text = number;
            numberField.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies the width and hight values.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void VerifyWidthAndHeightValues(string width, string height)
        {
            HtmlInputText widthField = this.EM.Media.VideoPropertiesScreen.WidthNumber
                                              .AssertIsPresent("width");

            HtmlInputText heightField = this.EM.Media.VideoPropertiesScreen.HeightNumber
                                              .AssertIsPresent("height");
            Assert.AreEqual(width, widthField.Text);
            Assert.AreEqual(height, heightField.Text); 
        }
    }
}