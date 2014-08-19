using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation
{
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
        /// Gets the naviagtion widget backend
        /// </summary>
        public NavigationWidgetEditScreen NavigationWidgetEditScreen
        {
            get
            {
                return new NavigationWidgetEditScreen(this.find);
            }
        }

        private Find find;
    }
}
