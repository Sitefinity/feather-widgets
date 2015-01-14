using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.WidgetTemplates
{
        /// <summary>
        /// Provides access to WidgetTemplates screen
        /// </summary>
        public class WidgetTemplatesGrid : HtmlElementContainer
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NavigationWidgetEditScreen" /> class.
            /// </summary>
            /// <param name="find">The find.</param>
            public WidgetTemplatesGrid(Find find)
                : base(find)
            {
            }

            /// <summary>
            /// Gets template selector.
            /// </summary>
            public HtmlAnchor CreateTemplateButton
            {
                get
                {
                    return this.Get<HtmlAnchor>("id=?_createTemplateWidget_ctl00_ctl00_createATemplate");
                }
            }
        }
}