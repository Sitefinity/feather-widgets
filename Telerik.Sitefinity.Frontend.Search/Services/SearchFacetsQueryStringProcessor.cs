using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Publishing.PublishingPoints;
using Telerik.Sitefinity.Search;
using Telerik.Sitefinity.Search.Impl;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Services.Search.Data;

namespace Telerik.Sitefinity.Frontend.Search.Services
{
    internal class SearchFacetsQueryStringProcessor
    {
        internal IDictionary<string, IList<FacetResponse>> GetFacets(string query, string language, string filterParameters, string indexCatalogName, List<string> selectedFacetableFieldNames, string[] searchableFields, bool? resultsForAllSites = null)
        {
            IDictionary<string, IList<FacetResponse>> facetResult = new Dictionary<string, IList<FacetResponse>>();

            if (!selectedFacetableFieldNames.Any())
            {
                return facetResult;
            }

            List<FilterModel> currentFilter = new List<FilterModel>();
            FilterParameter filterParameter = new FilterParameter();
            if (!string.IsNullOrEmpty(filterParameters))
            {
                filterParameter = JsonConvert.DeserializeObject<FilterParameter>(filterParameters);
                currentFilter = filterParameter.AppliedFilters;
            }

            IResultSet searchResultsForTheNewFilter = this.GetFacetsInternal(query, language, currentFilter, indexCatalogName, selectedFacetableFieldNames, searchableFields, resultsForAllSites);
            facetResult = searchResultsForTheNewFilter.Facets;

            if (currentFilter.Any() && !filterParameter.IsDeselected && !string.IsNullOrEmpty(filterParameter.LastSelectedFilterGropName))
            {
                // Don't recalculate the last selected selected group in order to enable multiple selection (OR case)
                List<FilterModel> previoustFilterModel = currentFilter.Select(x => x).ToList();
                var itemToRemove = previoustFilterModel.FirstOrDefault(e => e.FieldName == filterParameter.LastSelectedFilterGropName);
                if (itemToRemove != null)
                {
                    previoustFilterModel.Remove(itemToRemove);
                }

                IResultSet previousSearchResults = this.GetFacetsInternal(query, language, previoustFilterModel, indexCatalogName, selectedFacetableFieldNames, searchableFields, resultsForAllSites);
                var previousSearchResultsFacets = previousSearchResults.Facets;
                var facetsFromPrivousSearchToInclude = previousSearchResultsFacets[filterParameter.LastSelectedFilterGropName];
                facetResult[filterParameter.LastSelectedFilterGropName] = facetsFromPrivousSearchToInclude;
            }

            if (currentFilter.Any())
            {
                this.SetSelectedMissingFiltersToResult(currentFilter, facetResult);
            }

            return facetResult;
        }

        internal ISearchFilter BuildFacetFilter(string filterQueryString, string indexCatalogue)
        {
            if (string.IsNullOrEmpty(filterQueryString))
            {
                throw new ArgumentException(nameof(filterQueryString));
            }

            var filterParameter = JsonConvert.DeserializeObject<FilterParameter>(filterQueryString);
            return this.BuildFacetFilterInternal(filterParameter.AppliedFilters, indexCatalogue);
        }

        internal IDictionary<string, FacetableFieldSettings> GetFacetableFields(string indexCatalogName)
        {
            if (string.IsNullOrEmpty(indexCatalogName))
            {
                throw new ArgumentNullException(nameof(indexCatalogName));
            }

            IEnumerable<SearchIndexAdditionalField> additionalProps = new List<SearchIndexAdditionalField>();
            PublishingManager publishingManger = PublishingManager.GetManager(PublishingConfig.SearchProviderName);

            var a = publishingManger.GetPublishingPoints().Where(p => p.Name == indexCatalogName).FirstOrDefault();
            SearchIndexPipeSettings pipeSettings = publishingManger
               .GetPipeSettings<SearchIndexPipeSettings>()
               .Where(ps => ps.CatalogName == indexCatalogName)
               .FirstOrDefault();

            if (pipeSettings != null)
            {
                additionalProps = PublishingUtilities.SearchIndexAdditionalFields(pipeSettings);
            }

            if (additionalProps != null && additionalProps.Any())
            {
                return additionalProps
                   .Where(f => f.IsFacetable)
                   .ToDictionary(x => x.FieldName, v => new FacetableFieldSettings()
                   {
                       FieldType = v.FieldType,
                       FieldName = v.FieldName,
                       FieldTitle = v.FieldTitle
                   });
            }

            return new Dictionary<string, FacetableFieldSettings>();
        }

        /// <summary>
        /// If the current facets don't have some of the currenlty selected, add them with count 0
        /// </summary>
        /// <param name="currentFilters">The currenlty applied filters</param>
        /// <param name="facetResult">The facet result</param>
        private void SetSelectedMissingFiltersToResult(List<FilterModel> currentFilters, IDictionary<string, IList<FacetResponse>> facetResult)
        {
            foreach (var facetGroup in facetResult)
            {
                var currentFilter = currentFilters.FirstOrDefault(f => f.FieldName == facetGroup.Key);
                if (currentFilter != null)
                {
                    var filterValues = facetGroup.Value.Select(x => x.FacetValue);
                    var missingValues = currentFilter.FilterValues
                        .Select(HttpUtility.UrlDecode)
                        .Except(filterValues, StringComparer.OrdinalIgnoreCase);

                    foreach (var item in missingValues)
                    {
                        facetGroup.Value.Add(new FacetResponse() { FacetValue = item, Count = 0 });
                    }
                }
            }
        }

