using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Event widget
    /// </summary>
    [ObjectInfo(typeof(EventResources), ResourceClassId = "EventResources", Title = "EventResourcesTitle", Description = "EventResourcesDescription")]
    public class EventResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventResources"/> class. 
        /// Initializes new instance of <see cref="EventResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public EventResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public EventResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Meta resources

        /// <summary>
        /// Gets Event widget resources title.
        /// </summary>
        [ResourceEntry("EventResourcesTitle", 
            Value = "Event widget resources", 
            Description = "Title for the event widget resources class.", 
            LastModified = "2016/04/06")]
        public string EventResourcesTitle
        {
            get
            {
                return this["EventResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Event widget resources description.
        /// </summary>
        [ResourceEntry("EventResourcesDescription", 
            Value = "Localizable strings for the event widget.", 
            Description = "Description for the event widget resources class.",
            LastModified = "2016/04/06")]
        public string EventResourcesDescription
        {
            get
            {
                return this["EventResourcesDescription"];
            }
        }

        #endregion

        #region Date resources

        /// <summary>
        /// Gets word : at
        /// </summary>
        [ResourceEntry("At",
            Value = "at",
            Description = "word: at",
            LastModified = "2016/04/12")]
        public string At
        {
            get
            {
                return this["At"];
            }
        }
        
        #endregion

        #region Frontend resources

        /// <summary>
        /// Gets word : by
        /// </summary>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2016/04/06")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// Gets word : "Add to:"
        /// </summary>
        [ResourceEntry("AddTo",
            Value = "Add to:",
            Description = "word: Add to:",
            LastModified = "2016/04/08")]
        public string AddTo
        {
            get
            {
                return this["AddTo"];
            }
        }
        
        /// <summary>
        /// The title for the outlook export widget.
        /// </summary>
        /// <value>Outlook</value>
        [ResourceEntry("OutlookEventExporterTitle",
            Value = "Outlook",
            Description = "The title for the outlook export widget.",
            LastModified = "2016/04/08")]
        public string OutlookEventExporterTitle
        {
            get
            {
                return this["OutlookEventExporterTitle"];
            }
        }

        /// <summary>
        /// The title for the ICal export widget.
        /// </summary>
        /// <value>ICal</value>
        [ResourceEntry("ICalEventExporterTitle",
            Value = "ICal",
            Description = "The title for the ICal export widget.",
            LastModified = "2016/04/08")]
        public string ICalEventExporterTitle
        {
            get
            {
                return this["ICalEventExporterTitle"];
            }
        }

        /// <summary>
        /// The title for the Google Calendar export widget.
        /// </summary>
        /// <value>Google Calendar</value>
        [ResourceEntry("GoogleEventExporterTitle",
            Value = "Google Calendar",
            Description = "The title for the Google Calendar export widget.",
            LastModified = "2016/04/08")]
        public string GoogleEventExporterTitle
        {
            get
            {
                return this["GoogleEventExporterTitle"];
            }
        }

        #endregion
        
        #region Designer resources

        /// <summary>
        /// phrase: Some of the selected events were deleted.
        /// </summary>
        [ResourceEntry("MissingEvents",
            Value = "Some of the selected events were deleted.",
            Description = "phrase: Some of the selected events were deleted.",
            LastModified = "2016/04/06")]
        public string MissingEvents
        {
            get
            {
                return this["MissingEvents"];
            }
        }

        /// <summary>
        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2016/04/06")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Gets phrase : Which events to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which events to display?",
            Description = "phrase : Which events to display?",
            LastModified = "2016/04/06")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// <summary>
        /// Gets word : Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2016/04/06")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// Gets phrase : All events
        /// </summary>
        [ResourceEntry("AllEvents",
            Value = "All events",
            Description = "All events",
            LastModified = "2016/04/06")]
        public string AllEvents
        {
            get
            {
                return this["AllEvents"];
            }
        }

        /// <summary>
        /// Gets phrase : Events by date...
        /// </summary>
        [ResourceEntry("EventsByDate",
            Value = "Events by date...",
            Description = "Events by date...",
            LastModified = "2016/04/06")]
        public string EventsByDate
        {
            get
            {
                return this["EventsByDate"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected events...
        /// </summary>
        [ResourceEntry("SelectedEvents",
            Value = "Selected events...",
            Description = "phrase : Selected events...",
            LastModified = "2016/04/06")]
        public string SelectedEvents
        {
            get
            {
                return this["SelectedEvents"];
            }
        }

        /// <summary>
        /// Gets phrase : Narrow selection
        /// </summary>
        [ResourceEntry("NarrowSelection",
            Value = "Narrow selection",
            Description = "phrase : Narrow selection",
            LastModified = "2016/04/06")]
        public string NarrowSelection
        {
            get
            {
                return this["NarrowSelection"];
            }
        }

        /// <summary>
        /// Gets phrase : All published events
        /// </summary>
        [ResourceEntry("AllPublishedEvents",
            Value = "All published events",
            Description = "phrase :All published events",
            LastModified = "2016/04/06")]
        public string AllPublishedEvents
        {
            get
            {
                return this["AllPublishedEvents"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Filtered events by...
        /// </summary>
        [ResourceEntry("FilteredEventsBy",
            Value = "Filtered events by...",
            Description = "phrase : Filtered events by...",
            LastModified = "2016/04/06")]
        public string FilteredEventsBy
        {
            get
            {
                return this["FilteredEventsBy"];
            }
        }

        /// <summary>
        /// phrase: List settings
        /// </summary>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2016/04/06")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// Phrase: Use paging
        /// </summary>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "Phrase: Use paging",
            LastModified = "2016/04/06")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Phrase: Divide the list on pages up to {0} items per page
        /// </summary>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "Phrase: Divide the list on pages up to {0} items per page",
            LastModified = "2016/04/06")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2016/04/06")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Phrase: Show only limited number of items {0} items in total
        /// </summary>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "Phrase: Show only limited number of items {0} items in total",
            LastModified = "2016/04/06")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Phrase: No limit and paging
        /// </summary>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "Phrase: No limit and paging",
            LastModified = "2016/04/06")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Phrase: Show all published items at once
        /// </summary>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "Phrase: Show all published items at once",
            LastModified = "2016/04/06")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Phrase: Sort events
        /// </summary>
        [ResourceEntry("SortItems",
            Value = "Sort events",
            Description = "Phrase: Sort events",
            LastModified = "2016/04/06")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published on top
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published on top",
            Description = "phrase : Last published on top",
            LastModified = "2016/04/06")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified on top
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified on top",
            Description = "phrase : Last modified on top",
            LastModified = "2016/04/06")]
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
            LastModified = "2016/04/06")]
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
            LastModified = "2016/04/06")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// phrase: By Start date (newest on top)
        /// </summary>
        [ResourceEntry("NewestTop",
            Value = "By Start date (newest on top)",
            Description = "phrase: By Start date (newest on top)",
            LastModified = "2016/04/06")]
        public string NewestTop
        {
            get
            {
                return this["NewestTop"];
            }
        }

        /// <summary>
        /// phrase: By Start date (oldest on top)
        /// </summary>
        [ResourceEntry("OldestTop",
            Value = "By Start date (oldest on top)",
            Description = "phrase: By Start date (oldest on top)",
            LastModified = "2016/04/06")]
        public string OldestTop
        {
            get
            {
                return this["OldestTop"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2016/04/06")]
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
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2016/04/06")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2016/04/06")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }
        
        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2016/04/06")]
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
            LastModified = "2016/04/06")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2016/04/06")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2016/04/06")]
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
            LastModified = "2016/04/06")]
        public string ShowInSamePage
        {
            get
            {
                return this["ShowInSamePage"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected existing page...
        /// </summary>
        [ResourceEntry("ShowInExistingPage",
            Value = "Selected existing page...",
            Description = "phrase : Selected existing page...",
            LastModified = "2016/04/06")]
        public string ShowInExistingPage
        {
            get
            {
                return this["ShowInExistingPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2016/04/06")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : current events
        /// </summary>
        [ResourceEntry("CurrentEvents",
            Value = "current events",
            Description = "phrase : current events",
            LastModified = "2016/04/07")]
        public string CurrentEvents
        {
            get
            {
                return this["CurrentEvents"];
            }
        }

        /// <summary>
        /// Gets phrase : past events...
        /// </summary>
        [ResourceEntry("PastEvents",
            Value = "past events...",
            Description = "phrase : past events...",
            LastModified = "2016/04/07")]
        public string PastEvents
        {
            get
            {
                return this["PastEvents"];
            }
        }

        /// <summary>
        /// Gets phrase : upcoming events...
        /// </summary>
        [ResourceEntry("UpcomingEvents",
            Value = "upcoming events...",
            Description = "phrase : upcoming events...",
            LastModified = "2016/04/07")]
        public string UpcomingEvents
        {
            get
            {
                return this["UpcomingEvents"];
            }
        }

        /// <summary>
        /// Gets phrase : Display upcoming events start in...
        /// </summary>
        [ResourceEntry("DisplayUpcomingStartsIn",
            Value = "Display upcoming events start in...",
            Description = "phrase : Display upcoming events start in...",
            LastModified = "2016/04/07")]
        public string DisplayUpcomingStartsIn
        {
            get
            {
                return this["DisplayUpcomingStartsIn"];
            }
        }

        /// <summary>
        /// Gets phrase : Display past events end in...
        /// </summary>
        [ResourceEntry("DisplayPastEndsIn",
            Value = "Display past events end in...",
            Description = "phrase : Display past events end in...",
            LastModified = "2016/04/07")]
        public string DisplayPastEndsIn
        {
            get
            {
                return this["DisplayPastEndsIn"];
            }
        }

        /// <summary>
        /// Gets Event widget calendat list title.
        /// </summary>
        /// <value>Calendars</value>
        [ResourceEntry("Calendars",
            Value = "Calendars",
            Description = "word: Calendars",
            LastModified = "2016/06/13")]
        public string Calendars
        {
            get
            {
                return this["Calendars"];
            }
        }

        #endregion
    }
}