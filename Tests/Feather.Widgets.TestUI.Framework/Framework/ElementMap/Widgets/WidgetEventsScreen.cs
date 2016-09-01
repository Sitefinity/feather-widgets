using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Widgets
{
    public class WidgetEventsScreen : WidgetDesignerScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetEventsScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public WidgetEventsScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets List Settings tab  
        /// </summary>
        public HtmlAnchor ListSettings
        {
            get
            {
                return this.Get<HtmlAnchor>("class=~ng-binding", "Innertext=List settings");
            }
        }

        /// <summary>
        /// Gets the List Template drop down
        /// </summary>
        public HtmlSelect ListTemplateSelector
        {
            get
            {
                return this.Get<HtmlSelect>("TagName=select", "id=listTemplateName");
            }
        }
    }
}