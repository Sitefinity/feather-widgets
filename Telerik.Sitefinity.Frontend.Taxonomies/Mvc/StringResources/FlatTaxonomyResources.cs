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
        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/21")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/04/21")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : News
        /// </summary>
        [ResourceEntry("NewsItem",
            Value = "News",
            Description = "The phrase: News.",
            LastModified = "2015.05.19")]
        public string NewsItem
        {
            get
            {
                return this["NewsItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Blog posts
        /// </summary>
        [ResourceEntry("BlogPost",
                     Value = "Blog posts",
                     Description = "The phrase: Blog posts.",
                     LastModified = "2015.05.19")]
        public string BlogPost
        {
            get
            {
                return this["BlogPost"];
            }
        }

        /// <summary>
        /// Gets phrase : Blogs
        /// </summary>
        [ResourceEntry("Blog",
                     Value = "Blogs",
                     Description = "The phrase: Blogs.",
                     LastModified = "2015.05.19")]
        public string Blog
        {
            get
            {
                return this["Blog"];
            }
        }

        /// <summary>
        /// Gets phrase : Images
        /// </summary>
        [ResourceEntry("Image",
                     Value = "Images",
                     Description = "The phrase: Images.",
                     LastModified = "2015.05.19")]
        public string Image
        {
            get
            {
                return this["Image"];
            }
        }

        /// <summary>
        /// Gets phrase : Documents & files
        /// </summary>
        [ResourceEntry("Document",
                     Value = "Documents & files",
                     Description = "The phrase: Documents & files.",
                     LastModified = "2015.05.19")]
        public string Document
        {
            get
            {
                return this["Document"];
            }
        }

        /// <summary>
        /// Gets phrase : Videos
        /// </summary>
        [ResourceEntry("Video",
                       Value = "Videos",
                       Description = "The phrase: Videos.",
                       LastModified = "2015.05.19")]
        public string Video
        {
            get
            {
                return this["Video"];
            }
        }

        /// <summary>
        /// Gets phrase : List items
        /// </summary>
        [ResourceEntry("ListItem",
                       Value = "List items",
                       Description = "The phrase: List items.",
                       LastModified = "2015.05.19")]
        public string ListItem
        {
            get
            {
                return this["ListItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Only tags used by content type...
        /// </summary>
        [ResourceEntry("ShowContentTypeTagsLabel",
                       Value = "Only tags used by content type...",
                       Description = "The phrase: Only tags used by content type...",
                       LastModified = "2015.05.19")]
        public string ShowContentTypeTagsLabel
        {
            get
            {
                return this["ShowContentTypeTagsLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected tags...
        /// </summary>
        [ResourceEntry("SelectedTagsLabel",
                       Value = "Selected tags...",
                       Description = "The phrase: Selected tags...",
                       LastModified = "2015.05.19")]
        public string SelectedTagsLabel
        {
            get
            {
                return this["SelectedTagsLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : All tags
        /// </summary>
        [ResourceEntry("AllTags",
                       Value = "All tags",
                       Description = "The phrase: All tags",
                       LastModified = "2015.05.19")]
        public string AllTags
        {
            get
            {
                return this["AllTags"];
            }
        }

        /// <summary>
        /// Gets phrase : Which tags to display?
        /// </summary>
        [ResourceEntry("WhichTagsLabel",
                       Value = "Which tags to display?",
                       Description = "The phrase: Which tags to display?",
                       LastModified = "2015.05.19")]
        public string WhichTagsLabel
        {
            get
            {
                return this["WhichTagsLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Show item count
        /// </summary>
        [ResourceEntry("ShowItemCountLabel",
                       Value = "Show item count",
                       Description = "The phrase: Show item count",
                       LastModified = "2015.05.19")]
        public string ShowItemCountLabel
        {
            get
            {
                return this["ShowItemCountLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Show empty tags
        /// </summary>
        [ResourceEntry("ShowEmptyTags",
                       Value = "Show empty tags",
                       Description = "The phrase: Show empty tags",
                       LastModified = "2015.05.19")]
        public string ShowEmptyTags
        {
            get
            {
                return this["ShowEmptyTags"];
            }
        }

        /// <summary>
        /// Gets phrase : Sort tags
        /// </summary>
        [ResourceEntry("SortTagsLabel",
                       Value = "Sort tags",
                       Description = "The phrase: Sort tags",
                       LastModified = "2015.05.19")]
        public string SortTagsLabel
        {
            get
            {
                return this["SortTagsLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Template
        /// </summary>
        [ResourceEntry("TemplateLabel",
                       Value = "Template",
                       Description = "The phrase: Template",
                       LastModified = "2015.05.19")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published",
            Description = "phrase : Last published",
            LastModified = "2014/08/22")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified",
            Description = "phrase : Last modified",
            LastModified = "2014/08/22")]
        public string LastModified
        {
            get
            {
                return this["LastModified"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAZ",
            Value = "By Title (A-Z)",
            Description = "phrase : By Title (A-Z)",
            LastModified = "2014/08/22")]
        public string ByTitleAZ
        {
            get
            {
                return this["ByTitleAZ"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (Z-A)
        /// </summary>
        [ResourceEntry("ByTitleZA",
            Value = "By Title (Z-A)",
            Description = "phrase : By Title (Z-A)",
            LastModified = "2014/08/22")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        /// <value>As set manually</value>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2015/01/12")]
        public string AsSetManually
        {
            get
            {
                return this["AsSetManually"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/01/12")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }
        #endregion
    }
}