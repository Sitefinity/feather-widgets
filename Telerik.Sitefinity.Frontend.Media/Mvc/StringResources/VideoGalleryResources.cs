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
    }
}
