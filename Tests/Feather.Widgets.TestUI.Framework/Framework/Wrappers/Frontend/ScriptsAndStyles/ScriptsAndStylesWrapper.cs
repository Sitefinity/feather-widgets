using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.ScriptsAndStyles
{
    /// <summary>
    /// This is the entry point class for css widget on the frontend.
    /// </summary>
    public class ScriptsAndStylesWrapper : BaseWrapper
    {
        /// <summary>
        /// Checks if a style is present on the frontend.
        /// </summary>
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns>Is contained</returns>
        public bool IsCodePresentOnFrontend(string style)
        {
            var isContained = ActiveBrowser.ContainsText(style);

            return isContained;
        }

        /// <summary>
        /// Verifies the java script in head tag.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyJavaScriptInHeadTag(string text)
        {
            var head = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=head");
            head.Find.ByExpression<HtmlContainerControl>("type=text/javascript", "TextContent=~" + text).AssertIsNotNull("text");
        }

        /// <summary>
        /// Verifies the java script before the closing body tag.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyJavaScriptBeforeTheClosingBodyTag(string text)
        {
            var body = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=body");
            body.Find.ByExpression<HtmlContainerControl>("type=text/javascript", "TextContent=~" + text).AssertIsNotNull("text");
            int childrenCount = body.ChildNodes.Count();
            var lastBodyChild = body.ChildNodes[childrenCount - 1];
            string valueType = lastBodyChild.GetAttribute("type").Value;
            Assert.AreEqual(valueType, "text/javascript");
            Assert.AreEqual(lastBodyChild.TextContent, text);            
        }

        /// <summary>
        /// Verifies the java script file before the closing body tag.
        /// </summary>
        /// <param name="src">The src.</param>
        public void VerifyJavaScriptFileBeforeTheClosingBodyTag(string src)
        {
            var body = ActiveBrowser.Find.ByExpression<HtmlContainerControl>("tagname=body");
            body.Find.ByExpression<HtmlContainerControl>("type=text/javascript", "src=~" + src).AssertIsNotNull("src");           
        }

        /// <summary>
        /// Verifies the script result in about field of profile widget.
        /// </summary>
        /// <param name="value">The value.</param>
        public void VerifyScriptResultInAboutFieldOfProfileWidget(string value)
        {
            var about = ActiveBrowser.Find.ByExpression<HtmlInputText>("tagname=input", "id=Profile_About_");
            Assert.AreEqual(value, about.Value);
        }

        /// <summary>
        /// Verifies the java script in head tag.
        /// </summary>
        /// <param name="text">The text.</param>
        public void VerifyJavaScriptWhereTheWidgetIsDropped(string text)
        {
            var widgetDiv = ActiveBrowser.Find.ByExpression<HtmlDiv>("class=sfPublicWrapper", "id=PublicWrapper").AssertIsNotNull("div");
            widgetDiv.Find.ByExpression<HtmlContainerControl>("type=text/javascript", "TextContent=~" + text).AssertIsNotNull("text");
        }
    }
}
