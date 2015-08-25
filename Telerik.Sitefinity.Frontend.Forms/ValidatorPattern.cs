using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This class contains validate patterns for various input types.
    /// </summary>
    public static class ValidatorPattern
    {
        /// <summary>
        /// The email regex pattern
        /// </summary>
        public static readonly string EmailRegexPattern = @"\A[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}\z";

        /// <summary>
        /// The numeric regex pattern
        /// </summary>
        public static readonly string NumericRegexPattern = @"\A^[-+]?[0-9]+((,|\.)[0-9]+)?\z";

        /// <summary>
        /// The color regex pattern
        /// </summary>
        public static readonly string ColorRegexPattern = "^#(?:[0-9a-fA-F]{3}){1,2}$";

        /// <summary>
        /// The URL regex pattern
        /// </summary>
        public static readonly string UrlRegexPattern = @"\b(?:(?:https?|ftp|file)://|www\.|ftp\.)[-A-Z0-9+&@#/%=~_|$?!:,.]*[A-Z0-9+&@#/%=~_|$]";
    }
}
