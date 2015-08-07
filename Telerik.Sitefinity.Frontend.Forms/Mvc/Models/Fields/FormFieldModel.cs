using System;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields
{
    /// <summary>
    /// Implements API for working with form fields.
    /// </summary>
    public class FormFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the value of forms field.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public virtual object Value
        {
            get;
            set;
        }

        /// <inheritDocs />
        public string CssClass { get; set; }

        /// <inheritDocs />
        [Browsable(false)]
        public virtual MvcHtmlString ValidationAttributes
        {
            get
            {
                var attributesString = this.BuildValidationAttributes();

                return new MvcHtmlString(attributesString);
            }
        }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = new ValidatorDefinition();
                }

                return this.validatorDefinition;
            }

            set
            {
                this.validatorDefinition = value;
            }
        }

        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets the instance of <see cref="Validator"/> configured for the field's <see cref="ValidatorDefinition"/>.
        /// </summary>
        /// <value></value>
        public virtual Validator Validator
        {
            get
            {
                if (this.validator == null)
                {
                    Func<IComparingValidatorDefinition, object> comparingFunc = null;
                    this.validator = new Validator(this.ValidatorDefinition, comparingFunc);
                }
                else
                {
                    // If the validator is already instantiated synchronies the validatorDefinition changes to the old instance.
                    this.validator.Configure(this.ValidatorDefinition);
                }

                return this.validator;
            }
        }

        /// <inheritDocs />
        public virtual IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = formFieldControl.LoadDefaultMetaField();

            return metaField;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid(object value)
        {
            return this.Validator.IsValid(value);
        }

        /// <summary>
        /// Builds the validation attributes.
        /// </summary>
        /// <returns></returns>
        protected virtual string BuildValidationAttributes()
        {
            var attributes = string.Empty;

            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
                attributes = "required='required'";

            return attributes;
        }

        private Validator validator;
        private ValidatorDefinition validatorDefinition;
    }
}