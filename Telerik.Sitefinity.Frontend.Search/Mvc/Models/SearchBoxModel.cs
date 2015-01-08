using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// This class represents the model used by Search box controller.
    /// </summary>
    public class SearchBoxModel : ISearchBoxModel
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxModel" /> class.
        /// </summary>
        /// <param name="suggestionsRoute">The suggestions service end point.</param>
        /// <param name="minSuggestionLength">The minimal suggestion length.</param>
        /// <param name="suggestionFields">The suggestion fields.</param>
        /// <param name="language">The current UI language.</param>
        public SearchBoxModel(string suggestionsRoute, int minSuggestionLength, string suggestionFields, string language)
        {
            this.SuggestionFields = suggestionFields;
            this.SuggestionsRoute = suggestionsRoute;
            this.MinSuggestionLength = minSuggestionLength;
            this.Language = language;
        }

        #endregion

        #region Properties
        /// <inheritdoc />
        public WordsMode WordsMode { get; set; }

        /// <inheritdoc />
        public string ResultsPageId { get; set; }

        /// <inheritdoc />
        public string ResultsUrl
        {
            get
            {
                if (String.IsNullOrEmpty(this.resultsUrl))
                {
                    this.resultsUrl = this.GetResultsUrl(this.ResultsPageId);
                }

                return this.resultsUrl;
            }
            set
            {
            }
        }

        /// <inheritdoc />
        public string SiteRootName { get; set; }

        /// <inheritdoc />
        public string SearchIndexPipeId
        {
            get
            {
                return this.searchIndexPipeId;
            }
            set
            {
                this.searchIndexPipeId = value;
                this.catalogueName = null;
            }
        }

        /// <inheritdoc />
        public string IndexCatalogue
        {
            get
            {
                if (this.catalogueName == null)
                {
                    //// this.catalogueName = this.GetCatalogueName(new Guid(this.searchIndexPipeId));
                    this.catalogueName = string.Empty;
                }
                return this.catalogueName;
            }
            set
            {
                this.catalogueName = value;
            }
        }

        /// <inheritdoc />
        public string SuggestionFields { get; set; }

        /// <inheritdoc />
        public string SuggestionsRoute { get; set; }

        /// <inheritdoc />
        public bool DisableSuggestions { get; set; }

        /// <inheritdoc />
        public int MinSuggestionLength { get; set; }

        /// <inheritdoc />
        public string Language { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }        
        #endregion

        #region Private methods
        private SiteMapProvider GetSiteMapProvider()
        {
            SiteMapProvider provider;
            try
            {
                if (string.IsNullOrEmpty(this.SiteRootName))
                    provider = SiteMapBase.GetSiteMapProvider(SiteMapBase.DefaultSiteMapProviderName);
                else
                    provider = SiteMapBase.GetSiteMapProvider(this.SiteRootName);

                return provider;
            }
            catch (Exception ex)
            {
                provider = null;
            }
            return provider;
        }

        private string GetResultsUrl(string resultsPageId)
        {
            var resultsUrl = string.Empty;

            if (resultsPageId.IsGuid())
            {
                var provider = this.GetSiteMapProvider();
                if (provider != null)
                {
                    var node = provider.FindSiteMapNodeFromKey(resultsPageId);
                    if (node != null)
                    {
                        resultsUrl = node.Url;
                    }
                }
            }

            if (string.IsNullOrEmpty(resultsUrl))
            {
                var node = SiteMapBase.GetActualCurrentNode();
                if (node != null)
                    resultsUrl = node.Url;
            }

            // If ML is using different domains, the url does not need to be resolved
            if (!RouteHelper.IsCompleteUrl(resultsUrl))
            {
                return RouteHelper.ResolveUrl(resultsUrl, UrlResolveOptions.Rooted);
            }
            else
            {
                return resultsUrl;
            }
        }

        //// private string GetCatalogueName(Guid searchIndexPipeId)
        //// {
        ////    var searchManager = PublishingManager.GetManager(PublishingConfig.SearchProviderName);
        ////    var pipeSettings = searchManager.GetPipeSettings<SearchIndexPipeSettings>();
        ////    var pipe = pipeSettings.SingleOrDefault(p => p.Id == searchIndexPipeId);
        ////    if (pipe != null)
        ////    {
        ////        if (!SystemManager.CurrentContext.IsMultisiteMode)
        ////            return pipe.CatalogName;
        ////        else
        ////        {
        ////            var siteId = SystemManager.CurrentContext.CurrentSite.Id;
        ////            var sites = PublishingManager.GetSitesByPointFromCache(pipe.PublishingPoint);
        ////            if (sites.Contains(siteId))
        ////                return pipe.CatalogName;
        ////        }
        ////    }
        ////    return string.Empty;
        //// }
        #endregion

        #region Private fields and constants
        private string resultsUrl;
        private string searchIndexPipeId;
        private string catalogueName;
        #endregion
    }
}
