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
    /// This is the entry point class for unsubscribe on the frontend.
    /// </summary>
    public class UnsubscribeWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify unsubscribe message
        /// </summary>
        public void VerifyUnsubscribeMessageOnTheFrontend()
        {
            HtmlForm unSubscribeForm = this.EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeForm.AssertIsPresent("Unsubscribe form");
            bool isPresentSubscribe = unSubscribeForm.InnerText.Contains("Unsubscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = unSubscribeForm.InnerText.Contains("Unsubscribe from our email newsletter");
            Assert.IsTrue(isPresentMessage);
        }

        /// <summary>
        /// Press unsubscribe button
        /// </summary>
        public void ClickUnsubscribeButton()
        {
            HtmlButton unsubscribeButton = EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeButton
            .AssertIsPresent("Unsubscribe button");
            unsubscribeButton.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        ///  Verify successfully unsubscribed message
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullyUnsubscribedMessageOnTheFrontend(string email)
        {
            HtmlForm unsubscribeForm = this.EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeForm.AssertIsPresent("Unsubscribe form");
            bool isPresentSubscribe = unsubscribeForm.InnerText.Contains("You have been successfully unsubscribed from our newsletter (" + email + ")");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        ///  Verify not existing email address message
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifyNotExistingEmailMessageOnTheFrontend(string email)
        {
            HtmlDiv notExistingMail = this.EM.EmailCampaigns.UnsubscribeFrontend.NotExisting.AssertIsPresent("Not existing");
            bool isPresentSubscribe = notExistingMail.InnerText.Contains(email.ToLower() + " does not exist in our mailing list");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Verify unsubscribe message on hybrid page
        /// </summary>
        public void VerifyUnsubscribeMessageOnTheFrontendHybridPage()
        {
            HtmlDiv unSubscribeForm = this.EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeDiv.AssertIsPresent("Unsubscribe form");
            bool isPresentSubscribe = unSubscribeForm.InnerText.Contains("Unsubscribe");
            Assert.IsTrue(isPresentSubscribe);

            bool isPresentMessage = unSubscribeForm.InnerText.Contains("Unsubscribe from our email newsletter");
            Assert.IsTrue(isPresentMessage);
        }

        /// <summary>
        ///  Verify successfully unsubscribed message not styled
        /// </summary>
        /// <param name="email">Email</param>
        public void VerifySuccessfullyUnsubscribedMessageOnTheFrontendNotStyled(string email)
        {
            HtmlDiv unsubscribeForm = this.EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeDiv.AssertIsPresent("Unsubscribe form");
            bool isPresentSubscribe = unsubscribeForm.InnerText.Contains("You have been successfully unsubscribed from our newsletter (" + email + ")");
            Assert.IsTrue(isPresentSubscribe);
        }

        /// <summary>
        /// Press unsubscribe button not styled
        /// </summary>
        public void ClickUnsubscribeButtonNotStyled()
        {
            HtmlInputSubmit unsubscribeButton = EM.EmailCampaigns.UnsubscribeFrontend.UnsubscribeButtonHybridPage
            .AssertIsPresent("Unsubscribe button");
            unsubscribeButton.MouseClick();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }
    }
}
