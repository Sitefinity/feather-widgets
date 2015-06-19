using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.UnsubscribeForm
{
    public class DummyUnsubscribeFormController : UnsubscribeFormController
    {
        private readonly bool isLicensed;
        private readonly bool isModuleActivated;

        public override string LicensingMessage
        {
            get
            {
                return "No valid license";
            }
        }

        public override string NewslettersModuleDeactivatedMessage
        {
            get
            {
                return "Not installed module";
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public DummyUnsubscribeFormController(bool isLicensed = true, bool isModuleActivated = true)
        {
            this.isLicensed = isLicensed;
            this.isModuleActivated = isModuleActivated;
        }

        protected override bool IsNewslettersModuleActivated()
        {
            return this.isModuleActivated;
        }

        public override bool IsLicensed
        {
            get
            {
                return this.isLicensed;
            }
        }

        public IUnsubscribeFormModel MockModel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUnit.DummyClasses.Media.DummyUnsubscribeFormModel.#ctor(System.String,System.String,System.String)")]
        public override IUnsubscribeFormModel Model
        {
            get
            {
                return this.MockModel ?? new DummyUnsubscribeFormModel { ListId = new Guid("51C6563A-C165-40B0-B950-E3F99CF1ED98") };
            }
        }
    }
}
