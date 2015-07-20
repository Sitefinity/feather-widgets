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
    /// UnsubscribeWidgetOnFoundationAndSemanticPage arrangement class.
    /// </summary>
    public class UnsubscribeWidgetOnFoundationAndSemanticPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateIdSemantic = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameSemantic);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageNameSemantic, templateIdSemantic);          

            Guid templateIdFoundation = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameFoundation);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageNameFoundation, templateIdFoundation);

            Guid mailingListId = Guid.NewGuid();
            Guid subscriberId = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(mailingListId, MailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId, SubscriberFirstName, SubscriberLastName, SubscriberEmail);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId, mailingListId);

            Guid mailingListId2 = Guid.NewGuid();
            Guid subscriberId2 = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(mailingListId2, SecondMailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId2, SubscriberFirstName2, SubscriberLastName2, SubscriberEmail2);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId2, mailingListId2);
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

        private const string PageNameSemantic = "UnsubscribePageSemantic";
        private const string PageNameFoundation = "UnsubscribeFoundation";
        private const string PageTemplateNameSemantic = "SemanticUI.default";
        private const string PageTemplateNameFoundation = "Foundation.default";
        private const string MailingList = "MailList";
        private const string SecondMailingList = "SecondMailList";
        private const string SubscriberFirstName = "FirstName";
        private const string SubscriberLastName = "LastName";
        private const string SubscriberEmail = "test@email.com";
        private const string SubscriberFirstName2 = "FirstName2";
        private const string SubscriberLastName2 = "LastName2";
        private const string SubscriberEmail2 = "test2@email.com";
    }
}
