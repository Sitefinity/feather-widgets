using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Css
{
    /// <summary>
    /// This is the entry point class for css widget on the frontend.
    /// </summary>
    public class CssWrapper : BaseWrapper
    {
        /// <summary>
        /// Checks if a style is present on the frontend.
        /// </summary>
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns>Is contained</returns>
        public bool IsStylePresentOnFrontend(string style)
        {
            var isContained = ActiveBrowser.ContainsText(style);

            return isContained;
        }
    }
}
