using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("FlatTaxonomyResources", 
        ResourceClassId = "FlatTaxonomyResources", 
        Title = "FlatTaxonomyResourcesTitle", 
        TitlePlural = "FlatTaxonomyResourcesTitlePlural",
        Description = "FlatTaxonomyResourcesDescription")]
    public class FlatTaxonomyResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="FlatTaxonomyResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public FlatTaxonomyResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="FlatTaxonomyResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public FlatTaxonomyResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>FlatTaxonomyResources labels</value>
        [ResourceEntry("FlatTaxonomyResourcesTitle",
            Value = "HierarchicalTaxonomyResources labels",
            Description = "The title of this class.",
            LastModified = "2015.05.19")]
        public string FlatTaxonomyResourcesTitle
        {
            get
            {
                return this["FlatTaxonomyResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>FlatTaxonomyResources labels</value>
        [ResourceEntry("FlatTaxonomyResourcesTitlePlural",
            Value = "FlatTaxonomyResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015.05.19")]
        public string FlatTaxonomyResourcesTitlePlural
        {
            get
            {
                return this["FlatTaxonomyResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("FlatTaxonomyResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015.05.19")]
        public string FlatTaxonomyResourcesDescription
        {
            get
            {
                return this["FlatTaxonomyResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        #endregion
    }
}