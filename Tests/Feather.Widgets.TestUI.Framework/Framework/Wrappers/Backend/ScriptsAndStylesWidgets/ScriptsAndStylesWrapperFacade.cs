using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Css
{
    /// <summary>
    /// This is the entry point class for ScriptsAndStyles widgets backend wrappers.
    /// </summary>
    public class ScriptsAndStylesWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the CssWidgetEditWrapper 
        /// </summary>
        /// <returns>Returns the CssWidgetEditWrapper</returns>
        public CssWidgetEditWrapper CssWidgetEditWrapper()
        {
            return new CssWidgetEditWrapper();
        }

        /// <summary>
        /// Javas the script widget edit wrapper.
        /// </summary>
        /// <returns></returns>
        public JavaScriptWidgetEditWrapper JavaScriptWidgetEditWrapper()
        {
            return new JavaScriptWidgetEditWrapper();
        }
    }
}
