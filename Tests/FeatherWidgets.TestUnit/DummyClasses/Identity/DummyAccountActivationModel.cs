using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    public class DummyAccountActivationModel : AccountActivationModel
    {
        public DummyAccountActivationModel()
        {
        }

        public string CssClass { get; set; }

        public string MembershipProvider { get; set; }

        public Guid? ProfilePageId { get; set; }

        public override AccountActivationViewModel GetViewModel()
        {
            return new AccountActivationViewModel();
        }
    }
}
