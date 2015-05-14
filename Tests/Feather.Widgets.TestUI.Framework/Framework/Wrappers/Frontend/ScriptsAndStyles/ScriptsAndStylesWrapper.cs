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
    }
}
