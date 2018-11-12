using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha
{
    /// <summary>
    /// This class represents view model for Captcha field.
    /// </summary>
    public class CaptchaViewModel
    {
        /// <summary>
        /// Gets or sets the generate URL.
        /// </summary>
        /// <value>
        /// The generate URL.
        /// </value>
        public string GenerateUrl { get; set; }

        /// <summary>
        /// Gets or sets the captcha answer form key.
        /// </summary>
        /// <value>
        /// The captcha answer form key.
        /// </value>
        public string CaptchaAnswerFormKey { get; set; }

        /// <summary>
        /// Gets or sets the captcha correct answer form key.
        /// </summary>
        /// <value>
        /// The captcha correct answer form key.
        /// </value>
        public string CaptchaCorrectAnswerFormKey { get; set; }

        /// <summary>
        /// Gets or sets the captcha initialization vector form key.
        /// </summary>
        /// <value>
        /// The captcha initialization vector form key.
        /// </value>
        public string CaptchaInitializationVectorFormKey { get; set; }

        /// <summary>
        /// Gets or sets the captcha key form key.
        /// </summary>
        /// <value>
        /// The captcha key form key.
        /// </value>
        public string CaptchaKeyFormKey { get; set; }


        /// <summary>
        /// Gets or sets a validation mechanism for the control.
        /// </summary>
        /// <value>The validation.</value>
        public ValidatorDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display button for reading the code.
        /// </summary>
        /// <value>
        /// <c>true</c> if the audio button should be visible; otherwise, <c>false</c>.
        /// </value>
        public bool EnableAudioCode { get; set; }
    }
}