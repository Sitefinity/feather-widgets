using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.News.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the News widget
    /// </summary>
    [ObjectInfo(typeof(NewsWidgetResources), Title = "NewsResourcesTitle", Description = "NewsResourcesDescription")]
    public class NewsWidgetResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetResources"/> class.
        /// </summary>
        public NewsWidgetResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public NewsWidgetResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the News widget resources class.
        /// </summary>
        [ResourceEntry("NewsResourcesTitle",
            Value = "News widget resources",
            Description = "Title for the News widget resources class.",
            LastModified = "2014/08/20")]
        public string NewsResourcesTitle
        {
            get
            {
                return this["NewsResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the News widget resources class.
        /// </summary>
        [ResourceEntry("NewsResourcesDescription",
            Value = "Localizable strings for the News widget.",
            Description = "Description for the News widget resources class.",
            LastModified = "2014/08/20")]
        public string NewsResourcesDescription
        {
            get
            {
                return this["NewsResourcesDescription"];
            }
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>The display.</value>
        [ResourceEntry("Display",
            Value = "Display",
            Description = "word : Display",
            LastModified = "2014/08/20")]
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
            LastModified = "2014/08/20")]
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
            LastModified = "2014/08/20")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Which news to display?
        /// </summary>
        [ResourceEntry("NewsToDisplay",
            Value = "Which news to display?",
            Description = "phrase : Which news to display?",
            LastModified = "2014/08/22")]
        public string NewsToDisplay
        {
            get
            {
                return this["NewsToDisplay"];
            }
        }

        /// <summary>
        /// Gets phrase : All published news
        /// </summary>
        [ResourceEntry("AllPublishedNews",
            Value = "All published news",
            Description = "phrase : All published news",
            LastModified = "2014/08/22")]
        public string AllPublishedNews
        {
            get
            {
                return this["AllPublishedNews"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected news
        /// </summary>
        [ResourceEntry("SelectedNews",
            Value = "Selected news...",
            Description = "phrase : Selected news...",
            LastModified = "2014/08/22")]
        public string SelectedNews
        {
            get
            {
                return this["SelectedNews"];
            }
        }

        /// <summary>
        /// Gets phrase : Narrow selection by...
        /// </summary>
        [ResourceEntry("NarrowSelection",
            Value = "Filtered news by...",
            Description = "phrase : Filtered news by...",
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Divide the list on pages up to {TextBox} items per page
        /// </summary>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "phrase : Divide the list on pages up to {TextBox} items per page",
            LastModified = "2014/08/22")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Show only limited number of items {TextBox} items in total
        /// </summary>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "phrase : Show only limited number of items {TextBox} items in total",
            LastModified = "2014/08/22")]
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
            LastModified = "2014/08/22")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Sort news
        /// </summary>
        [ResourceEntry("SortNews",
            Value = "Sort news",
            Description = "phrase : Sort news",
            LastModified = "2014/08/22")]
        public string SortNews
        {
            get
            {
                return this["SortNews"];
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
        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2014/09/08")]
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
            LastModified = "2014/10/29")]
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
            LastModified = "2014/10/29")]
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
            LastModified = "2014/09/08")]
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
            LastModified = "2014/08/29")]
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
            LastModified = "2014/08/22")]
        public string By
        {
            get
            {
                return this["By"];
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
    }
}
