using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.DynamicContent.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the News widget
    /// </summary>
    [ObjectInfo(typeof(DynamicContentResources), Title = "NewsResourcesTitle", Description = "NewsResourcesDescription")]
    public class DynamicContentResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicContentResources"/> class.
        /// </summary>
        public DynamicContentResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicContentResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public DynamicContentResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// phrase: This widget is no longer available since the module providing its content is deleted  or deactivated.
        /// </summary>
        [ResourceEntry("DeletedModuleWarning",
            Value = "This widget is no longer available since the module providing its content is deleted  or deactivated.",
            Description = "phrase:This widget is no longer available since the module providing its content is deleted or deactivated.",
            LastModified = "2014/11/05")]
        public string DeletedModuleWarning
        {
            get
            {
                return this["DeletedModuleWarning"];
            }
        }

        /// <summary>
        /// Gets Title for the Dynamic content widget resources class.
        /// </summary>
        [ResourceEntry("DynamicContentResourcesTitle",
            Value = "Dynamic content widget resources",
            Description = "Title for the Dynamic content widget resources class.",
            LastModified = "2014/11/06")]
        public string DynamicContentResourcesTitle
        {
            get
            {
                return this["DynamicContentResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Dynamic content widget resources class.
        /// </summary>
        [ResourceEntry("DynamicContentResourcesDescription",
            Value = "Localizable strings for the Dynamic content widget.",
            Description = "Description for the Dynamic content widget resources class.",
            LastModified = "2014/11/06")]
        public string DynamicContentResourcesDescription
        {
            get
            {
                return this["DynamicContentResourcesDescription"];
            }
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        [ResourceEntry("Display",
            Value = "Display",
            Description = "word : Display",
            LastModified = "2014/11/06")]
        public string Display
        {
            get
            {
                return this["Display"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2014/11/06")]
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
            LastModified = "2014/11/06")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2014/11/06")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Gets phrase : List settings
        /// </summary>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase : List settings",
            LastModified = "2014/11/06")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2014/11/06")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2014/11/06")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2014/11/06")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Which {ModuleName} to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which {0} to display?",
            Description = "phrase : Which {ModuleName} to display?",
            LastModified = "2014/11/06")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// <summary>
        /// Gets phrase : All published {ModuleName}
        /// </summary>
        [ResourceEntry("AllPublishedItems",
            Value = "All published {0}",
            Description = "phrase : All published {ModuleName}",
            LastModified = "2014/11/06")]
        public string AllPublishedItems
        {
            get
            {
                return this["AllPublishedItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected {ModuleName}
        /// </summary>
        [ResourceEntry("SelectedItems",
            Value = "Selected {0}...",
            Description = "phrase : Selected {ModuleName} ...",
            LastModified = "2014/11/06")]
        public string SelectedItems
        {
            get
            {
                return this["SelectedItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Narrow selection by...
        /// </summary>
        [ResourceEntry("NarrowSelection",
            Value = "Narrow selection by...",
            Description = "phrase : Narrow selection by...",
            LastModified = "2014/11/06")]
        public string NarrowSelection
        {
            get
            {
                return this["NarrowSelection"];
            }
        }

        /// <summary>
        /// Gets word : Tags...
        /// </summary>
        [ResourceEntry("Tags",
            Value = "Tags...",
            Description = "word : Tags",
            LastModified = "2014/11/06")]
        public string Tags
        {
            get
            {
                return this["Tags"];
            }
        }

        /// <summary>
        /// Gets phrase : Use paging
        /// </summary>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "phrase : Use paging",
            LastModified = "2014/11/06")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2014/11/06")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Gets phrase : No limit and paging
        /// </summary>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "phrase : No limit and paging",
            LastModified = "2014/11/06")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Divide the list on pages up to {0} items per page
        /// </summary>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "phrase : Divide the list on pages up to {0} items per page",
            LastModified = "2014/11/06")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Show only limited number of items {0} items in total
        /// </summary>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "phrase : Show only limited number of items {0} items in total",
            LastModified = "2014/11/06")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Show all published items at once
        /// </summary>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "phrase : Show all published items at once",
            LastModified = "2014/11/06")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Sort {ModuleName}
        /// </summary>
        [ResourceEntry("SortItems",
            Value = "Sort {0}",
            Description = "phrase : Sort {ModuleName}",
            LastModified = "2014/11/06")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2014/11/06")]
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
            LastModified = "2014/11/06")]
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
            LastModified = "2014/11/06")]
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
            LastModified = "2014/11/06")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2014/11/06")]
        public string OpenSingleItem
        {
            get
            {
                return this["OpenSingleItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Auto-generated page
        /// </summary>
        [ResourceEntry("ShowInSamePage",
            Value = "Auto-generated page",
            Description = "phrase : Auto-generated page (with the same layout as the list page)",
            LastModified = "2014/11/06")]
        public string ShowInSamePage
        {
            get
            {
                return this["ShowInSamePage"];
            }
        }

        /// <summary>
        /// Gets phrase : (with the same layout as the list page)
        /// </summary>
        [ResourceEntry("ShowInSamePageNote",
            Value = "(with the same layout as the list page)",
            Description = "phrase : (with the same layout as the list page)",
            LastModified = "2014/11/06")]
        public string ShowInSamePageNote
        {
            get
            {
                return this["ShowInSamePageNote"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected existing page...
        /// </summary>
        [ResourceEntry("ShowInExistingPage",
            Value = "Selected existing page...",
            Description = "phrase : Selected existing page...",
            LastModified = "2014/11/06")]
        public string ShowInExistingPage
        {
            get
            {
                return this["ShowInExistingPage"];
            }
        }

        /// <summary>
        /// Gets the Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2014/11/06")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// Gets word : by
        /// </summary>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2014/11/06")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// Selection of {ModuleName}...
        /// </summary>
        [ResourceEntry("SelectionOfItems",
            Value = "Selection of {0}...",
            Description = "Selection of {ModuleName}...",
            LastModified = "2014/11/10")]
        public string SelectionOfItems
        {
            get
            {
                return this["SelectionOfItems"];
            }
        }

        /// <summary>
        /// From currently open
        /// </summary>
        [ResourceEntry("FromCurrentlyOpen",
            Value = "From currently open",
            Description = "From currently open",
            LastModified = "2014/11/10")]
        public string FromCurrentlyOpen
        {
            get
            {
                return this["FromCurrentlyOpen"];
            }
        }

        /// <summary>
        /// From all {ModuleName}
        /// </summary>
        [ResourceEntry("FromAll",
            Value = "From all {0}",
            Description = "From all {ModuleName}",
            LastModified = "2014/11/10")]
        public string FromAll
        {
            get
            {
                return this["FromAll"];
            }
        }

        /// <summary>
        /// From selected {ModuleName} only...
        /// </summary>
        [ResourceEntry("FromSelected",
            Value = "From selected {0} only...",
            Description = "From selected {ModuleName} only...",
            LastModified = "2014/11/10")]
        public string FromSelected
        {
            get
            {
                return this["FromSelected"];
            }
        }

        /// <summary>
        /// Displays {ContentTypePlural} from the currently open {ParentTypeSingular}
        /// </summary>
        [ResourceEntry("DisplaysFromCurrentlyOpen",
            Value = "Displays {0} from the currently open {1}",
            Description = "Displays {ContentTypePlural} from the currently open {ParentTypeSingular}",
            LastModified = "2014/11/12")]
        public string DisplaysFromCurrentlyOpen
        {
            get
            {
                return this["DisplaysFromCurrentlyOpen"];
            }
        }

        /// <summary>
        /// Any parent content type
        /// </summary>
        [ResourceEntry("AnyParentContentType",
            Value = "Any parent content type",
            Description = "Any parent content type",
            LastModified = "2014/11/20")]
        public string AnyParentContentType
        {
            get
            {
                return this["AnyParentContentType"];
            }
        }
    }
}
