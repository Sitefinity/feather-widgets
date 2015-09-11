using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    public class FileFieldModel : FormFieldModel, IFileFieldModel
    {
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
