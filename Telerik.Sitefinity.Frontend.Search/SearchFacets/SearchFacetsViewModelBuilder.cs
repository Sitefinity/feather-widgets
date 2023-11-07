using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Publishing.PublishingPoints;
using Telerik.Sitefinity.Search.Facets;
using Telerik.Sitefinity.Search.Impl.Facets;

namespace Telerik.Sitefinity.Frontend.Search.SearchFacets
{
    internal class SearchFacetsViewModelBuilder
    {
        public SearchFacetsViewModelBuilder()
        {
            this.widgetSettingsFacetFieldMapper = new WidgetSettingsFacetFieldMapper();
        }

        internal IList<SearchFacetsViewModel> BuildFacetsViewModel(
            IList<FacetWidgetFieldModel> facetsWidgetDefinition,
            IDictionary<string, IList<FacetResponse>> facets,
            IDictionary<string, FacetableFieldSettings> facetableFieldsFromIndex,
            string sortType)
        {
            IList<SearchFacetsViewModel> searchFacetsViewModel = new List<SearchFacetsViewModel>();
            if (facetableFieldsFromIndex.Any())
            {
                IDictionary<string, FacetWidgetFieldModel> widgetFacetableFields =
                    facetsWidgetDefinition
                        .Where(f => facetableFieldsFromIndex.Keys.Contains(f.FacetableFieldNames[0]))
                        .GroupBy(x => x.FacetableFieldNames[0])
                        .Select(f => f.LastOrDefault())
                        .ToDictionary(x => x.FacetableFieldNames[0], v => v);

                foreach (var facet in facets)
                {
                    List<FacetResponse> facetResponses = null;
                    if (facet.Value.Any(f => f.FacetType == SitefinityFacetType.Value))
                    {
                        facetResponses = facet.Value.Where(f => !string.IsNullOrEmpty(f.FacetValue)).ToList();
                    }
                    else
                    {
                        facetResponses = facet.Value.ToList();
                    }

                    List<FacetElementViewModel> facetElementValues = this.MapToFacetElementModel(facetResponses, facet.Key, widgetFacetableFields);
                    FacetWidgetFieldModel facetWidgetFieldModel = widgetFacetableFields[facet.Key];
                    var searchFacetViewModel = new SearchFacetsViewModel(facetWidgetFieldModel, facetElementValues);
                    searchFacetsViewModel.Add(searchFacetViewModel);
                }

                searchFacetsViewModel = this.SortFacetsModel(widgetFacetableFields, searchFacetsViewModel, sortType);
            }

            return searchFacetsViewModel;
        }

        private IList<SearchFacetsViewModel> SortFacetsModel(IDictionary<string, FacetWidgetFieldModel> facetableFieldsFromIndex, IList<SearchFacetsViewModel> searchFacetsViewModel, string sortType)
        {
            if (sortType == AlphabeticallySort)
            {
                searchFacetsViewModel = searchFacetsViewModel
                                        .OrderBy(f => f.FacetTitle)
                                        .ToList();
            }
            else
            {
                var facetsOrder = facetableFieldsFromIndex
                                    .Values
                                    .Select(x => x.FacetableFieldNames[0])
                                    .ToList();

                searchFacetsViewModel = searchFacetsViewModel
                                     .OrderBy(f => facetsOrder.IndexOf(f.FacetFieldName))
                                     .ToList();
            }

            return searchFacetsViewModel;
        }

        private List<FacetElementViewModel> MapToFacetElementModel(List<FacetResponse> facetResponses, string facetName, IDictionary<string, FacetWidgetFieldModel> widgetFacetableFields)
        {
            var facetElementsViewModel = new List<FacetElementViewModel>();

            foreach (var facet in facetResponses)
            {
                var facetViewModel = new FacetElementViewModel();
                string facetElementLabel;
                if (this.TryGetFacetLabel(facet, widgetFacetableFields[facetName], out facetElementLabel))
                {
                    facetViewModel.FacetLabel = facetElementLabel;
                    facetViewModel.FacetCount = facet.Count;
                    facetViewModel.FacetValue = this.ComputeFacetValue(facet);

                    facetElementsViewModel.Add(facetViewModel);
                }
            }

            return facetElementsViewModel;
        }

