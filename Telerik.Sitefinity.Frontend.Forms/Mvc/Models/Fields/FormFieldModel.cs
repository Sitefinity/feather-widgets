using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields
{
    /// <summary>
    /// This class provides API for working with <see cref="IFormFieldControl"/> objects.
    /// </summary>
    public abstract class FormFieldModel : FormElementModel, IFormFieldModel
    {
        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets the default metafield based on the field control.
        /// </summary>
        /// <param name="formFieldControl">The field control.</param>
        /// <returns>The meta field.</returns>
        public virtual IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = formFieldControl.LoadDefaultMetaField();

            return metaField;
        }

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            return this.GetViewModel(value, this.MetaField);
        }

        /// <inheritDocs />
        public abstract object GetViewModel(object value, IMetaField metaField);
    }
}
