using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.WidgetTemplates
{
    /// <summary>
    /// WidgetTemplates Map
    /// </summary>
    public class WidgetTemplatesMap
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="WidgetTemplatesMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public WidgetTemplatesMap(Find find)
        {
            this.find = find;
        }
 
        /// <summary>
        /// Gets the widget templates grid.
        /// </summary>
        /// <value>The widget templates grid.</value>
        public WidgetTemplatesGrid WidgetTemplatesGrid
        {
            get
            {
                return new WidgetTemplatesGrid(this.find);
            }
        }

        /// <summary>
        /// Gets the widget templates create frame screen.
        /// </summary>
        /// <value>The widget templates create frame screen.</value>
        public WidgetTemplatesCreateScreenFrame WidgetTemplatesCreateScreenFrame
        {
            get
            {
                return new WidgetTemplatesCreateScreenFrame(this.find);
            }
        }

        private Find find;
    }
}
