using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Facade for Widgets and selectors wrappers
    /// </summary>
    public class WidgetsFacade
    {
        /// <summary>
        /// Widgets content screen wrapper.
        /// </summary>
        /// <returns></returns>
        public WidgetDesignerContentScreenWrapper WidgetDesignerContentScreenWrapper()
        {
            return new WidgetDesignerContentScreenWrapper();
        }

        /// <summary>
        ///  Selectors wrapper.
        /// </summary>
        /// <returns></returns>
        public SelectorsWrapper SelectorsWrapper()
        {
            return new SelectorsWrapper();
        }
    }
}
