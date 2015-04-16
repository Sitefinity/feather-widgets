using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.StringResources
{
    /// <summary>
    /// This class contains localizable strings related to the <see cref="Telerik.Sitefinity.Frontend.Media.Mvc.Controllers.VideoController"/>
    /// </summary>
    [ObjectInfo("VideoResources", ResourceClassId = "VideoResources", Title = "VideoResourcesTitle", TitlePlural = "VideoResourcesTitlePlural", Description = "VideoResourcesDescription")]
    public class VideoResources : Resource
    {
        #region Construction

        /// <summary>
        /// Initializes new instance of <see cref="VideoResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public VideoResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="VideoResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public VideoResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>VideoResources labels</value>
        [ResourceEntry("VideoResourcesTitle",
            Value = "VideoResources labels",
            Description = "The title of this class.",
            LastModified = "2015/04/13")]
        public string VideoResourcesTitle
        {
            get
            {
                return this["VideoResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>VideoResources labels</value>
        [ResourceEntry("VideoResourcesTitlePlural",
            Value = "VideoResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/04/13")]
        public string VideoResourcesTitlePlural
        {
            get
            {
                return this["VideoResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("VideoResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/04/13")]
        public string VideoResourcesDescription
        {
            get
            {
                return this["VideoResourcesDescription"];
            }
        }

        #endregion

        #region Resources

        /// <summary>
        /// Phrase: Select a video or other file
        /// </summary>
        /// <value>Select a video or other file</value>
        [ResourceEntry("SelectVideo",
            Value = "Select a video or other file",
            Description = "Phrase: Select a video or other file",
            LastModified = "2015/04/13")]
        public string SelectVideo
        {
            get
            {
                return this["SelectVideo"];
            }
        }

        /// <summary>
        /// Phrase: If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.
        /// </summary>
        /// <value>If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.</value>
        [ResourceEntry("VideoWillNotBeDisplayed",
            Value = "If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.",
            Description = "Phrase:If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.",
            LastModified = "2015/04/13")]
        public string VideoWillNotBeDisplayed
        {
            get
            {
                return this["VideoWillNotBeDisplayed"];
            }
        }

        /// <summary>
        /// Phrase: A video was not selected or has been deleted. Please select another one.
        /// </summary>
        /// <value>A video was not selected or has been deleted. Please select another one.</value>
        [ResourceEntry("VideoNotSelectedOrDeleted",
            Value = "A video was not selected or has been deleted. Please select another one.",
            Description = "Phrase:A video was not selected or has been deleted. Please select another one.",
            LastModified = "2015/04/16")]
        public string VideoNotSelectedOrDeleted
        {
            get
            {
                return this["VideoNotSelectedOrDeleted"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/13")]
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
            LastModified = "2015/04/13")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/04/13")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        #endregion
    }
}
