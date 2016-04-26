using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;
using SitefinityTestUtilities = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.EmailCampaigns
{
    [TestFixture]
    public class SubscribeFormTests
    {
        [Test]
        [Category(TestCategories.EmailCampaigns)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether user can inject parameters through view model in post action.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public void SubscribeForm_CheckForParamInjections()
        {
            this.pageOperations = new PagesOperations();
            this.newslettersManager = NewslettersManager.GetManager();
            Guid mailingListId = Guid.NewGuid();
            var subscribers = this.newslettersManager.GetSubscribers().Where(s => s.Email == this.testEmail).ToList();
            bool deleteSubsciber = (subscribers.Count > 0) ? false : true;
 
            try
            {
                ////Create a new mailing list
                string mailingListName = this.mailingListBaseName + this.mailingListIndex;
                SitefinityTestUtilities.ServerOperations.NewsLetter().CreateMailingList(mailingListId, mailingListName, string.Empty, string.Empty, string.Empty);

                ////Check if the mailing list was created successfully
                MailingList mailingList = this.newslettersManager.GetMailingLists().Where(l => l.Id == mailingListId).SingleOrDefault();
                Assert.IsNotNull(mailingList, "New mailing list was not created successfully!");

                ////Create simple page with a SubscribeForm widget that has SelectedMailingListId with the newly created mailing list
                var mvcControllerProxy = new MvcControllerProxy();
                mvcControllerProxy.ControllerName = typeof(SubscribeFormController).FullName;
                var subscribeFormController = new SubscribeFormController();
                subscribeFormController.Model.SelectedMailingListId = mailingListId;
                subscribeFormController.Model.SuccessfullySubmittedForm = SuccessfullySubmittedForm.OpenSpecificPage;

                mvcControllerProxy.Settings = new ControllerSettings(subscribeFormController);
                this.pageOperations.CreatePageWithControl(
                    mvcControllerProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                ////Create first simple page with a content block to redirect on it
                mvcControllerProxy.ControllerName = typeof(ContentBlockController).FullName;
                var contentBlockController = new ContentBlockController();
                contentBlockController.Content = this.searchValueText;
                mvcControllerProxy.Settings = new ControllerSettings(contentBlockController);
                this.pageOperations.CreatePageWithControl(
                    mvcControllerProxy, this.pageNamePrefixContentBlockPage, this.pageTitlePrefixContentBlockPage, this.urlNamePrefixContentBlockPage, this.pageIndexContentBlockPage);
 
                string subscribeFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                string redirectUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockPage);
                string postString = "Email=" + this.testEmail;

                ////Make an initial request to register the subscriber
                var responseContent = PageInvoker.PostWebRequest(subscribeFormPageUrl, postString, false);
                Assert.IsTrue(responseContent.Contains(this.subscribeValueText), "User was not successfully subscribed!");

                ////Make a secondary request to inject the RedirectPageUrl value
                postString  += "&RedirectPageUrl=" + redirectUrl;
                responseContent = PageInvoker.PostWebRequest(subscribeFormPageUrl, postString, false);

                Assert.IsFalse(responseContent.Contains(this.searchValueText), "RedirectPageUrl parameter was injected into the model!");
            }
            finally
            {
                ////Delete created pages
                this.pageOperations.DeletePages();

                ////Delete the created subsciber if he was created by the test
                if (deleteSubsciber)
                {
                    Subscriber subscriber = this.newslettersManager.GetSubscribers().Where(s => s.Email == this.testEmail).FirstOrDefault();
                    if (subscriber != null)
                    {
                        this.newslettersManager.DeleteSubscriber(subscriber.Id);
                    }
                }

                ////Delete created mailing list
                this.newslettersManager.DeleteMailingList(mailingListId);
                this.newslettersManager.SaveChanges();
            }
        }

        private string mailingListBaseName = "MailListTest";
        private int mailingListIndex = 1;

        private string pageNamePrefix = "SubscribeFormPage";
        private string pageTitlePrefix = "Subscribe Form";
        private string urlNamePrefix = "subscribe-block";
        private int pageIndex = 1;
        private string subscribeValueText = "Thank you";

        private string pageNamePrefixContentBlockPage = "ContentBlockPage";
        private string pageTitlePrefixContentBlockPage = "Content Block";
        private string urlNamePrefixContentBlockPage = "content-block";
        private int pageIndexContentBlockPage = 1;
        private string searchValueText = "Redirect Page Text";

        private string testEmail = "test@test.com";

        private PagesOperations pageOperations;
        private NewslettersManager newslettersManager;
    }
}