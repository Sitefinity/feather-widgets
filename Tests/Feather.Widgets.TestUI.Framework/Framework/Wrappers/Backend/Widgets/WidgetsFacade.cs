using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    /// <summary>
    /// Facade for Widgets wrapper
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
    }
}
