using System.ComponentModel;
using System.Text;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField
{
    /// <summary>
    /// Implements API for working with form paragraph text fields.
    /// </summary>
    public class ParagraphTextFieldModel : FormFieldModel, IParagraphTextFieldModel
    {
        /// <inheritDocs />
        public string PlaceholderText { get; set; }

        /// <inheritDocs />
        public bool Hidden { get; set; }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = base.ValidatorDefinition;

                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FormResources>().RequiredInputErrorMessage;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessage;
                    if (string.IsNullOrEmpty(this.validatorDefinition.RegularExpression))
                    {
                        this.validatorDefinition.RegularExpression = this.MetaField?.RegularExpression;
                        this.validatorDefinition.RegularExpressionViolationMessage = Res.Get<FormResources>().InvalidInputErrorMessage;
                    }
                }

                return this.validatorDefinition;
            }

            set
            {
                this.validatorDefinition = value;
            }
        }

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
            this.Value = value;
            var viewModel = new ParagraphTextFieldViewModel()
            {
                Value = value as string ?? this.MetaField.DefaultValue ?? string.Empty,
                MetaField = this.MetaField,
                PlaceholderText = this.PlaceholderText,
                ValidationAttributes = this.BuildValidationAttributesString(),
                RequiredViolationMessage = BuildErrorMessage(this.ValidatorDefinition.RequiredViolationMessage, metaField.Title),
                MaxLengthViolationMessage = BuildErrorMessage(this.ValidatorDefinition.MaxLengthViolationMessage, metaField.Title),
                ValidatorDefinition = this.BuildValidatorDefinition(this.ValidatorDefinition, metaField.Title),
                CssClass = this.CssClass,
                Hidden = this.Hidden && (!Sitefinity.Services.SystemManager.IsDesignMode || Sitefinity.Services.SystemManager.IsPreviewMode)
            };

            return viewModel;
        }

        /// <inheritDocs />
        private string BuildValidationAttributesString()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
            {
                attributes.Append("required='required' ");
            }

            if (this.ValidatorDefinition.MaxLength > 0)
            {
                attributes.Append("maxlength='" + this.ValidatorDefinition.MaxLength + "' ");
            }

            if (this.ValidatorDefinition.MinLength > 0)
            {
                attributes.Append("minlength='" + this.ValidatorDefinition.MinLength + "' ");
            }

            if (!string.IsNullOrEmpty(this.ValidatorDefinition.RegularExpression))
            {
                attributes.AppendFormat(@"pattern=""{0}""", this.ValidatorDefinition.RegularExpression);
            }

            return attributes.ToString();
        }

        private ValidatorDefinition validatorDefinition;
    }
}