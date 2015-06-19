using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.UnsubscribeForm
{
    public class DummyUnsubscribeFormModel : IUnsubscribeFormModel
    {
        public Guid ListId { get; set; }

        public string ProviderName { get; set; }

        public string WidgetTitle { get; set; }

        public string WidgetDescription { get; set; }

        public string Message { get; set; }

        public UnsubscribeMode UnsubscribeMode { get; set; }

        public Guid PageId { get; set; }

        public SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }

        public string LinkCssClass { get; set; }

        public string EmailAddressCssClass { get; set; }

        public UnsubscribeFormViewModel CreateViewModel()
        {
            return new UnsubscribeFormViewModel();
        }

        public void ExecuteAction(string subscriberId, string issueId, bool shouldSubscribe)
        {
        }

        public bool Unsubscribe(UnsubscribeFormViewModel viewModel, out string error)
        {
            error = string.Empty;
            return true;
        }
    }
}
