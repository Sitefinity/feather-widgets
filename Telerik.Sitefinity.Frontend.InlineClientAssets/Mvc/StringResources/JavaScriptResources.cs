using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("JavaScriptResources", ResourceClassId = "JavaScriptResources", Title = "JavaScriptResourcesTitle", TitlePlural = "JavaScriptResourcesTitlePlural", Description = "JavaScriptResourcesDescription")]
    public class JavaScriptResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="JavaScriptResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public JavaScriptResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="JavaScriptResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public JavaScriptResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>JavaScriptResources labels</value>
        [ResourceEntry("JavaScriptResourcesTitle",
            Value = "JavaScriptResources labels",
            Description = "The title of this class.",
            LastModified = "2015/04/28")]
        public string JavaScriptResourcesTitle
        {
            get
            {
                return this["JavaScriptResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>JavaScriptResources labels</value>
        [ResourceEntry("JavaScriptResourcesTitlePlural",
            Value = "JavaScriptResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/04/28")]
        public string JavaScriptResourcesTitlePlural
        {
            get
            {
                return this["JavaScriptResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("JavaScriptResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/04/28")]
        public string JavaScriptResourcesDescription
        {
            get
            {
                return this["JavaScriptResourcesDescription"];
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Phrase: Included in the HTML <head> tag
        /// </summary>
        /// <value>Included in the HTML <head> tag</value>
        [ResourceEntry("IncludedInTheHead",
            Value = "Included in the HTML <head> tag",
            Description = "Phrase: Included in the HTML <head> tag",
            LastModified = "2015/04/28")]
        public string IncludedInTheHead
        {
            get
            {
                return this["IncludedInTheHead"];
            }
        }

        /// <summary>
        /// Phrase: Included where the widget is dropped
        /// </summary>
        /// <value>Included where the widget is dropped</value>
        [ResourceEntry("IncludedWhereDropped",
            Value = "Included where the widget is dropped",
            Description = "Phrase: Included where the widget is dropped",
            LastModified = "2015/04/28")]
        public string IncludedWhereDropped
        {
            get
            {
                return this["IncludedWhereDropped"];
            }
        }

        /// <summary>
        /// Phrase: Included before the closing body tag
        /// </summary>
        /// <value>Included before the closing body tag</value>
        [ResourceEntry("IncludedBeforeTheBodyEnd",
            Value = "Included before the closing body tag",
            Description = "Phrase: Included before the closing body tag",
            LastModified = "2015/04/28")]
        public string IncludedBeforeTheBodyEnd
        {
            get
            {
                return this["IncludedBeforeTheBodyEnd"];
            }
        }
        #endregion
    }
}
