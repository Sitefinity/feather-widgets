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

        /// <summary>
        /// Phrase: Where to search?
        /// </summary>
        /// <value>Where to search?</value>
        [ResourceEntry("WhereToSearch",
            Value = "Where to search?",
            Description = "Phrase: Where to search?",
            LastModified = "2015/01/06")]
        public string WhereToSearch
        {
            get
            {
                return this["WhereToSearch"];
            }
        }

        /// <summary>
        /// Phrase: - Select search index -
        /// </summary>
        /// <value>- Select search index -</value>
        [ResourceEntry("SelectSearchIndex",
            Value = "- Select search index -",
            Description = "Phrase: - Select search index -",
            LastModified = "2015/01/06")]
        public string SelectSearchIndex
        {
            get
            {
                return this["SelectSearchIndex"];
            }
        }

        /// <summary>
        /// Phrase: What's this?
        /// </summary>
        /// <value>What's this?</value>
        [ResourceEntry("WhatIsThis",
            Value = "What's this?",
            Description = "Phrase: What's this?",
            LastModified = "2015/01/06")]
        public string WhatIsThis
        {
            get
            {
                return this["WhatIsThis"];
            }
        }

        /// <summary>
        /// Phrase: Search indexes You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website. You can manage search indexes in Administration > Search indexes
        /// </summary>
        /// <value>Search indexes You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website. You can manage search indexes in Administration > Search indexes</value>
        [ResourceEntry("SearchIndexesDescription",
            Value = "Search indexes You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website. You can manage search indexes in Administration > Search indexes",
            Description = "Phrase: Search indexes You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website. You can manage search indexes in Administration > Search indexes",
            LastModified = "2015/01/06")]
        public string SearchIndexesDescription
        {
            get
            {
                return this["SearchIndexesDescription"];
            }
        }

        /// <summary>
        /// Phrase: More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "Phrase: More options",
            LastModified = "2015/01/06")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Phrase: CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "Phrase: CSS classes",
            LastModified = "2015/01/06")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Phrase: Where to display search results?
        /// </summary>
        [ResourceEntry("WhereToDisplaySearchResults",
            Value = "Where to display search results?",
            Description = "Phrase: Where to display search results?",
            LastModified = "2015/01/06")]
        public string WhereToDisplaySearchResults
        {
            get
            {
                return this["WhereToDisplaySearchResults"];
            }
        }

        /// <summary>
        /// Phrase: This is the page where you have dropped
        /// </summary>
        [ResourceEntry("ThisIsPageWhereYouHaveDropped",
            Value = "This is the page where you have dropped",
            Description = "Phrase: This is the page where you have dropped",
            LastModified = "2015/01/06")]
        public string ThisIsPageWhereYouHaveDropped
        {
            get
            {
                return this["ThisIsPageWhereYouHaveDropped"];
            }
        }

        /// <summary>
        /// Phrase: Search results widget
        /// </summary>
        [ResourceEntry("SearchResultsWidget",
            Value = "Search results widget",
            Description = "Phrase: Search results widget",
            LastModified = "2015/01/06")]
        public string SearchResultsWidget
        {
            get
            {
                return this["SearchResultsWidget"];
            }
        }

        /// <summary>
        /// Word: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "Word: Template",
            LastModified = "2015/01/06")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Phrase: No search indexes have been created yet
        /// </summary>
        [ResourceEntry("NoSearchIndexes",
            Value = "No search indexes have been created yet",
            Description = "Phrase: No search indexes have been created yet",
            LastModified = "2015/01/06")]
        public string NoSearchIndexes
        {
            get
            {
                return this["NoSearchIndexes"];
            }
        }

        /// <summary>
        /// Phrase: Set where to search
        /// </summary>
        /// <value>Set where to search</value>
        [ResourceEntry("SearchBoxEmptyLinkText",
            Value = "Set where to search",
            Description = "Phrase: Set where to search",
            LastModified = "2015/01/07")]
        public string SearchBoxEmptyLinkText
        {
            get
            {
                return this["SearchBoxEmptyLinkText"];
            }
        }
        #endregion
    }
}
