using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Content block widget
    /// </summary>
    [ObjectInfo(typeof(ListsWidgetResources), Title = "ListsWidgetResourcesTitle", Description = "ListsWidgetResourcesDescription")]
    public class ListsWidgetResources : Resource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListsWidgetResources"/> class. 
        /// Initializes new instance of <see cref="ListsWidgetResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ListsWidgetResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListsWidgetResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ListsWidgetResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// Gets Title for the List widget resources class.
        /// </summary>
        [ResourceEntry("ListsWidgetResourcesTitle",
            Value = "List widget resources",
            Description = "Title for the List widget resources class.",
            LastModified = "2015/03/26")]
        public string ListsWidgetResourcesTitle
        {
            get
            {
                return this["ListsWidgetResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the List widget resources class.
        /// </summary>
        [ResourceEntry("ListsWidgetResourcesDescription",
            Value = "Localizable strings for the List widget.",
            Description = "Description for the List widget resources class.",
            LastModified = "2015/03/26")]
        public string ListsWidgetResourcesDescription
        {
            get
            {
                return this["ListsWidgetResourcesDescription"];
            }
        }
        #endregion

        /// <summary>
        /// phrase: More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase: More options",
            LastModified = "2015/03/26")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// phrase: CSS classes
        /// </summary>
        /// <value>CSS classes</value>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase: CSS classes",
            LastModified = "2015/03/26")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "word: Template",
            LastModified = "2015/03/26")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// phrase: Sort list items
        /// </summary>
        /// <value>Sort list items</value>
        [ResourceEntry("SortListItems",
            Value = "Sort list items",
            Description = "phrase: Sort list items",
            LastModified = "2015/03/26")]
        public string SortListItems
        {
            get
            {
                return this["SortListItems"];
            }
        }

        /// <summary>
        /// phrase: Filter list items by...
        /// </summary>
        /// <value>Filter list items by...</value>
        [ResourceEntry("FilterListItems",
            Value = "Filter list items by...",
            Description = "phrase: Filter list items by...",
            LastModified = "2015/03/26")]
        public string FilterListItems
        {
            get
            {
                return this["FilterListItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2015/03/26")]
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
            LastModified = "2015/03/26")]
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
            LastModified = "2015/03/26")]
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
            LastModified = "2015/03/26")]
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
            LastModified = "2015/03/26")]
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
            LastModified = "2015/03/26")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// phrase: Which list to display?
        /// </summary>
        /// <value>Which list to display?</value>
        [ResourceEntry("WhichListToDisplay",
            Value = "Which list to display?",
            Description = "phrase: Which list to display?",
            LastModified = "2015/03/26")]
        public string WhichListToDisplay
        {
            get
            {
                return this["WhichListToDisplay"];
            }
        }

        /// <summary>
        /// phrase: Expand all
        /// </summary>
        /// <value>Expand all</value>
        [ResourceEntry("ExpandAll",
            Value = "Expand all",
            Description = "phrase: Expand all",
            LastModified = "2015/03/30")]
        public string ExpandAll
        {
            get
            {
                return this["ExpandAll"];
            }
        }

        /// <summary>
        /// phrase: Collapse all
        /// </summary>
        /// <value>Collapse all</value>
        [ResourceEntry("CollapseAll",
            Value = "Collapse all",
            Description = "phrase: Collapse all",
            LastModified = "2015/03/30")]
        public string CollapseAll
        {
            get
            {
                return this["CollapseAll"];
            }
        }
    }
}
