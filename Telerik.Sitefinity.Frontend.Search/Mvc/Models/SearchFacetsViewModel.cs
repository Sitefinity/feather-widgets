using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Model representing search facets for a particular field
    /// </summary>
    public class SearchFacetsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the SearchFacetsViewModel
        /// </summary>
        public SearchFacetsViewModel()
        {
            this.FacetElements = new List<FacetElementViewModel>();
        }

        /// <summary>
        /// Initializes a new instance of the SearchFacetsViewModel
        /// </summary>
        /// <param name="facetTitle">The friendly title of the field that the facets are for</param>
        /// <param name="facetFieldName">The developer name of the field that the facets are for</param>
        /// <param name="facetElements">The facets for the field</param>
        public SearchFacetsViewModel(string facetTitle, string facetFieldName, IList<FacetElementViewModel> facetElements)
        {
            if (string.IsNullOrEmpty(facetFieldName))
            {
                throw new ArgumentNullException(nameof(facetFieldName));
            }

            if (facetElements == null)
            {
                throw new ArgumentNullException(nameof(facetElements));
            }

            this.FacetTitle = facetTitle;
            this.FacetFieldName = facetFieldName;
            this.FacetElements = facetElements;
        }

        /// <summary>
        /// Gets or seths the search facets
        /// </summary>
        public IList<FacetElementViewModel> FacetElements { get; set; }

        /// <summary>
        /// Gets or sets the title of the field that the facets are for
        /// </summary>
        public string FacetTitle { get; set; }

        /// <summary>
        /// Gets or sets the field name of the field that the facets are for
        /// </summary>
        public string FacetFieldName { get; set; }
    }
}
