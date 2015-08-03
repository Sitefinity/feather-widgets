using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    public class FormsFieldModel : IFormsFieldModel
    {
        /// <inheritDocs />
        public virtual object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a validation mechanism for the control.
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

        /// <inheritDocs />
        public virtual IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = formFieldControl.LoadDefaultMetaField();
            string validFieldName;
            if (MetaDataExtensions.TryCreateValidFieldName(formFieldControl.GetType().Name, out validFieldName))
            {
                metaField.FieldName = validFieldName + Guid.NewGuid().ToString("N");
            }

            return metaField;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return true;
        }

        private ValidatorDefinition validatorDefinition;
    }
}