using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper methods for <see cref="Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField.TextType"/>
    /// </summary>
    public static class TextTypeHelpers
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="textType">Type of the text.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string GetInputType(this TextType textType)
        {
            string typeString;

            if (textType == TextType.DateTimeLocal)
            {
                typeString = "datetime-local";
            }
            else
            {
                typeString = textType.ToString().ToLowerInvariant();
            }

            return typeString;
        }
    }
}
