using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField
{
    /// <summary>
    /// This class represents view model for reCaptcha field.
    /// </summary>
    public class RecaptchaFieldViewModel
    {
        /// <summary>
        /// Gets or sets the  color theme of the widget.
        /// </summary>
        /// <value>
        /// The theme.
        /// </value>
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the type of CAPTCHA to serve.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the size of the widget.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display only for unauthenticated users.
        /// </summary>
        /// <value>
        /// <c>true</c> if will be visible only for unauthenticated users; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayOnlyForUnauthenticatedUsers { get; set; }
    }
}
