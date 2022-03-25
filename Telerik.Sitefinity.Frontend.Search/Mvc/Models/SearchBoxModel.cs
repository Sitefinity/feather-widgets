﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;

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
                if (this.catalogueName == null && this.SearchIndexPipeId != null)
                {
                    this.catalogueName = this.GetCatalogueName(new Guid(this.SearchIndexPipeId));
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

        /// <inheritdoc />
        public string BackgroundHint
        {
            get
            {
                if (string.IsNullOrEmpty(this.backgroundHint))
                {
                    this.backgroundHint = Res.Get<SearchWidgetsResources>().BackgroundHint;
                }

                return this.backgroundHint;
            }
            set
            {
                this.backgroundHint = value;
            }
        }

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
            catch (Exception)
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
                    var sitemapBase = provider as SiteMapBase;
                    var node = sitemapBase == null ? provider.FindSiteMapNodeFromKey(this.ResultsPageId) : sitemapBase.FindSiteMapNodeFromKey(this.ResultsPageId, false);

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

        private string GetCatalogueName(Guid searchIndexPipeId)
        {
            var searchManager = PublishingManager.GetManager(PublishingConfig.SearchProviderName);
            var pipeSettings = searchManager.GetPipeSettings<SearchIndexPipeSettings>();
            var pipe = pipeSettings.SingleOrDefault(p => p.Id == searchIndexPipeId);
            if (pipe != null)
            {
                var siteId = SystemManager.CurrentContext.CurrentSite.Id;

                IList<Guid> sites;
                if (pipe.PublishingPoint.IsSharedWithAllSites)
                    sites = SystemManager.CurrentContext.GetSites().Select(s => s.Id).ToList();
                else
                    sites = this.GetSitesByPoint(pipe.PublishingPoint).Select(l => l.SiteId).ToList();

                if (sites.Contains(siteId))
                    return pipe.CatalogName;
            }
            return string.Empty;
        }

        private IEnumerable<SiteItemLink> GetSitesByPoint(PublishingPoint point)
        {
            var provider = ((IDataItem)point).Provider;
            if (provider == null)
                throw new ArgumentException("The passed publishing point does not have provider.");

            if (provider is PublishingDataProviderBase)
            {
                return ((PublishingDataProviderBase)provider).GetSiteItemLinks().Where(l => l.ItemId == point.Id);
            }

            return new List<SiteItemLink>(0);
        }
        #endregion

        #region Private fields and constants
        private string resultsUrl;
        private string searchIndexPipeId;
        private string catalogueName;
        private string backgroundHint;
        #endregion
    }
}
