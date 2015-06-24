using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

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

        /// <summary>
        ///  Verify successfully subscribed message
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullySubscribeMessageOnTheFrontend(string email)
        {
            HtmlForm subscribeForm = this.EM.EmailCampaigns.SubscribeFormFrontend.SubscribeForm.AssertIsPresent("Subscribe form");
            bool isPresentSubscribe = subscribeForm.InnerText.Contains("Thank you. You have successfully subscribed to our newsletter (" + email + ")");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Fill user email
        /// </summary>
        /// <param name="firstName">Email address</param>
        public void FillEmail(string email)
        {
            HtmlInputText emailInput = EM.EmailCampaigns.SubscribeFormFrontend.EmailAddressField
                .AssertIsPresent("Email field");

            emailInput.Text = string.Empty;
            emailInput.Text = email;
        }

        /// <summary>
        /// Press subscribe button
        /// </summary>
        public void ClickSubscribeButton()
        {
            HtmlButton subscribeButton = EM.EmailCampaigns.SubscribeFormFrontend.SubscribeButton
            .AssertIsPresent("Subscribe button");
            subscribeButton.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }
    }
}
