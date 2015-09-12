
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// This interface provides API for form file fields.
    /// </summary>
    public interface IFileFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple file attachments.
        /// </summary>
        bool AllowMultipleFiles { get; set; }
    }
}
