using System.Runtime.Serialization;
using System.Web.Mvc;
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

        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple files.
        /// </summary>
        /// <value>
        ///   <c>true</c> if multiple files are allowed; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool AllowMultipleFiles { get; set; }

        /// <summary>
        /// Gets or sets the accepted file types.
        /// </summary>
        /// <value>
        /// The accepted file types.
        /// </value>
        [DataMember]
        public string[] AcceptedFileTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file selection is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if a file selection is required; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the min file size in megabytes (MB).
        /// </summary>
        [DataMember]
        public int MinFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the max file size in megabytes (MB).
        /// </summary>
        [DataMember]
        public int MaxFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the validation attributes.
        /// </summary>
        /// <value>
        /// The validation attributes.
        /// </value>
        public string ValidationAttributes { get; set; }

        /// <summary>
        /// Gets or sets the file size violation message.
        /// </summary>
        /// <value>
        /// The file size violation message.
        /// </value>
        public string FileSizeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the required violation message.
        /// </summary>
        /// <value>
        /// The required violation message.
        /// </value>
        public string RequiredViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the file type violation message.
        /// </summary>
        /// <value>
        /// The file type violation message.
        /// </value>
        public string FileTypeViolationMessage { get; set; }

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
