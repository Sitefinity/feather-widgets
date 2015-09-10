﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

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
                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FieldResources>().RequiredErrorMessageValue;
                    this.validatorDefinition.MaxLengthViolationMessage = Res.Get<FieldResources>().TooLargeErrorMessageValue;
                }

                if (string.IsNullOrEmpty(this.validatorDefinition.RegularExpression))
                {
                    this.validatorDefinition.RegularExpression = this.InputTypeRegexPatterns.ContainsKey(this.InputType.ToString()) ? this.InputTypeRegexPatterns[this.InputType.ToString()] : string.Empty;
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
        public TextFieldViewModel GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;
            var viewModel = new TextFieldViewModel()
            {
                Value = value,
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
        protected override MvcHtmlString BuildValidationAttributes()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes.Append("required='required' ");

            var minMaxLength = string.Empty;
            if (this.ValidatorDefinition.MaxLength > 0)
                minMaxLength = ".{" + this.ValidatorDefinition.MinLength + "," + this.ValidatorDefinition.MaxLength + "}";
            else if (this.ValidatorDefinition.MinLength > 0)
                minMaxLength = ".{" + this.ValidatorDefinition.MinLength + ",}";

            if (this.InputType == TextType.Tel)
            {
                attributes.Append("pattern='");
                if(!string.IsNullOrEmpty(minMaxLength))
                {
                    attributes.Append("(?=^");
                    attributes.Append(minMaxLength);
                    attributes.Append("$)");
                }
                attributes.Append(Telerik.Sitefinity.Web.UI.Validation.Validator.TelRegexPattern);
                attributes.Append("' ");
            }
            else if (!string.IsNullOrEmpty(minMaxLength))
            {
                attributes.Append("pattern='");
                attributes.Append(minMaxLength);
                attributes.Append("' ");
            }

            return new MvcHtmlString(attributes.ToString());
        }

        private ValidatorDefinition validatorDefinition;
    }
}