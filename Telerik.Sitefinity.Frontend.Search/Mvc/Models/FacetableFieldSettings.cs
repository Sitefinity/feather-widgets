using Telerik.Sitefinity.Publishing.PublishingPoints;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Model of the facetable field settings
    /// </summary>
    internal class FacetableFieldSettings
    {
        /// <summary>
        /// Gets or sets the field type
        /// </summary>
        public SearchIndexAdditonalFieldType FieldType { get; set; }

        /// <summary>
        /// Gets or sets the fiently name of the field
        /// </summary>
        public string FieldTitle { get; set; }

        /// <summary>
        /// Gets or sets the developer name of the field
        /// </summary>
        public string FieldName { get; set; }
    }
}
