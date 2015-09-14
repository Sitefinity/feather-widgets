using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// Implements API for working with form file fields.
    /// </summary>
    public class FileFieldModel : FormFieldModel, IFileFieldModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple file attachments.
        /// </summary>
        public bool AllowMultipleFiles
        {
            get;
            set;
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
            return new FileFieldViewModel()
                {
                    CssClass = this.CssClass,
                    MetaField = this.MetaField,
                    AllowMultipleFiles = this.AllowMultipleFiles
                };
        }
    }
}
