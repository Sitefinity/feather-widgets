using Telerik.Sitefinity.Publishing.PublishingPoints;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Facet view model
    /// </summary>
    public class FacetElementViewModel
    {
        /// <summary>
        /// Gets or sets the facet value
        /// </summary>
        public string FacetValue { get; set; }

        /// <summary>
        /// Gets or sets the facet count
        /// </summary>
        public long FacetCount { get; set; }

        /// <summary>
        /// Gets or sets the facet field type
        /// </summary>
        public SearchIndexAdditonalFieldType FacetFieldType { get; set; }
    }
}
