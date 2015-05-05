using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Css
{
    /// <summary>
    /// This is the entry point class for css widget frontend wrappers.
    /// </summary>
    public class CssWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the CssWrapper 
        /// </summary>
        /// <returns>Returns the CssWrapper</returns>
        public CssWrapper CssWrapper()
        {
            return new CssWrapper();
        }
    }
}
