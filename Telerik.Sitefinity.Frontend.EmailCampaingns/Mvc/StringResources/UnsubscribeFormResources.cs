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
            Value = "You have been successfully unsubscribed from our newsletter ({|Subscriber.Email|}). \r\nIf you change your mind you can {|MergeContextItems.SubscribeLink|} to our newsletter again. \r\nThank you.",
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
        /// Message shown when the user is successfully subscribed.
        /// </summary>
        /// <value><strong>Thank you</strong> <br/> You have successfully subscribed to our newsletter ({0})</value>
        [ResourceEntry("SubscribeSuccessful",
            Value = "<strong>Thank you</strong> <br/> You have successfully subscribed to our newsletter ({0})",
            Description = "Message shown when the user is successfully subscribed.",
            LastModified = "2015/06/17")]
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
        /// word: Unsubscribe by
        /// </summary>
        /// <value>subscribe</value>
        [ResourceEntry("UnsubscribeBy",
            Value = "Unsubscribe by",
            Description = "word: Unsubscribe by",
            LastModified = "2015/06/15")]
        public string UnsubscribeBy
        {
            get
            {
                return this["UnsubscribeBy"];
            }
        }

        /// <summary>
        /// word: Emal
        /// </summary>
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
        /// word: Email address
        /// </summary>
        /// <value>Email address</value>
        [ResourceEntry("EmailAddress",
            Value = "Email address",
            Description = "word: Email address",
            LastModified = "2015/06/15")]
        public string EmailAddress
        {
            get
            {
                return this["EmailAddress"];
            }
        }

        /// <summary>
        /// word: Unsubscribe
        /// </summary>
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
        /// word: Link
        /// </summary>
        /// <value>Link</value>
        [ResourceEntry("Link",
            Value = "Link",
            Description = "word: Link",
            LastModified = "2015/06/15")]
        public string Link
        {
            get
            {
                return this["Link"];
            }
        }

        /// <summary>
        /// phrase: Email address is invalid
        /// </summary>
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
        /// word: included in newsletter
        /// </summary>
        /// <value>included in newsletter</value>
        [ResourceEntry("IncludedInNewsletter",
            Value = "included in newsletter",
            Description = "word: Link",
            LastModified = "2015/06/15")]
        public string IncludedInNewsletter
        {
            get
            {
                return this["IncludedInNewsletter"];
            }
        }

        #region Email address tab resources
        /// <summary>
        /// Gets phrase : Template
        /// </summary>
        [ResourceEntry("TemplateLabel",
            Value = "Template",
            Description = "The phrase: Template",
            LastModified = "2015/05/19")]
        public string TemplateLabel
        {
            get
            {
                return this["TemplateLabel"];
            }
        }

        /// <summary>
        /// Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.
        /// </summary>
        /// <value>{0} does not exist in our mailing list</value>
        [ResourceEntry("YouDontBelongToTheMailingList",
            Value = "{0} does not exist in our mailing list",
            Description = "Message displayed to user who tries to unsubscribe from the campaign he or she does not belong to.",
            LastModified = "2015/06/17")]
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
        /// <value><strong>Unsubscribe successful</strong> <br/>You have been successfully unsubscribed from our newsletter ({0}) <br/> Thank you.</value>
        [ResourceEntry("UnsubscribedFromMailingListSuccessMessage",
            Value = "<strong>Unsubscribe successful</strong> <br/>You have been successfully unsubscribed from our newsletter ({0}) <br/> Thank you.",
            Description = "Unsubscribe message shown when the user is successfully unsubscribed by email address.",
            LastModified = "2015/06/17")]
        public string UnsubscribedFromMailingListSuccessMessage
        {
            get
            {
                return this["UnsubscribedFromMailingListSuccessMessage"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/04/21")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Unsubscribe widget title shown when EmailAddress unsubscribe mode is selected.
        /// </summary>
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
        /// phrase: Mailing lists
        /// </summary>
        /// <value>Mailing lists</value>
        [ResourceEntry("MailingList",
            Value = "Mailing lists",
            Description = "phrase: Mailing lists",
            LastModified = "2015/01/12")]
        public string MailingList
        {
            get
            {
                return this["MailingList"];
            }
        }

        /// <summary>
        /// phrase: Unsubscribe users from selected mailing list
        /// </summary>
        /// <value>Unsubscribe users from selected mailing list</value>
        [ResourceEntry("UsersUnSubscribeLists",
            Value = "Unsubscribe users from selected mailing list",
            Description = "phrase: Unsubscribe users from selected mailing list",
            LastModified = "2015/01/12")]
        public string UsersUnSubscribeLists
        {
            get
            {
                return this["UsersUnSubscribeLists"];
            }
        }

        /// <summary>
        /// phrase: When the form is successfully submitted...
        /// </summary>
        /// <value>When the form is successfully submitted...</value>
        [ResourceEntry("SuccessfullySubmittedLabel",
            Value = "When the form is successfully submitted...",
            Description = "phrase: When the form is successfully submitted...",
            LastModified = "2015/01/12")]
        public string SuccessfullySubmittedLabel
        {
            get
            {
                return this["SuccessfullySubmittedLabel"];
            }
        }

        /// <summary>
        /// phrase: Show message
        /// </summary>
        /// <value>Show message</value>
        [ResourceEntry("ShowMessageLabel",
            Value = "Show message",
            Description = "phrase: Show message",
            LastModified = "2015/01/12")]
        public string ShowMessageLabel
        {
            get
            {
                return this["ShowMessageLabel"];
            }
        }

        /// <summary>
        /// phrase: Open a specially prepared page...
        /// </summary>
        /// <value>Open a specially prepared page...</value>
        [ResourceEntry("OpenSpecificPageLabel",
            Value = "Open a specially prepared page...",
            Description = "phrase: Open a specially prepared page...",
            LastModified = "2015/01/12")]
        public string OpenSpecificPageLabel
        {
            get
            {
                return this["OpenSpecificPageLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/21")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }
        #endregion

        #region Link tab resources
        /// <summary>
        /// word: Insert dynamic data
        /// </summary>
        /// <value>Insert dynamic data</value>
        [ResourceEntry("InsertDynamicData",
            Value = "Insert dynamic data",
            Description = "word: Insert dynamic data",
            LastModified = "2015/06/15")]
        public string InsertDynamicData
        {
            get
            {
                return this["InsertDynamicData"];
            }
        }

        /// <summary>
        /// word: Copy / paste in the text...
        /// </summary>
        /// <value>Copy / paste in the text...</value>
        [ResourceEntry("CopyPaste",
            Value = "Copy / paste in the text...",
            Description = "word: Copy / paste in the text...",
            LastModified = "2015/06/15")]
        public string CopyPaste
        {
            get
            {
                return this["CopyPaste"];
            }
        }

        /// <summary>
        /// word: to display...
        /// </summary>
        /// <value>to display...</value>
        [ResourceEntry("ToDsiplay",
            Value = "to display...",
            Description = "word: to display...",
            LastModified = "2015/06/15")]
        public string ToDsiplay
        {
            get
            {
                return this["ToDsiplay"];
            }
        }
        #endregion

        /// <summary>
        /// Unsubscribe widget description shown when EmailAddress unsubscribe mode is selected.
        /// </summary>
        /// <value>Unsubscribe from our email newsletter</value>
        [ResourceEntry("UnsubscribeWidgetDescription",
            Value = "Unsubscribe from our email newsletter",
            Description = "Unsubscribe widget description shown when EmailAddress unsubscribe mode is selected.",
            LastModified = "2015/06/17")]
        public string UnsubscribeWidgetDescription
        {
            get
            {
                return this["UnsubscribeWidgetDescription"];
            }
        }

        /// <summary>
        /// Message shown when the newsletters module is deactivated
        /// </summary>
        /// <value>This widget doesn't work, because Newsletters module has been deactivated.</value>
        [ResourceEntry("NewslettersModuleDeactivatedMessage",
            Value = "This widget doesn't work, because Newsletters module has been deactivated.",
            Description = "Message shown when the newsletters module is deactivated",
            LastModified = "2015/06/17")]
        public string NewslettersModuleDeactivatedMessage
        {
            get
            {
                return this["NewslettersModuleDeactivatedMessage"];
            }
        }

        /// <summary>
        /// word: Close
        /// </summary>
        /// <value>Close</value>
        [ResourceEntry("Close",
            Value = "Close",
            Description = "word: Close",
            LastModified = "2015/06/17")]
        public string Close
        {
            get
            {
                return this["Close"];
            }
        }
    }
}
