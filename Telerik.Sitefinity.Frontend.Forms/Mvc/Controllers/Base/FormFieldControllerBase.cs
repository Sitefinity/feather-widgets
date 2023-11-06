using System;
using System.ComponentModel;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base
{
    /// <summary>
    /// This class contains common functionality for all form's fields.
    /// </summary>
    public abstract class FormFieldControllerBase<T> : FormElementControllerBase<T>, IFormFieldControl, IFormFieldController<T>
        where T : IFormFieldModel
    {
        /// <inheritDocs />
        [Browsable(false)]
        public virtual IMetaField MetaField
        {
            get
            {
                if (this.Model.MetaField == null)
                {
                    this.Model.MetaField = this.Model.GetMetaField(this);
                }

                return this.Model.MetaField;
            }

            set
            {
                this.Model.MetaField = value;
            }
        }

        /// <inheritDocs />
        protected override System.Web.Mvc.ViewResult View(object value, string templateName)
        {
            var viewModel = this.Model.GetViewModel(value, this.MetaField);
            return this.View(templateName, viewModel);
        }
    }
}