using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField
{
    /// <summary>
    /// This interface provides API for form reCaptcha field.
    /// </summary>
    public interface IRecaptchaFieldModel
    {
        /// <summary>
        /// Gets or sets the value of the form field.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        object Value { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool IsValid(object value);

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
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        RecaptchaFieldViewModel GetViewModel(object value);

        /// <summary>
        /// Should the render captcha.
        /// </summary>
        /// <returns></returns>
        bool ShouldRenderCaptcha();
    }
}
