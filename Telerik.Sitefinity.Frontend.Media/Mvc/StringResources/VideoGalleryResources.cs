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

        #region Designer

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
