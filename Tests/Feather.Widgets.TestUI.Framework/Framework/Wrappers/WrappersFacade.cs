using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers
{
    public class WrappersFacade
    {
        /// <summary>
        /// Entry point to the backedn wrappers.
        /// </summary>
        /// <returns>BackendWrappersFacade instance.</returns>
        public BackendWrappersFacade Backend()
        {
            return new BackendWrappersFacade();
        }

        /// <summary>
        ///  Entry point to the frontend wrappers.
        /// </summary>
        /// <returns>FrontendWrappesFacade instance.</returns>
        public FrontendWrappesFacade Frontend()
        {
            return new FrontendWrappesFacade();
        }
    }
}
