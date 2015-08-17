using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.FeedWidget
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Feed widget back-end screens.
    /// </summary>
    public class FeedWidgetMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedWidgetMap" /> class.
        /// </summary>        
        public FeedWidgetMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the Feed widget backend
        /// </summary>
        public FeedWidgetFrontend FeedWidgetFrontend
        {
            get
            {
                return new FeedWidgetFrontend(this.find);
            }
        }

        private Find find;
    }
}
