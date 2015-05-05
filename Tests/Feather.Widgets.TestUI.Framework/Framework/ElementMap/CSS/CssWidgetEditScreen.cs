using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CSS
{
    /// <summary>
    /// Provides access to css widget screen
    /// </summary>
    public class CssWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CssWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets code mirror lines.
        /// </summary>
        public HtmlDiv CodeMirrorLines
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "class=CodeMirror-lines");
            }
        }
    }
}
