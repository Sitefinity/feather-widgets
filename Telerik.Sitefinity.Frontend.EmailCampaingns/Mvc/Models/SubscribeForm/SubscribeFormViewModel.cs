using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Attributes;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models
{
    /// <summary>
    /// This class represents the view model of the Email campaigns item.
    /// </summary>
    public class SubscribeFormViewModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the redirect page URL.
        /// </summary>
        /// <value>The redirect page URL.</value>
        public string RedirectPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceName = "EmailAddressErrorMessageResourceName",
                      ErrorMessageResourceType = typeof(StaticSubscribeFormResources))]
        public string Email { get; set; }
    }
}
