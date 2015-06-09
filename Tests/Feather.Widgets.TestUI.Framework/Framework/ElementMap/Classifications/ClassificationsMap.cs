using System;
using System.Linq;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Classifications
{
    /// <summary>
    /// Classifications Map
    /// </summary>
    public class ClassificationsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassificationsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ClassificationsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the tags widget edit screen.
        /// </summary>
        /// <value>The tags widget edit screen.</value>
        public TagsWidgetEditScreen TagsWidgetEditScreen
        {
            get
            {
                return new TagsWidgetEditScreen(this.find);
            }
        }

        private Find find;
    }
}
