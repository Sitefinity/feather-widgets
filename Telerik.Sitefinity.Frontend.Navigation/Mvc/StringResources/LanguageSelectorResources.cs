using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Language selector widget
    /// </summary>
    [ObjectInfo(typeof(LanguageSelectorResources), ResourceClassId = "LanguageSelectorResources", Title = "LanguageSelectorResourcesTitle", Description = "LanguageSelectorResourcesDescription")]
    public class LanguageSelectorResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageSelectorResources"/> class.
        /// </summary>
        public LanguageSelectorResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageSelectorResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public LanguageSelectorResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Language selector widget resources class.
        /// </summary>
        [ResourceEntry("LanguageSelectorResourcesTitle",
            Value = "Language selector widget resources",
            Description = "Title for the Language selector widget resources class.",
            LastModified = "2015/05/06")]
        public string LanguageSelectorResourcesTitle
        {
            get
            {
                return this["LanguageSelectorResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Language selector widget resources class.
        /// </summary>
        [ResourceEntry("LanguageSelectorResourcesDescription",
            Value = "Localizable strings for the Language selector widget.",
            Description = "Description for the Language selector widget resources class.",
            LastModified = "2015/05/06")]
        public string LanguageSelectorResourcesDescription
        {
            get
            {
                return this["LanguageSelectorResourcesDescription"];
            }
        }

        /// <summary>
        /// Phrase: Include the current language in the selector
        /// </summary>
        /// <value>Include the current language in the selector</value>
        [ResourceEntry("IncludeCurrentLanguage",
            Value = "Include the current language in the selector",
            Description = "Phrase: Include the current language in the selector",
            LastModified = "2015/05/06")]
        public string IncludeCurrentLanguage
        {
            get
            {
                return this["IncludeCurrentLanguage"];
            }
        }

        /// <summary>
        /// Phrase: What to do with languages without translations
        /// </summary>
        /// <value>What to do with languages without translations</value>
        [ResourceEntry("LanguageWithoutTranslation",
            Value = "What to do with languages without translations",
            Description = "Phrase: What to do with languages without translations",
            LastModified = "2015/05/06")]
        public string LanguageWithoutTranslation
        {
            get
            {
                return this["LanguageWithoutTranslation"];
            }
        }

        /// <summary>
        /// Phrase: Some pages or content items may not be translated to all languages. How should the language selector behave when a translation is missing?
        /// </summary>
        /// <value>Some pages or content items may not be translated to all languages. How should the language selector behave when a translation is missing?</value>
        [ResourceEntry("LanguageWithoutTranslationDescription",
            Value = "Some pages or content items may not be translated to all languages. How should the language selector behave when a translation is missing?",
            Description = "Phrase: Some pages or content items may not be translated to all languages. How should the language selector behave when a translation is missing?",
            LastModified = "2015/05/06")]
        public string LanguageWithoutTranslationDescription
        {
            get
            {
                return this["LanguageWithoutTranslationDescription"];
            }
        }

        /// <summary>
        /// Phrase: Hide the link to the missing translation
        /// </summary>
        /// <value>Hide the link to the missing translation</value>
        [ResourceEntry("HideLink",
            Value = "Hide the link to the missing translation",
            Description = "Phrase: Hide the link to the missing translation",
            LastModified = "2015/05/06")]
        public string HideLink
        {
            get
            {
                return this["HideLink"];
            }
        }

        /// <summary>
        /// Phrase: Redirect to the home page in the language of the missing translation
        /// </summary>
        /// <value>Redirect to the home page in the language of the missing translation</value>
        [ResourceEntry("RedirectToHomePage",
            Value = "Redirect to the home page in the language of the missing translation",
            Description = "Phrase: Redirect to the home page in the language of the missing translation",
            LastModified = "2015/05/06")]
        public string RedirectToHomePage
        {
            get
            {
                return this["RedirectToHomePage"];
            }
        }

        /// <summary>
        /// Word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "Word: Template",
            LastModified = "2015/05/07")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Phrase: CSS classes
        /// </summary>
        /// <value>CSS classes</value>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "Phrase: CSS classes",
            LastModified = "2015/05/07")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Phrase: More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "Phrase: More options",
            LastModified = "2015/05/07")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }
    }
}
