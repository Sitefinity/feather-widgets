using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace News.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the News widget
    /// </summary>
    [ObjectInfo(typeof(NewsResources), Title = "NewsResourcesTitle", Description = "NewsResourcesDescription")]
    public class NewsResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsResources"/> class.
        /// </summary>
        public NewsResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public NewsResources(ResourceDataProvider dataProvider)
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
        /// <value>
        /// The display.
        /// </value>
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
        /// Gets word : Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "word : Template",
            LastModified = "2014/08/20")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }
    }
}
