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

        /// <summary>
        /// Phrase: Write JavaScript
        /// </summary>
        /// <value>Write JavaScript</value>
        [ResourceEntry("WriteJavaScript",
            Value = "Write JavaScript",
            Description = "Phrase: Write JavaScript",
            LastModified = "2015/04/28")]
        public string WriteJavaScript
        {
            get
            {
                return this["WriteJavaScript"];
            }
        }

        /// <summary>
        /// Phrase: Link to JavaScript file
        /// </summary>
        /// <value>Link to JavaScript file</value>
        [ResourceEntry("LinkToJavaScriptFile",
            Value = "Link to JavaScript file",
            Description = "Phrase: Link to JavaScript file",
            LastModified = "2015/04/28")]
        public string LinkToJavaScriptFile
        {
            get
            {
                return this["LinkToJavaScriptFile"];
            }
        }

        /// <summary>
        /// Phrase: Start writing your JavaScript directly
        /// </summary>
        /// <value>Start writing your JavaScript directly</value>
        [ResourceEntry("StartWritingJavaScript",
            Value = "Start writing your JavaScript directly",
            Description = "Phrase: Start writing your JavaScript directly",
            LastModified = "2015/04/28")]
        public string StartWritingJavaScript
        {
            get
            {
                return this["StartWritingJavaScript"];
            }
        }

        /// <summary>
        /// Phrase: Do not add <script> tag, it will be added automatically. Start writing your JavaScript directly.
        /// </summary>
        /// <value>Do not add &lt;script&gt; tag, it will be added automatically. Start writing your JavaScript directly.</value>
        [ResourceEntry("DoNotAddScriptTag",
            Value = "Do not add &lt;script&gt; tag, it will be added automatically. Start writing your JavaScript directly.",
            Description = "Phrase: Do not add <script> tag, it will be added automatically. Start writing your JavaScript directly.",
            LastModified = "2015/04/28")]
        public string DoNotAddScriptTag
        {
            get
            {
                return this["DoNotAddScriptTag"];
            }
        }

        /// <summary>
        /// Code
        /// </summary>
        /// <value><code>var str = \"Hello World!\";</code></value>
        [ResourceEntry("JavaScriptCodeExample",
            Value = "<code>var str = \"Hello World!\";</code>",
            Description = "Code",
            LastModified = "2015/04/28")]
        public string JavaScriptCodeExample
        {
            get
            {
                return this["JavaScriptCodeExample"];
            }
        }

        /// <summary>
        /// Word: Tips
        /// </summary>
        /// <value>Tips</value>
        [ResourceEntry("Tips",
            Value = "Tips",
            Description = "Word: Tips",
            LastModified = "2015/04/28")]
        public string Tips
        {
            get
            {
                return this["Tips"];
            }
        }

        /// <summary>
        /// Word: URL
        /// </summary>
        /// <value>URL</value>
        [ResourceEntry("Url",
            Value = "URL",
            Description = "Word: URL",
            LastModified = "2015/04/28")]
        public string Url
        {
            get
            {
                return this["Url"];
            }
        }

        /// <summary>
        /// Phrase: Where to include in HTML?
        /// </summary>
        /// <value>Where to include in HTML?</value>
        [ResourceEntry("WhereToInclude",
            Value = "Where to include in HTML?",
            Description = "Phrase: Where to include in HTML?",
            LastModified = "2015/04/28")]
        public string WhereToInclude
        {
            get
            {
                return this["WhereToInclude"];
            }
        }

        /// <summary>
        /// Phrase: More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "Phrase: More options",
            LastModified = "2015/04/28")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Word: Description
        /// </summary>
        /// <value>Description</value>
        [ResourceEntry("Description",
            Value = "Description",
            Description = "Word: Description",
            LastModified = "2015/04/28")]
        public string Description
        {
            get
            {
                return this["Description"];
            }
        }

        /// <summary>
        /// Word: Example
        /// </summary>
        /// <value>Example</value>
        [ResourceEntry("Example",
            Value = "Example",
            Description = "Word: Example",
            LastModified = "2015/04/28")]
        public string Example
        {
            get
            {
                return this["Example"];
            }
        }
    }
}
