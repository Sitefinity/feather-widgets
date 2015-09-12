using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    public class FileFieldModel : FormFieldModel, IFileFieldModel
    {
        /// <inheritDocs />
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            if (string.IsNullOrEmpty(metaField.Title))
            {
                metaField.Title = Res.Get<FieldResources>().Untitled;
            }

            return metaField;
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            return new FileFieldViewModel()
                {
                    CssClass = this.CssClass,
                    MetaField = this.MetaField
                };
        }
    }
}
