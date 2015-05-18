using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources
{
    /// <summary>
    /// Sitefinity localizable strings
    /// </summary>
    [ObjectInfo("EmbedCodeResources",
                    ResourceClassId = "EmbedCodeResources",
                    Title = "EmbedCodeResourcesTitle",
                    TitlePlural = "EmbedCodeResourcesTitlePlural",
                    Description = "EmbedCodeResourcesDescription")]
    public class EmbedCodeResources : Resource
    {
        #region Construction
        /// <summary>
        /// Initializes new instance of <see cref="EmbedCodeResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public EmbedCodeResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="EmbedCodeResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public EmbedCodeResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        /// <summary>
        /// The title of the class.
        /// </summary>
        /// <value>EmbedCodeResources labels</value>
        [ResourceEntry("EmbedCodeResourcesTitle",
            Value = "EmbedCodeResources labels",
            Description = "The title of this class.",
            LastModified = "2015/04/28")]
        public string EmbedCodeResourcesTitle
        {
            get
            {
                return this["EmbedCodeResourcesTitle"];
            }
        }

        /// <summary>
        /// The plural title of this class.
        /// </summary>
        /// <value>EmbedCodeResources labels</value>
        [ResourceEntry("EmbedCodeResourcesTitlePlural",
            Value = "EmbedCodeResources labels",
            Description = "The plural title of this class.",
            LastModified = "2015/04/28")]
        public string EmbedCodeResourcesTitlePlural
        {
            get
            {
                return this["EmbedCodeResourcesTitlePlural"];
            }
        }

        /// <summary>
        /// The description of this class.
        /// </summary>
        /// <value>Contains localizable resources.</value>
        [ResourceEntry("EmbedCodeResourcesDescription",
            Value = "Contains localizable resources.",
            Description = "The description of this class.",
            LastModified = "2015/04/28")]
        public string EmbedCodeResourcesDescription
        {
            get
            {
                return this["EmbedCodeResourcesDescription"];
            }
        }
        #endregion

        [ResourceEntry("EmbedCode",
            Value = "Embed code",
            Description = "The phrase: Embed code",
            LastModified = "2015/04/28")]
        public string EmbedCode
        {
            get
            {
                return this["EmbedCode"];
            }
        }

        /// <summary>
        /// Phrase: Write CSS, JavaScript or paste embed code like Google Analytics
        /// </summary>
        /// <value>Write CSS, JavaScript or paste embed code like Google Analytics</value>
        [ResourceEntry("StartWritingEmbedCode",
            Value = "Write CSS, JavaScript or paste embed code like Google Analytics",
            Description = "Phrase: Write CSS, JavaScript or paste embed code like Google Analytics",
            LastModified = "2015/04/28")]
        public string StartWritingEmbedCode
        {
            get
            {
                return this["StartWritingEmbedCode"];
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
        /// Gets the embed code example.
        /// </summary>
        /// <value>The embed code example.</value>
        [ResourceEntry("EmbedCodeExample",
            Value = "Write CSS, JavaScript or paste embed code like Google Analytics or YouTube video. Code will be Included where the widget is dropped.",
            Description = "Phrase: Write CSS, JavaScript or paste embed code like Google Analytics or YouTube video.",
            LastModified = "2015/04/28")]
        public string EmbedCodeExample
        {
            get
            {
                return this["EmbedCodeExample"];
            }
        }

        /// <summary>
        /// Gets the css script code label.
        /// </summary>
        [ResourceEntry("CssCode",
            Value = "Write CSS rules by &lt;style&gt; tag:",
            Description = "Code",
            LastModified = "2015/04/28")]
        public string CssCode
        {
            get
            {
                return this["CssCode"];
            }
        }

        /// <summary>
        /// Gets the css script code example.
        /// </summary>
        [ResourceEntry("CssCodeExample",
            Value = "<code>&lt;style type=\"text/css\"&gt; <br/> body {font-size: 12px;} <br/> &lt;/style&gt;</code>",
            Description = "Code",
            LastModified = "2015/04/28")]
        public string CssCodeExample
        {
            get
            {
                return this["CssCodeExample"];
            }
        }

        /// <summary>
        /// Gets the java script code label.
        /// </summary>
        /// <value>Write your javascript by <script> tag:</value>
        [ResourceEntry("JavaScriptCode",
            Value = "Write your javascript by &lt;script&gt; tag:",
            Description = "Code",
            LastModified = "2015/04/28")]
        public string JavaScriptCode
        {
            get
            {
                return this["JavaScriptCode"];
            }
        }

        /// <summary>
        /// Gets the java script code example.
        /// </summary>
        /// <value><code><script type=\"text/javascript\">var x = 10;</script></code>"</value>
        [ResourceEntry("JavaScriptCodeExample",
            Value = "<code>&lt;script type=\"text/javascript\"&gt;<br/>var x = 10; <br/> &lt;/script&gt; </code>",
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
        /// Phrase: Included where the widget is dropped
        /// </summary>
        /// <value>Included where the widget is dropped</value>
        [ResourceEntry("IncludedWhereDropped",
            Value = "Included where the widget is dropped",
            Description = "Phrase: Included where the widget is dropped",
            LastModified = "2015/05/05")]
        public string IncludedWhereDropped
        {
            get
            {
                return this["IncludedWhereDropped"];
            }
        }
    }
}
