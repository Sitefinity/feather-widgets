using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather subscribe form and unsubscribe widgets.
    /// </summary>
    public class EmailCampaignsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailCampaignsMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public EmailCampaignsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the subscribe form frontend
        /// </summary>
        public SubscribeFormFrontend SubscribeFormFrontend
        {
            get
            {
                return new SubscribeFormFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the subscribe form edit screen
        /// </summary>
        public SubscribeFormEditScreen SubscribeFormEditScreen
        {
            get
            {
                return new SubscribeFormEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the unsubscribe edit screen
        /// </summary>
        public UnsubscribeEditScreen UnsubscribeEditScreen
        {
            get
            {
                return new UnsubscribeEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the unsubscribe frontend
        /// </summary>
        public UnsubscribeFrontend UnsubscribeFrontend
        {
            get
            {
                return new UnsubscribeFrontend(this.find);
            }
        }

        private Find find;
    }
}
