using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.HiddenField
{
    /// <summary>
    /// Hidden field model
    /// </summary>
    public class HiddenFieldModel : FormFieldModel, IHiddenFieldModel
    {
        /// <inheritDocs />
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            return metaField;
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;

            var viewModel = new HiddenFieldViewModel()
            {
                MetaField = metaField
            };

            return viewModel;
        }
    }
}