using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("HierarchicalTaxonomyResources", ResourceClassId = "HierarchicalTaxonomyResources", Title = "HierarchicalTaxonomyResourcesTitle", TitlePlural = "HierarchicalTaxonomyResourcesTitlePlural", Description = "HierarchicalTaxonomyResourcesDescription")]
    public class HierarchicalTaxonomyResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="HierarchicalTaxonomyResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public HierarchicalTaxonomyResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="HierarchicalTaxonomyResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public HierarchicalTaxonomyResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>HierarchicalTaxonomyResources labels</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesTitle",
            Value = "HierarchicalTaxonomyResources labels",
            Description = "The title of this class.",
            LastModified = "2015/05/19")]
        public string HierarchicalTaxonomyResourcesTitle
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>HierarchicalTaxonomyResources labels</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesTitlePlural",
            Value = "HierarchicalTaxonomyResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/05/19")]
        public string HierarchicalTaxonomyResourcesTitlePlural
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/05/19")]
        public string HierarchicalTaxonomyResourcesDescription
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Phrase: Which [taxonomies] to display?
        /// </summary>
        /// <value>Which {0} to display?</value>
        [ResourceEntry("WhichHierarchicalTaxonomiesLabel",
            Value = "Which {0} to display?",
            Description = "Phrase: Which [taxonomies] to display?",
            LastModified = "2015/05/27")]
        public string WhichHierarchicalTaxonomiesLabel
        {
            get
            {
                return this["WhichHierarchicalTaxonomiesLabel"];
            }
        }

        /// <summary>
        /// Phrase: All [taxonomies]
        /// </summary>
        /// <value>All {0}</value>
        [ResourceEntry("AllHierarchicalTaxonomies",
            Value = "All {0}",
            Description = "Phrase: All [taxonomies]",
            LastModified = "2015/05/27")]
        public string AllHierarchicalTaxonomies
        {
            get
            {
                return this["AllHierarchicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: Top level [taxonomies] only
        /// </summary>
        /// <value>Top level {0} only</value>
        [ResourceEntry("TopLevelHierarchicalTaxonomies",
            Value = "Top level {0} only",
            Description = "Phrase: Top level [taxonomies] only",
            LastModified = "2015/05/27")]
        public string TopLevelHierarchicalTaxonomies
        {
            get
            {
                return this["TopLevelHierarchicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: All [taxonomies] under particular [taxonomy]...
        /// </summary>
        /// <value>All {0} under particular {1}...</value>
        [ResourceEntry("UnderParticularHierarchicalTaxonomies",
            Value = "All {0} under particular {1}...",
            Description = "Phrase: All [taxonomies] under particular [taxonomy]...",
            LastModified = "2015/05/27")]
        public string UnderParticularHierarchicalTaxonomies
        {
            get
            {
                return this["UnderParticularHierarchicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: Selected [taxonomies]...
        /// </summary>
        /// <value>Selected {0}...</value>
        [ResourceEntry("SelectedHierachicalTaxonomies",
            Value = "Selected {0}...",
            Description = "Phrase: Selected [taxonomies]...",
            LastModified = "2015/05/27")]
        public string SelectedHierachicalTaxonomies
        {
            get
            {
                return this["SelectedHierachicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: Only [taxonomies] used by content type...
        /// </summary>
        /// <value>Only {0} used by content type...</value>
        [ResourceEntry("ContentTypeHierarchicalTaxonomies",
            Value = "Only {0} used by content type...",
            Description = "Phrase: Only [taxonomies] used by content type...",
            LastModified = "2015/05/27")]
        public string ContentTypeHierarchicalTaxonomies
        {
            get
            {
                return this["ContentTypeHierarchicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: Show item count
        /// </summary>
        /// <value>Show item count</value>
        [ResourceEntry("ShowItemCountLabel",
            Value = "Show item count",
            Description = "Phrase: Show item count",
            LastModified = "2015/05/22")]
        public string ShowItemCountLabel
        {
            get
            {
                return this["ShowItemCountLabel"];
            }
        }

        /// <summary>
        /// Phrase: Show empty [taxonomies]
        /// </summary>
        /// <value>Show empty {0}</value>
        [ResourceEntry("ShowEmptyHierarchicalTaxonomies",
            Value = "Show empty {0}",
            Description = "Phrase: Show empty [taxonomies]",
            LastModified = "2015/05/27")]
        public string ShowEmptyHierarchicalTaxonomies
        {
            get
            {
                return this["ShowEmptyHierarchicalTaxonomies"];
            }
        }

        /// <summary>
        /// Phrase: Sort [taxonomies]
        /// </summary>
        /// <value>Sort {0}</value>
        [ResourceEntry("SortHierarchicalTaxonomiesLabel",
            Value = "Sort {0}",
            Description = "Phrase: Sort [taxonomies]",
            LastModified = "2015/05/27")]
        public string SortHierarchicalTaxonomiesLabel
        {
            get
            {
                return this["SortHierarchicalTaxonomiesLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2014/08/22")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified",
            Description = "phrase : Last modified",
            LastModified = "2014/08/22")]
        public string LastModified
        {
            get
            {
                return this["LastModified"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAZ",
            Value = "By Title (A-Z)",
            Description = "phrase : By Title (A-Z)",
            LastModified = "2014/08/22")]
        public string ByTitleAZ
        {
            get
            {
                return this["ByTitleAZ"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (Z-A)
        /// </summary>
        [ResourceEntry("ByTitleZA",
            Value = "By Title (Z-A)",
            Description = "phrase : By Title (Z-A)",
            LastModified = "2014/08/22")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        /// <value>As set manually</value>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2015/01/12")]
        public string AsSetManually
        {
            get
            {
                return this["AsSetManually"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/01/12")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/21")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/04/21")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }
        #endregion

        /// <summary>
        /// Word: Content
        /// </summary>
        /// <value>Content</value>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "Word: Content",
            LastModified = "2015/05/22")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Word: Settings
        /// </summary>
        /// <value>Settings</value>
        [ResourceEntry("Settings",
            Value = "Settings",
            Description = "Word: Settings",
            LastModified = "2015/05/22")]
        public string Settings
        {
            get
            {
                return this["Settings"];
            }
        }

        /// <summary>
        /// Phrase: Select [taxonomies]
        /// </summary>
        /// <value>Select {0}</value>
        [ResourceEntry("SelectTaxonomiesHeader",
            Value = "Select {0}",
            Description = "Phrase: Select [taxonomies]",
            LastModified = "2015/05/27")]
        public string SelectTaxonomiesHeader
        {
            get
            {
                return this["SelectTaxonomiesHeader"];
            }
        }

        /// <summary>
        /// word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("TemplateLabel",
            Value = "Template",
            Description = "word: Template",
            LastModified = "2015/06/02")]
        public string TemplateLabel
        {
            get
            {
                return this["TemplateLabel"];
            }
        }
    }
}