        private ISearchFilter BuildFacetFilterInternal(List<FilterModel> filterModel, string indexCatalogue)
        {
            ISearchFilter facetFilter = ObjectFactory.Resolve<ISearchFilter>();
            var filtersByCategory = new List<ISearchFilter>();
            var facetableFieldsForIndex = this.GetFacetableFields(indexCatalogue);

            foreach (var filter in filterModel)
            {
                string decodedFieldName = HttpUtility.UrlDecode(filter.FieldName);

                FacetableFieldSettings facetableFieldSetting = this.GetFacetableFieldsettings(facetableFieldsForIndex, decodedFieldName);
                var filterOperator = this.GetFacetFilterSettings(facetableFieldSetting.FieldType);
                var searchClauses = filter.FilterValues.Select(filterValue => new SearchFilterClause(decodedFieldName, this.GetFieldValue(HttpUtility.UrlDecode(filterValue), facetableFieldSetting.FieldType), filterOperator));
                ISearchFilter currentFilter = ObjectFactory.Resolve<ISearchFilter>();
                currentFilter.Clauses = searchClauses;
                currentFilter.Operator = QueryOperator.Or;
                filtersByCategory.Add(currentFilter);
            }

            facetFilter.Groups = filtersByCategory;
            facetFilter.Operator = QueryOperator.And;

            return facetFilter;
        }

        private IResultSet GetFacetsInternal(string query, string language, List<FilterModel> filterModel, string indexCatalogName, List<string> facetableFields, string[] searchableFields, bool? resultsForAllSites)
        {
            var service = ServiceBus.ResolveService<ISearchService>();
            var queryBuilder = ObjectFactory.Resolve<IQueryBuilder>();
            var config = Config.Get<SearchConfig>();
            var enableExactMatch = config.EnableExactMatch;

            ISearchQuery searchQuery = queryBuilder.BuildQuery(query, searchableFields, language, resultsForAllSites);
            searchQuery.IndexName = indexCatalogName;
            searchQuery.Facets = facetableFields;
            searchQuery.EnableExactMatch = enableExactMatch;

            if (filterModel != null && filterModel.Any())
            {
                ISearchFilter filter = this.ConstructFilterObject(filterModel, indexCatalogName, searchQuery);
                searchQuery.Filter = filter;
            }

            var searchOptions = new SearchOptions(SearchType.StartsWith);
            IResultSet result = service.Search(searchQuery, searchOptions);
            return result;
        }

        private ISearchFilter ConstructFilterObject(List<FilterModel> filterModel, string indexName, ISearchQuery searchQuery)
        {
            var filterGroups = this.GetFilterGroups(searchQuery);
            var facetFilters = this.BuildFacetFilterInternal(filterModel, indexName);
            filterGroups.Add(facetFilters);

            ISearchFilter filter = ObjectFactory.Resolve<ISearchFilter>();
            filter.Operator = QueryOperator.And;
            filter.Groups = filterGroups;
            return filter;
        }

        private IList<ISearchFilter> GetFilterGroups(ISearchQuery searchQuery)
        {
            if (searchQuery != null && searchQuery.Filter != null && searchQuery.Filter.Groups != null)
            {
                return searchQuery.Filter.Groups.ToList();
            }

            return new List<ISearchFilter>();
        }

        private FilterOperator GetFacetFilterSettings(SearchIndexAdditonalFieldType fieldType)
        {
            FilterOperator filterOperator = this.GetFilterOperator(fieldType);

            return filterOperator;
        }

        private FacetableFieldSettings GetFacetableFieldsettings(IDictionary<string, FacetableFieldSettings> facetableFieldsSettings, string fieldName)
        {
            if (!facetableFieldsSettings.ContainsKey(fieldName))
            {
                throw new KeyNotFoundException("No such facetable field found");
            }

            var facetableFieldSetting = facetableFieldsSettings[fieldName];
            return facetableFieldSetting;
        }

        // The ISearchFilter needs the value object needs to be the correct type e.g. the value is suppose to be a number we need to pass it as number, not a string
        private object GetFieldValue(object value, SearchIndexAdditonalFieldType fieldType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (fieldType == SearchIndexAdditonalFieldType.NumberWhole)
            {
                long valueLong;
                if (long.TryParse(value.ToString(), out valueLong))
                {
                    return valueLong;
                }
            }

            if (fieldType == SearchIndexAdditonalFieldType.YesNo)
            {
                bool valueBool;
                if (bool.TryParse(value.ToString(), out valueBool))
                {
                    return valueBool;
                }
            }

            if (fieldType == SearchIndexAdditonalFieldType.NumberDecimal)
            {
                decimal decimalValue;
                string formatedValueAsString = value.ToString().Replace(',', '.');
                if (decimal.TryParse(formatedValueAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue))
                {
                    return decimalValue;
                }
            }

            if (fieldType == SearchIndexAdditonalFieldType.DateAndTime)
            {

                DateTime dateTimevalue;
                if (DateTime.TryParse(value.ToString(), out dateTimevalue))
                {
                    return dateTimevalue;
                }
            }

            return value.ToString();
        }

        private FilterOperator GetFilterOperator(SearchIndexAdditonalFieldType fieltType)
        {
            switch (fieltType)
            {
                case SearchIndexAdditonalFieldType.ShortText:
                case SearchIndexAdditonalFieldType.LongText:
                case SearchIndexAdditonalFieldType.DateAndTime:
                case SearchIndexAdditonalFieldType.YesNo:
                case SearchIndexAdditonalFieldType.NumberDecimal:
                case SearchIndexAdditonalFieldType.NumberWhole:
                    return FilterOperator.Equals;
                case SearchIndexAdditonalFieldType.Classification:
                case SearchIndexAdditonalFieldType.Choices:
                    return FilterOperator.Contains;
                default:
                    return FilterOperator.Equals;
            }
        }
    }
}
