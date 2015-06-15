using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Unsubscribe widget
    /// </summary>
    [ObjectInfo(typeof(UnsubscribeFormResources), ResourceClassId = "UnsubscribeFormResources", Title = "UnsubscribeFormResourcesTitle", Description = "UnsubscribeFormResourcesDescription")]
    public class UnsubscribeFormResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeFormResources"/> class.
        /// </summary>
        public UnsubscribeFormResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeFormResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public UnsubscribeFormResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Unsubscribe widget resources class.
        /// </summary>
        [ResourceEntry("UnsubscribeFormResourcesTitle",
            Value = "Unsubscribe widget resources",
            Description = "Title for the Unsubscribe widget resources class.",
            LastModified = "2015/06/15")]
        public string UnsubscribeFormResourcesTitle
        {
            get
            {
                return this["UnsubscribeFormResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Unsubscribe widget resources class.
        /// </summary>
        [ResourceEntry("UnsubscribeFormResourcesDescription",
            Value = "Localizable strings for the Unsubscribe widget.",
            Description = "Description for the Unsubscribe widget resources class.",
            LastModified = "2015/06/15")]
        public string UnsubscribeFormResourcesDescription
        {
            get
            {
                return this["UnsubscribeFormResourcesDescription"];
            }
        }

        /// <summary>
        /// Module Not Licensed message
        /// </summary>
        /// <value>Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.</value>
        [ResourceEntry("ModuleNotLicensed",
            Value = "Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.",
            Description = "Module Not Licensed message",
            LastModified = "2015/06/15")]
        public string ModuleNotLicensed
        {
            get
            {
                return this["ModuleNotLicensed"];
            }
        }

        /// <summary>
        /// Unsubscribe message shown on success.
        /// </summary>
        /// <value><strong>Unsubscribe successful</strong> <br/> You have been successfully unsubscribed from our newsletter ({|Subscriber.Email|}) <br /> If you change your mind you can {|MergeContextItems.SubscribeLink|} to our newsletter again. <br /> Thank you.</value>
        [ResourceEntry("UnsubscribeMessageOnSuccess",
            Value = "<strong>Unsubscribe successful</strong> <br/> You have been successfully unsubscribed from our newsletter ({|Subscriber.Email|}) <br /> If you change your mind you can {|MergeContextItems.SubscribeLink|} to our newsletter again. <br /> Thank you.",
            Description = "Unsubscribe message shown on success.",
            LastModified = "2015/06/15")]
        public string UnsubscribeMessageOnSuccess
        {
            get
            {
                return this["UnsubscribeMessageOnSuccess"];
            }
        }

        /// <summary>
        /// phrase: Unsubscribe successful
        /// </summary>
        /// <value>Unsubscribe successful</value>
        [ResourceEntry("SuccessfulUnsubscribe",
            Value = "Unsubscribe successful",
            Description = "phrase: Unsubscribe successful",
            LastModified = "2015/06/15")]
        public string SuccessfulUnsubscribe
        {
            get
            {
                return this["SuccessfulUnsubscribe"];
            }
        }
    }
}
