using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base
{
    /// <summary>
    /// This interface defines API for working with forms element's controllers.
    /// </summary>
    public interface IFormElementController<out T> : IValidatable, IHasFieldDisplayMode 
        where T : IFormElementdModel
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
        /// Renders the field in <see cref="FormDisplayMode"/> Read of the parent <see cref="Form"/>.
        /// </summary>
        /// <returns>The value of the forms field.</returns>
        ActionResult Read(object value);

        /// <summary>
        /// Renders the field in <see cref="FormDisplayMode"/> Write of the parent <see cref="Form"/>.
        /// </summary>
        /// <returns>The value of the forms field.</returns>
        ActionResult Write(object value);
    }
}