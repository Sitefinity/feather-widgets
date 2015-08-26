using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
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
                var validatorDefinition = base.ValidatorDefinition;

                if (string.IsNullOrEmpty(validatorDefinition.RegularExpression))
                {
                    validatorDefinition.RegularExpression = this.InputTypeRegexPatterns.ContainsKey(this.InputType.ToString()) ? this.InputTypeRegexPatterns[this.InputType.ToString()] : string.Empty;
                }

                return validatorDefinition;
            }

            set
            {
                base.ValidatorDefinition = value;
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
                dictionary.Add(TextType.Email.ToString(), ValidatorPattern.EmailRegexPattern);
                dictionary.Add(TextType.Color.ToString(), ValidatorPattern.ColorRegexPattern);
                dictionary.Add(TextType.Number.ToString(), ValidatorPattern.NumericRegexPattern);
                dictionary.Add(TextType.Range.ToString(), ValidatorPattern.NumericRegexPattern);
                dictionary.Add(TextType.Url.ToString(), ValidatorPattern.UrlRegexPattern);

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

            if (this.ValidatorDefinition.MaxLength > 0)
                attributes.Append("pattern='.{" + this.ValidatorDefinition.MinLength + "," + this.ValidatorDefinition.MaxLength + "}' ");
            else if (this.ValidatorDefinition.MinLength > 0)
                attributes.Append("pattern='.{" + this.ValidatorDefinition.MinLength + ",}' ");

            return new MvcHtmlString(attributes.ToString());
        }
    }
}