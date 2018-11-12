using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// Implements API for working with form file fields.
    /// </summary>
    public class FileFieldModel : FormFieldModel, IFileFieldModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFieldModel"/> class.
        /// </summary>
        public FileFieldModel()
        {
            this.RequiredViolationMessage = this.RequiredViolationMessage ?? Res.Get<FieldResources>().RequiredErrorMessageValue;
            this.FileSizeViolationMessage = this.FileSizeViolationMessage ?? Res.Get<FieldResources>().FileSizeViolationMessage;
            this.FileTypeViolationMessage = this.FileTypeViolationMessage ?? Res.Get<FieldResources>().FileTypeViolationMessage;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple file attachments.
        /// </summary>
        public bool AllowMultipleFiles { get; set; }

        /// <summary>
        /// Gets or sets the allowed file types.
        /// </summary>
        /// <value>
        /// The allowed file types.
        /// </value>
        [TypeConverter(typeof(StringFlagsEnumTypeConverter))]
        public AllowedFileTypes AllowedFileTypes { get; set; }

        /// <summary>
        /// Gets or sets the other file types.
        /// </summary>
        /// <value>
        /// The other file types.
        /// </value>
        [TypeConverter(typeof(StringArrayConverter))]
        public Array OtherFileTypes { get; set; }

        /// <summary>
        /// Gets or sets the file type violation message.
        /// </summary>
        /// <value>
        /// The file type violation message.
        /// </value>
        public string FileTypeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file selection is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if a file selection is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the required violation message.
        /// </summary>
        /// <value>
        /// The required violation message.
        /// </value>
        public string RequiredViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the min file size in megabytes (MB).
        /// </summary>
        public int MinFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the max file size in megabytes (MB).
        /// </summary>
        public int MaxFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the file size violation message.
        /// </summary>
        /// <value>
        /// The file size violation message.
        /// </value>
        public string FileSizeViolationMessage { get; set; }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = base.ValidatorDefinition;

                    if (!string.IsNullOrEmpty(this.RequiredViolationMessage))
                    {
                        this.validatorDefinition.RequiredViolationMessage = this.RequiredViolationMessage;
                    }
                    else
                    {
                        this.validatorDefinition.RequiredViolationMessage = Res.Get<FormResources>().RequiredInputErrorMessage;
                    }
                    
                    this.validatorDefinition.Required = this.IsRequired;
                }

                return this.validatorDefinition;
            }

            set
            {
                this.validatorDefinition = value;
            }
        }

        /// <summary>
        /// Gets the default metafield based on the field control.
        /// </summary>
        /// <param name="formFieldControl">The field control.</param>
        /// <returns>The meta field.</returns>
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            if (string.IsNullOrEmpty(metaField.Title))
            {
                metaField.Title = Res.Get<FieldResources>().Untitled;
            }

            return metaField;
        }

        /// <summary>
        /// Gets the populated ViewModel associated with the element.
        /// </summary>
        /// <param name="value">The value of the element.</param>
        /// <param name="metaField">The meta field of the field control.</param>
        /// <returns></returns>
        public override object GetViewModel(object value, IMetaField metaField)
        {
            var acceptedFileTypes = this.GetAcceptedFileTypes();
            return new FileFieldViewModel()
                {
                    CssClass = this.CssClass,
                    MetaField = this.MetaField,
                    AllowMultipleFiles = this.AllowMultipleFiles,
                    AcceptedFileTypes = acceptedFileTypes ?? new string[0],
                    ValidationAttributes = this.GenerateValidationAttributes(acceptedFileTypes),
                    IsRequired = this.IsRequired,
                    MinFileSizeInMb = this.MinFileSizeInMb,
                    MaxFileSizeInMb = this.MaxFileSizeInMb,
                    FileSizeViolationMessage = this.FileSizeViolationMessage,
                    FileTypeViolationMessage = this.FileTypeViolationMessage,
                    RequiredViolationMessage = this.RequiredViolationMessage
                };
        }

        /// <summary>
        /// Determines whether the value are valid for this model.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            IEnumerable<HttpPostedFileBase> fileList = value as IList<HttpPostedFileBase>;
            if (fileList == null)
            {
                return base.IsValid(fileList);
            }

            fileList = fileList.Where(f => !f.FileName.IsNullOrEmpty());

            if (this.IsRequired && fileList.Count() == 0)
                return base.IsValid(value);

            var acceptedFileTypes = this.GetAcceptedFileTypes();
            if (acceptedFileTypes != null && acceptedFileTypes.Length > 0)
            {
                foreach (var file in fileList)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!acceptedFileTypes.Contains(extension, StringComparer.OrdinalIgnoreCase))
                        return false;
                }
            }

            foreach (var file in fileList)
            {
                if (this.MinFileSizeInMb > 0 && file.InputStream.Length < (long)this.MinFileSizeInMb << 20)
                    return false;

                if (this.MaxFileSizeInMb > 0 && file.InputStream.Length > (long)this.MaxFileSizeInMb << 20)
                    return false;
            }

            return true;
        }

        private string[] GetAcceptedFileTypes()
        {
            if (this.AllowedFileTypes == AllowedFileTypes.All)
                return null;

            var acceptValues = new List<string>();

            var possibleFileTypes = Enum.GetValues(typeof(AllowedFileTypes));
            foreach (AllowedFileTypes fileType in possibleFileTypes)
            {
                if (fileType != AllowedFileTypes.All && this.AllowedFileTypes.HasFlag(fileType))
                {
                    if (fileType == AllowedFileTypes.Other)
                    {
                        if (this.OtherFileTypes != null && this.OtherFileTypes.Length > 0)
                        {
                            acceptValues.AddRange(this.OtherFileTypes.OfType<string>()
                                .Select(t => t.Trim().ToLowerInvariant())
                                .Select(t => t.StartsWith(".") ? t : "." + t));
                        }
                    }
                    else
                    {
                        acceptValues.AddRange(FileFieldModel.PredifinedAcceptValues[fileType]);
                    }
                }
            }

            return acceptValues.ToArray();
        }

        private string GenerateValidationAttributes(string[] acceptedFileTypes)
        {
            if (this.AllowedFileTypes == AllowedFileTypes.All || acceptedFileTypes == null || acceptedFileTypes.Length == 0)
                return string.Empty;
            else
                return "accept=\"{0}\"".Arrange(HttpUtility.HtmlAttributeEncode(string.Join(",", acceptedFileTypes)));
        }

        private static readonly Dictionary<AllowedFileTypes, string[]> PredifinedAcceptValues = new Dictionary<AllowedFileTypes, string[]>()
        {
            { AllowedFileTypes.All, null },
            { AllowedFileTypes.Audio, new string[] { ".mp3", ".ogg", ".wav", ".wma" } },
            { AllowedFileTypes.Video, new string[] { ".avi", ".mpg", ".mpeg", ".mov", ".mp4", ".wmv" } },
            { AllowedFileTypes.Images, new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" } },
            { AllowedFileTypes.Documents, new string[] { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".pps", ".ppsx", ".xls", ".xlsx" } },
            { AllowedFileTypes.Other, null }
        };

        private ValidatorDefinition validatorDefinition;
    }
}
