using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector
{
    /// <summary>
    /// Instances of this class are used to store information about a language.
    /// </summary>
    public class LanguageSelectorItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language" /> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="culture">The culture.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public LanguageSelectorItem(string displayName, string url, string culture)
        {
            this.DisplayName = displayName;
            this.Url = url;
            this.Culture = culture;
        }

        /// <summary>
        /// Gets or sets the display name of the language.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the URL to the laguage version.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public string Culture { get; set; }
    }
}
