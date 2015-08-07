using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This class provides API for working with forms field's controllers.
    /// </summary>
    public abstract class FormFieldController : Controller, IFormFieldController
    {
        /// <summary>
        /// Gets the form field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [Browsable(false)]
        public abstract IFormFieldModel FieldModel { get; }

        /// <inheritDocs />
        [Browsable(false)]
        public virtual IMetaField MetaField
        {
            get
            {
                if (this.FieldModel.MetaField == null)
                {
                    this.FieldModel.MetaField = this.FieldModel.GetMetaField(this);
                }

                return this.FieldModel.MetaField;
            }

            set
            {
                this.FieldModel.MetaField = value;
            }
        }

        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        public virtual FieldDisplayMode DisplayMode
        {
            get
            {
                return this.displayMode;
            }

            set
            {
                this.displayMode = value;
            }
        }

        /// <summary>
        /// Provides view for Read mode of the forms field
        /// </summary>
        /// <param name="value">The value of the forms field.</param>
        /// <returns></returns>
        public abstract ActionResult Read(object value);

        /// <summary>
        /// Provides view for Write mode of the forms field
        /// </summary>
        /// <param name="value">The value of the forms field.</param>
        /// <returns></returns>
        public abstract ActionResult Write(object value);

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return this.FieldModel.IsValid(this.FieldModel.Value);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            if (this.DisplayMode == FieldDisplayMode.Read)
            {
                this.Read(null).ExecuteResult(this.ControllerContext);
            }
            else if (this.DisplayMode == FieldDisplayMode.Write)
            {
                this.Write(null).ExecuteResult(this.ControllerContext);
            }
        }

        private IMetaField metaField;
        private FieldDisplayMode displayMode = FieldDisplayMode.Write;
    }
}