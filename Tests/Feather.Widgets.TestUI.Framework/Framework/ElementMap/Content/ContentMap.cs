using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Content block widget back-end screens.
    /// </summary>
    public class ContentMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ContentMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the content block widget backend
        /// </summary>
        public ContentBlockWidgetScreen ContentBlockWidget
        {
            get
            {
                return new ContentBlockWidgetScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the content block link selector.
        /// </summary>
        /// <value>The content block link selector.</value>
        public ContentBlockLinkSelectorScreen ContentBlockLinkSelector
        {
            get
            {
                return new ContentBlockLinkSelectorScreen(this.find);
            }
        }

        private Find find;
    }
}
