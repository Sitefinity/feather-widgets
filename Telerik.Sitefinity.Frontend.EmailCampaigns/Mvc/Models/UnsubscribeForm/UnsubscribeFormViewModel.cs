using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Attributes;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// This class represents the view model of Unsubscribe widget.
    /// </summary>
    public class UnsubscribeFormViewModel
    {
        /// <summary>
        /// Gets or sets the message that will be shown when Link unsubscribe mode is selected.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the title of the widget when EmailAddress unsubscribe mode is selected.
        /// </summary>
        public string WidgetTitle { get; set; }

        /// <summary>
        /// Gets or sets the description of the widget when EmailAddress unsubscribe mode is selected.
        /// </summary>
        public string WidgetDescription { get; set; }

        /// <summary>
        /// Gets or sets the redirect page URL.
        /// </summary>
        /// <value>The redirect page URL.</value>
        public string RedirectPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceName = "EmailAddressErrorMessageResourceName",
                      ErrorMessageResourceType = typeof(StaticUnsubscribeFormControllerResources))]
        public string Email { get; set; }
    }
}
