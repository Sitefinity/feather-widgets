using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DragAndDropSubscribeFormWidgetOnPageAndSelectMailingList arrangement class.
    /// </summary>
    public class DragAndDropSubscribeFormWidgetOnPageAndSelectMailingList : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);

            Guid mailingListId = Guid.NewGuid();
            Guid subscriberId = Guid.NewGuid();

            ServerOperations.NewsLetter().CreateMailingList(mailingListId, MailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId, SubscriberFirstName, SubscriberLastName, SubscriberEmail);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId, mailingListId);
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

        private const string PageName = "SubscribeFormPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string MailingList = "MailList";
        private const string SubscriberFirstName = "FirstName";
        private const string SubscriberLastName = "LastName";
        private const string SubscriberEmail = "test@email.com";
    }
}
