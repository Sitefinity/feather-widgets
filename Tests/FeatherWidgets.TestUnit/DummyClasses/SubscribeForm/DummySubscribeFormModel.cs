using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    public class DummySubscribeFormModel : SubscribeFormModel
    {
        public override bool AddSubscriber(SubscribeFormViewModel viewModel, out string error)
        {
            error = string.Empty;

            return true;
        }
    }
}
