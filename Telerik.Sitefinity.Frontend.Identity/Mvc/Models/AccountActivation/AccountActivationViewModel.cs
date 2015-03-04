namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation
{
    /// <summary>
    /// DTO used for displaying the index action of <see cref="AccountActivationController"/>
    /// </summary>
    public class AccountActivationViewModel
    {
        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }
        
        /// <summary>
        /// Gets or sets the profile page URL.
        /// </summary>
        /// <value>
        /// The profile page URL.
        /// </value>
        public string ProfilePageUrl { get; set; }
    }
}
