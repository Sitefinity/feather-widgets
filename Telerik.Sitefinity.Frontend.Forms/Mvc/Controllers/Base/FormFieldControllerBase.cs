using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base
{
    public abstract class FormFieldControllerBase<T>: FormElementControllerBase<T>, IFormFieldControl
        where T: IFormFieldModel
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

        protected override System.Web.Mvc.ActionResult Process(object value, string templateName)
        {
            var viewModel = this.Model.GetViewModel(value, this.MetaField);
            return this.View(templateName, viewModel);
        }
    }
}
