using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for short text dynamic fields.
    /// </summary>
    public class ShortTextField : Field
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortTextField"/> class.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public ShortTextField(DynamicModuleType moduleType)
        {
            this.moduleType = moduleType;
        }

        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && (field.FieldType == FieldType.ShortText || field.FieldType == FieldType.Guid)
                && field.Name != this.moduleType.MainShortTextFieldName;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Format(ShortTextField.FieldMarkupTempalte, field.Name, field.Title);

            return markup;
        }

        private DynamicModuleType moduleType;
        private const string FieldMarkupTempalte = @"@Html.Sitefinity().ShortTextField((object)Model.Item.{0}, ""{0}"", fieldTitle: ""{1}"", cssClass: ""sfitemShortTxtWrp"")";
    }
}
