using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.TextField
{
    public class TextFieldModel : FormsFieldModel, ITextFieldModel
    {
        /// <inheritDocs />
        public string PlaceholderText
        {
            get;
            set;
        }

        protected override string BuildValidationAttributes()
        {
            var attributes = new StringBuilder();

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes.Append("required='required' ");
            if (this.ValidatorDefinition.MaxLength > 0)
                attributes.Append("maxlength=' " + this.ValidatorDefinition.MaxLength + "'");

            return attributes.ToString();
        }
    }
}