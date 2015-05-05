using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// This is the entry point class for css widget edit wrapper.
    /// </summary>
    public class CssWidgetEditWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill css to the css widget
        /// </summary>
        /// <param name="css">The css value</param>
        public void FillCssToCssWidget(string css)
        {
            HtmlDiv editable = EM.Css
                                       .CssWidgetEditScreen
                                       .CodeMirrorLines
                                       .AssertIsPresent("Editable area");

            editable.ScrollToVisible();
            editable.Focus();
            editable.MouseClick();

            Manager.Current.Desktop.KeyBoard.TypeText(css);
        }
    }
}
