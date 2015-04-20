using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Video Gallery widget
    /// </summary>
    [ObjectInfo(typeof(VideoGalleryResources), ResourceClassId = "VideoGalleryResources", Title = "VideoGalleryResourcesTitle", Description = "VideoGalleryResourcesDescription")]
    public class VideoGalleryResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="VideoGalleryResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public VideoGalleryResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoGalleryResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public VideoGalleryResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// Gets Title for the Video Gallery widget resources class.
        /// </summary>
        [ResourceEntry("VideoGalleryResourcesTitle",
            Value = "Video Gallery widget resources",
            Description = "Title for the Video Gallery widget resources class.",
            LastModified = "2015/04/16")]
        public string VideoGalleryResourcesTitle
        {
            get
            {
                return this["VideoGalleryResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Video Gallery widget resources class.
        /// </summary>
        [ResourceEntry("VideoGalleryResourcesDescription",
            Value = "Localizable strings for the Video Gallery widget.",
            Description = "Description for the Video Gallery widget resources class.",
            LastModified = "2015/04/16")]
        public string VideoGalleryResourcesDescription
        {
            get
            {
                return this["VideoGalleryResourcesDescription"];
            }
        }

        #endregion

        /// <summary>
        /// phrase: Next video
        /// </summary>
        [ResourceEntry("NextVideo",
            Value = "Next video",
            Description = "phrase: Next video",
            LastModified = "2015/04/20")]
        public string NextVideo
        {
            get
            {
                return this["NextVideo"];
            }
        }

        /// <summary>
        /// phrase: Previous video
        /// </summary>
        /// <value>Previous video</value>
        [ResourceEntry("PreviousVideo",
            Value = "Previous video",
            Description = "phrase: Previous video",
            LastModified = "2015/04/20")]
        public string PreviousVideo
        {
            get
            {
                return this["PreviousVideo"];
            }
        }

        /// <summary>
        /// word: by
        /// </summary>
        /// <value>by</value>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2015/04/20")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// phrase: Back to all videos
        /// </summary>
        /// <value>Back to all videos</value>
        [ResourceEntry("BackToAllVideos",
            Value = "Back to all videos",
            Description = "phrase: Back to all videos",
            LastModified = "2015/04/20")]
        public string BackToAllVideos
        {
            get
            {
                return this["BackToAllVideos"];
            }
        }

        /// <summary>
        /// phrase: [Index] of [TotalCount]
        /// </summary>
        /// <value>{0} of {1}</value>
        [ResourceEntry("IndexOfTotal",
            Value = "{0} of {1}",
            Description = "phrase: [Index] of [TotalCount]",
            LastModified = "2015/04/20")]
        public string IndexOfTotal
        {
            get
            {
                return this["IndexOfTotal"];
            }
        }
        
        #region Designer

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/16")]
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
            LastModified = "2015/04/16")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }
        /// <summary>
        /// phrase: List settings
        /// </summary>
        /// <value>List settings</value>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2015/04/20")]
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
        /// <value>Use paging</value>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "Phrase: Use paging",
            LastModified = "2015/04/20")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// phrase: Some of the selected libraries were moved or deleted.
        /// </summary>
        /// <value>Some of the selected libraries were moved or deleted.</value>
        [ResourceEntry("MissingLibraries",
            Value = "Some of the selected libraries were moved or deleted.",
            Description = "phrase: Some of the selected libraries were moved or deleted.",
            LastModified = "2015/04/20")]
        public string MissingLibraries
        {
            get
            {
                return this["MissingLibraries"];
            }
        }

        /// <summary>
        /// Phrase: Divide the list on pages up to {0} items per page
        /// </summary>
        /// <value>Divide the list on pages up to {0} items per page</value>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "Phrase: Divide the list on pages up to {0} items per page",
            LastModified = "2015/04/20")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2015/04/20")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Phrase: Show only limited number of items {0} items in total
        /// </summary>
        /// <value>Show only limited number of items {0} items in total</value>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "Phrase: Show only limited number of items {0} items in total",
            LastModified = "2015/04/20")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// Gets phrase : Which videos to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which videos to display?",
            Description = "phrase : Which videos to display?",
            LastModified = "2015/04/20")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// <summary>
        /// Phrase: No limit and paging
        /// </summary>
        /// <value>No limit and paging</value>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "Phrase: No limit and paging",
            LastModified = "2015/04/20")]
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
        /// <value>Show all published items at once</value>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "Phrase: Show all published items at once",
            LastModified = "2015/04/20")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Phrase: Sort videos
        /// </summary>
        /// <value>Sort videos</value>
        [ResourceEntry("SortItems",
            Value = "Sort videos",
            Description = "Phrase: Sort videos",
            LastModified = "2015/04/20")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// Gets word : Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2015/04/20")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }


        /// <summary>
        /// phrase: From all libraries
        /// </summary>
        [ResourceEntry("AllLibraries",
            Value = "From all libraries",
            Description = "phrase: From all libraries",
            LastModified = "2015/04/20")]
        public string AllLibraries
        {
            get
            {
                return this["AllLibraries"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2015/04/20")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Phrase: From selected libraries only...
        /// </summary>
        /// <value>From selected libraries only...</value>
        [ResourceEntry("FromSelectedLibraries",
            Value = "From selected libraries only...",
            Description = "Phrase: From selected libraries only...",
            LastModified = "2015/04/20")]
        public string FromSelectedLibraries
        {
            get
            {
                return this["FromSelectedLibraries"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified",
            Description = "phrase : Last modified",
            LastModified = "2015/04/20")]
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
            LastModified = "2015/04/20")]
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
            LastModified = "2015/04/20")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// Phrase: From currently opened library
        /// </summary>
        /// <value>From currently opened library</value>
        [ResourceEntry("FromCurrentlyOpen",
            Value = "From currently opened library",
            Description = "Phrase: From currently opened library",
            LastModified = "2015/04/20")]
        public string FromCurrentlyOpen
        {
            get
            {
                return this["FromCurrentlyOpen"];
            }
        }


        /// <summary>
        /// Phrase: Selection of videos...
        /// </summary>
        /// <value>Selection of videos...</value>
        [ResourceEntry("SelectionOfVideos",
            Value = "Selection of videos...",
            Description = "Phrase: Selection of videos...",
            LastModified = "2015/04/20")]
        public string SelectionOfVideos
        {
            get
            {
                return this["SelectionOfVideos"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        /// <value>As set manually</value>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2015/04/20")]
        public string AsSetManually
        {
            get
            {
                return this["AsSetManually"];
            }
        }

        /// <summary>
        /// phrase : Narrow selection by...
        /// </summary>
        /// <value>Narrow selection by...</value>
        [ResourceEntry("NarrowSelection",
            Value = "Narrow selection by...",
            Description = "phrase : Narrow selection by...",
            LastModified = "2015/04/20")]
        public string NarrowSelection
        {
            get
            {
                return this["NarrowSelection"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/04/20")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// Phrase: All published videos
        /// </summary>
        /// <value>All published videos</value>
        [ResourceEntry("AllPublishedVideos",
            Value = "All published videos",
            Description = "Phrase: All published videos",
            LastModified = "2015/04/20")]
        public string AllPublishedVideos
        {
            get
            {
                return this["AllPublishedVideos"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2015/04/20")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/04/20")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// phrase: Thumbnail size
        /// </summary>
        /// <value>Thumbnail size</value>
        [ResourceEntry("ThumbnailSize",
            Value = "Thumbnail size",
            Description = "phrase: Thumbnail size",
            LastModified = "2015/04/20")]
        public string ThumbnailSize
        {
            get
            {
                return this["ThumbnailSize"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2015/04/20")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        #endregion
    }
}
