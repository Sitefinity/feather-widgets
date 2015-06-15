using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("EmailCampaignsResources",
        ResourceClassId = "EmailCampaignsResources",
        Title = "EmailCampaignsResourcesTitle",
        TitlePlural = "EmailCampaignsResourcesTitlePlural",
        Description = "EmailCampaignsResourcesDescription")]
    public class EmailCampaignsResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="FlatTaxonomyResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public EmailCampaignsResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="FlatTaxonomyResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public EmailCampaignsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>EmailCampaignsResources labels</value>
        [ResourceEntry("EmailCampaignsResourcesTitle",
            Value = "EmailCampaignsResourcesTitle labels",
            Description = "The title of this class.",
            LastModified = "2015/05/19")]
        public string EmailCampaignsResourcesTitle
        {
            get
            {
                return this["EmailCampaignsResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>EmailCampaignsResources labels</value>
        [ResourceEntry("EmailCampaignsResourcesTitlePlural",
            Value = "EmailCampaignsResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/05/19")]
        public string EmailCampaignsResourcesTitlePlural
        {
            get
            {
                return this["EmailCampaignsResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("EmailCampaignsResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/05/19")]
        public string EmailCampaignsResourcesDescription
        {
            get
            {
                return this["EmailCampaignsResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/21")]
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
            LastModified = "2015/04/21")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : Template
        /// </summary>
        [ResourceEntry("TemplateLabel",
            Value = "Template",
            Description = "The phrase: Template",
            LastModified = "2015/05/19")]
        public string TemplateLabel
        {
            get
            {
                return this["TemplateLabel"];
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

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/01/12")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }
        #endregion
    }
}
