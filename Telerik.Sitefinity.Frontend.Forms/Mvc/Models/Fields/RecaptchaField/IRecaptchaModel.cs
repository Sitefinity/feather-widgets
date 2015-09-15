using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField
{
    /// <summary>
    /// This interface provides API for form reCaptcha field.
    /// </summary>
    public interface IRecaptchaModel : IFormElementModel
    {
        /// <summary>

        /// <summary>
        /// Gets or sets the  color theme of the widget.
        /// </summary>
        /// <value>
        /// The theme.
        /// </value>
        string Theme { get; set; }

        /// <summary>
        /// Gets or sets the type of CAPTCHA to serve.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        string DataType { get; set; }

        /// <summary>
        /// Gets or sets the size of the widget.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        string Size { get; set; }

        /// <summary>
        /// Gets or sets the data sitekey.
        /// </summary>
        /// <value>
        /// The data sitekey.
        /// </value>
        string DataSitekey { get; set; }

        /// <summary>
        /// The shared key between your site and ReCAPTCHA.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        string Secret { get; set; }

        /// <summary>
        /// Gets or sets the validation timeout miliseconds.
        /// </summary>
        /// <value>
        /// The validation timeout miliseconds.
        /// </value>
        long ValidationTimeoutMiliseconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display only for unauthenticated users.
        /// </summary>
        /// <value>
        /// <c>true</c> if will be visible only for unauthenticated users; otherwise, <c>false</c>.
        /// </value>
        bool DisplayOnlyForUnauthenticatedUsers { get; set; }


        /// <summary>
        /// Should the render captcha.
        /// </summary>
        /// <returns></returns>
        bool ShouldRenderCaptcha();
    }
}
