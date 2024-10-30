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
        /// Word: Search input
        /// </summary>
        /// <value>Search input</value>
        [ResourceEntry("SearchInput",
            Value = "Search input",
            Description = "Word: Search",
            LastModified = "2015/07/04")]
        public string SearchInput
        {
            get
            {
                return this["SearchInput"];
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
            Description = "Phrase:- Select search index -",
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
        /// Phrase: Search indexes
        /// </summary>
        /// <value>Search indexes</value>
        [ResourceEntry("SearchIndexesDescriptionTitle",
            Value = "Search indexes",
            Description = "Phrase: Search indexes",
            LastModified = "2015/01/06")]
        public string SearchIndexesDescriptionTitle
        {
            get
            {
                return this["SearchIndexesDescriptionTitle"];
            }
        }

        /// <summary>
        /// Phrase: You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website.
        /// </summary>
        /// <value>You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website.</value>
        [ResourceEntry("SearchIndexesDescription",
            Value = "You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website.",
            Description = "Phrase: You use search indexes to define different sets of content to be searched by the visitiors on your website by using the internal search of the website.",
            LastModified = "2015/01/06")]
        public string SearchIndexesDescription
        {
            get
            {
                return this["SearchIndexesDescription"];
            }
        }

        /// <summary>
        /// Phrase: You can manage search indexes in Administration > Search indexes
        /// </summary>
        /// <value>You can manage search indexes in Administration > Search indexes</value>
        [ResourceEntry("SearchIndexesDescriptionWhere",
            Value = "You can manage search indexes in Administration > Search indexes",
            Description = "You can manage search indexes in Administration > Search indexes",
            LastModified = "2015/01/06")]
        public string SearchIndexesDescriptionWhere
        {
            get
            {
                return this["SearchIndexesDescriptionWhere"];
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

        /// <summary>
        /// Phrase: search results for <em>{0}</em>
        /// </summary>
        [ResourceEntry("SearchResultsStatusMessageShort",
            Value = "search results for <em>{0}</em>",
            Description = "Phrase: search results for <em>{0}</em>",
            LastModified = "2015/01/08")]
        public string SearchResultsStatusMessageShort
        {
            get
            {
                return this["SearchResultsStatusMessageShort"];
            }
        }

        #region SearchResult designer resources
        /// <summary>
        /// Phrase: Sort results
        /// </summary>
        /// <value>Sort results</value>
        [ResourceEntry("SortResults",
            Value = "Sort results",
            Description = "Phrase: Sort results",
            LastModified = "2015/01/08")]
        public string SortResults
        {
            get
            {
                return this["SortResults"];
            }
        }

        /// <summary>
        /// Phrase: Most relevant results on top
        /// </summary>
        /// <value>Most relevant results on top</value>
        [ResourceEntry("MostRelevant",
            Value = "Most relevant results on top",
            Description = "Phrase: Most relevant results on top",
            LastModified = "2015/01/08")]
        public string MostRelevant
        {
            get
            {
                return this["MostRelevant"];
            }
        }

        /// <summary>
        /// Word: Relevance
        /// </summary>
        /// <value>Relevance</value>
        [ResourceEntry("Relevance",
            Value = "Relevance",
            Description = "Word: Relevance",
            LastModified = "2015/01/08")]
        public string Relevance
        {
            get
            {
                return this["Relevance"];
            }
        }

        /// <summary>
        /// Phrase: Newest first
        /// </summary>
        /// <value>Newest first</value>
        [ResourceEntry("NewestFirst",
            Value = "Newest first",
            Description = "Phrase: Newest first",
            LastModified = "2015/01/08")]
        public string NewestFirst
        {
            get
            {
                return this["NewestFirst"];
            }
        }

        /// <summary>
        /// Phrase: Oldest first
        /// </summary>
        /// <value>Oldest first</value>
        [ResourceEntry("OldestFirst",
            Value = "Oldest first",
            Description = "Phrase: Oldest first",
            LastModified = "2015/01/08")]
        public string OldestFirst
        {
            get
            {
                return this["OldestFirst"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAZ",
            Value = "By Title (A-Z)",
            Description = "phrase : By Title (A-Z)",
            LastModified = "2015/01/08")]
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
            LastModified = "2015/01/08")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// Phrase: New-modified first
        /// </summary>
        /// <value>New-modified first</value>
        [ResourceEntry("NewModified",
            Value = "New-modified first",
            Description = "Phrase: New-modified first",
            LastModified = "2015/01/08")]
        public string NewModified
        {
            get
            {
                return this["NewModified"];
            }
        }

        /// <summary>
        /// Phrase: Allow users to sort results
        /// </summary>
        /// <value>Allow users to sort results</value>
        [ResourceEntry("AllowUserToSort",
            Value = "Allow users to sort results",
            Description = "Phrase: Allow users to sort results",
            LastModified = "2015/01/08")]
        public string AllowUserToSort
        {
            get
            {
                return this["AllowUserToSort"];
            }
        }

        /// <summary>
        /// Gets phrase : Use paging
        /// </summary>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "phrase : Use paging",
            LastModified = "2015/01/08")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/01/08")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Gets phrase : No limit and paging
        /// </summary>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "phrase : No limit and paging",
            LastModified = "2015/01/08")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Divide the list on pages up to {TextBox} items per page
        /// </summary>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "phrase : Divide the list on pages up to {TextBox} items per page",
            LastModified = "2015/01/08")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Show only limited number of items {TextBox} items in total
        /// </summary>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "phrase : Show only limited number of items {TextBox} items in total",
            LastModified = "2015/01/08")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Show all published items at once
        /// </summary>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "phrase : Show all published items at once",
            LastModified = "2015/01/08")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }
        #endregion

        /// <summary>
        /// phrase: Show results in
        /// </summary>
        /// <value>Show results in</value>
        [ResourceEntry("ChangeResultsLanguageLabel",
            Value = "Show results in",
            Description = "phrase: Show results in",
            LastModified = "2015/01/08")]
        public string ChangeResultsLanguageLabel
        {
            get
            {
                return this["ChangeResultsLanguageLabel"];
            }
        }

        /// <summary>
        /// word: or
        /// </summary>
        /// <value>or</value>
        [ResourceEntry("OrLabel",
            Value = "or",
            Description = "word: or",
            LastModified = "2015/01/09")]
        public string OrLabel
        {
            get
            {
                return this["OrLabel"];
            }
        }

        /// <summary>
        /// word: No
        /// </summary>
        /// <value>No</value>
        [ResourceEntry("No",
            Value = "No",
            Description = "word: No",
            LastModified = "2015/01/20")]
        public string No
        {
            get
            {
                return this["No"];
            }
        }

        /// <summary>
        /// Phrase: Sort by
        /// </summary>
        /// <value>Sort by</value>
        [ResourceEntry("SortBy",
            Value = "Sort by",
            Description = "Phrase: Sort by",
            LastModified = "2015/01/20")]
        public string SortBy
        {
            get
            {
                return this["SortBy"];
            }
        }

        /// <summary>
        /// Phrase: Search
        /// </summary>
        /// <value>Search</value>
        [ResourceEntry("BackgroundHint",
            Value = "Search",
            Description = "Phrase: Search",
            LastModified = "2015/03/05")]
        public string BackgroundHint
        {
            get
            {
                return this["BackgroundHint"];
            }
        }

        /// <summary>
        /// phrase: Sort dropdown
        /// </summary>
        /// <value>Sort dropdown</value>
        [ResourceEntry("SortDropdown",
            Value = "Sort dropdown",
            Description = "phrase: Sort dropdown",
            LastModified = "2015/06/05")]
        public string SortDropdown
        {
            get
            {
                return this["SortDropdown"];
            }
        }

        /// <summary>
        /// word: Next
        /// </summary>
        /// <value>Next</value>
        [ResourceEntry("Next",
            Value = "Next",
            Description = "word: Next",
            LastModified = "2017/01/05")]
        public string Next
        {
            get
            {
                return this["Next"];
            }
        }

        /// <summary>
        /// word: Prev
        /// </summary>
        /// <value>Prev</value>
        [ResourceEntry("Prev",
            Value = "Prev",
            Description = "word: Prev",
            LastModified = "2017/01/05")]
        public string Prev
        {
            get
            {
                return this["Prev"];
            }
        }

        /// <summary>
        /// Control name: Search
        /// </summary>
        [ResourceEntry("SearchBoxTitle",
            Value = "Search box",
            Description = "Control title: Search",
            LastModified = "2019/06/03")]
        public string SearchBoxTitle
        {
            get
            {
                return this["SearchBoxTitle"];
            }
        }

        /// <summary>
        /// Control description: A search box control for entering search criteria.
        /// </summary>
        [ResourceEntry("SearchBoxDescription",
            Value = "Search box",
            Description = "Control description: A search box control for entering search criteria.",
            LastModified = "2019/06/03")]
        public string SearchBoxDescription
        {
            get
            {
                return this["SearchBoxDescription"];
            }
        }

        /// <summary>
        /// Control name: Search results
        /// </summary>
        [ResourceEntry("SearchResultsTitle",
            Value = "Search results",
            Description = "Control title: Search results",
            LastModified = "2019/06/03")]
        public string SearchResultsTitle
        {
            get
            {
                return this["SearchResultsTitle"];
            }
        }

        /// <summary>
        /// Control description: A control for displaying search results.
        /// </summary>
        [ResourceEntry("SearchResultsDescription",
            Value = "List of search results",
            Description = "Control description: A control for displaying search results.",
            LastModified = "2019/06/03")]
        public string SearchResultsDescription
        {
            get
            {
                return this["SearchResultsDescription"];
            }
        }

        /// <summary>
        /// Control description: Boost search results.
        /// </summary>
        [ResourceEntry("BoostSearchResults",
            Value = "Boost search results",
            Description = "Control description: Boost search results.",
            LastModified = "2021/10/05")]
        public string BoostSearchResults
        {
            get
            {
                return this["BoostSearchResults"];
            }

        }

        /// <summary>
        /// Control description:Scoring profiles are part of the search index, and they consist of weighted fields, functions, and parameters. You use scoring profiles to boost search results by customizing the way different fields are ranked. You can manage scoring profiles in the Azure portal.
        /// </summary>
        [ResourceEntry("ScoringProfilesDescription",
            Value = "Scoring profiles are part of the search index, and they consist of weighted fields, functions, and parameters. You use scoring profiles to boost search results by customizing the way different fields are ranked. You can manage scoring profiles in the Azure portal.",
            Description = "Control description: Scoring profiles are part of the search index, and they consist of weighted fields, functions, and parameters. You use scoring profiles to boost search results by customizing the way different fields are ranked. You can manage scoring profiles in the Azure portal.",
            LastModified = "2021/10/05")]
        public string ScoringProfilesDescription
        {
            get
            {
                return this["ScoringProfilesDescription"];
            }

        }

        /// <summary>
        /// Phrase: - Select scoring profile -.
        /// </summary>
        [ResourceEntry("SelectScoringProfile",
            Value = "- Select scoring profile -",
            Description = "Phrase: - Select scoring profile -.",
            LastModified = "2021/10/05")]
        public string SelectScoringProfile
        {
            get
            {
                return this["SelectScoringProfile"];
            }

        }

        /// <summary>
        /// Label: scoring profile has been deleted. Select different scoring profile or create a new one.
        /// </summary>
        /// <value> scoring profile has been deleted. Select different scoring profile or create a new one.</value>
        [ResourceEntry("ScoringProfileHasBeenDeleted",
            Value = " scoring profile has been deleted. Select different scoring profile or create a new one.",
            Description = "Label: scoring profile has been deleted. Select different scoring profile or create a new one.",
            LastModified = "2021/11/22")]
        public string ScoringProfileHasBeenDeleted
        {
            get
            {
                return this["ScoringProfileHasBeenDeleted"];
            }
        }

        /// <summary>
        /// Label: No scoring profiles have been created yet.
        /// </summary>
        /// <value>No scoring profiles have been created yet.</value>
        [ResourceEntry("NoScoringProfileCreated",
            Value = "No scoring profiles have been created yet.",
            Description = "Label: No scoring profiles have been created yet.",
            LastModified = "2021/11/22")]
        public string NoScoringProfileCreated
        {
            get
            {
                return this["NoScoringProfileCreated"];
            }
        }

        /// <summary>
        /// Label: Search facets
        /// </summary>
        /// <value>Facets widget</value>
        [ResourceEntry("FacetsWidgetTitle",
            Value = "Search facets",
            Description = "Label: Search facets",
            LastModified = "2022/07/11")]
        public string FacetsWidgetTitle
        {
            get
            {
                return this["FacetsWidgetTitle"];
            }
        }

        /// <summary>
        /// Label: Lists with search facets
        /// </summary>
        /// <value>Facets widget</value>
        [ResourceEntry("FacetsWidgDescription",
            Value = "Lists with search facets",
            Description = "Label: Lists with search facets",
            LastModified = "2022/07/11")]
        public string FacetsWidgDescription
        {
            get
            {
                return this["FacetsWidgDescription"];
            }
        }

        /// <summary>
        /// Label: Facets widget empty view text
        /// </summary>
        /// <value>Select search facets</value>
        [ResourceEntry("FacetWidgetEmptyResult",
            Value = "Select search facets",
            Description = "Label: Select search facets",
            LastModified = "2022/08/11")]
        public string FacetWidgetEmptyResult
        {
            get
            {
                return this["FacetWidgetEmptyResult"];
            }
        }

        /// <summary>
        /// Phrase: This widget cannot be used with your current search service. Search facets works only with Azure Search and Elasticsearch services.
        /// </summary>
        /// <value>Select search facets</value>
        [ResourceEntry("FacetsNotSupportedMessage",
            Value = "This widget cannot be used with your current search service. Search facets works only with Azure Search and Elasticsearch services.",
            Description = "Phrase: This widget cannot be used with your current search service. Search facets works only with Azure Search and Elasticsearch services.",
            LastModified = "2022/08/12")]
        public string FacetsNotSupportedMessage
        {
            get
            {
                return this["FacetsNotSupportedMessage"];
            }
        }

        /// <summary>
        /// Message shown when the search module is deactivated
        /// </summary>
        /// <value>This widget doesn't work, because Search module has been deactivated.</value>
        [ResourceEntry("SearchModuleDeactivatedMessage",
            Value = "This widget doesn't work, because Search module has been deactivated.",
            Description = "Message shown when the search module is deactivated",
            LastModified = "2015/06/17")]
        public string SearchModuleDeactivatedMessage
        {
            get
            {
                return this["SearchModuleDeactivatedMessage"];
            }

            #endregion
        }
    }
}