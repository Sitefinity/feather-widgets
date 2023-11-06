using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.PublishingPoints;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Model representing search facets for a particular field
    /// </summary>
    public class SearchFacetsViewModel
    {
        private FacetWidgetFieldModel facetWidgetFieldModel;

        /// <summary>
        /// Initializes a new instance of the SearchFacetsViewModel.
        /// </summary>
        public SearchFacetsViewModel()
        {
            this.FacetElements = new List<FacetElementViewModel>();
        }

        /// <summary>
        /// Initializes a new instance of the SearchFacetsViewModel.
        /// </summary>
        /// <param name="facetWidgetFieldModel">The facet widget field model which contains the name of the facet field and its settings</param>
        /// <param name="facetElements">A collection of facet values</param>
        public SearchFacetsViewModel(FacetWidgetFieldModel facetWidgetFieldModel, List<FacetElementViewModel> facetElements)
        {
            this.facetWidgetFieldModel = facetWidgetFieldModel;
            this.FacetElements = facetElements;

            this.FacetTitle = facetWidgetFieldModel.FacetableFieldLabels;
            this.FacetFieldName = facetWidgetFieldModel.FacetableFieldNames[0];
            this.FacetFieldType = facetWidgetFieldModel.FacetFieldSettings.FacetType;
        }

        /// <summary>
        /// Gets or seths the search facets
        /// </summary>
        public IList<FacetElementViewModel> FacetElements { get; set; }

        /// <summary>
        /// Gets or sets the title of the field that the facets are for.
        /// </summary>
        public string FacetTitle { get; set; }

        /// <summary>
        /// Gets or sets the field name of the field that the facets are for.
        /// </summary>
        public string FacetFieldName { get; set; }

        /// <summary>
        /// Get the facet field type.
        /// </summary>
        public string FacetFieldType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether number custom ranges should be shown.
        /// </summary>
        public bool ShowNumberCustomRange
        {
            get
            {
                return this.facetWidgetFieldModel.FacetFieldSettings.DisplayCustomRange &&
                (this.FacetFieldType == SearchIndexAdditonalFieldType.NumberWhole.ToString() ||
                 this.FacetFieldType == SearchIndexAdditonalFieldType.NumberDecimal.ToString());
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether date custom ranges should be shown.
        /// </summary>
        public bool ShowDateCustomRanges
        {
            get
            {
                return this.facetWidgetFieldModel.FacetFieldSettings.RangeType == 1 &&
                this.facetWidgetFieldModel.FacetFieldSettings.DisplayCustomRange &&
                this.FacetFieldType == SearchIndexAdditonalFieldType.DateAndTime.ToString();
            }
        }
    }
}
