using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Search box and Search results widgets
    /// </summary>
    [ObjectInfo("SearchWidgetsResources", ResourceClassId = "SearchWidgetsResources", Title = "SearchWidgetsResourcesTitle", TitlePlural = "SearchWidgetsResourcesTitlePlural", Description = "SearchWidgetsResourcesDescription")]
    public class SearchWidgetsResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="SearchWidgetsResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public SearchWidgetsResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="SearchWidgetsResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public SearchWidgetsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>SearchWidgetsResources labels</value>
        [ResourceEntry("SearchWidgetsResourcesTitle",
            Value = "SearchWidgetsResources labels",
            Description = "The title of this class.",
            LastModified = "2015/01/05")]
        public string SearchWidgetsResourcesTitle
        {
            get
            {
                return this["SearchWidgetsResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>SearchWidgetsResources labels</value>
        [ResourceEntry("SearchWidgetsResourcesTitlePlural",
            Value = "SearchWidgetsResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/01/05")]
        public string SearchWidgetsResourcesTitlePlural
        {
            get
            {
                return this["SearchWidgetsResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("SearchWidgetsResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/01/05")]
        public string SearchWidgetsResourcesDescription
        {
            get
            {
                return this["SearchWidgetsResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Word: Search
        /// </summary>
        /// <value>Search</value>
        [ResourceEntry("SearchLabel",
            Value = "Search",
            Description = "Word: Search",
            LastModified = "2015/01/05")]
        public string SearchLabel
        {
            get
            {
                return this["SearchLabel"];
            }
        }
        #endregion
    }
}
