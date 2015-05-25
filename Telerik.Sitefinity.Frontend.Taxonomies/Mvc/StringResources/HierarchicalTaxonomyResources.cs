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
        /// Phrase: Which categories to display?
        /// </summary>
        /// <value>Which categories to display?</value>
        [ResourceEntry("WhichCategoriesLabel",
            Value = "Which categories to display?",
            Description = "Phrase: Which categories to display?",
            LastModified = "2015/05/22")]
        public string WhichCategoriesLabel
        {
            get
            {
                return this["WhichCategoriesLabel"];
            }
        }

        /// <summary>
        /// Phrase: All categories
        /// </summary>
        /// <value>All categories</value>
        [ResourceEntry("AllCategories",
            Value = "All categories",
            Description = "Phrase: All categories",
            LastModified = "2015/05/22")]
        public string AllCategories
        {
            get
            {
                return this["AllCategories"];
            }
        }

        /// <summary>
        /// Phrase: Top level categories only
        /// </summary>
        /// <value>Top level categories only</value>
        [ResourceEntry("TopLevelCategories",
            Value = "Top level categories only",
            Description = "Phrase: Top level categories only",
            LastModified = "2015/05/22")]
        public string TopLevelCategories
        {
            get
            {
                return this["TopLevelCategories"];
            }
        }

        /// <summary>
        /// Phrase: All categories under particular category...
        /// </summary>
        /// <value>All categories under particular category...</value>
        [ResourceEntry("UnderParticularCategory",
            Value = "All categories under particular category...",
            Description = "Phrase: All categories under particular category...",
            LastModified = "2015/05/22")]
        public string UnderParticularCategory
        {
            get
            {
                return this["UnderParticularCategory"];
            }
        }

        /// <summary>
        /// Phrase: Selected categories...
        /// </summary>
        /// <value>Selected categories...</value>
        [ResourceEntry("SelectedCategories",
            Value = "Selected categories...",
            Description = "Phrase: Selected categories...",
            LastModified = "2015/05/22")]
        public string SelectedCategories
        {
            get
            {
                return this["SelectedCategories"];
            }
        }

        /// <summary>
        /// Phrase: Only categories used by content type...
        /// </summary>
        /// <value>Only categories used by content type...</value>
        [ResourceEntry("ContentTypeCategories",
            Value = "Only categories used by content type...",
            Description = "Phrase: Only categories used by content type...",
            LastModified = "2015/05/22")]
        public string ContentTypeCategories
        {
            get
            {
                return this["ContentTypeCategories"];
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
        /// Phrase: Show empty categories
        /// </summary>
        /// <value>Show empty categories</value>
        [ResourceEntry("ShowEmptyCategories",
            Value = "Show empty categories",
            Description = "Phrase: Show empty categories",
            LastModified = "2015/05/22")]
        public string ShowEmptyCategories
        {
            get
            {
                return this["ShowEmptyCategories"];
            }
        }

        /// <summary>
        /// Phrase: Sort categories
        /// </summary>
        /// <value>Sort categories</value>
        [ResourceEntry("SortCategoriesLabel",
            Value = "Sort categories",
            Description = "Phrase: Sort categories",
            LastModified = "2015/05/22")]
        public string SortCategoriesLabel
        {
            get
            {
                return this["SortCategoriesLabel"];
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
    }
}
