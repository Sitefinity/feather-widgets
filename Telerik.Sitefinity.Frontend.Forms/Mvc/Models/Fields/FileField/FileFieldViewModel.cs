using System.Runtime.Serialization;
using Newtonsoft.Json;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// View model for the file field.
    /// </summary>
    [DataContract]
    public class FileFieldViewModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        public IMetaField MetaField { get; set; }

        [DataMember]
        public bool AllowMultipleFiles { get; set; }

        /// <summary>
        /// Serializes this instance in JSON format.
        /// </summary>
        /// <returns>This instance serialized in JSON format.</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
