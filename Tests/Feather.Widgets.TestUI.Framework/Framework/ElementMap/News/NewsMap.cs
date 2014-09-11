using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.News
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather News widget back-end screens.
    /// </summary>
    public class NewsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public NewsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the news widget backend
        /// </summary>
        public NewsWidgetContentScreen NewsWidgetContentScreen
        {
            get
            {
                return new NewsWidgetContentScreen(this.find);
            }
        }

        private Find find;
    }
}
