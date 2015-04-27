using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for working with StyleSheets
    /// </summary>
    [ObjectInfo("StyleSheetResources", ResourceClassId = "StyleSheetResources", Title = "StyleSheetResourcesTitle", Description = "StyleSheetResourcesDescription")]
    public class StyleSheetResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="DocumentResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public StyleSheetResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="DocumentResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public StyleSheetResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description

        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>StyleSheetResources labels</value>
        [ResourceEntry("StyleSheetResourcesTitle",
            Value = "StyleSheetResources labels",
            Description = "The title of this class.",
            LastModified = "2015/04/27")]
        public string StyleSheetResourcesTitle
        {
            get
            {
                return this["StyleSheetResourcesTitle"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("StyleSheetResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/04/27")]
        public string StyleSheetResourcesDescription
        {
            get
            {
                return this["StyleSheetResourcesDescription"];
            }
        }
        #endregion

        #region Designer resources

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/27")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        #endregion
    }
}
