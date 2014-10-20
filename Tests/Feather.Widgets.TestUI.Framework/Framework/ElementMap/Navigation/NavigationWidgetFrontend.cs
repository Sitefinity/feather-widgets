using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
    /// <summary>
    /// Elements from Navigation widget on the frontend.
    /// </summary>
    public class NavigationWidgetFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationWidgetFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NavigationWidgetFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the Toggle button on the frontend when toggle menu is present
        /// </summary>
        public HtmlButton ToggleButton
        {
            get
            {
                return this.Get<HtmlButton>("class=^navbar-toggle", "InnerText=~Toggle navigation");
            }
        }
    }
}
