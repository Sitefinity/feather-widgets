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
    /// SubscribeFormWidgetOnFoundationAndSemanticPage arrangement class.
    /// </summary>
    public class SubscribeFormWidgetOnFoundationAndSemanticPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateIdSemantic = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameSemantic);
            Guid pageIdSubscribeSemantic = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageNameSemantic, templateIdSemantic);
            pageIdSubscribeSemantic = ServerOperations.Pages().GetPageNodeId(pageIdSubscribeSemantic);
            ServerOperationsFeather.Pages().AddSubscribeFormWidgetToPage(pageIdSubscribeSemantic, PlaceHolderId);

            Guid templateIdFoundation = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateNameFoundation);
            Guid pageIdSubscribeFoundation = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageNameFoundation, templateIdFoundation);
            pageIdSubscribeFoundation = ServerOperations.Pages().GetPageNodeId(pageIdSubscribeFoundation);
            ServerOperationsFeather.Pages().AddSubscribeFormWidgetToPage(pageIdSubscribeFoundation, PlaceHolderId);

            Guid mailingListId = Guid.NewGuid();
            Guid subscriberId = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(mailingListId, MailingList, string.Empty, string.Empty, string.Empty);
            ServerOperations.NewsLetter().CreateSubscriber(subscriberId, SubscriberFirstName, SubscriberLastName, SubscriberEmail);
            ServerOperations.NewsLetter().AddSubscriberToMailingList(subscriberId, mailingListId);
        }

        /// <summary>
        /// Delete mailing list
        /// </summary>
        [ServerArrangement]
        public void DeleteMailingList()
        {
            ServerOperations.NewsLetter().DeleteAllSubscribers();
            ServerOperations.NewsLetter().DeleteAllMailingLists();
            Guid secondMailingListId = Guid.NewGuid();
            ServerOperations.NewsLetter().CreateMailingList(secondMailingListId, SecondMailingList, string.Empty, string.Empty, string.Empty);
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

        private const string PageNameSemantic = "SubscribeFormPageSemantic";
        private const string PageNameFoundation = "SubscribeFormPageFoundation";
        private const string PageTemplateNameSemantic = "SemanticUI.default";
        private const string PageTemplateNameFoundation = "Foundation.default";
        private const string MailingList = "MailList";
        private const string SecondMailingList = "SecondMailList";
        private const string SubscriberFirstName = "FirstName";
        private const string SubscriberLastName = "LastName";
        private const string SubscriberEmail = "test@email.com";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
