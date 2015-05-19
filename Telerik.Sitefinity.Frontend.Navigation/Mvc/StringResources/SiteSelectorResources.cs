using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings for the Site selector widget.
    /// </summary>
    [ObjectInfo("SiteSelectorResources", ResourceClassId = "SiteSelectorResources", Title = "SiteSelectorResourcesTitle", TitlePlural = "SiteSelectorResourcesTitlePlural", Description = "SiteSelectorResourcesDescription")]
    public class SiteSelectorResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="SiteSelectorResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public SiteSelectorResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="SiteSelectorResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public SiteSelectorResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>SiteSelectorResources labels</value>
        [ResourceEntry("SiteSelectorResourcesTitle",
            Value = "SiteSelectorResources labels",
            Description = "The title of this class.",
            LastModified = "2015/05/12")]
        public string SiteSelectorResourcesTitle
        {
            get
            {
                return this["SiteSelectorResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>SiteSelectorResources labels</value>
        [ResourceEntry("SiteSelectorResourcesTitlePlural",
            Value = "SiteSelectorResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/05/12")]
        public string SiteSelectorResourcesTitlePlural
        {
            get
            {
                return this["SiteSelectorResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("SiteSelectorResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/05/12")]
        public string SiteSelectorResourcesDescription
        {
            get
            {
                return this["SiteSelectorResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Phrase: Include the current site in the selector
        /// </summary>
        /// <value>Include the current site in the selector</value>
        [ResourceEntry("IncludeCurrentSite",
            Value = "Include the current site in the selector",
            Description = "Phrase: Include the current site in the selector",
            LastModified = "2015/05/13")]
        public string IncludeCurrentSite
        {
            get
            {
                return this["IncludeCurrentSite"];
            }
        }

        /// <summary>
        /// Phrase: Display each language version as a separate sit
        /// </summary>
        /// <value>Display each language version as a separate site</value>
        [ResourceEntry("LanguageVersionAsSeparateSite",
            Value = "Display each language version as a separate site",
            Description = "Phrase: Display each language version as a separate sit",
            LastModified = "2015/05/13")]
        public string LanguageVersionAsSeparateSite
        {
            get
            {
                return this["LanguageVersionAsSeparateSite"];
            }
        }

        /// <summary>
        /// Phrase: Show site names and languages
        /// </summary>
        /// <value>Show site names and languages</value>
        [ResourceEntry("SiteNamesAndLanguages",
            Value = "Show site names and languages",
            Description = "Phrase: Show site names and languages",
            LastModified = "2015/05/13")]
        public string SiteNamesAndLanguages
        {
            get
            {
                return this["SiteNamesAndLanguages"];
            }
        }

        /// <summary>
        /// Phrase: Show languages only
        /// </summary>
        /// <value>Show languages only</value>
        [ResourceEntry("LanguagesOnly",
            Value = "Show languages only",
            Description = "Phrase: Show languages only",
            LastModified = "2015/05/13")]
        public string LanguagesOnly
        {
            get
            {
                return this["LanguagesOnly"];
            }
        }

        /// <summary>
        /// Phrase: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "Phrase: Template",
            LastModified = "2015/05/13")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Phrase: More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "Phrase: More options",
            LastModified = "2015/05/13")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Phrase: CSS classes
        /// </summary>
        /// <value>CSS classes</value>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "Phrase: CSS classes",
            LastModified = "2015/05/13")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Phrase: Global site - English
        /// </summary>
        /// <value>Global site - English</value>
        [ResourceEntry("NameAndLanguageExample1",
            Value = "Global site - English",
            Description = "Phrase: Global site - English",
            LastModified = "2015/05/14")]
        public string NameAndLanguageExample1
        {
            get
            {
                return this["NameAndLanguageExample1"];
            }
        }

        /// <summary>
        /// Word: Example
        /// </summary>
        /// <value>Example</value>
        [ResourceEntry("Example",
            Value = "Example",
            Description = "Word: Example",
            LastModified = "2015/05/14")]
        public string Example
        {
            get
            {
                return this["Example"];
            }
        }

        /// <summary>
        /// Phrase: Global site - Español
        /// </summary>
        /// <value>Global site - Español</value>
        [ResourceEntry("NameAndLanguageExample2",
            Value = "Global site - Español",
            Description = "Phrase: Global site - Español",
            LastModified = "2015/05/14")]
        public string NameAndLanguageExample2
        {
            get
            {
                return this["NameAndLanguageExample2"];
            }
        }

        /// <summary>
        /// Word: English
        /// </summary>
        /// <value>English</value>
        [ResourceEntry("LanguageOnlyExample1",
            Value = "English",
            Description = "Word: English",
            LastModified = "2015/05/14")]
        public string LanguageOnlyExample1
        {
            get
            {
                return this["LanguageOnlyExample1"];
            }
        }

        /// <summary>
        /// Word: French
        /// </summary>
        /// <value>French</value>
        [ResourceEntry("LanguageOnlyExample2",
            Value = "French",
            Description = "Word: French",
            LastModified = "2015/05/14")]
        public string LanguageOnlyExample2
        {
            get
            {
                return this["LanguageOnlyExample2"];
            }
        }

        /// <summary>
        /// Word: German
        /// </summary>
        /// <value>German</value>
        [ResourceEntry("LanguageOnlyExample3",
            Value = "German",
            Description = "Word: German",
            LastModified = "2015/05/14")]
        public string LanguageOnlyExample3
        {
            get
            {
                return this["LanguageOnlyExample3"];
            }
        }

        /// <summary>
        /// Word: Sites
        /// </summary>
        /// <value>Sites</value>
        [ResourceEntry("Sites",
            Value = "Sites",
            Description = "Word: Sites",
            LastModified = "2015/05/14")]
        public string Sites
        {
            get
            {
                return this["Sites"];
            }
        }

        /// <summary>
        /// phrase: - Select a site -
        /// </summary>
        /// <value>-Select a site-</value>
        [ResourceEntry("SelectSite",
            Value = "- Select a site -",
            Description = "phrase: - Select a site -",
            LastModified = "2015/05/14")]
        public string SelectSite
        {
            get
            {
                return this["SelectSite"];
            }
        }

        #endregion
    }
}
