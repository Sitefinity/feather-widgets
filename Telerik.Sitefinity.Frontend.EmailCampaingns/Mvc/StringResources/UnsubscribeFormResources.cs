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
        /// phrase: Subscribe Successful
        /// </summary>
        /// <value>Subscribe Successful</value>
        [ResourceEntry("SubscribeSuccessful",
            Value = "Subscribe Successful",
            Description = "phrase: Subscribe Successful",
            LastModified = "2015/06/15")]
        public string SubscribeSuccessful
        {
            get
            {
                return this["SubscribeSuccessful"];
            }
        }

        /// <summary>
        /// phrase: A subscriber with given email is already subscribed to this mailing list.
        /// </summary>
        /// <value>A subscriber with the given email is already subscribed to this mailing list.</value>
        [ResourceEntry("EmailExistsInTheMailingList",
            Value = "A subscriber with the given email is already subscribed to this mailing list.",
            Description = "phrase: A subscriber with given email is already subscribed to this mailing list.",
            LastModified = "2015/06/15")]
        public string EmailExistsInTheMailingList
        {
            get
            {
                return this["EmailExistsInTheMailingList"];
            }
        }

        /// <summary>
        /// word: subscribe
        /// </summary>
        /// <value>subscribe</value>
        [ResourceEntry("SubscribeLink",
            Value = "subscribe",
            Description = "word: subscribe",
            LastModified = "2015/06/15")]
        public string SubscribeLink
        {
            get
            {
                return this["SubscribeLink"];
            }
        }

        /// <summary>
        /// word: Emal
        /// </summary>
        /// <value>Email</value>
        [ResourceEntry("Email",
            Value = "Email",
            Description = "word: Emal",
            LastModified = "2015/06/16")]
        public string Email
        {
            get
            {
                return this["Email"];
            }
        }

        /// <summary>
        /// word: Unsubscribe
        /// </summary>
        /// <value>Unsubscribe</value>
        [ResourceEntry("ButtonUnsubscribe",
            Value = "Unsubscribe",
            Description = "word: Unsubscribe",
            LastModified = "2015/06/16")]
        public string ButtonUnsubscribe
        {
            get
            {
                return this["ButtonUnsubscribe"];
            }
        }

        /// <summary>
        /// phrase: Email address is invalid
        /// </summary>
        /// <value>Email address is invalid</value>
        [ResourceEntry("EmailAddressErrorMessageResourceName",
            Value = "Email address is invalid",
            Description = "phrase: Email address is invalid",
            LastModified = "2015/06/16")]
        public string EmailAddressErrorMessageResourceName
        {
            get
            {
                return this["EmailAddressErrorMessageResourceName"];
            }
        }

        /// <summary>
        /// Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.
        /// </summary>
        /// <value>You don't belong to the mailing list and cannot unsubscribe.</value>
        [ResourceEntry("YouDontBelongToTheMailingList",
            Value = "You don't belong to the mailing list and cannot unsubscribe.",
            Description = "Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.",
            LastModified = "2015/06/16")]
        public string YouDontBelongToTheMailingList
        {
            get
            {
                return this["YouDontBelongToTheMailingList"];
            }
        }

        /// <summary>
        /// Unsubscribe message shown when the user is successfully unsubscribed by email address.
        /// </summary>
        /// <value><strong>Unsubscribe successful</strong> <br/> You have successfully unsubscribed from the mailing list. You will no longer receive the newsletters sent to this mailing list.</value>
        [ResourceEntry("UnsubscribedFromMailingListSuccessMessage",
            Value = "<strong>Unsubscribe successful</strong> <br/> You have successfully unsubscribed from the mailing list. You will no longer receive the newsletters sent to this mailing list.",
            Description = "Unsubscribe message shown when the user is successfully unsubscribed by email address.",
            LastModified = "2015/06/16")]
        public string UnsubscribedFromMailingListSuccessMessage
        {
            get
            {
                return this["UnsubscribedFromMailingListSuccessMessage"];
            }
        }

        /// <summary>
        /// Unsubscribe widget title shown when EmailAddress unsubscribe mode is selected.
        /// </summary>
        /// <value>Unsubscribe</value>
        [ResourceEntry("UnsubscribeWidgetTitle",
            Value = "Unsubscribe",
            Description = "Unsubscribe widget title shown when EmailAddress unsubscribe mode is selected.",
            LastModified = "2015/06/16")]
        public string UnsubscribeWidgetTitle
        {
            get
            {
                return this["UnsubscribeWidgetTitle"];
            }
        }

        /// <summary>
        /// Unsubscribe widget description shown when EmailAddress unsubscribe mode is selected.
        /// </summary>
        /// <value>Unsubscribe from our mailing list</value>
        [ResourceEntry("UnsubscribeWidgetDescription",
            Value = "Unsubscribe from our mailing list",
            Description = "Unsubscribe widget description shown when EmailAddress unsubscribe mode is selected.",
            LastModified = "2015/06/16")]
        public string UnsubscribeWidgetDescription
        {
            get
            {
                return this["UnsubscribeWidgetDescription"];
            }
        }
    }
}
