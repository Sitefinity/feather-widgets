using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ScriptsAndStyles
{
    /// <summary>
    /// Provides access to css widget screen
    /// </summary>
    public class JavaScriptEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public JavaScriptEditScreen(Find find) : base(find)
        {
        }

        /// <summary>
        /// Gets the include JavaScript in head.
        /// </summary>
        /// <value>The include JavaScript in head.</value>
        public HtmlDiv IncludeJavaScriptInHeadTag
        {
            get
            {
                return this.Get<HtmlDiv>("class=radio", "innertext~=In the head tag");
            }
        }

        /// <summary>
        /// Gets the include JavaScript where the widget is dropped.
        /// </summary>
        /// <value>The include JavaScript where the widget is dropped.</value>
        public HtmlDiv IncludeJavaScriptWhereTheWidgetIsDropped
        {
            get
            {
                return this.Get<HtmlDiv>("class=radio", "innertext~=Where the widget is dropped");
            }
        }

        /// <summary>
        /// Gets the include JavaScript before the closing body tag.
        /// </summary>
        /// <value>The include JavaScript before the closing body tag.</value>
        public HtmlDiv IncludeJavaScriptBeforeTheClosingBodyTag
        {
            get
            {
                return this.Get<HtmlDiv>("class=radio", "innertext~=Before the closing body tag");
            }
        }
    }
}
