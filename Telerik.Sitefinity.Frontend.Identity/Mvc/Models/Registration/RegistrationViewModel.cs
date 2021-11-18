using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class represents view model for the <see cref="RegistrationController"/>.
    /// </summary>
    [Bind(Include = "RequiresQuestionAndAnswer, Password, Question, Answer, ReTypePassword, Email, Profile, ExternalProviders")]
    public class RegistrationViewModel : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModel"/> class.
        /// </summary>
        public RegistrationViewModel()
        {
            this.Profile = new Dictionary<string, string>();
        }

        /// <summary>
        /// Holds the login page to be redirected, when clicking Log in
        /// </summary>
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the membership provider.
        /// </summary>
        /// <value>
        /// The name of the membership provider.
        /// </value>
        public string MembershipProviderName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page that will be opened on successful registration.
        /// </summary>
        /// <value>
        /// The successful registration page URL.
        /// </value>
        public string SuccessfulRegistrationPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the id of the page that will be used to confirm the registration.
        /// </summary>
        /// <value>The confirmation page id.</value>
        public virtual Guid? ConfirmationPageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the membership provider settings require question and answer for reset/retrieval password functionality.
        /// </summary>
        /// <value>
        /// <c>true</c> if the membership provider requires question and answer; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresQuestionAndAnswer { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [Display(Name = "Password", ResourceType = typeof(RegistrationStaticResources))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        [Display(Name = "Question", ResourceType = typeof(RegistrationStaticResources))]
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        [Display(Name = "Answer", ResourceType = typeof(RegistrationStaticResources))]
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation value.
        /// </summary>
        /// <value>
        /// The retyped password.
        /// </value>
        [System.Web.Mvc.Compare("Password")]
        [Display(Name = "ReTypePassword", ResourceType = typeof(RegistrationStaticResources))]
        public string ReTypePassword { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(RegistrationStaticResources))]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the profile object.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public IDictionary<string, string> Profile { get; set; }

        /// <summary>
        /// Gets or sets the external providers.
        /// </summary>
        /// <value>
        /// External providers.
        /// </value>
        public IDictionary<string, string> ExternalProviders { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var info in Profile)
            {
                var validation = RegistrationHelper.GetFieldValidatorDefinition(info.Key);

                if (validation != null)
                {
                    if (validation.Required.HasValue && validation.Required.Value && string.IsNullOrEmpty(info.Value))
                    {
                        var requiredErrorMessage = this.GetErrorMessageFromResource(validation.RequiredViolationMessage);

                        yield return new ValidationResult($"{requiredErrorMessage}", new List<string> { "Profile[" + info.Key + "]" });
                    }
                    else if (info.Value.Length > 1)
                    {
                        if (info.Value.Length < (int)validation.MinLength || ((int)validation.MaxLength != 0 && info.Value.Length > (int)validation.MaxLength))
                        {
                            var lengthErrorMessage = this.GetErrorMessageFromResource(validation.MaxLengthViolationMessage);
                           
                            yield return new ValidationResult($"{lengthErrorMessage}", new List<string> { "Profile[" + info.Key + "]" });
                        }
                    }
                }
            }
        }

        private string GetErrorMessageFromResource(string violationMessage)
        {
            const string MissingResourcePrefix = "#ResourceNotFound#";
            string errorMessage = string.Empty;

            if (violationMessage != null)
            {
                errorMessage = Res.Get<ErrorMessages>(violationMessage).StartsWith(MissingResourcePrefix) ? violationMessage : Res.Get<ErrorMessages>(violationMessage);
            }

            return errorMessage;
        }
    }
}
