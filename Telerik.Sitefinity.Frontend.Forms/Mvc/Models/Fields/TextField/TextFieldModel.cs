using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
        public string PlaceholderText
        {
            get;
            set;
        }

        /// <inheritDocs />
        public TextType InputType
        {
            get;
            set;
        }

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
                dictionary.Add(TextType.Email.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.EmailAddressRegexPattern);
                dictionary.Add(TextType.Color.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.ColorRegexPattern);
                dictionary.Add(TextType.Number.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.NumericRegexPattern);
                dictionary.Add(TextType.Range.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.NumericRegexPattern);
                dictionary.Add(TextType.Url.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.InternetUrlRegexPattern);
                dictionary.Add(TextType.Tel.ToString(), Telerik.Sitefinity.Web.UI.Validation.Validator.TelRegexPattern);

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
                ValidatorDefinition = this.ValidatorDefinition,
                PlaceholderText = this.PlaceholderText,
                InputType = this.InputType
            };

            return viewModel;
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
                patternAttribute = this.GetRegExForExpectedFormat(this.ValidatorDefinition.ExpectedFormat);
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
                attributes.AppendFormat(@"pattern=""{0}""", patternAttribute);
            }

            return attributes.ToString();
        }

        private string GetRegExForExpectedFormat(ValidationFormat expectedFormat)
        {
            string regexPattern = string.Empty;

            switch (expectedFormat)
            {
                case ValidationFormat.None:
                    return string.Empty;
                case ValidationFormat.AlphaNumeric:
                    regexPattern = Validator.AlphaNumericRegexPattern;
                    break;
                case ValidationFormat.Currency:
                    regexPattern = Validator.CurrencyRegexPattern;
                    break;
                case ValidationFormat.EmailAddress:
                    regexPattern = Validator.EmailAddressRegexPattern;
                    break;
                case ValidationFormat.Integer:
                    regexPattern = Validator.IntegerRegexPattern;
                    break;
                case ValidationFormat.InternetUrl:
                    regexPattern = Validator.InternetUrlRegexPattern;
                    break;
                case ValidationFormat.NonAlphaNumeric:
                    regexPattern = Validator.NonAlphaNumericRegexPattern;
                    break;
                case ValidationFormat.Numeric:
                    regexPattern = Validator.NumericRegexPattern;
                    break;
                case ValidationFormat.Percentage:
                    regexPattern = Validator.PercentRegexPattern;
                    break;
                case ValidationFormat.USSocialSecurityNumber:
                    regexPattern = Validator.USSocialSecurityRegexPattern;
                    break;
                case ValidationFormat.USZipCode:
                    regexPattern = Validator.USZipCodeRegexPattern;
                    break;
                case ValidationFormat.Custom:
                    throw new ArgumentException("You must specify a valid RegularExpression.");
            }

            return regexPattern;
        }

        private ValidatorDefinition validatorDefinition;
    }
}