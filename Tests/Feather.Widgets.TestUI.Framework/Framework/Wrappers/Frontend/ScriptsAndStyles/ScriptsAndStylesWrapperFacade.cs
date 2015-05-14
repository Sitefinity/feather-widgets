using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.ScriptsAndStyles
{
    /// <summary>
    /// This is the entry point class for css widget frontend wrappers.
    /// </summary>
    public class ScriptsAndStylesWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ScriptsAndStylesWrapper 
        /// </summary>
        /// <returns>Returns the ScriptsAndStylesWrapper</returns>
        public ScriptsAndStylesWrapper ScriptsAndStylesWrapper()
        {
            return new ScriptsAndStylesWrapper();
        }
    }
}
