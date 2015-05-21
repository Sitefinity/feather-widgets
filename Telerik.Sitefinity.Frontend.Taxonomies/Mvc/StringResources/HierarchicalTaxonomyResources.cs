using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("HierarchicalTaxonomyResources", ResourceClassId = "HierarchicalTaxonomyResources", Title = "HierarchicalTaxonomyResourcesTitle", TitlePlural = "HierarchicalTaxonomyResourcesTitlePlural", Description = "HierarchicalTaxonomyResourcesDescription")]
    public class HierarchicalTaxonomyResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="HierarchicalTaxonomyResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public HierarchicalTaxonomyResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="HierarchicalTaxonomyResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public HierarchicalTaxonomyResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>HierarchicalTaxonomyResources labels</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesTitle",
            Value = "HierarchicalTaxonomyResources labels",
            Description = "The title of this class.",
            LastModified = "2015.05.19")]
        public string HierarchicalTaxonomyResourcesTitle
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>HierarchicalTaxonomyResources labels</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesTitlePlural",
            Value = "HierarchicalTaxonomyResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015.05.19")]
        public string HierarchicalTaxonomyResourcesTitlePlural
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("HierarchicalTaxonomyResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015.05.19")]
        public string HierarchicalTaxonomyResourcesDescription
        {
            get
            {
                return this["HierarchicalTaxonomyResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        #endregion
    }
}