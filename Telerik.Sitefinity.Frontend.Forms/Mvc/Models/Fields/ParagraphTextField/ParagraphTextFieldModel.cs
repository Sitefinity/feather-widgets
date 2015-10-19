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
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = base.ValidatorDefinition;

                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FieldResources>().RequiredErrorMessageValue;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FieldResources>().TooLargeErrorMessageValue;
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
                RequiredViolationMessage = this.ValidatorDefinition.RequiredViolationMessage,
                MaxLengthViolationMessage = this.ValidatorDefinition.MaxLengthViolationMessage,
                CssClass = this.CssClass
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

            return attributes.ToString();
        }

        private ValidatorDefinition validatorDefinition;
    }
}