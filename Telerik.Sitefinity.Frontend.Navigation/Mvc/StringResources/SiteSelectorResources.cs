using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings for the Site selector widget.
    /// </summary>
    [ObjectInfo("SiteSelectorResources", ResourceClassId = "SiteSelectorResources", Title = "SiteSelectorResourcesTitle", TitlePlural = "SiteSelectorResourcesTitlePlural", Description = "SiteSelectorResourcesDescription")]
    public class SiteSelectorResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="SiteSelectorResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public SiteSelectorResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="SiteSelectorResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public SiteSelectorResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>SiteSelectorResources labels</value>
        [ResourceEntry("SiteSelectorResourcesTitle",
            Value = "SiteSelectorResources labels",
            Description = "The title of this class.",
            LastModified = "2015.05.12")]
        public string SiteSelectorResourcesTitle
        {
            get
            {
                return this["SiteSelectorResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>SiteSelectorResources labels</value>
        [ResourceEntry("SiteSelectorResourcesTitlePlural",
            Value = "SiteSelectorResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015.05.12")]
        public string SiteSelectorResourcesTitlePlural
        {
            get
            {
                return this["SiteSelectorResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("SiteSelectorResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015.05.12")]
        public string SiteSelectorResourcesDescription
        {
            get
            {
                return this["SiteSelectorResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        #endregion
    }
}