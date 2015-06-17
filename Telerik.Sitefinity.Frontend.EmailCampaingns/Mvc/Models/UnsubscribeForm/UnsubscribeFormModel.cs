using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// This class represents the model used for Unsubscribe widget.
    /// </summary>
    public class UnsubscribeFormModel : IUnsubscribeFormModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeFormModel" /> class.
        /// </summary>
        public UnsubscribeFormModel()
        {
            this.message = Res.Get<UnsubscribeFormResources>().UnsubscribeMessageOnSuccess;
            this.widgetTitle = Res.Get<UnsubscribeFormResources>().UnsubscribeWidgetTitle;
            this.widgetDescription = Res.Get<UnsubscribeFormResources>().UnsubscribeWidgetDescription;
        }

        #region Properties

        /// <inheritDoc/>
        public Guid ListId { get; set; }

        /// <inheritDoc/>
        public string ProviderName { get; set; }

        /// <inheritDoc/>
        public string WidgetTitle
        {
            get
            {
                return this.widgetTitle;
            }
            set
            {
                this.widgetTitle = value;
            }
        }
        
        /// <inheritDoc/>
        public string WidgetDescription
        {
            get
            {
                return this.widgetDescription;
            }

            set
            {
                this.widgetDescription = value;
            }
        }
        
        /// <inheritDoc/>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
            }
        }

        /// <inheritDoc/>
        public UnsubscribeMode UnsubscribeMode { get; set; }

        /// <inheritDoc/>
        public SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }

        /// <inheritDoc/>
        public Guid PageId { get; set; }

        /// <inheritDoc/>
        public string LinkCssClass { get; set; }

        /// <inheritDoc/>
        public string EmailAddressCssClass { get; set; }

        #endregion

        #region Public methods

        /// <inheritDoc/>
        public UnsubscribeFormViewModel CreateViewModel()
        {
            var viewModel = new UnsubscribeFormViewModel();

            viewModel.Message = this.message;

            if (this.UnsubscribeMode == UnsubscribeMode.Link)
            {
                viewModel.CssClass = this.LinkCssClass;
            }
            else
            {
                viewModel.WidgetTitle = this.WidgetTitle;
                viewModel.WidgetDescription = this.WidgetDescription;
                viewModel.CssClass = this.EmailAddressCssClass;
            }

            return viewModel;
        }

        /// <inheritDoc/>
        public virtual void ExecuteAction(string subscriberId, string issueId, bool shouldSubscribe)
        {
            var newslettersManager = NewslettersManager.GetManager(this.ProviderName);

            var issueGuid = Guid.Empty;
            var subscriberGuid = Guid.Empty;

            if (!string.IsNullOrEmpty(subscriberId) && Guid.TryParse(subscriberId, out subscriberGuid)
                && (!string.IsNullOrEmpty(issueId) && Guid.TryParse(issueId, out issueGuid)))
            {
                Guid mailingListId = Guid.Empty;
                var issue = newslettersManager.GetIssue(issueGuid);
                if (issue != null)
                {
                    mailingListId = issue.List.Id;
                }

                var subscriber = newslettersManager.GetSubscriber(subscriberGuid);

                if (shouldSubscribe)
                {
                    this.Subscribe(newslettersManager, subscriber, mailingListId);
                }
                else
                {
                    this.Unsubscribe(newslettersManager, subscriber, mailingListId, issue);
                }
            }
        }

        /// <inheritDoc/>
        public virtual bool Unsubscribe(string email, out string error)
        {
            error = string.Empty;

            var newslettersManager = NewslettersManager.GetManager(this.ProviderName);

            email = email.ToLower();
            IQueryable<Subscriber> subscribers = newslettersManager.GetSubscribers().Where(s => s.Email == email);

            if (subscribers.Count() == 0)
            {
                error = Res.Get<UnsubscribeFormResources>().YouDontBelongToTheMailingList;
                return false;
            }

            var hasUnsubscribedUser = false;

            foreach (Subscriber subscriber in subscribers)
            {
                if (subscriber != null)
                {
                    var isUnsubscribed = newslettersManager.Unsubscribe(subscriber, this.ListId);
                    hasUnsubscribedUser = hasUnsubscribedUser || isUnsubscribed;
                }
            }

            if (hasUnsubscribedUser)
            {
                newslettersManager.SaveChanges();
                ////TODO: momchi asked for different message (see wireframes). is it possible?
                this.Message = Res.Get<UnsubscribeFormResources>().UnsubscribedFromMailingListSuccessMessage;
                return true;
            }
            else
            {
                error = Res.Get<UnsubscribeFormResources>().YouDontBelongToTheMailingList;
                return false;
            }
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Subscribes by the provided in the query string subscriber id and issue id.
        /// </summary>
        /// <param name="newslettersManager">The newsletters manager.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="mailingListId">The issue's mailing list id.</param>
        private void Subscribe(NewslettersManager newslettersManager, Subscriber subscriber, Guid mailingListId)
        {
            // check if the user is already subscribed for the mailing list.
            if (!subscriber.Lists.Any(ml => ml.Id == mailingListId))
            {
                bool isSubscribed = newslettersManager.Subscribe(subscriber, mailingListId);
                if (isSubscribed)
                {
                    newslettersManager.SaveChanges();
                    this.Message = Res.Get<UnsubscribeFormResources>().SubscribeSuccessful;
                }
            }
            else
            {
                this.Message = Res.Get<UnsubscribeFormResources>().EmailExistsInTheMailingList;
            }
        }

        /// <summary>
        /// Unsubscribes by the provided in the query string subscriber id and issue id.
        /// </summary>
        /// <param name="newslettersManager">The newsletters manager.</param>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="mailingListId">The mailing list id.</param>
        /// <param name="issue">The issue.</param>
        private void Unsubscribe(NewslettersManager newslettersManager, Subscriber subscriber, Guid mailingListId, Campaign issue)
        {
            this.Message = this.GetUnsubscribeSuccessfulMessage(subscriber, issue);

            var isUnsubscribed = newslettersManager.Unsubscribe(subscriber, mailingListId, issue);
            if (isUnsubscribed)
            {
                newslettersManager.SaveChanges();
            }
        }

        /// <summary>
        /// Resolves the unsubscribe successful message.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        private string GetUnsubscribeSuccessfulMessage(Subscriber subscriber, Campaign issue)
        {
            //resolves the merge tags
            var mergeContextItemsObject = new MergeContextItems();

            var pageUri = SystemManager.CurrentHttpContext.Request.Url.PathAndQuery;
            var subscribeAnchor = @"<a href=""{0}&subscribe={1}"">{2}</a>";
            mergeContextItemsObject.SubscribeLink = subscribeAnchor.Arrange(pageUri, true, Res.Get<UnsubscribeFormResources>().SubscribeLink);
            string resolvedMessageBody = Merger.MergeTags(this.Message, issue.List, issue, subscriber, mergeContextItemsObject);

            return resolvedMessageBody;
        }
        
        #endregion

        #region Private fields and constants
        private string message;
        private string widgetTitle;
        private string widgetDescription;
        #endregion

        /// <summary>
        /// Helper class for merge context
        /// </summary>
        /// <remarks></remarks>
        private class MergeContextItems
        {
            /// <summary>
            /// Gets or sets the subscribe link.
            /// </summary>
            public string SubscribeLink { get; set; }
        }
    }
}