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
        /// Initializes new instance of <see cref="StyleSheetResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public StyleSheetResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="StyleSheetResources"/> class with the provided <see cref="ResourceDataProvider"/>.
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

        #region Widget resources

        /// <summary>
        /// Gets phrase : Set CSS
        /// </summary>
        [ResourceEntry("SetCss",
            Value = "Set CSS",
            Description = "phrase : Set CSS",
            LastModified = "2015/04/28")]
        public string SetCss
        {
            get
            {
                return this["SetCss"];
            }
        }

        // <summary>
        /// Gets phrase : Included in the HTML &lt;head&gt; tag
        /// </summary>
        [ResourceEntry("IncludedInHead",
            Value = "Included in the HTML &lt;head&gt; tag",
            Description = "phrase : Included in the HTML &lt;head&gt; tag",
            LastModified = "2015/04/28")]
        public string IncludedInHead
        {
            get
            {
                return this["IncludedInHead"];
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

        /// <summary>
        /// Gets word: URL
        /// </summary>
        [ResourceEntry("Url",
            Value = "URL",
            Description = "word : URL",
            LastModified = "2015/04/27")]
        public string Url
        {
            get
            {
                return this["Url"];
            }
        }

        /// Gets phrase : Write CSS
        /// </summary>
        [ResourceEntry("WriteCss",
            Value = "Write CSS",
            Description = "phrase : Write CSS",
            LastModified = "2015/04/27")]
        public string WriteCss
        {
            get
            {
                return this["WriteCss"];
            }
        }

        /// <summary>
        /// Gets phrase : Link to CSS file
        /// </summary>
        [ResourceEntry("LinkToCssFile",
            Value = "Link to CSS file",
            Description = "phrase : Link to CSS file",
            LastModified = "2015/04/27")]
        public string LinkToCssFile
        {
            get
            {
                return this["LinkToCssFile"];
            }
        }

        /// <summary>
        /// Gets phrase : Media
        /// </summary>
        [ResourceEntry("Media",
            Value = "Media",
            Description = "phrase : Media",
            LastModified = "2015/04/27")]
        public string Media
        {
            get
            {
                return this["Media"];
            }
        }

        /// <summary>
        /// Gets phrase : All
        /// </summary>
        [ResourceEntry("All",
            Value = "All",
            Description = "phrase : All",
            LastModified = "2015/04/27")]
        public string All
        {
            get
            {
                return this["All"];
            }
        }

        /// <summary>
        /// Gets phrase : Select media types...
        /// </summary>
        [ResourceEntry("SelectedMediaTypes",
            Value = "Select media types...",
            Description = "phrase : Select media types...",
            LastModified = "2015/04/27")]
        public string SelectedMediaTypes
        {
            get
            {
                return this["AllSelectedMediaTypes"];
            }
        }

        /// <summary>
        /// Gets phrase : Description
        /// </summary>
        [ResourceEntry("Description",
            Value = "Description",
            Description = "phrase : Description",
            LastModified = "2015/04/27")]
        public string Description
        {
            get
            {
                return this["Description"];
            }
        }

        /// <summary>
        /// Gets phrase : Start writing CSS rules directly
        /// </summary>
        [ResourceEntry("StartWritingCss",
            Value = "Start writing CSS rules directly",
            Description = "phrase : Start writing CSS rules directly",
            LastModified = "2015/04/27")]
        public string StartWritingCss
        {
            get
            {
                return this["StartWritingCss"];
            }
        }

        /// <summary>
        /// word: Tips
        /// </summary>
        /// <value>Tips</value>
        [ResourceEntry("Tips",
            Value = "Tips",
            Description = "word: Tips",
            LastModified = "2015/04/28")]
        public string Tips
        {
            get
            {
                return this["Tips"];
            }
        }

        /// <summary>
        /// CssAreaDescription
        /// </summary>
        /// <value>CSS written here will be included in the HTML <head> tag.</value>
        [ResourceEntry("CssAreaDescription",
            Value = "CSS written here will be included in the HTML &lt;head&gt; tag.",
            Description = "Description for the css area field.",
            LastModified = "2015/04/28")]
        public string CssAreaDescription
        {
            get
            {
                return this["CssAreaDescription"];
            }
        }

        /// <summary>
        /// DoNotAddStyleTag
        /// </summary>
        /// <value>Do not add <style> tag, it will be added automatically. Start writing CSS rules directly.</value>
        [ResourceEntry("DoNotAddStyleTag",
            Value = "Do not add &lt;style&gt; tag, it will be added automatically. Start writing CSS rules directly.",
            Description = "Description for the css area field.",
            LastModified = "2015/04/28")]
        public string DoNotAddStyleTag
        {
            get
            {
                return this["DoNotAddStyleTag"];
            }
        }

        /// <summary>
        /// word: Example
        /// </summary>
        /// <value>Example</value>
        [ResourceEntry("Example",
            Value = "Example",
            Description = "word: Example",
            LastModified = "2015/04/28")]
        public string Example
        {
            get
            {
                return this["Example"];
            }
        }

        /// <summary>
        /// Css code example
        /// </summary>
        /// <value><code>body {<br/>background: #fff;<br/>font-size: 12px;<br/>}</code></value>
        [ResourceEntry("CssCodeExample",
            Value = "<code>body {<br/>background: #fff;<br/>font-size: 12px;<br/>}</code>",
            Description = "word: Example",
            LastModified = "2015/04/28")]
        public string CssCodeExample
        {
            get
            {
                return this["CssCodeExample"];
            }
        }

        #endregion
    }
}
