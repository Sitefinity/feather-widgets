using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.DropdownListField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeaderField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SubmitButton;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This class is used to describe the bindings which will be used by the Ninject container when resolving classes
    /// </summary>
    public class InterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IFormRenderer>().To<FormRazorRenderer>();

            Bind<IFormModel>().To<FormModel>();
            Bind<IFormElementdModel>().To<FormElementModel>();
            Bind<ITextFieldModel>().To<TextFieldModel>();
            Bind<IRecaptchaModel>().To<RecaptchaModel>();
            Bind<ISubmitButtonModel>().To<SubmitButtonModel>();
            Bind<ISectionHeaderModel>().To<SectionHeaderFieldModel>();
            Bind<IParagraphTextFieldModel>().To<ParagraphTextFieldModel>();
            Bind<IDropdownListFieldModel>().To<DropdownListFieldModel>();
            Bind<IMultipleChoiceFieldModel>().To<MultipleChoiceFieldModel>();
            Bind<ICheckboxesFieldModel>().To<CheckboxesFieldModel>();
            Bind<IFileFieldModel>().To<FileFieldModel>();
        }
    }
}
