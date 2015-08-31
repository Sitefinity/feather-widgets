using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This interface defines API for working with forms field's controllers.
    /// </summary>
    public interface IFormFieldController<T> : IFormFieldControl, IHasFieldDisplayMode 
        where T : IFormFieldModel
    {
        /// <summary>
        /// Gets the form field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [Browsable(false)]
        T Model { get; }

        /// <summary>
        /// Provides the view depending on the current <see cref="FieldDisplayMode"/> of the field.
        /// </summary>
        /// <returns>The value of the forms field.</returns>
        ActionResult Index(object value = null);
    }
}