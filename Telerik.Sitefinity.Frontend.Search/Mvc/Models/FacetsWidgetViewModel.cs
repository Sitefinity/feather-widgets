using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Model for displaying search facets
    /// </summary>
    public class FacetsWidgetViewModel
    {
        /// <summary>
        /// Initializes a new instance of the view model
        /// </summary>
        public FacetsWidgetViewModel()
        {
            this.SearchFacets = new List<SearchFacetsViewModel>();
        }

        /// <summary>
        /// Gets or sets the list of search facets
        /// </summary>
        public IList<SearchFacetsViewModel> SearchFacets { get; set; }

        /// <summary>
        /// Gets or sets the clear all label
        /// </summary>
        public string ClearAllLabel { get; set; }

        /// <summary>
        /// Gets or sets the filter results label
        /// </summary>
        public string FilterResultsLabel { get; set; }

        /// <summary>
        /// Gets or sets the applied to label
        /// </summary>
        public string AppliedFiltersLabel { get; set; }

        /// <summary>
        /// Gets or sets the show more label
        /// </summary>
        public string ShowMoreLabel { get; set; }

        /// <summary>
        /// Gets or sets the show less label
        /// </summary>
        public string ShowLessLabel { get; set; }

        /// <summary>
        /// Gets or sets whatever the button is active or not 
        /// </summary>
        public bool IsShowMoreLessButtonActive { get; set; }

        /// <summary>
        /// Gets or sets whatever the facets are displyed with their count 
        /// </summary>
        public bool DisplayItemCount { get; set; }
    }
}
