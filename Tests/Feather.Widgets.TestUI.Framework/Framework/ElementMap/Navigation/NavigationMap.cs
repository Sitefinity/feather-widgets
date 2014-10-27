using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather navigation widget back-end screens.
    /// </summary>
    public class NavigationMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NavigationMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the navigation widget backend
        /// </summary>
        public NavigationWidgetEditScreen NavigationWidgetEditScreen
        {
            get
            {
                return new NavigationWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the navigation widget frontend.
        /// </summary>
        public NavigationWidgetFrontend NavigationWidgetFrontend
        {
            get
            {
                return new NavigationWidgetFrontend(this.find);
            }
        }

        private Find find;
    }
}
