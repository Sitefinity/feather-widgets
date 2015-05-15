using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for JavaScript widget edit wrapper.
    /// </summary>
    public class JavaScriptWidgetEditWrapper : ScriptsAndStylesCommonWrapper
    {
        /// <summary>
        /// Includes the java script in tag header.
        /// </summary>
        public void IncludeJavaScriptInTagHeader()
        {
            HtmlInputRadioButton includeJavaScriptInTagHeader = EM.ScriptsAndStyles.JavaScriptEditScreen.IncludeJavaScriptInHeadTag
            .AssertIsPresent("Include JavaScript In Head Tag radio  button");
            includeJavaScriptInTagHeader.Click();
        }

        /// <summary>
        /// Includes the java script where the widget is dropped.
        /// </summary>
        public void IncludeJavaScriptWhereTheWidgetIsDropped()
        {
            HtmlInputRadioButton includeJavaScriptWhereTheWidgetIsDropped = EM.ScriptsAndStyles.JavaScriptEditScreen.IncludeJavaScriptWhereTheWidgetIsDropped
            .AssertIsPresent("Include JavaScript Where The Widget Is Dropped radio  button");
            includeJavaScriptWhereTheWidgetIsDropped.Click();
        }

        /// <summary>
        /// Includes the java script before the closing body tag.
        /// </summary>
        public void IncludeJavaScriptBeforeTheClosingBodyTag()
        {
            HtmlInputRadioButton includeJavaScriptBeforeTheClosingBodyTag = EM.ScriptsAndStyles.JavaScriptEditScreen.IncludeJavaScriptBeforeTheClosingBodyTag
            .AssertIsPresent("Include JavaScript Before The Closing Body Tag radio  button");
            includeJavaScriptBeforeTheClosingBodyTag.Click();
        }

        /// <summary>
        /// Verifies the checked radio button option.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyCheckedRadioButtonOption(string text)
        {
            HtmlDiv radioButton = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=radio", "innertext=~" + text)
                  .AssertIsPresent("radio button");
            var button = radioButton.Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                  .AssertIsPresent("radio button");
            Assert.IsTrue(button.Checked);
        }

        /// <summary>
        /// Verifies the tips.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyTips(string text)
        {
            ActiveBrowser.Find.ByExpression<HtmlAnchor>("tagname=a","sf-popover-title=Tips", "sf-popover-content=~" + text).AssertIsPresent("Tips");     
        }
    }
}
