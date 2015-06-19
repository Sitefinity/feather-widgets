using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList
{
    public class DummySubscribeFormController : SubscribeFormController
    {
        private readonly bool isLicensed;
        private readonly bool isModuleActivated;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private readonly bool isEmpty;

        public override string LicensingMessage
        {
            get
            {
                return "No valid license";
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public DummySubscribeFormController(
               bool isLicensed = true,
               bool isModuleActivated = true,
               bool isEmpty = false)
        {
            this.isLicensed = isLicensed;
            this.isModuleActivated = isModuleActivated;
            this.isEmpty = isEmpty;
        }

        protected override string GetResource(string resourceName)
        {
            return resourceName;
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

        public ISubscribeFormModel MockModel { get; set; }

        public override ISubscribeFormModel Model
        {
            get
            {
                return this.MockModel ?? new DummySubscribeFormModel { SelectedMailingListId = new Guid("51C6563A-C165-40B0-B950-E3F99CF1ED98") };
            }
        }
    }
}
