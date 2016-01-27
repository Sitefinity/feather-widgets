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

        /// <summary>
        /// Gets the categories widget edit screen.
        /// </summary>
        /// <value>The categories widget edit screen.</value>
        public CategoriesWidgetEditScreen CategoriesWidgetEditScreen
        {
            get
            {
                return new CategoriesWidgetEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the categories widget frontend.
        /// </summary>
        /// <value>The categories widget frontend.</value>
        public CategoriesWidgetFrontend CategoriesWidgetFrontend
        {
            get
            {
                return new CategoriesWidgetFrontend(this.find);
            }
        }

        private readonly Find find;
    }
}
