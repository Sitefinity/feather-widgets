using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Widgets
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Edit Content Screen back-end screens.
    /// </summary>
    public class WidgetDesignerMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditContentScreenMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public WidgetDesignerMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the news widget backend
        /// </summary>
        public WidgetDesignerScreen WidgetDesignerContentScreen
        {
            get
            {
                return new WidgetDesignerScreen(this.find);
            }
        }

        private Find find;
    }
}
