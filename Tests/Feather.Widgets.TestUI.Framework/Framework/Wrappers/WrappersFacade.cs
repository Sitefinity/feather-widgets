using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers
{
    /// <summary>
    /// This is the entry point class for wrappers facade.
    /// </summary>
    public class WrappersFacade
    {
        /// <summary>
        /// Entry point to the backend wrappers.
        /// </summary>
        /// <returns>BackendWrappersFacade instance.</returns>
        public BackendWrappersFacade Backend()
        {
            return new BackendWrappersFacade();
        }

        /// <summary>
        ///  Entry point to the frontend wrappers.
        /// </summary>
        /// <returns>Frontend Wrappers Facade instance.</returns>
        public FrontendWrappesFacade Frontend()
        {
            return new FrontendWrappesFacade();
        }
    }
}
