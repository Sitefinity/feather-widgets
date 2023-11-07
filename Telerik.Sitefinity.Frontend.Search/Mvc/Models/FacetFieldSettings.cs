using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Contains the settings for a facet field
    /// </summary>
    public class FacetFieldSettings
    {
        /// <summary>
        /// Gets or sets whether the facets are grouped automatically or by custom range.
        /// </summary>
        [DisplayName("Range type")]
        [DefaultValue(0)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Auto\",\"Name\":\"Auto\",\"Value\":0,\"Icon\":null},{\"Title\":\"Custom\",\"Name\":\"Custom\",\"Value\":1,\"Icon\":null}]")]
        public int RangeType { get; set; }

        #region Date
        /// <summary>
        /// Get or sets the date ranges.
        /// </summary>
        [TableView(Reorderable = false, Selectable = false, MultipleSelect = false)]
        [DisplayName("Set ranges")]
        [Description("Specify the ranges for generating facets.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":1 }," +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Equals\",\"value\":\"DateAndTime\" }]}")]
        public IList<DateFieldRangeSettings> DateRanges { get; set; }

        /// <summary>
        /// Gets or sets step used for date facets.
        /// </summary>
        [DisplayName("Set a range step")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Description("Select a step to automatically group values by time period.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":0 }," +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Equals\",\"value\":\"DateAndTime\" }]}")]
        [Choice(Choices = "[{\"Title\":\"day\",\"Name\":\"0\",\"Value\":0,\"Icon\":\"ban\"},{\"Title\":\"week\",\"Name\":\"1\",\"Value\":1,\"Icon\":null},{\"Title\":\"month\",\"Name\":\"2\",\"Value\":2,\"Icon\":null},{\"Title\":\"quarter\",\"Name\":\"3\",\"Value\":3,\"Icon\":null},{\"Title\":\"year\",\"Name\":\"4\",\"Value\":4,\"Icon\":null}]")]
        public string DateStep { get; set; }
        #endregion

        #region Number
        /// <summary>
        /// Gets or sets step used for number facets.
        /// </summary>
        [DisplayName("Set a range step")]
        [Description("Specify a step to automatically generate ranges. For example, if the step is set to 10, generated ranges will be 0 - 10, 10 - 20, 20 - 30, etc. If a step is not set, all values are displayed.")]
        [Placeholder("type number...")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":0 }, " +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Contains\",\"value\":\"Number\" }]}")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter a positive number")]
        [DefaultValue("")]
        public int? NumberStep { get; set; }

        /// <summary>
        /// Get or sets the whole number ranges.
        /// </summary>
        [TableView(Reorderable = false, Selectable = false, MultipleSelect = false)]
        [DisplayName("Set ranges")]
        [Description("Specify the ranges for generating facets.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":1 }," +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Equals\",\"value\":\"NumberWhole\" }]}")]
        public IList<NumberFieldRangeSetting> NumberRanges { get; set; }

        /// <summary>
        /// Get or sets the decimal number ranges.
        /// </summary>
        [TableView(Reorderable = false, Selectable = false, MultipleSelect = false)]
        [DisplayName("Set ranges")]
        [Description("Specify the ranges for generating facets.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":1 }," +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Equals\",\"value\":\"NumberDecimal\" }]}")]
        public IList<NumberFieldDecimalRangeSetting> NumberRangesDecimal { get; set; }

        /// <summary>
        /// Gets or sets the prefix for number fields.
        /// </summary>
        [DisplayName("Prefix")]
        [Description("Add text before the values, such as units, currency, etc. For example, $0 - $10, $10 - $20, $20 - $30, etc.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[" +
            "{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":0 }, " +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Contains\",\"value\":\"Number\" }]}")]
        [StringLength(20, ErrorMessage = "Your text must be less than 20 characters")]
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix for number fields.
        /// </summary>
        [DisplayName("Suffix")]
        [Description("Add text after the values, such as units, currency, etc. For example, 0 in - 10 in, 10 in - 20 in, 20 in - 30 in, etc.")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":0 }, " +
            "{\"fieldName\":\"FacetType\",\"operator\":\"Contains\",\"value\":\"Number\" }]}")]
        [StringLength(20, ErrorMessage = "Your text must be less than 20 characters")]
        public string Suffix { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether there is a custom range selector.
        /// </summary>
        [DisplayName("Users can enter custom values")]
        [Description("If enabled, empty fields for entering custom values are displayed on your site.")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"RangeType\",\"operator\":\"Equals\",\"value\":1 },{\"fieldName\":\"FacetType\",\"operator\":\"Contains\",\"value\":\"Number\" }]}")]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":true,\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":false,\"Icon\":null}]")]
        public bool DisplayCustomRange
        {
            get
            {
                return this.displayCustomRange;
            }
            set
            {
                this.displayCustomRange = value;
            }
        }

        /// <summary>
        /// Gets or the field of the facet.
        /// </summary>
        [DisplaySettings(Hide = true)]
        public string FacetType { get; set; }

        private bool displayCustomRange = false;
    }
}
