using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Range settings
    /// </summary>
    public class NumberFieldDecimalRangeSetting
    {
        /// <summary>
        /// Gets or sets the value at the start of the range.
        /// </summary>
        [DisplayName("min value")]
        [Placeholder("type min value...")]
        [Required]
        public double? From { get; set; }

        /// <summary>
        ///Gets or sets the value at the end of the range.
        /// </summary>
        [DisplayName("max value")]
        [Placeholder("type max value...")]
        [Required]
        public double? To { get; set; }

        /// <summary>
        /// Gets or sets the additional facetable fields labels.
        /// </summary>
        [DisplayName("label")]
        [Placeholder("type label...")]
        [Description("Add a label for this range on your site. For example, 5kg - 10kg or above 10kg.")]
        [Required]
        public string Label { get; set; }
    }
}