        private bool TryGetFacetLabel(FacetResponse facetResponse, FacetWidgetFieldModel facetWidgetFieldModel, out string facetLabel)
        {
            facetLabel = string.Empty;
            if (facetWidgetFieldModel.FacetFieldSettings.IsValueFacet())
            {
                facetLabel = facetResponse.FacetValue;
                return true;
            }

            var facetTableFieldType = facetWidgetFieldModel.FacetFieldSettings.FacetType;
            if (facetTableFieldType == SearchIndexAdditonalFieldType.NumberDecimal.ToString()
                    || facetTableFieldType == SearchIndexAdditonalFieldType.NumberWhole.ToString())
            {
                if (facetResponse.FacetType == SitefinityFacetType.Interval)
                {
                    facetLabel = this.GetIntervalNumberLabel(facetResponse, facetWidgetFieldModel);
                    return true;
                }
                else if (facetResponse.FacetType == SitefinityFacetType.Range)
                {
                    facetLabel = this.GetRangeNumberLabel(facetResponse, facetWidgetFieldModel);
                    return !string.IsNullOrEmpty(facetLabel);
                }
            }
            else if (facetTableFieldType == SearchIndexAdditonalFieldType.DateAndTime.ToString())
            {
                if (facetResponse.FacetType == SitefinityFacetType.Range)
                {
                    facetLabel = this.GetRangeDateLabel(facetResponse, facetWidgetFieldModel);
                    return !string.IsNullOrEmpty(facetLabel);
                }
                else
                {
                    DateTime.TryParse(facetResponse.From.ToString(), out DateTime fromValue);
                    var dateStep = this.widgetSettingsFacetFieldMapper.GetIntervalDateTime(facetWidgetFieldModel.FacetFieldSettings.DateStep);
                    facetLabel = this.FormatDateInterval(dateStep, fromValue);
                    return !string.IsNullOrEmpty(facetLabel);
                }
            }

            facetLabel = facetResponse.FacetValue;
            return true;
        }

        private string GetRangeDateLabel(FacetResponse facetResponse, FacetWidgetFieldModel facetableFieldSettings)
        {
            var dateRanges = facetableFieldSettings.FacetFieldSettings.DateRanges;

            var dateRange = dateRanges.FirstOrDefault(r =>
                r.From.HasValue && r.From.Value.ToUniversalTime().ToString(WidgetSettingsFacetFieldMapper.DateTimeFormat) == facetResponse.From &&
                r.To.HasValue && r.To.Value.ToUniversalTime().ToString(WidgetSettingsFacetFieldMapper.DateTimeFormat) == facetResponse.To);
               
            if (dateRange != null)
            {
                return dateRange.Label;
            }

            return string.Empty;
        }

        private string FormatDateInterval(string dateStep, DateTime intervalValue)
        {
            switch (dateStep)
            {
                case "day":
                case "week":
                case "quarter":
                    return intervalValue.ToString("MMM dd yyyy");
                case "month":
                    return intervalValue.ToString("MMM yyyy");
                case "year":
                    return intervalValue.ToString("yyyy");
                default:
                    return null;
            }
        }

        private string GetRangeNumberLabel(FacetResponse facetResponse, FacetWidgetFieldModel facetableFieldSettings)
        {
            double? from = this.ParseRangeValue(facetResponse.From);
            double? to = this.ParseRangeValue(facetResponse.To);

            var numberRanges = facetableFieldSettings.FacetFieldSettings.NumberRanges;
            if (numberRanges != null)
            {
                var numberRange = numberRanges.FirstOrDefault(r => r.From == from && r.To == to);
                if (numberRange != null)
                {
                    return numberRange.Label;
                }
            }

            var decimalNumbeRanges = facetableFieldSettings.FacetFieldSettings.NumberRangesDecimal;
            if (decimalNumbeRanges != null)
            {
                var numberRangeDecimal = decimalNumbeRanges.FirstOrDefault(r => r.From == from && r.To == to);
                if (numberRangeDecimal != null)
                {
                    return numberRangeDecimal.Label;
                }
            }

            return string.Empty;
        }

        private string GetIntervalNumberLabel(FacetResponse facetResponse, FacetWidgetFieldModel facetableFieldSettings)
        {
            string facetLabel;
            string prefix = facetableFieldSettings.FacetFieldSettings.Prefix;
            string suffix = facetableFieldSettings.FacetFieldSettings.Suffix;

            facetLabel = $"{prefix}{facetResponse.From}{suffix} - {prefix}{facetResponse.To}{suffix}";
            return facetLabel;
        }

        private double? ParseRangeValue(string val)
        {
            double? parsedValue = double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out var tempVal) ? tempVal : (double?)null;
            return parsedValue;
        }

        private string ComputeFacetValue(FacetResponse f)
        {
            return f.FacetType == SitefinityFacetType.Value ? f.FacetValue : $"{f.From}{SearchFacetsQueryStringProcessor.RangeSeparator}{f.To}";
        }

        private const string AlphabeticallySort = "2";
        private readonly WidgetSettingsFacetFieldMapper widgetSettingsFacetFieldMapper;
    }
}
