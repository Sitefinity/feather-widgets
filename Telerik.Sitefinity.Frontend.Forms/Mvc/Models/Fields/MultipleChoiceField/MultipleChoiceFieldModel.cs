using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField
{
    /// <summary>
    /// Implements API for working with form multiple choice fields.
    /// </summary>
    public class MultipleChoiceFieldModel : FormFieldModel, IMultipleChoiceFieldModel
    {
        /// <inheritDocs />
        public bool HasOtherChoice { get; set; }

        /// <inheritDocs />
        public bool Hidden { get; set; }

        /// <inheritDocs />
        public string SerializedChoices
        {
            get
            {
                if (string.IsNullOrEmpty(this.serializedChoices))
                {
                    this.serializedChoices = this.BuildInitialChoicesString();
                }

                return this.serializedChoices;
            }

            set
            {
                string sanitizedValue = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(value);
                this.serializedChoices = sanitizedValue;
            }
        }

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
                }

                if (this.validatorDefinition.MaxLength == 0 && this.MetaField != null && int.TryParse(this.MetaField.DBLength, out int dbLength))
                {
                    this.validatorDefinition.MaxLength = dbLength;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessageWithRange;
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
                metaField.Title = Res.Get<FieldResources>().SelectChoice;
            }

            return metaField;
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;
            var viewModel = new MultipleChoiceFieldViewModel()
            {
                Choices = this.DeserializeChoices(),
                Value = value,
                MetaField = this.MetaField,
                HasOtherChoice = this.HasOtherChoice,
                IsRequired = this.ValidatorDefinition.Required.HasValue ? this.ValidatorDefinition.Required.Value : false,
                ValidationAttributes = this.BuildValidationAttributesString(),
                MaxLengthViolationMessage = this.BuildErrorMessage(this.ValidatorDefinition.MaxLengthViolationMessage, metaField.Title, this.ValidatorDefinition.MaxLength.ToString()),
                RequiredViolationMessage = BuildErrorMessage(this.ValidatorDefinition.RequiredViolationMessage, metaField.Title),
                CssClass = this.CssClass,
                Hidden = this.Hidden && (!Sitefinity.Services.SystemManager.IsDesignMode || Sitefinity.Services.SystemManager.IsPreviewMode)
            };

            return viewModel;
        }

        /// <summary>
        /// Prepopulates the initial choices.
        /// </summary>
        /// <returns></returns>
        public virtual string BuildInitialChoicesString()
        {
            var initialChoices = new string[]
            {
                Res.Get<FieldResources>().OptionFirstChoice,
                Res.Get<FieldResources>().OptionSecondChoice,
                Res.Get<FieldResources>().OptionThirdChoice
            };

            return JsonConvert.SerializeObject(initialChoices);
        }

        /// <inheritDocs />
        public override bool IsValid(object value)
        {
            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value && !this.HasOtherChoice)
            {
                var strValue = value as string;
                if (!string.IsNullOrEmpty(strValue))
                {
                    var choices = this.DeserializeChoices();
                    if (choices.Contains(strValue))
                    {
                        return base.IsValid(value);
                    }
                }

                return false;
            }
            else
            {
                return base.IsValid(value);
            }
        }

        public IEnumerable<string> DeserializeChoices()
        {
            if (string.IsNullOrEmpty(this.SerializedChoices))
            {
                return Enumerable.Empty<string>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<string>>(this.SerializedChoices);
        }

        /// <inheritDocs />
        private string BuildValidationAttributesString()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes.Append(@"required=""required"" ");

            var patternAttribute = ".{" + this.ValidatorDefinition.MinLength + "," + this.ValidatorDefinition.MaxLength + "}";
            attributes.Append($"pattern=\"{HttpUtility.HtmlAttributeEncode(patternAttribute)}\" ");

            return attributes.ToString();
        }

        private ValidatorDefinition validatorDefinition;
        private string serializedChoices;
    }
}