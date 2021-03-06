﻿using System;
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
        /// Phrase: In the head tag
        /// </summary>
        /// <value>In the head tag</value>
        [ResourceEntry("IncludedInTheHead",
            Value = "In the head tag",
            Description = "Phrase: In the head tag",
            LastModified = "2015/05/05")]
        public string IncludedInTheHead
        {
            get
            {
                return this["IncludedInTheHead"];
            }
        }

        /// <summary>
        /// Phrase: Where the widget is dropped
        /// </summary>
        /// <value>Where the widget is dropped</value>
        [ResourceEntry("IncludedWhereDropped",
            Value = "Where the widget is dropped",
            Description = "Phrase: Where the widget is dropped",
            LastModified = "2015/05/05")]
        public string IncludedWhereDropped
        {
            get
            {
                return this["IncludedWhereDropped"];
            }
        }

        /// <summary>
        /// Phrase: Before the closing body tag
        /// </summary>
        /// <value>Before the closing body tag</value>
        [ResourceEntry("IncludedBeforeTheBodyEnd",
            Value = "Before the closing body tag",
            Description = "Phrase: Before the closing body tag",
            LastModified = "2015/05/05")]
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

        /// <summary>
        /// Phrase: Select a JavaScript file
        /// </summary>
        /// <value>Select a JavaScript file</value>
        [ResourceEntry("SelectJsFile",
            Value = "Select a JavaScript file",
            Description = "Phrase: Select a JavaScript file",
            LastModified = "2015/05/04")]
        public string SelectJsFile
        {
            get
            {
                return this["SelectJsFile"];
            }
        }

        /// <summary>
        /// Phrase: Set JavaScript
        /// </summary>
        /// <value>Set JavaScript</value>
        [ResourceEntry("SetJS",
            Value = "Set JavaScript",
            Description = "Phrase: Set JavaScript",
            LastModified = "2015/05/05")]
        public string SetJS
        {
            get
            {
                return this["SetJS"];
            }
        }

        /// <summary>
        /// Phrase: For your convenience only
        /// </summary>
        /// <value>For your convenience only</value>
        [ResourceEntry("ForYourConvenience",
            Value = "For your convenience only",
            Description = "Phrase: For your convenience only",
            LastModified = "2015/05/05")]
        public string ForYourConvenience
        {
            get
            {
                return this["ForYourConvenience"];
            }
        }

        /// <summary>
        /// Control name: Embed Java script
        /// </summary>
        [ResourceEntry("JavaScriptEmbedControlTitle",
            Value = "JavaScript",
            Description = "Control title: Embed Java Script",
            LastModified = "2019/06/03")]
        public string JavaScriptEmbedControlTitle
        {
            get
            {
                return this["JavaScriptEmbedControlTitle"];
            }
        }

        /// <summary>
        /// Control description: A control for embedding Java Script files/code.
        /// </summary>
        [ResourceEntry("JavaScriptEmbedControlDescription",
            Value = "Embeds JavaScript files or custom code in this page",
            Description = "Control description: A control for embedding Java Script files/code.",
            LastModified = "2019/06/03")]
        public string JavaScriptEmbedControlDescription
        {
            get
            {
                return this["JavaScriptEmbedControlDescription"];
            }
        }
    }
}
