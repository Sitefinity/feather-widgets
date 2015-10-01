using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Engagement.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Blog widget
    /// </summary>
    [ObjectInfo(typeof(EngagementResources), ResourceClassId = "EngagementResources", Title = "EngagementResourcesTitle", Description = "EngagementResourcesDescription")]
    public class EngagementResources : Resource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class. 
        /// Initializes new instance of <see cref="BlogListResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public EngagementResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public EngagementResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Meta resources
        /// <summary>
        /// Title for the Engagement widget resources class.
        /// </summary>
        [ResourceEntry("EngagementTitle",
            Value = "Engagement widget resources",
            Description = "Title for the Engagement widget resources class.",
            LastModified = "2015/09/22")]
        public string EngagementResourcesTitle
        {
            get
            {
                return this["EngagementResourcesTitle"];
            }
        }

        /// <summary>
        /// Description for the Engagement widget resources class
        /// </summary>
        [ResourceEntry("EngagementResourcesDescription",
            Value = "Localizable strings for the Engagement widget.",
            Description = "Description for the Engagement widget resources class",
            LastModified = "2015/09/22")]
        public string EngagementResourcesDescription
        {
            get
            {
                return this["EngagementResourcesDescription"];
            }
        }
        #endregion

        /// <summary>
        /// This phrase is displayed when image was unpublished or has been deleted.
        /// </summary>
        /// <value>An image was not selected or has been deleted. Please select another one.</value>
        [ResourceEntry("ImageWasNotSelectedOrHasBeenDeleted",
            Value = "An image was not selected or has been deleted. Please select another one.",
            Description = "This phrase is displayed when image was unpublished or has been deleted.",
            LastModified = "2015/09/22")]
        public string ImageWasNotSelectedOrHasBeenDeleted
        {
            get
            {
                return this["ImageWasNotSelectedOrHasBeenDeleted"];
            }
        }

        /// <summary>
        /// phrase : More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/09/22")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        ///  word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = " word: Template",
            LastModified = "2015/09/22")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// phrase : CSS classes
        /// </summary>
        /// <value>CSS classes</value>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/09/22")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// word: Style
        /// </summary>
        /// <value>Style</value>
        [ResourceEntry("Style",
            Value = "Style",
            Description = "word: Style",
            LastModified = "2015/09/22")]
        public string Style
        {
            get
            {
                return this["Style"];
            }
        }
    }
}
