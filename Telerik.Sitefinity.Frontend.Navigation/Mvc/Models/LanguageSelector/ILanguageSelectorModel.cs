using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector
{
    /// <summary>
    /// Classes that implement this interface could be used as model for the Language selector widget.
    /// </summary>
    public interface ILanguageSelectorModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include the current language in the list of languages.
        /// </summary>
        /// <value><c>true</c> in order to include the current language in the list of languages; otherwise, <c>false</c>.</value>
        bool IncludeCurrentLanguage { get; set; }

        /// <summary>
        /// Gets or sets the behavior of the control for missing translations.
        /// </summary>
        /// <value>The behavior of the control for missing translations.</value>
        NoTranslationAction MissingTranslationAction { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the Language selector widget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Creates the language selector view model.
        /// </summary>
        LanguageSelectorViewModel CreateViewModel();
    }
}
