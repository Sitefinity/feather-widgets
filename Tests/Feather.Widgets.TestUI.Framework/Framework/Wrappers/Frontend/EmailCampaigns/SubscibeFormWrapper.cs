using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.EmailCampaigns
{
    /// <summary>
    /// This is the entry point class for subscribe form on the frontend.
    /// </summary>
    public class SubscibeFormWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify subscribe message
        /// </summary>
        public void VerifySubscribeMessageOnTheFrontend()
        {
            HtmlForm subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeForm.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Subscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = subscribeForm.InnerText.Contains("Subscribe to our email newsletter to receive updates");
            Assert.IsTrue(isPresentMessage);
        }
    }
}
