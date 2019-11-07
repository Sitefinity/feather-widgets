using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.EmailTextField
{
    /// <summary>
    /// Implements API for working with form text fields.
    /// </summary>
    public class EmailTextFieldModel : FormFieldModel, IEmailTextFieldModel
    {
        /// <inheritDocs />
        public string PlaceholderText { get; set; }

        /// <inheritDocs />
        public TextType InputType
        {
            get
            {
                return TextType.Email;
            }
        }

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
                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FormResources>().RequiredEmailErrorMessage;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FormResources>().MaxLengthInputErrorMessage;
                }

                if (string.IsNullOrEmpty(this.validatorDefinition.RegularExpression))
                {
                    this.validatorDefinition.RegularExpression = this.InputTypeRegexPatterns.ContainsKey(this.InputType.ToString()) ? this.InputTypeRegexPatterns[this.InputType.ToString()] : string.Empty;
                    this.validatorDefinition.RegularExpressionViolationMessage = Res.Get<FormResources>().InvalidEmailErrorMessage;
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
                metaField.Title = Res.Get<FieldResources>().Email;

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
                dictionary.Add("Email", Validator.EmailAddressRegexPattern);
                return dictionary;
            }
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;
            var viewModel = new EmailTextFieldViewModel()
            {
                Value = value as string ?? this.MetaField.DefaultValue ?? string.Empty,
                MetaField = metaField,
                ValidationAttributes = this.BuildValidationAttributes(),
                CssClass = this.CssClass,
                ValidatorDefinition = this.ValidatorDefinition,
                PlaceholderText = this.PlaceholderText,
                InputType = this.InputType,
                Hidden = this.Hidden && (!Sitefinity.Services.SystemManager.IsDesignMode || Sitefinity.Services.SystemManager.IsPreviewMode)
            };

            return viewModel;
        }

        /// <inheritDocs />
        protected override string BuildValidationAttributes()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes.Append(@"required=""required"" ");

            var patternAttribute = string.Empty;

            if (!string.IsNullOrWhiteSpace(this.ValidatorDefinition.ExpectedFormat.ToString()))
            {
                patternAttribute = this.GetRegExForExpectedFormat(this.ValidatorDefinition.ExpectedFormat);
            }

            if (!string.IsNullOrEmpty(this.ValidatorDefinition.RegularExpression))
            {
                patternAttribute = this.ValidatorDefinition.RegularExpression;
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