using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models
{
    /// <summary>
    /// The model of the Email campaigns widget.
    /// </summary>
    public class EmailCampaignsModel : IEmailCampaignsModel
    {
        #region Properties
        /// <inheritdoc />
        public string Provider { get; set; }

        /// <inheritdoc />
        public Guid SelectedMailingListId { get; set; }

        /// <inheritdoc />
        public SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public Guid PageId { get; set; }
        #endregion

        #region Public and virtual methods

        /// <inheritdoc />
        public EmailCampaignsViewModel CreateViewModel()
        {
            return new EmailCampaignsViewModel
            {
                CssClass = this.CssClass
            };
        }

        /// <inheritdoc />
        public virtual bool AddSubscriber(EmailCampaignsViewModel viewModel, out string error)
        {
            error = string.Empty;

            if (NewsletterValidator.IsValidEmail(viewModel.Email))
            {
                var newslettersManager = NewslettersManager.GetManager(this.Provider);

                // check if subscriber exists
                var email = viewModel.Email.ToLower();
                IQueryable<Subscriber> matchingSubscribers = newslettersManager.GetSubscribers().Where(s => s.Email == email);
                bool subscriberAlreadyInList = false;
                foreach (Subscriber s in matchingSubscribers)
                {
                    if (s.Lists.Any(ml => ml.Id == this.SelectedMailingListId))
                    {
                        subscriberAlreadyInList = true;
                        break;
                    }
                }

                if (subscriberAlreadyInList)
                {
                    error = Res.Get<EmailCampaignsResources>().EmailExistsInTheMailingList;
                    return false;
                }
                else
                {
                    Subscriber subscriber = matchingSubscribers.FirstOrDefault();
                    if (subscriber == null)
                    {
                        subscriber = newslettersManager.CreateSubscriber(true);
                        subscriber.Email = viewModel.Email;
                        subscriber.FirstName = viewModel.FirstName;
                        subscriber.LastName = viewModel.LastName;
                    }

                    // check if the mailing list exists
                    if (newslettersManager.Subscribe(subscriber, this.SelectedMailingListId))
                    {
                        if (this.SuccessfullySubmittedForm == SuccessfullySubmittedForm.OpenSpecificPage)
                        {
                            this.GenerateRedirectPageUrl(viewModel);
                        }

                        newslettersManager.SaveChanges();

                        return true;
                    }
                }
            }
            error = Res.Get<EmailCampaignsResources>().EmailAddressErrorMessageResourceName;
            return false;
        }
        #endregion

        private void GenerateRedirectPageUrl(EmailCampaignsViewModel model)
        {
            var pageManager = PageManager.GetManager();
            var node = pageManager.GetPageNode(this.PageId);
            if (node != null)
            {
                var relativeUrl = node.GetFullUrl();
                model.RedirectPageUrl = UrlPath.ResolveUrl(relativeUrl, true);
            }
        }
    }
}
