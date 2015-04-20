using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
