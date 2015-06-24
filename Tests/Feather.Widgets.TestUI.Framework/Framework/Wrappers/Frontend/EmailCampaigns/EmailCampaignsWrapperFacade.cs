using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.EmailCampaigns
{
    /// <summary>
    /// This is an entry point for subscribe form and unsubscribe wrappers for the frontend.
    /// </summary>
    public class EmailCampaignsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the SubscibeFormWrapper
        /// </summary>
        /// <returns>Returns the SubscibeFormWrapper</returns>
        public SubscibeFormWrapper SubscibeFormWrapper()
        {
            return new SubscibeFormWrapper();
        }
    }
}
