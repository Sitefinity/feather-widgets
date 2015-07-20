using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeleteSelectedMailListInSubscribeFormWidget arrangement class.
    /// </summary>
    public class DeleteSelectedMailListAndPageInUnsubscribeWidget : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageIdSubscribe = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageIdSubscribe = ServerOperations.Pages().GetPageNodeId(pageIdSubscribe);
            ServerOperationsFeather.Pages().AddUnsubscribeWidgetToPage(pageIdSubscribe, PlaceHolderId);

            Guid mailingListId = Guid.NewGuid();
            Guid subscriberId = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(mailingListId, MailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId, SubscriberFirstName, SubscriberLastName, SubscriberEmail);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId, mailingListId);

            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(FirstPageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockContent, PlaceHolderId);
        }

        /// <summary>
        /// Delete mailing list and page
        /// </summary>
        [ServerArrangement]
        public void DeleteMailingListAndPage()
        {
            ServerOperations.NewsLetter().DeleteAllSubscribers();
            ServerOperations.NewsLetter().DeleteAllMailingLists();
            ServerOperations.Pages().DeletePage(FirstPageName);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(SecondPageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockContent, PlaceHolderId);
            Guid secondMailingListId = Guid.NewGuid();
            Guid subscriberId = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(secondMailingListId, SecondMailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId, SubscriberFirstName, SubscriberLastName, SubscriberEmail);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId, secondMailingListId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.NewsLetter().DeleteAllSubscribers();
            ServerOperations.NewsLetter().DeleteAllMailingLists();
        }

        private const string PageName = "UnsubscribeWidgetOnPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string MailingList = "MailList";
        private const string SecondMailingList = "SecondMailList";
        private const string SubscriberFirstName = "FirstName";
        private const string SubscriberLastName = "LastName";
        private const string SubscriberEmail = "test@email.com";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string FirstPageName = "FirstPage";
        private const string SecondPageName = "SecondPage";
        private const string ContentBlockContent = "You are redirect to second page";
    }
}
