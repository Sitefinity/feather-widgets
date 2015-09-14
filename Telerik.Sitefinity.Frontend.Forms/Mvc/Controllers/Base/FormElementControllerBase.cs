using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base
{
    /// <summary>
    /// This class contains common functionality for all form's elements.
    /// </summary>
    public abstract class FormElementControllerBase<T> : Controller, IFormElementController<T>
        where T: IFormElementdModel
    {
        #region Properties

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public abstract T Model
        {
            get;
        }

        /// <inheritDocs />
        public virtual FieldDisplayMode DisplayMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when field is in write view.
        /// </summary>
        /// <value></value>
        public virtual string WriteTemplateName
        {
            get
            {
                return this.writeTemplateName;
            }
            set
            {
                this.writeTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the prefix of the Write template's name that is used to detect the field specific views.
        /// </summary>
        /// <value></value>
        protected virtual string WriteTemplateNamePrefix
        {
            get
            {
                return FormElementControllerBase<T>.writeTemplateNamePrefix;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when field is in read view.
        /// </summary>
        /// <value></value>
        public virtual string ReadTemplateName
        {
            get
            {
                return this.readTemplateName;
            }
            set
            {
                this.readTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the prefix of the Read template's name that is used to detect the field specific views.
        /// </summary>
        /// <value></value>
        protected virtual string ReadTemplateNamePrefix
        {
            get
            {
                return FormElementControllerBase<T>.readTemplateNamePrefix;
            }
        }

        #endregion

        #region Actions

        /// <inheritDocs />
        public virtual ActionResult Read(object value)
        {
            string templateName = this.ReadTemplateNamePrefix + this.ReadTemplateName;
            return this.View(value, templateName);
        }

        /// <inheritDocs />
        public virtual ActionResult Write(object value)
        {
            string templateName = this.WriteTemplateNamePrefix + this.WriteTemplateName;
            return this.View(value, templateName);
        }

        /// <summary>
        ///  Creates a System.Web.Mvc.ViewResult object that renders the specified IView
        /// </summary>
        /// <param name="value">The element's value.</param>
        /// <param name="templateName">The name of the element's template.</param>
        /// <returns></returns>
        protected virtual ViewResult View(object value, string templateName)
        {
            var viewModel = this.Model.GetViewModel(value);
            return this.View(templateName, viewModel);
        }

        #endregion

        #region Public and protected methods

        /// <inheritDocs />
        public bool IsValid()
        {
            return this.Model.IsValid(this.Model.Value);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Write(null).ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private fields and Constants

        private string writeTemplateName = "Default";
        private string readTemplateName = "Default";
        private const string writeTemplateNamePrefix = "Write.";
        private const string readTemplateNamePrefix = "Read.";

        #endregion
    }
}
