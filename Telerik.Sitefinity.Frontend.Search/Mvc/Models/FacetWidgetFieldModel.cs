using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// Facet widget field model
    /// </summary>
    public class FacetWidgetFieldModel
    {
        /// <summary>
        /// Gets or sets the additional facetable fieldSearchFacetsQueryStringProcessors names.
        /// </summary>
        [DisplayName("Field")]
        [DataType(customDataType: "facetTaxa")]
        [DefaultValue(new string[0])]
        public string[] FacetableFieldNames { get; set; }

        /// <summary>
        /// Gets or sets the additional facetable fields labels.
        /// </summary>
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Add a name of the facetable field that is\",\"Presentation\":[]},{\"Value\":\"visible on your site.\",\"Presentation\":[]}]}]")]
        [DisplayName("Label")]
        [DefaultValue("")]
        public string FacetableFieldLabels { get; set; }

        /// <summary>
        /// Gets or sets the facet field widget settings.
        /// </summary>
        [DisplayName("Configuration")]
        [DataType(KnownFieldTypes.PencilButton)]
        [Dialog("{\"buttons\":[{\"type\":\"confirm\", \"title\":\"Save\"}, {\"type\":\"cancel\", \"title\":\"Cancel\"}], \"urlKey\":\"facetFieldSettings\"}")]
        public FacetFieldSettings FacetFieldSettings
        {
            get
            {
                if (facetFieldSettings == null)
                {
                    facetFieldSettings = new FacetFieldSettings();

                }

                return facetFieldSettings;
            }

            set
            {
                facetFieldSettings = value;
            }
        }


        private FacetFieldSettings facetFieldSettings;
    }
}