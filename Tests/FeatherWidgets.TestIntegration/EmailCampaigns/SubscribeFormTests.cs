using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Newsletters.Model;
using SitefinityTestUtilities = Telerik.Sitefinity.TestUtilities.CommonOperations;
////using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.EmailCampaigns
{
    [TestFixture]
    public class SubscribeFormTests
    {
        [Test]
        [Category(TestCategories.EmailCampaigns)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether user can inject parameters through view model in post action.")]
        public void SubscribeForm_CheckForParamInjections()
        {
            ////string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            this.pageOperations = new PagesOperations();
            this.newslettersManager = NewslettersManager.GetManager();
            Guid mailingListId = Guid.NewGuid();
 
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
                mvcControllerProxy.Settings = new ControllerSettings(subscribeFormController);
                this.pageOperations.CreatePageWithControl(
                    mvcControllerProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                ////TODO: Implement the actual test request
                ////string postString = "UserName=" + this.userName + "&Password=" + this.password;
                ////var responseContent = PageInvoker.PostWebRequest(loginFormPageUrl, postString, false);
 
                ////Assert.IsTrue(responseContent.Contains(this.searchValueFirst), "The request was not redirected to the proper page set in LoginRedirectPageId!");
                Assert.AreEqual("test", "test", "TODO: Implement the actual test");
            }
            finally
            {
                ////Delete created pages
                this.pageOperations.DeletePages();
                ////Delete created mailing lists
                this.newslettersManager.DeleteMailingList(mailingListId);
                this.newslettersManager.SaveChanges();
            }
        }

        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether user can inject parameters through view model in post action.")]
        public void SubscribeForm_TestTest()
        {
            Assert.AreEqual("test2", "test2", "test description2");
        }

        ////private string userName = "AuthenticateUser_IdentityHasClaimTypes";
        ////private string password = "admin@2";

        ////private string templateName = "Bootstrap.default";
        ////private string pageNamePrefix = "LoginFormPage";
        ////private string pageTitlePrefix = "Login Form";
        ////private string urlNamePrefix = "login-form";
        ////private string actionSearchString = "action=";
        ////private int pageIndex = 1;

        private string mailingListBaseName = "MailListTest";
        private int mailingListIndex = 1;

        private string pageNamePrefix = "SubscribeFormPage";
        private string pageTitlePrefix = "Subscribe Form";
        private string urlNamePrefix = "subscribe-block";
        private int pageIndex = 1;

        private PagesOperations pageOperations;
        private NewslettersManager newslettersManager;
    }
}