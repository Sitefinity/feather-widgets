using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.EmailCampaigns
{
    /// <summary>
    /// This is an entry point for subscribe form and unsubsribe wrappers for the backend.
    /// </summary>
    public class EmailCampaignsWrapperFacade
    {
        /// <summary>
        /// Provides access to SubscribeFormWrapper
        /// </summary>
        /// <returns>New instance of SubscribeFormWrapper</returns>
        public SubscribeFormWrapper SubscribeFormWrapper()
        {
            return new SubscribeFormWrapper();
        }

        /// <summary>
        /// Provides access to UnsubscribeWrapper
        /// </summary>
        /// <returns>New instance of UnsubscribeWrapper</returns>
        public UnsubscribeWrapper UnsubscribeWrapper()
        {
            return new UnsubscribeWrapper();
        }
    }
}
