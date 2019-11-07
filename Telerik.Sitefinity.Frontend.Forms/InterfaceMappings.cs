using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.DropdownListField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.EmailTextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.HiddenField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.PageBreak;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader;
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
            Bind<IFormElementModel>().To<FormElementModel>();
            Bind<ITextFieldModel>().To<TextFieldModel>();
            Bind<IEmailTextFieldModel>().To<EmailTextFieldModel>();
            Bind<IHiddenFieldModel>().To<HiddenFieldModel>();
            Bind<IPageBreakModel>().To<PageBreakModel>();
            Bind<INavigationFieldModel>().To<NavigationFieldModel>();
            Bind<ISubmitButtonModel>().To<SubmitButtonModel>();
            Bind<ISectionHeaderModel>().To<SectionHeaderModel>();
            Bind<IParagraphTextFieldModel>().To<ParagraphTextFieldModel>();
            Bind<IDropdownListFieldModel>().To<DropdownListFieldModel>();
            Bind<IMultipleChoiceFieldModel>().To<MultipleChoiceFieldModel>();
            Bind<ICheckboxesFieldModel>().To<CheckboxesFieldModel>();
            Bind<IFileFieldModel>().To<FileFieldModel>();
            Bind<ICaptchaModel>().To<CaptchaModel>();
        }
    }
}
