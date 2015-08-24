using System;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
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
                PlaceholderText = this.PlaceholderText
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