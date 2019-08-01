using System;
using System.ComponentModel;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// This interface provides API for form file fields.
    /// </summary>
    public interface IFileFieldModel : IFormFieldModel, IHideable
    {
        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple file attachments.
        /// </summary>
        bool AllowMultipleFiles { get; set; }

        /// <summary>
        /// Gets or sets the allowed file types.
        /// </summary>
        /// <value>
        /// The allowed file types.
        /// </value>
        [TypeConverter(typeof(StringFlagsEnumTypeConverter))]
        AllowedFileTypes AllowedFileTypes { get; set; }

        /// <summary>
        /// Gets or sets the other file types.
        /// </summary>
        /// <value>
        /// The other file types.
        /// </value>
        [TypeConverter(typeof(StringArrayConverter))]
        Array OtherFileTypes { get; set; }

        /// <summary>
        /// Gets or sets the file type violation message.
        /// </summary>
        /// <value>
        /// The file type violation message.
        /// </value>
        string FileTypeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file selection is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if a file selection is required; otherwise, <c>false</c>.
        /// </value>
        bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the required violation message.
        /// </summary>
        /// <value>
        /// The required violation message.
        /// </value>
        string RequiredViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the min file size in megabytes (MB).
        /// </summary>
        int MinFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the max file size in megabytes (MB).
        /// </summary>
        int MaxFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the file size violation message.
        /// </summary>
        /// <value>
        /// The file size violation message.
        /// </value>
        string FileSizeViolationMessage { get; set; }
    }
}
