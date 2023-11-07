using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Range settings
    /// </summary>
    public class DateFieldRangeSettings
    {
        /// <summary>
        /// Gets or sets the value at the start of the range.
        /// </summary>
        [DisplayName("start date")]
        [DefaultValue("")]
        [DateSettings(ShowTime = false)]
        [Placeholder("Start date")]
        [Required]
        public DateTime? From { get; set; }

        /// <summary>
        /// Gets or sets the value at the end of the range.
        /// </summary>
        [DisplayName("end date")]
        [DefaultValue("")]
        [DateSettings(ShowTime = false)]
        [Placeholder("End date")]
        [Required]
        public DateTime? To { get; set; }

        /// <summary>
        /// Gets or sets the additional facetable fields labels.
        /// </summary>
        [DisplayName("label")]
        [DefaultValue("")]
        [Placeholder("type label...")]
        [Description("Add a label for this range on your site. For example, 2021 - 2022 or May 2022.")]
        [Required]
        public string Label { get; set; }
    }
}
