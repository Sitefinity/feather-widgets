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
        #region Properties
        
        /// <inheritDoc/>
        public Guid ListId { get; set; }
        
        /// <inheritDoc/>
        public string ProviderName { get; set; }
        
        /// <inheritDoc/>
        public string WidgetTitle { get; set; }
        
        /// <inheritDoc/>
        public string WidgetDescription { get; set; }
        
        /// <inheritDoc/>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                }
            }
        }
        
        /// <inheritDoc/>
        public UnsubscribeMode UnsubscribeMode { get; set; }
        
        /// <inheritDoc/>
        public string LinkCssClass { get; set; }
        
        /// <inheritDoc/>
        public string EmailAddressCssClass { get; set; }
        
        #endregion
        
        /// <inheritDoc/>
        public UnsubscribeFormViewModel CreateViewModel()
        {
            var viewModel = new UnsubscribeFormViewModel();
            
            if (this.UnsubscribeMode == UnsubscribeMode.Link)
            {
                viewModel.Message = this.message;
                viewModel.CssClass = this.LinkCssClass;


            }
            
            return viewModel;
        }


        public void RemoveSubscriber(string subscriberId, string issueId, string subscribe)
        {
            if (this.UnsubscribeMode == UnsubscribeMode.Link)
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
                    if (subscriber == null || mailingListId == Guid.Empty)
                    {
                        ////TODO: add error to the viewModel
                        ////this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().YouCannotUnsubscribe);
                        return;
                    }
                    else
                    {
                        if (subscribe != null)
                        {
                            this.Subscribe(newslettersManager, subscriber, mailingListId);
                        }
                        else
                        {
                            this.Unsubscribe(newslettersManager, subscriber, mailingListId, issue);
                        }
                    }
                }
            }
        }

        #region Private methods

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
            //resolves the merge tags
            var mergeContextItemsObject = new MergeContextItems();

            var pageUri = SystemManager.CurrentHttpContext.Request.Url.PathAndQuery;
            var subscribeAnchor = @"<a href=""{0}&subscribe={1}"">{2}</a>";
            mergeContextItemsObject.SubscribeLink = subscribeAnchor.Arrange(pageUri, true, Res.Get<UnsubscribeFormResources>().SubscribeLink);
            string resolvedMessageBody = Merger.MergeTags(this.Message, issue.List, issue, subscriber, mergeContextItemsObject);
            this.Message = resolvedMessageBody;

            var isUnsubscribed = newslettersManager.Unsubscribe(subscriber, mailingListId, issue);
            if (isUnsubscribed)
            {
                newslettersManager.SaveChanges();
            }
        }

        #endregion

        #region Private fields and constants

        private string message = Res.Get<UnsubscribeFormResources>().UnsubscribeMessageOnSuccess;

        #endregion

        /// <summary>
        /// Helper class for merge context
        /// </summary>
        /// <remarks></remarks>
        class MergeContextItems
        {
            /// <summary>
            /// Gets or sets the subscribe link.
            /// </summary>
            public string SubscribeLink { get; set; }
        }
    }
}