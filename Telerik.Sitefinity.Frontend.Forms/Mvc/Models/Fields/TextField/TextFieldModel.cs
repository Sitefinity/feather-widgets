using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
    /// <summary>
    /// Implements API for working with form text fields.
    /// </summary>
    public class TextFieldModel : FormFieldModel, ITextFieldModel
    {
        /// <inheritDocs />
        public string PlaceholderText { get; set; }

        /// <inheritDocs />
        public TextType InputType { get; set; }

        /// <inheritDocs />
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = new ValidatorDefinition();
                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FormResources>().RequiredInputErrorMessage;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessage;
                }

                if (string.IsNullOrEmpty(this.validatorDefinition.RegularExpression))
                {
                    this.validatorDefinition.RegularExpression = this.InputTypeRegexPatterns.ContainsKey(this.InputType.ToString()) ? this.InputTypeRegexPatterns[this.InputType.ToString()] : string.Empty;
                    this.validatorDefinition.RegularExpressionViolationMessage = Res.Get<FormResources>().InvalidInputErrorMessage;
                }

                if (this.MetaField != null)
                {
                    int.TryParse(this.MetaField.DBLength, out int dbLength);
                    if (dbLength > 0 && (this.validatorDefinition.MaxLength == 0 || this.validatorDefinition.MaxLength > dbLength))
                    {
                        this.validatorDefinition.MaxLength = dbLength;
                        this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessageWithRange;
                    }
                }

                return this.validatorDefinition;
            }

            set
            {
                this.validatorDefinition = value;
            }
        }

        /// <summary>
        /// Gets the serialized input type regex patterns.
        /// </summary>
        /// <value>
        /// The serialized input type regex patterns.
        /// </value>
        public string SerializedInputTypeRegexPatterns
        {
            get
            {
                var serializedInputTypeRegexPatterns = new JavaScriptSerializer().Serialize(this.InputTypeRegexPatterns);

                return serializedInputTypeRegexPatterns;
            }
        }

        /// <inheritDocs />
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            if (string.IsNullOrEmpty(metaField.Title))
                metaField.Title = Res.Get<FieldResources>().Untitled;

            return metaField;
        }

        /// <summary>
        /// Gets the input type regex patterns.
        /// </summary>
        /// <value>
        /// The input type regex patterns.
        /// </value>
        [Browsable(false)]
        public virtual Dictionary<string, string> InputTypeRegexPatterns
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add(TextType.Email.ToString(), Validator.EmailAddressRegexPattern);
                dictionary.Add(TextType.Color.ToString(), Validator.ColorRegexPattern);
                dictionary.Add(TextType.Number.ToString(), Validator.NumericRegexPattern);
                dictionary.Add(TextType.Range.ToString(), Validator.NumericRegexPattern);
                dictionary.Add(TextType.Url.ToString(), Validator.InternetUrlRegexPattern);
                dictionary.Add(TextType.Tel.ToString(), Validator.TelRegexPattern);

                return dictionary;
            }
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;
            var viewModel = new TextFieldViewModel()
            {
                Value = value as string ?? this.MetaField.DefaultValue ?? string.Empty,
                MetaField = metaField,
                ValidationAttributes = this.BuildValidationAttributes(),
                CssClass = this.CssClass,
                ValidatorDefinition = this.BuildValidatorDefinition(this.ValidatorDefinition, metaField.Title),
                PlaceholderText = this.PlaceholderText,
                InputType = this.InputType,
                Hidden = this.Hidden && (!Sitefinity.Services.SystemManager.IsDesignMode || Sitefinity.Services.SystemManager.IsPreviewMode)
            };

            return viewModel;
        }

        /// <inheritDocs />
        protected override ValidatorDefinition BuildValidatorDefinition(ValidatorDefinition definition, string fieldTitle)
        {
            var validatorDefinition = new ValidatorDefinition(definition.ConfigDefinition);
            validatorDefinition.RequiredViolationMessage = this.BuildErrorMessage(definition.RequiredViolationMessage, fieldTitle);
            validatorDefinition.MaxLengthViolationMessage = this.BuildErrorMessage(definition.MaxLengthViolationMessage, fieldTitle, definition.MaxLength.ToString());
            validatorDefinition.RegularExpressionViolationMessage = this.BuildErrorMessage(definition.RegularExpressionViolationMessage, fieldTitle);
            validatorDefinition.MinLength = definition.MinLength;
            validatorDefinition.MaxLength = definition.MaxLength;
            validatorDefinition.Required = definition.Required;

            return validatorDefinition;
        }

        /// <inheritDocs />
        protected override string BuildValidationAttributes()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes.Append(@"required=""required"" ");

            var minMaxLength = string.Empty;
            if (this.ValidatorDefinition.MaxLength > 0)
                minMaxLength = ".{" + this.ValidatorDefinition.MinLength + "," + this.ValidatorDefinition.MaxLength + "}";
            else if (this.ValidatorDefinition.MinLength > 0)
                minMaxLength = ".{" + this.ValidatorDefinition.MinLength + ",}";

            var patternAttribute = string.Empty;

            if (!string.IsNullOrWhiteSpace(this.ValidatorDefinition.ExpectedFormat.ToString()))
            {
                string expectedFormatPattern = this.GetRegExForExpectedFormat(this.ValidatorDefinition.ExpectedFormat);
                if (!string.IsNullOrWhiteSpace(expectedFormatPattern))
                {
                    patternAttribute = expectedFormatPattern;
                }
            }

            if (!string.IsNullOrEmpty(this.ValidatorDefinition.RegularExpression))
            {
                patternAttribute = this.ValidatorDefinition.RegularExpression;
            }

            if (!string.IsNullOrEmpty(minMaxLength))
            {
                if (this.InputType == TextType.Tel)
                {
                    patternAttribute = string.Format("(?=^{0}$){1}", minMaxLength, Validator.TelRegexPattern);
                }
                else if (!string.IsNullOrEmpty(patternAttribute))
                {
                    patternAttribute = string.Format("(?=^{0}$){1}", minMaxLength, patternAttribute);
                }
                else
                {
                    patternAttribute = minMaxLength;
                }
            }

            if (!string.IsNullOrEmpty(patternAttribute))
            {
                attributes.Append($"pattern=\"{HttpUtility.HtmlAttributeEncode(patternAttribute)}\" ");
            }

            return attributes.ToString();
        }

        /// <inheritDocs />
        public override bool IsValid(object value)
        {
            this.ValidatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessage;
            return base.IsValid(value);
        }

        private string GetRegExForExpectedFormat(ValidationFormat expectedFormat)
        {
            switch (expectedFormat)
            {
                case ValidationFormat.AlphaNumeric:
                    return Validator.AlphaNumericRegexPattern;
                case ValidationFormat.Currency:
                    return Validator.CurrencyRegexPattern;
                case ValidationFormat.EmailAddress:
                    return Validator.EmailAddressRegexPattern;
                case ValidationFormat.Integer:
                    return Validator.IntegerRegexPattern;
                case ValidationFormat.InternetUrl:
                    return Validator.InternetUrlRegexPattern;
                case ValidationFormat.NonAlphaNumeric:
                    return Validator.NonAlphaNumericRegexPattern;
                case ValidationFormat.Numeric:
                    return Validator.NumericRegexPattern;
                case ValidationFormat.Percentage:
                    return Validator.PercentRegexPattern;
                case ValidationFormat.USSocialSecurityNumber:
                    return Validator.USSocialSecurityRegexPattern;
                case ValidationFormat.USZipCode:
                    return Validator.USZipCodeRegexPattern;
                default:
                    return string.Empty;
            }
        }

        private ValidatorDefinition validatorDefinition;
    }
}