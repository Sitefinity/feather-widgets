using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}