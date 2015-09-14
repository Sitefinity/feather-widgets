using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields
{
    public interface IFormFieldModel: IFormElementdModel
    {
        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets the meta field.
        /// </summary>
        /// <param name="formFieldControl">The form field control.</param>
        /// <returns></returns>
        IMetaField GetMetaField(IFormFieldControl formFieldControl);

        /// <summary>
        /// Gets the populated ViewModel associated with the element.
        /// </summary>
        /// <param name="value">The value of the element.</param>
        /// <returns></returns>
        object GetViewModel(object value, IMetaField metaField);
    }
}
