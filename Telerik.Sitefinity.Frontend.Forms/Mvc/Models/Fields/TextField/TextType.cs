using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
    /// <summary>
    /// Enumeration defines possible types for text field input type.
    /// </summary>
    public enum TextType
    {
        /// <summary>
        /// Default. Defines a single-line text field.
        /// </summary>
        Text,

        /// <summary>
        /// Defines a color picker
        /// </summary>
        Color,

        /// <summary>
        /// Defines a date control (year, month and day (no time))
        /// </summary>
        Date,

        /// <summary>
        /// Defines a date and time control (year, month, day, hour, minute, second, and fraction of a second (no time zone)
        /// </summary>
        DateTimeLocal,

        /// <summary>
        /// Defines a field for an e-mail address
        /// </summary>
        Email,

        /// <summary>
        /// Defines a hidden input field
        /// </summary>
        Hidden,

        /// <summary>
        /// Defines a month and year control (no time zone)
        /// </summary>
        Month,

        /// <summary>
        /// Defines a field for entering a number
        /// </summary>
        Number,

        /// <summary>
        /// Defines a password field (characters are masked)
        /// </summary>
        Password,

        /// <summary>
        /// Defines a control for entering a number whose exact value is not important (like a slider control)
        /// </summary>
        Range,

        /// <summary>
        /// Defines a field for entering a telephone number
        /// </summary>
        Tel,

        /// <summary>
        /// Defines a control for entering a time (no time zone)
        /// </summary>
        Time,

        /// <summary>
        /// Defines a field for entering a URL
        /// </summary>
        Url,

        /// <summary>
        /// Defines a week and year control (no time zone)
        /// </summary>
        Week
    }
}
