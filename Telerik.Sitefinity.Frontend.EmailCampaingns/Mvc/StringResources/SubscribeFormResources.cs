using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("SubscribeFormResources",
        ResourceClassId = "SubscribeFormResources",
        Title = "SubscribeFormResourcesTitle",
        TitlePlural = "SubscribeFormResourcesTitlePlural",
        Description = "SubscribeFormResourcesDescription")]
    public class SubscribeFormResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="SubscribeFormResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public SubscribeFormResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="SubscribeFormResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public SubscribeFormResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>SubscribeFormResources labels</value>
        [ResourceEntry("SubscribeFormResourcesTitle",
            Value = "SubscribeFormResourcesTitle labels",
            Description = "The title of this class.",
            LastModified = "2015/05/19")]
        public string SubscribeFormResourcesTitle
        {
            get
            {
                return this["SubscribeFormResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>SubscribeFormResources labels</value>
        [ResourceEntry("SubscribeFormResourcesTitlePlural",
            Value = "SubscribeFormResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/05/19")]
        public string SubscribeFormResourcesTitlePlural
        {
            get
            {
                return this["SubscribeFormResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("SubscribeFormResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/05/19")]
        public string SubscribeFormResourcesDescription
        {
            get
            {
                return this["SubscribeFormResourcesDescription"];
            }
        }
        #endregion

        #region Resources
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
        /// phrase: Subscribe users to selected mailing lists
        /// </summary>
        /// <value>Subscribe users to selected mailing lists</value>
        [ResourceEntry("UsersSubscribeLists",
            Value = "Subscribe users to selected mailing lists",
            Description = "phrase: Subscribe users to selected mailing lists",
            LastModified = "2015/01/12")]
        public string UsersSubscribeLists
        {
            get
            {
                return this["UsersSubscribeLists"];
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
        /// phrase: The selected mailing list has been deleted.
        /// </summary>
        /// <value>The selected mailing list has been deleted.</value>
        [ResourceEntry("MissingMailingList",
            Value = "The selected mailing list has been deleted.",
            Description = "phrase: The selected mailing list has been deleted.",
            LastModified = "2015/01/12")]
        public string MissingMailingList
        {
            get
            {
                return this["MissingMailingList"];
            }
        }

        /// <summary>
        /// phrase: Select a mailing list
        /// </summary>
        /// <value>Select a mailing list</value>
        [ResourceEntry("EmptyLinkText",
            Value = "Select a mailing list",
            Description = "phrase: Select a mailing list",
            LastModified = "2015/01/12")]
        public string EmptyLinkText
        {
            get
            {
                return this["EmptyLinkText"];
            }
        }

        /// <summary>
        /// phrase: Subscribe
        /// </summary>
        /// <value>Subscribe</value>
        [ResourceEntry("Subscribe",
            Value = "Subscribe",
            Description = "phrase: Subscribe",
            LastModified = "2015/01/12")]
        public string Subscribe
        {
            get
            {
                return this["Subscribe"];
            }
        }

        /// <summary>
        /// phrase: Subscribe to our email newsletter to receive updates
        /// </summary>
        /// <value>Subscribe to our email newsletter to receive updates</value>
        [ResourceEntry("SubscribeMail",
            Value = "Subscribe to our email newsletter to receive updates",
            Description = "phrase: Subscribe to our email newsletter to receive updates",
            LastModified = "2015/01/12")]
        public string SubscribeMail
        {
            get
            {
                return this["SubscribeMail"];
            }
        }

        /// <summary>
        /// phrase: First name.
        /// </summary>
        /// <value>First name.</value>
        [ResourceEntry("FirstName",
            Value = "First name",
            Description = "phrase: First name",
            LastModified = "2015/01/12")]
        public string FirstName
        {
            get
            {
                return this["FirstName"];
            }
        }

        /// <summary>
        /// phrase: Last name.
        /// </summary>
        /// <value>Last name.</value>
        [ResourceEntry("LastName",
            Value = "Last name",
            Description = "phrase: Last name",
            LastModified = "2015/01/12")]
        public string LastName
        {
            get
            {
                return this["LastName"];
            }
        }

        /// <summary>
        /// phrase: (Optional)
        /// </summary>
        /// <value>(Optional)</value>
        [ResourceEntry("Optional",
            Value = "(Optional)",
            Description = "phrase: (Optional)",
            LastModified = "2015/01/12")]
        public string Optional
        {
            get
            {
                return this["Optional"];
            }
        }

        /// <summary>
        /// phrase: Email
        /// </summary>
        /// <value>Email</value>
        [ResourceEntry("Email",
            Value = "Email",
            Description = "phrase: Email",
            LastModified = "2015/01/12")]
        public string Email
        {
            get
            {
                return this["Email"];
            }
        }

        /// <summary>
        /// phrase: Subscribe
        /// </summary>
        /// <value>Subscribe</value>
        [ResourceEntry("ButtonSubscribe",
            Value = "Subscribe",
            Description = "phrase: Subscribe",
            LastModified = "2015/01/12")]
        public string ButtonSubscribe
        {
            get
            {
                return this["ButtonSubscribe"];
            }
        }

        /// <summary>
        /// phrase: Thank you
        /// </summary>
        /// <value>Thank you</value>
        [ResourceEntry("ThankYou",
            Value = "Thank you",
            Description = "phrase: Thank you",
            LastModified = "2015/01/12")]
        public string ThankYou
        {
            get
            {
                return this["ThankYou"];
            }
        }

        /// <summary>
        /// phrase: You have successfully subscribed to our newsletter
        /// </summary>
        /// <value>You have successfully subscribed to our newsletter</value>
        [ResourceEntry("ThankYouMessage",
            Value = "You have successfully subscribed to our newsletter",
            Description = "phrase: You have successfully subscribed to our newsletter",
            LastModified = "2015/01/12")]
        public string ThankYouMessage
        {
            get
            {
                return this["ThankYouMessage"];
            }
        }

        /// <summary>
        /// phrase: Email address is invalid
        /// </summary>
        /// <value>Email address is invalid</value>
        [ResourceEntry("EmailAddressErrorMessageResourceName",
            Value = "Email address is invalid",
            Description = "phrase: Email address is invalid",
            LastModified = "2015/01/12")]
        public string EmailAddressErrorMessageResourceName
        {
            get
            {
                return this["EmailAddressErrorMessageResourceName"];
            }
        }

        /// <summary>
        /// phrase: is already added to this mailing list
        /// </summary>
        [ResourceEntry("EmailExistsInTheMailingList",
            Value = "is already added to this mailing list",
            Description = "phrase: is already added to this mailing list",
            LastModified = "2010/10/28")]
        public string EmailExistsInTheMailingList
        {
            get
            {
                return this["EmailExistsInTheMailingList"];
            }
        }

        /// <summary>
        /// phrase: You have successfully subscribed to this mailing list. Thank you.
        /// </summary>
        [ResourceEntry("SuccessfulSubscription",
            Value = "You have successfully subscribed to this mailing list. Thank you.",
            Description = "phrase: You have successfully subscribed to this mailing list. Thank you.",
            LastModified = "2010/10/28")]
        public string SuccessfulSubscription
        {
            get
            {
                return this["SuccessfulSubscription"];
            }
        }

        /// <summary>
        /// phrase: Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.
        /// </summary>
        [ResourceEntry("ModuleNotLicensed",
            Value = "Email Campaigns functionality is disabled since the Email Campaigns module is not licensed.",
            Description = "Module Not Licensed message",
            LastModified = "2012/01/05")]
        public string ModuleNotLicensed
        {
            get
            {
                return this["ModuleNotLicensed"];
            }
        }
        #endregion

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
